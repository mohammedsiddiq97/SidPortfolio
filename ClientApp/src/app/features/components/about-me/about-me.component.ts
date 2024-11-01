import { Component, OnInit } from '@angular/core';
import { TechnicalSkillService } from '../../../core/http/technical-skill.service';
import { IResponse } from '../../../core/models/response-model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-about-me',
  templateUrl: './about-me.component.html',
  styleUrl: './about-me.component.css'
})
export class AboutMeComponent implements OnInit {
  technicalSkills: string[] = [];

  constructor(private technicalSkillService: TechnicalSkillService, private toastr: ToastrService) {
  }
  ngOnInit(): void {
    this.getTechnicalSkills();
  }
  getTechnicalSkills() {
    this.technicalSkillService.getTechnicalSkills().subscribe({
      next: (technicalSkillsResponse: IResponse) => {
        if (technicalSkillsResponse.isSuccess) {
          this.technicalSkills = technicalSkillsResponse.value;
        } else {
          this.toastr.error("Failed to load Certifications", "Error");
        }
      },
      error: (e) => {
        console.log("Failed to Fetch Technical Skills");
      }
    });
  }
}
