import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITask } from '../../models/itask';
import { Observable } from 'rxjs';
import { IChangeStateTask } from '../../models/ichange-state-task';
import { IUpdateTask } from '../../models/iupdate-task';
import { IFilterTask } from '../../models/ifilter-task';

@Injectable({
  providedIn: 'root'
})
export class TaskService { 
  private readonly URL = 'https://localhost:7089/task';
  private httpClient: HttpClient = inject(HttpClient);

  constructor() { }

  ReadTaskPerUser(idUser: number): Observable<any> {
    return this.httpClient.get<any>(`${this.URL}/user/${idUser}`);
  }

  createTask(taskData: { [key: string]: any }): Observable<any> {
    return this.httpClient.post<any>(`${this.URL}/create`, taskData);
  }

  changeTaskState(changeData: IChangeStateTask): Observable<any> {
    return this.httpClient.post<any>(`${this.URL}/changestatus`, changeData);
  }

  updateTask(idTask: number, data: IUpdateTask): Observable<any>{
    return this.httpClient.patch<any>(`${this.URL}/update/${idTask}`, data);
  }

  filterTaskPerUser(idUser: number, body: IFilterTask): Observable<any> {
    return this.httpClient.post<any>(`${this.URL}/filter/${idUser}`, body);
  }

  getAllInfo(idTask: number) {
    return this.httpClient.get<any[]>(`${this.URL}/allinfo/${idTask}`);
  }
}
