import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { AdminModule } from './admin/admin.module';
import { AuthService } from './_services/auth.service';

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    LoginComponent,
    HomeComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AdminModule
  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl },
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }



export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
