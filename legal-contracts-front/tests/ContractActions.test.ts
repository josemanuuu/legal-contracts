import { createVuetify } from 'vuetify'
import { fireEvent, render } from '@testing-library/vue'
import ContractActions from '../src/components/ContractActions.vue'
import { describe, expect, test, vi } from 'vitest'

// Mock del diálogo
vi.mock('../src/components/ContractDetailsDialog.vue', () => ({
  default: { template: '<div data-testid="dialog" />', props: ['contract','open'] }
}))

const vuetify = createVuetify()

const sampleContract = { 
  id: 1, 
  author: 'Alice', 
  entityName: 'Company A', 
  createdAt: new Date().toISOString(), 
  updatedAt: new Date().toISOString()
}

// Helper para renderizar con Vuetify
// eslint-disable-next-line @typescript-eslint/no-explicit-any
function renderWithVuetify(component: any, options: any = {}) {
  return render(component, {
    global: {
      plugins: [vuetify],
      ...options.global,
    },
    ...options,
  })
}

describe('ContractActions', () => {
  test('emits edit event', async () => {
    const { container, emitted } = renderWithVuetify(ContractActions, { props: { contract: sampleContract } })
    const buttons = container.querySelectorAll('button')
    await fireEvent.click(buttons[1]) // segundo botón = edit
    expect(emitted().edit[0]).toEqual([sampleContract])
  })

  test('emits delete event', async () => {
    const { container, emitted } = renderWithVuetify(ContractActions, { props: { contract: sampleContract } })
    const buttons = container.querySelectorAll('button')
    await fireEvent.click(buttons[2]) // tercer botón = delete
    expect(emitted().delete[0]).toEqual([sampleContract.id])
  })

  test('opens dialog event', async () => {
    const { container, getByTestId } = renderWithVuetify(ContractActions, { props: { contract: sampleContract } })
    const buttons = container.querySelectorAll('button')
    await fireEvent.click(buttons[0]) // primer botón = show details
    expect(getByTestId('dialog')).toBeTruthy()
  })
})
