<template>
  <v-dialog v-model="dialog" max-width="500">
    <v-card :title="props.contract ? 'Edit contract' : 'Create new contract'">
      <v-card-text>
        <v-row dense>
          <v-col cols="12">
            <v-text-field v-model="author" label="Author*" required />
          </v-col>
          <v-col cols="12">
            <v-text-field v-model="entity" label="Legal Entity Name*" required />
          </v-col>
          <v-col cols="12">
            <v-text-field v-model="description" label="Description" required />
          </v-col>
        </v-row>
      </v-card-text>
      <v-divider></v-divider>
      <v-card-actions>
        <v-spacer />
        <v-btn text="Cancel" variant="plain" @click="dialog = false" />
        <v-btn
          color="primary"
          text="Save"
          variant="tonal"
          @click="handleSave"
          :disabled="!author || !entity"
        />
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from 'vue';
import { useContractsStore } from '@/stores/contracts';
import { useSnackbar } from '@/composables/useSnackbar';
import type { LegalContract } from '@/api';

const props = defineProps<{
  contract?: LegalContract | null;
  open: boolean;
}>();
const emit = defineEmits(['update:open', 'updated']);

const dialog = ref(props.open);
const author = ref('');
const entity = ref('');
const description = ref('');

watch(() => props.open, (val) => {
  dialog.value = val;
});
watch(dialog, (val) => {
  emit('update:open', val);
});
watch(() => props.contract, (newContract) => {
  if (newContract) {
    author.value = newContract.author || '';
    entity.value = newContract.entityName || '';
    description.value = newContract.description || '';
    dialog.value = true;
  }
});

const store = useContractsStore();
const { createContract, updateContract } = store;
const { triggerSnackbar } = useSnackbar();

const handleSave = async () => {
  try {
    if (props.contract) {
      await updateContract(props.contract.id!, {
        ...props.contract,
        author: author.value,
        entityName: entity.value,
        description: description.value,
        updatedAt: new Date().toISOString()
      });
      triggerSnackbar(`Contract ${props.contract.id} updated successfully`, 'success');
      emit('updated');
    }
    else {
      await createContract({
        author: author.value,
        entityName: entity.value,
        description: description.value,
        updatedAt: new Date().toISOString()
      });
      triggerSnackbar(`Contract created successfully`, 'success');
      emit('updated');
    }
  } catch (e) {
    console.error(e);
    triggerSnackbar('An error occurred while handling contract.', 'error');
  } finally {
    dialog.value = false;
  }
};
</script>