import { Routes } from '@angular/router';
import { Task } from './components/task/task';
import { Request } from './components/request/request';
import { Notification } from './components/notification/notification';
import { Reports } from './components/reports/reports';
import { MainLayout } from './components/main-layout/main-layout';
import { LoginLayout } from './components/login-layout/login-layout';
import { Login } from './components/login/login';
import { authGuard } from './guards/auth-guard';
import { TaskDetail } from './components/task-detail/task-detail';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard], // ⬅️ agregamos el guard acá
    children: [
      { path: 'task', component: Task },
      { path: 'taskdetail/:id', component: TaskDetail },
      { path: 'request', component: Request },
      { path: 'notification', component: Notification },
      { path: 'report', component: Reports },
      { path: '', redirectTo: 'task', pathMatch: 'full' }
    ]
  },
  {
    path: '',
    component: LoginLayout,
    children: [
      { path: 'login', component: Login }
    ]
  },
  { path: '**', redirectTo: '' }
];