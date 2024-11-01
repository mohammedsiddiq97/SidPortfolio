import { Component, OnInit } from '@angular/core';
import { ProjectDetailsService } from '../../../core/http/project.service';
import { ICertification, ICertificationAssociation } from '../../../core/models/certificationmodel';
import { IResponse } from '../../../core/models/response-model';
import { CertificationService } from '../../../core/http/certification.service';
import { IProjectDetail, IProjectDetailAssociation } from '../../../core/models/project-model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-certifications',
  templateUrl: './certifications.component.html',
  styleUrl: './certifications.component.css'
})
export class CertificationsComponent implements OnInit {
  certificationDetails: ICertification[] = [];
  projectDetails: IProjectDetail[] = [];

  constructor(private certificationImagesService: CertificationService,
    private projectDetailsService: ProjectDetailsService,
    private toastr: ToastrService
  ) { }
  ngOnInit() {
    this.getCertifications();
    this.getProjectDetails();
  }

  getCertifications() {
    this.certificationImagesService.getCertificationDetails().subscribe({
      next: (certificationDetails: IResponse) => {
        if (certificationDetails.isSuccess) {
          certificationDetails.value.forEach((certificationDetail: ICertification) => {
            let certification: ICertification = {
              fileName: certificationDetail.fileName,
              contentType: certificationDetail.contentType,
              base64Data: `data:${certificationDetail.contentType};base64,${certificationDetail.base64Data}`,
              certificationAssociation: certificationDetail.certificationAssociation
            };
            this.certificationDetails.push(certification);
          });
        } else {
          this.toastr.error("Failed to load Certifications", "Error");
        }
      },
      error: (e) => {
      }
    });
  }
  getProjectDetails() {
    this.projectDetailsService.getProjectDetails().subscribe({
      next: (projectDetail: IResponse) => {
        if (projectDetail.isSuccess) {
          projectDetail.value.forEach((projectDetail: IProjectDetail) => {
            let project: IProjectDetail = <IProjectDetail>{};
            project.fileName = projectDetail.fileName;
            project.contentType = projectDetail.contentType;
            project.base64Data = `data:${projectDetail.contentType};base64,${projectDetail.base64Data}`;
            project.projectDetailAssociation = projectDetail.projectDetailAssociation;
            this.projectDetails.push(project);
          });
        }
        else {
          this.toastr.error("Failed to load projects", "Error");
        }

      },
      error: (e) => {
        console.log("Failed to Fetch Project");
      }
    });
  }

}
