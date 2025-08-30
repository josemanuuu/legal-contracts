<template>
    <v-dialog v-model="dialog" max-width="500">
        <v-card>
            <v-card-title>
            Legal Contract {{ props.contract.id }}
            </v-card-title>

            <v-card-subtitle 
                class="text-sm italic text-grey"
                v-if="props.contract.updatedAt"
            >
                Last Update: {{ formatDate(props.contract.updatedAt) }}
            </v-card-subtitle>

            <v-card-text>
            <v-list density="compact">
                <v-list-item title="Author" :subtitle="props.contract.author || ''" />
                <v-list-item title="Legal Entity Name" :subtitle="props.contract.entityName || ''" />
                <v-list-item title="Creation Date" :subtitle="formatDate(props.contract.createdAt)" />
                <v-list-item title="Description">
                <template #subtitle>
                    <div class="whitespace-pre-line">
                    {{ props.contract.description || '' }}
                    </div>
                </template>
                </v-list-item>
            </v-list>
            </v-card-text>

            <v-divider></v-divider>
            <v-card-actions>
            <v-spacer />
            <v-btn text="Cancel" variant="plain" @click="dialog = false" />
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from 'vue';
import type { LegalContract } from '@/api';
import { formatDate } from '@/utils';

const props = defineProps<{
  contract: LegalContract;
  open: boolean;
}>();
const emit = defineEmits(['open']);

const dialog = ref(props.open);

watch(() => props.open, (val) => {
  dialog.value = val;
});
watch(dialog, (val) => {
  emit('open', val);
});
</script>