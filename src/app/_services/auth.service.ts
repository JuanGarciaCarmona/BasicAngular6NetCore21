import { Injectable, Inject } from '@angular/core';
import { Signup } from '../_models/signup.model';
import { Login } from '../_models/login.model';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { UserProfile } from '../_models/userProfile.model';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  userProfile: UserProfile;

  signupUser(user: Signup) {
    return this.http.post(this.baseUrl + 'api/account', user)
      .subscribe(data => {
        console.log(data, " this is what we got form server...")
      })
  }

  login(user: Login) {
    return this.http.post<Login>(this.baseUrl + 'api/auth/login', user)
      .subscribe(data => this.setSession(data))
  }

  logout() {
    localStorage.removeItem("id_token");
    localStorage.removeItem("auth_token");
    localStorage.removeItem("expires_at");
  }

  getUserProfile() {
    const authToken = localStorage.getItem("auth_token");
    console.log('using Auth Token: ' + authToken)
    var httpHeaders = new HttpHeaders()
    httpHeaders.set("Authorization", "Bearer " + authToken);

    return this.http.get(this.baseUrl + 'api/profile', { headers: httpHeaders })
      .subscribe(data => {
        console.log(data, " this is what we got form server...")
      })
  }

  private setSession(authResult) {

    console.log(authResult)
    const expiresAt = authResult.expiresIn * 1000 + Date.now();

    localStorage.setItem('id_token', authResult.id);
    localStorage.setItem('auth_token', authResult.authToken);
    localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
  }
}
