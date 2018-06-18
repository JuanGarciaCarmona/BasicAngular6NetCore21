import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-facebook',
  templateUrl: './login-facebook.component.html',
  styleUrls: ['./login-facebook.component.scss']
})
export class LoginFacebookComponent {

  private authWindow: Window;
  failed: boolean;
  error: string;
  errorDescription: string;
  isRequesting: boolean;


  launchFbLogin() {
    this.authWindow = window.open('https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=837090066476106&display=popup&redirect_uri=https://localhost:5001/facebook-auth.html&scope=email', null, 'width=600,height=400');
  }



  constructor(private authService: AuthService, private router: Router) {
    if (window.addEventListener) {
      window.addEventListener("message", this.handleMessage.bind(this), false);

    } else {
      (<any>window).attachEvent("onmessage", this.handleMessage.bind(this));
    }
  }



  handleMessage(event: Event) {

    const message = event as MessageEvent;
    console.log(event)
    // Only trust messages from the below origin.
    if (message.origin !== "https://localhost:5001") return;
    this.authWindow.close();

    const result = JSON.parse(message.data);
    if (!result.status) {
      this.failed = true;
      this.error = result.error;
      this.errorDescription = result.errorDescription;
    }

    else {
      this.failed = false;
      this.isRequesting = true;
      this.authService.facebookLogin(result.accessToken)
    }
  }
}
