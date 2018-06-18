import { Injectable, Inject } from '@angular/core';
import { Signup } from '../_models/signup.model';
import { Login } from '../_models/login.model';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { UserProfile } from '../_models/userProfile.model';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class AuthService {

  // Create a stream of logged in status to communicate throughout app
  loggedIn: boolean;
  loggedIn$ = new BehaviorSubject<boolean>(this.loggedIn);
  userProfile: UserProfile;

  constructor(
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    this.loggedIn = !!localStorage.getItem('auth_token');
  }


  signupUser(user: Signup) {

    return this.http.post(this.baseUrl + 'api/account', user)
      .subscribe(
        data => this.router.navigate(['/login'])
      )
  }

  login(user: Login) {

    return this.http.post<Login>(this.baseUrl + 'api/auth/login', user)
      .subscribe(
        data => {
          this.performLogin(data);
        }
      )
  }

  facebookLogin(accessToken: string) {
  
    return this.http.post(
      this.baseUrl + 'api/facebook/authenticate', JSON.stringify({ accessToken }), { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) })
      .subscribe(data => {
        this.performLogin(data);
      })
  }

  logout() {

    this.loggedIn = false
    localStorage.removeItem("id_token");
    localStorage.removeItem("auth_token");
    localStorage.removeItem("expires_at");
  }

  getUserProfile() {

    let authToken = localStorage.getItem("auth_token");

    if (this.userProfile == null) {
      this.http.get<UserProfile>(this.baseUrl + 'api/profile', { headers: new HttpHeaders({ 'Authorization': 'Bearer ' + authToken }) })
        .subscribe(data => {
          this.userProfile = data
        })
    }
    return this.userProfile
  }

  isLoggedIn() {

    return this.loggedIn;
  }

  private performLogin(data) {
    this.setSession(data);
    this.loggedIn = true;
    this.router.navigate(['/profile']);
  }

  private setSession(authResult) {

    const expiresAt = authResult.expiresIn * 1000 + Date.now();
    localStorage.setItem('id_token', authResult.id);
    localStorage.setItem('auth_token', authResult.authToken);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
  }
}
