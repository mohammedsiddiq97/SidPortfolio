import {  Component, OnInit, ViewChild } from '@angular/core';
import { IExperience } from '../../../core/models/experience-model';
import { ExperienceService } from '../../../core/http/experience.service';
import { IResponse } from '../../../core/models/response-model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-experience',
  templateUrl: './experience.component.html',
  styleUrl: './experience.component.css'
})
export class ExperienceComponent implements OnInit {
  experiences: IExperience[] = [];

  constructor(private experienceService: ExperienceService,
    private toastrService: ToastrService) { }
  ngOnInit(): void {
    this.getExperienceInfo();
  }
  getExperienceInfo() {
    this.experienceService.getExperienceInfo().subscribe({
      next: (experienceResponse: IResponse) => {
        if (experienceResponse.isSuccess) {
          this.experiences = experienceResponse.value;
        } else {
          this.toastrService.error("Failed to load ExperienceInfo", "Error");
        }
      },
      error: (e) => {
      }
    });
  }

}
