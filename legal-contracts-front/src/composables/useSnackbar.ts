import { ref } from 'vue';

const showSnackbar = ref(false);
const snackbarText = ref('');
const snackbarColor = ref('primary');

export function useSnackbar() {
  const triggerSnackbar = (text: string, color: string = 'primary') => {
    snackbarText.value = text;
    snackbarColor.value = color;
    showSnackbar.value = true;
  }

  return {
    showSnackbar,
    snackbarText,
    snackbarColor,
    triggerSnackbar,
  };
}