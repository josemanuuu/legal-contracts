import { defineStore } from 'pinia';
import { ref } from 'vue';
import { type LegalContract, type LegalContractCreateDto, LegalContractsService } from '@/api';

export const useContractsStore = defineStore('contracts', () => {
  const contracts = ref<LegalContract[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);

  const fetchContracts = async () => {
    loading.value = true;
    error.value = null;
    try {
      contracts.value = await LegalContractsService.getApiLegalContracts();
    } catch (err) {
      error.value = 'Error al cargar contratos';
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  const createContract = async (newContract: LegalContractCreateDto) => {
    loading.value = true;
    try {
      const created = await LegalContractsService.postApiLegalContracts(newContract);
      contracts.value.push(created);
    } catch (err) {
      error.value = 'Error al crear contrato';
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  const deleteContract = async (id: number) => {
    loading.value = true;
    try {
      await LegalContractsService.deleteApiLegalContracts(id);
      contracts.value = contracts.value.filter(c => c.id !== id);
    } catch (err) {
      error.value = 'Error al borrar contrato';
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  const updateContract = async (p0: number, updatedContract: LegalContract) => {
    loading.value = true;
    try {
      await LegalContractsService.putApiLegalContracts(updatedContract.id, updatedContract);
      // Actualiza el contrato en el array local
      const idx = contracts.value.findIndex(c => c.id === updatedContract.id);
      if (idx !== -1) {
        contracts.value[idx] = { ...updatedContract };
      }
    } catch (err) {
      error.value = 'Error al actualizar contrato';
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  return { contracts, loading, error, fetchContracts, createContract, deleteContract, updateContract };
});
