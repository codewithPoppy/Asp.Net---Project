import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Guest } from '../interfaces/guest.interface';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GuestService {
  private readonly apiController: string = "guests";

  get apiUrl(): string {
    return `${environment.apiUrl}/${this.apiController}`;
  }

  constructor(private http: HttpClient) { }

  public getList(): Observable<any> { return this.http.get(`${this.apiUrl}`); }

  public create(guest: Guest): Observable<any> {
    return this.http.post(`${this.apiUrl}`, guest);
  }

  public get(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  public update(guest: Guest): Observable<any> {
    return this.http.put(`${this.apiUrl}/${guest.id}`, guest);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
