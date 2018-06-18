import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserProfile } from '../_models/userProfile.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})

export class ProfileComponent implements OnInit {
  userProfile: UserProfile
  profileArray: any[]

  constructor(public auth: AuthService) { }

  ngOnInit() {
    this.userProfile = this.auth.getUserProfile();
    this.profileArray = this._makeProfileArray(this.userProfile);
  }

  private _makeProfileArray(obj) {
    const keyPropArray = [];

    for (const key in obj) {
      if (obj.hasOwnProperty(key)) {
        keyPropArray.push(key + ': ' + obj[key]);
      }
    }

    return keyPropArray;
  }
}
