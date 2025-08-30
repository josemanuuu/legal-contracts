<template>
  <v-card class="mx-auto responsive-card">
    <v-toolbar class="pa-4" flat>
      <v-toolbar-title>Contratos Legales</v-toolbar-title>
      <v-text-field
        v-model="filterContract"
        density="compact"
        placeholder="Filter contracts"
        prepend-inner-icon="mdi-magnify"
        variant="solo"
        max-width="250"
        flat
        hide-details
        single-line
      ></v-text-field>
      <v-btn
        color="primary"
        icon="mdi-plus"
        variant="text"
        v-tooltip="'Create new contract'"
        @click="openEditDialog(null)"
      ></v-btn>
    </v-toolbar>

    <v-list lines="two">
      <v-list-item
        v-for="contract in filteredContracts"
        :key="contract.id"
        :subtitle="contract.entityName || ''"
        :title="contract.author || 'Unknown'"
      >
        <template #append>
          <ContractActions
            :contract="contract"
            @edit="openEditDialog"
            @delete="handleDeleteContract"
          />
        </template>
      </v-list-item>
    </v-list>

    <!-- Loading & error -->
    <v-progress-linear
      v-if="loading"
      indeterminate
      color="primary"
      class="mt-4"
    />
    <v-alert
      v-if="error"
      type="error"
      class="mt-2"
      dense
    >
      {{ error }}
    </v-alert>
    
  </v-card>
  
  <EditContractDialog
    :open="formOpen"
    :contract="selectedContract"
    @update:open="formOpen = $event"
    @updated="fetchContracts"
  />

  <v-snackbar v-model="showSnackbar" :timeout="3000" :color="snackbarColor" location="top right">
    {{ snackbarText }}
  </v-snackbar>
</template>

<script setup lang="ts">
// Vue + Pinia (store)
import { computed, onMounted, ref } from 'vue';
import { storeToRefs } from 'pinia';

// Composables
import { useContractsStore } from '@/stores/contracts';
import { useSnackbar } from '@/composables/useSnackbar';

// Types
import type { LegalContract } from './api';

// Components
import EditContractDialog from './components/EditContractDialog.vue'
import ContractActions from './components/ContractActions.vue';


// Fetch contracts from the store
const store = useContractsStore();
const { contracts, loading, error } = storeToRefs(store);
const { fetchContracts, deleteContract } = store;

onMounted(() => {
  fetchContracts();
});

// Snackbar management
const { showSnackbar, triggerSnackbar, snackbarColor, snackbarText } = useSnackbar();

// Filter contracts logic
const filterContract = ref('');

const filteredContracts = computed(() =>
  contracts.value.filter(c =>
    (c.author || '').toLowerCase().includes(filterContract.value.toLowerCase()) ||
    (c.entityName || '').toLowerCase().includes(filterContract.value.toLowerCase())
  )
);

// Handle interactions
async function handleDeleteContract(id: number) {
  try {
    await deleteContract(id);
    triggerSnackbar(`Contrato ${id} eliminado correctamente`, 'success');
  } catch (e) {
    console.error(e);
    triggerSnackbar('Error al eliminar el contrato', 'error');
  }
}

const formOpen = ref(false)
const selectedContract = ref<LegalContract | null>(null)

function openEditDialog(contract: LegalContract | null) {
  selectedContract.value = contract
  formOpen.value = true
}
</script>

<style scoped>
.responsive-card {
  width: 100%; /* Full width on mobile*/
}

@media (min-width: 1024px) {
  .responsive-card {
    max-width: 35%; /* Big screen only use 35% of the width */
  }
}
</style>