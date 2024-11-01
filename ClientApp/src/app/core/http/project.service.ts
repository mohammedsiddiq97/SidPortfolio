import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { APIService } from './api.service';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root', 
})
export class ProjectDetailsService {
  private apiUrl = environment.appurl;
  constructor(private apiService: APIService) { }
getProjectDetails():Observable<any>{
  const url = `${this.apiUrl}/ProjectDetail`;
  return this.apiService.get(url);
} 
}
