import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { SpinnerService } from '../../sharedservices/spinner-service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.css'
})
export class SpinnerComponent {
  spinnerVisible: boolean = false;
  private spinnerSubscription: Subscription;

  constructor(private spinnerService: SpinnerService) {
    this.spinnerSubscription = this.spinnerService.spinnerObservable.subscribe(state => {
      this.spinnerVisible = state;
    });
  }
  ngOnDestroy(): void {
    this.spinnerSubscription.unsubscribe();
  }
}
