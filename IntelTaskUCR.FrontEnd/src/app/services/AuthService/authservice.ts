import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IAuth } from '../../models/iauth';
import { Observable } from 'rxjs';
import { IAuthResponse } from '../../models/iauth-response';

@Injectable({
  providedIn: 'root'
})
export class Authservice {
  private readonly URL = 'https://localhost:7089/auth';
  httpClient = inject(HttpClient);

  constructor() { }

  autenticarUsuario(data: IAuth): Observable<IAuthResponse>{
    return this.httpClient.post<IAuthResponse>(`${this.URL}/authenticate`, data);
  }

  guardarUsuarioEnLocalStorage(usuario: IAuthResponse): void {
    localStorage.setItem('usuario', JSON.stringify(usuario));
  }

  obtenerUsuario(): IAuthResponse | null {
    const stored = localStorage.getItem('usuario');
    return stored ? JSON.parse(stored) : null;
  }

  cerrarSesion(): void {
    localStorage.removeItem('usuario');
  }

  estaAutenticado(): boolean {
    return !!localStorage.getItem('usuario');
  }
}
