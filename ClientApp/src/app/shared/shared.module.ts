import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { SocialMediaProfilesComponent } from './components/social-media-profiles/social-media-profiles.component';


@NgModule({
  declarations: [
    SpinnerComponent,
    NavbarComponent,
    FooterComponent,
    SocialMediaProfilesComponent
  ],
  imports: [
    CommonModule,
  ],
  exports: [
    SpinnerComponent,
    NavbarComponent,
    FooterComponent,
    SocialMediaProfilesComponent
  ]
})
export class SharedModule { }
