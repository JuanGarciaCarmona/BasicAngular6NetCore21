import { Component, OnInit } from '@angular/core';
import { Login } from '../_models/login.model';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  constructor(private auth: AuthService) { }

  ngOnInit() {
  }
  
  loginUser(event) {
    event.preventDefault()
    const target = event.target
    var user: Login = new Login();
    user.userName = target.querySelector('#email').value
    user.password = target.querySelector('#password').value
    this.auth.login(user)
  }
}
