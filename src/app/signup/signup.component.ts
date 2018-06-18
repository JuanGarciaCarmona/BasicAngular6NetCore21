import { Component, OnInit } from '@angular/core';
import { Signup } from '../_models/signup.model';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})

export class SignupComponent implements OnInit {
  constructor(private auth: AuthService) { }
  ngOnInit() {
  }

  signupUser(event) {
    event.preventDefault()
    const target = event.target
    var user: Signup = new Signup();
    user.name = target.querySelector('#name').value
    user.familyName = target.querySelector('#familyName').value
    user.email = target.querySelector('#email').value
    user.password = target.querySelector('#password').value
    user.nickName = target.querySelector('#nickName').value
    user.location = target.querySelector('#location').value
    this.auth.signupUser(user)
  }
}
