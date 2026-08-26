import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', loadComponent: () => import('./features/auth/login/login').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./features/auth/register/register').then(m => m.RegisterComponent) },
  { path: 'comics', loadComponent: () => import('./features/comics/comic-list/comic-list').then(m => m.ComicListComponent), canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];