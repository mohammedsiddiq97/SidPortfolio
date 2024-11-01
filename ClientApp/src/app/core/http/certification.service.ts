import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { APIService } from './api.service';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root', 
})
export class CertificationService {
  private apiUrl = environment.appurl;

  constructor(private apiService: APIService) { }

getCertificationDetails():Observable<any>{
  const url = `${this.apiUrl}/Certification`;
  return this.apiService.get(url);
}

  
}
