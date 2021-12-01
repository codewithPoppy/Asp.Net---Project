import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Event } from '../interfaces/event.interface';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly apiController: string = "events";

  get apiUrl(): string {
    return `${environment.apiUrl}/${this.apiController}`;
  }

  constructor(private http: HttpClient) { }

  public getList(): Observable<any> { return this.http.get(`${this.apiUrl}`); }

  public create(event: Event): Observable<any> {
    return this.http.post(`${this.apiUrl}`, event);
  }

  public get(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  public update(event: Event): Observable<any> {
    return this.http.put(`${this.apiUrl}/${event.id}`, event);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
