import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PurchaseBillService {
  private readonly api = 'http://localhost:5000/api/PurchaseBill';
  constructor(private http: HttpClient) {}

  getLocations(): Observable<any[]> { return this.http.get<any[]>(`${this.api}/locations`); }
  addLine(line: any): Observable<any> { return this.http.post<any>(this.api, line); }
}