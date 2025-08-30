import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import { vuetify } from './plugins/vuetify'

const app = createApp(App)

app.use(createPinia()) // store
app.use(vuetify) // styled components

app.mount('#app')
