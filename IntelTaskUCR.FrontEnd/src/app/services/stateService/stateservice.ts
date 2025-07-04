import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { IStates } from '../../models/istates';
import { IComplexities } from '../../models/icomplexities';
import { IPriorities } from '../../models/ipriorities';

@Injectable({
  providedIn: 'root'
})
export class Stateservice {
  private readonly URL = 'https://localhost:7089/states';
  private httpClient: HttpClient = inject(HttpClient);
  private selectedIdsSubject = new BehaviorSubject<number[]>([]);
  selectedIds$ = this.selectedIdsSubject.asObservable();

  constructor() {}

  updateSelectedIds(ids: number[]) {
    this.selectedIdsSubject.next(ids);
  }

  getEstadosPosteriores(idEstadoActual: number): Observable<number[]> {
    return this.httpClient.get<number[]>(`${this.URL}/valid/${idEstadoActual}`);
  }

  getAllStates(): Observable<IStates[]>{
    return this.httpClient.get<IStates[]>(`${this.URL}/states`);
  }

  getAllComplexities(): Observable<IComplexities[]>{
    return this.httpClient.get<IComplexities[]>(`${this.URL}/complexities`);
  }

  getAllPriorities(): Observable<IPriorities[]>{
    return this.httpClient.get<IPriorities[]>(`${this.URL}/priorities`);
  }
}
