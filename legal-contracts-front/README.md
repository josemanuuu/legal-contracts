# legal-contracts-front

Frontend of the Legal Contracts project.

This application displays a list of legal contracts. Contracts can be **edited** or **deleted**.  

The list shows summary information for each contract; for more details, there is a specific button.  

New contracts can also be added.

## Models

All models are connected to the database, as well as the different endpoints and APIs that can be called.  
All models are defined in the `swagger.json` file.

To regenerate the client and services, run:

```sh
npx openapi-typescript-codegen --input ./swagger.json --output ./src/api --client fetch
```

## Folder Structure

### /src
Contains all application code.

- **/api**  
  Auto-generated folder from `swagger.json`.

- **/components**  
  Reusable components used throughout the application.

- **/composable**  
  Composables (Vue 3 reusable logic).

- **/plugins**  
  Plugins used in the project (currently Vuetify).

- **/stores**  
  State management. The project uses one global store for LegalContracts.

### /test
Contains component tests.

## Docker Deployment

The application is containerized and can be deployed alongside the backend using Docker Compose:

```sh
# Build and start all services
docker-compose up --build

# Start only frontend service
docker-compose up frontend

# Start backend and database only (for development)
docker-compose up db backend
```

Access the application at: [http://localhost:3000](http://localhost:3000)

## Testing

The project includes comprehensive testing using Vitest.  
A test suite has been implemented for the `ContractActions.vue` component to verify proper event emission and functionality.

Run the test suite:

```sh
npm run test
```

## Configuration

### Vite Configuration
The project uses Vite with the following key configurations:

- Vue 3 and JSX support  
- Vuetify integration with auto-import  
- Vue DevTools for development  
- API proxy setup for development  
- TypeScript path aliases (`@/` for `src/`)

### Environment Variables
Create a `.env` file for environment-specific variables:

```text
VITE_API_BASE_URL=http://localhost:5000
```

### API Integration
The frontend communicates with a .NET backend API through auto-generated clients based on the OpenAPI specification.  
All API endpoints and data models are defined in `swagger.json` and synchronized between frontend and backend.

### UI Framework
The application uses Vuetify as the UI component framework, providing:

- Material Design components  
- Responsive grid system  
- Theme customization  
- Accessibility support

###  Development Guidelines

- **Code Style:** Follow ESLint rules and Prettier formatting  
- **Type Safety:** Utilize TypeScript for all new components  
- **Component Design:** Use Composition API with TypeScript  
- **State Management:** Utilize Pinia stores for global state  
- **Testing:** Write tests for new components and functionality

## Original Information

### Customize Configuration

See [Vite Configuration Reference](https://vite.dev/config/).

### Project Setup

```sh
npm install
```

### Compile and Hot-Reload for Development

```sh
npm run dev
```

### Type-Check, Compile, and Minify for Production

```sh
npm run build
```

### Lint with [ESLint](https://eslint.org/)

```sh
npm run lint
```
