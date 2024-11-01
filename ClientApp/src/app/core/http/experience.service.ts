import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment.development";
import { APIService } from "./api.service";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root', 
  })
  export class ExperienceService {
    private apiUrl = environment.appurl;
  
    constructor(private apiService: APIService) { }
  
    getExperienceInfo(): Observable<any> {
      const url = `${this.apiUrl}/Experience`;
    return this.apiService.get(url);
    }
  }