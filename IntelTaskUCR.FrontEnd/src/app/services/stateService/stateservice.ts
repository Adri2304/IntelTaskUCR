import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Stateservice {
  private readonly URL = 'https://localhost:7089/status';
  private httpClient: HttpClient = inject(HttpClient);

  constructor() {}
 
  getEstadosPosteriores(idEstadoActual: number): Observable<number[]> {
    return this.httpClient.get<number[]>(`${this.URL}/valid/${idEstadoActual}`);
  }
}
