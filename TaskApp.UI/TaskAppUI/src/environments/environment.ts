export const environment = {
  production: (import.meta as any).env.NG_APP_IS_PROD || false,
  apiUrl: (import.meta as any).env.NG_APP_API_URL || 'https://localhost:7263', // (window as any)
};
