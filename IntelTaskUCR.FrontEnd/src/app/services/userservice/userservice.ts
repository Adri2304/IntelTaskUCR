import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../../models/iuser';

@Injectable({
  providedIn: 'root'
})
export class Userservice {
  private readonly URL = 'https://localhost:7089/user';
  httpClient = inject(HttpClient);

  constructor() { }

  readUsers(idUser?: number): Observable<IUser[]>{
    if (idUser) {
      return this.httpClient.get<IUser[]>(`${this.URL}/read/${idUser}`);
    }
    return this.httpClient.get<IUser[]>(`${this.URL}/read`);
  }
}
