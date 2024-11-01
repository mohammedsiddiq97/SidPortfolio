import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { APIService } from './api.service';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root', 
})
export class TechnicalSkillService {
  private apiUrl = environment.appurl;

  constructor(private apiService: APIService) { }

getTechnicalSkills():Observable<any>{
  const url = `${this.apiUrl}/Technicalskill`;
  return this.apiService.get(url);
}

  
}
