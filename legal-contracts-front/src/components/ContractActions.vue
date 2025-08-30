<template>
    <v-btn
      color="grey-lighten-1"
      icon="mdi-information"
      variant="text"
      v-tooltip="'Show contract details'"
      @click="showDetails = true"
    />

    <v-btn
      color="grey-lighten-1"
      icon="mdi-file-edit"
      variant="text"
      v-tooltip="'Edit contract'"
      @click="$emit('edit', contract)"
    />

    <v-btn
      color="grey-lighten-1"
      icon="mdi-delete"
      variant="text"
      v-tooltip="'Delete contract'"
      @click="$emit('delete', contract.id)"
    />

    <ContractDetailsDialog
      :contract="contract"
      :open="showDetails" 
      @open="showDetails = $event"
    />
</template>

<script setup lang="ts">
import type { LegalContract } from '@/api';
import ContractDetailsDialog from './ContractDetailsDialog.vue';
import { ref } from 'vue';

defineProps<{
  contract: LegalContract
}>()

defineEmits<{
  (e: "edit", contract: LegalContract): void
  (e: "delete", id: number): void
}>()

const showDetails = ref(false);
</script>
