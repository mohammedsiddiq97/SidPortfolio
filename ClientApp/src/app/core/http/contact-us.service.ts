import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { APIService } from './api.service';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root', 
})
export class ContactusService {
  private apiUrl = environment.appurl;

  constructor(private apiService: APIService) { }

  SaveUser(data: any): Observable<any> {
    const url = `${this.apiUrl}/ContactUs/SaveNewUser`;
  return this.apiService.post(url,data);
  }
}
