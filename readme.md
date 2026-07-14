# TaskApp

This is a simple app that manages tasks per status and user. You need to log in to be able to see all the tasks, create new ones and update the status of a task. The backend runs on net 10.0 and the UI runs on Angular 21. Additionally, you will need a SQL server to run the backend, run the migrations and seeds, they will run automatically.

## TaskApp.API

This API runs on net10.0, you can open the solution using Visual Studio, or run it directly using the console. But first, you need to update the Database Connection String located in
`TaskApp.Api\TaskApp.Api\appsettings.Development.json`
``` json
  "ConnectionStrings": {
    "DefaultConnection": "..."
  },
```

The first time the app runs it will create the database (if needed), and run the migrations to create the tables and seed data.

If you're using VS and runs the 'https' profile you can go to https://localhost:7263/swagger to verify the app is running and also check the current endpoints.

## TaskApp.UI

This app allows you to manage tasks, see them all by its status, create new tasks and also update them, but you need to authenticate first. Credentials are suggested in the login page.

The project runs on Angular 21, you will need Node versions ^20.19.0 || ^22.12.0 || ^24.0.0 to run it.

### First Step

Please update the API URL as needed in thefollowing location:
``` typescript
/// TaskAppUI\src\environments\environment.ts
export const environment = {
  apiUrl: 'https://localhost:7263',
};
```

Then, you need to install npm packages
``` bash
npm install
```

And finally, you can run the app
``` bash
npm run start
```

You can now go to http://localhost:4200/ and explore the app.
