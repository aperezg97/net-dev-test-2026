import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { KanbanComponent } from './components/kanban/kanban.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'tasks', component: KanbanComponent, canActivate: [authGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];
