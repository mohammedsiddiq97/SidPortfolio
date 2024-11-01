import { Component, OnInit } from '@angular/core';
import { ContactusService } from '../../../core/http/contact-us.service';
import { NgForm } from '@angular/forms';
import { IContactUs } from '../../../core/models/contact-us-model';
import { SpinnerService } from '../../../shared/sharedservices/spinner-service';
import { ToastrService } from 'ngx-toastr';
import { IResponse } from '../../../core/models/response-model';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit {
  isSuccess: boolean = false;
  popupStatus: 'success' | 'failed' = 'success';
  userDetails: IContactUs = <IContactUs>{};
  showButton: boolean = false;

  constructor(private contactusService: ContactusService,
    private spinnerService: SpinnerService,
    private toastr: ToastrService) {

  }
  ngOnInit(): void {
  }

  SaveUserDetails(contactus: NgForm) {
    this.spinnerService.show();
    this.showButton = true;
    let userDetails = this.userDetails;
    if (userDetails.phoneNumber)
      userDetails.phoneNumber = userDetails.phoneNumber.toString();
    this.contactusService.SaveUser(userDetails).subscribe({
      next: (data: IResponse) => {
        contactus.resetForm(this.userDetails);
        this.showButton = false;
        if (data.isSuccess)
          this.toastr.success(data.value, "Success");
        else
          this.toastr.error(data.value, "Error");
        this.spinnerService.hide();
      },
      error: (e) => {
        this.toastr.error("Something went wrong please try again later", "Error");
        this.spinnerService.hide();
        this.showButton = false;
      }
    });
  }
  ontest(NgForm: any){
    console.log(NgForm)
  }
}
