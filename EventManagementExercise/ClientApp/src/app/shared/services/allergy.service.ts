import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Allergy } from '../interfaces/allergy.interface';

@Injectable({
  providedIn: 'root'
})
export class AllergyService {
  private readonly apiController: string = "allergies";

  get apiUrl(): string {
    return `${environment.apiUrl}/${this.apiController}`;
  }

  constructor(private http: HttpClient) { }

  public getList(): Observable<any> { return this.http.get(`${this.apiUrl}`); }

  public create(allergy: Allergy): Observable<any> {
    return this.http.post(`${this.apiUrl}`, allergy);
  }

  public get(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  public update(allergy: Allergy): Observable<any> {
    return this.http.put(`${this.apiUrl}/${allergy.id}`, allergy);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
