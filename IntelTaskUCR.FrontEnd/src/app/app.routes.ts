import { Routes } from '@angular/router';
import { Task } from './components/task/task';
import { Request } from './components/request/request';
import { Notification } from './components/notification/notification';
import { Reports } from './components/reports/reports';

export const routes: Routes = [
    {path:"task", component:Task},
    {path:"request", component:Request},
    {path:"notification", component:Notification},
    {path:"report", component:Reports},
    { path: "", redirectTo: "task", pathMatch: "full" }
];
