import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiKeyInterceptor } from './Interceptor/api-key.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { APIService } from './http/api.service';
import { TechnicalSkillService } from './http/technical-skill.service';
import { ProjectDetailsService } from './http/project.service';
import { ContactusService } from './http/contact-us.service';
import { CertificationService } from './http/certification.service';
import { ExperienceService } from './http/experience.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,

  ],
  providers:[
    APIService,
    CertificationService,
    ContactusService,
    ProjectDetailsService,
    TechnicalSkillService,
    ExperienceService,
    {provide:HTTP_INTERCEPTORS, useClass:ApiKeyInterceptor,multi:true},
  ]
})
export class CoreModule { }
