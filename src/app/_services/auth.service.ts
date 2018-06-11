import { Injectable, Inject } from '@angular/core';
import { Signup } from '../_models/signup.model';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  signupUser(user: Signup) {
    console.log(user)
    console.log(this.baseUrl + 'api/account')
    return this.http.post(this.baseUrl + 'api/account', user)
      .subscribe(data => {
        console.log(data, " this is what we got form server...")
      })
  }

}
