"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// openid standard claims
// https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
var UserProfile = /** @class */ (function () {
    function UserProfile(sub, name, given_name, family_name, middle_name, nickname, preferred_username, profile, picture, website, email, email_verified, gender, birthdate, zoneinfo, locale, phone_number, phone_number_verified, address, updated_at) {
        this.sub = sub;
        this.name = name;
        this.given_name = given_name;
        this.family_name = family_name;
        this.middle_name = middle_name;
        this.nickname = nickname;
        this.preferred_username = preferred_username;
        this.profile = profile;
        this.picture = picture;
        this.website = website;
        this.email = email;
        this.email_verified = email_verified;
        this.gender = gender;
        this.birthdate = birthdate;
        this.zoneinfo = zoneinfo;
        this.locale = locale;
        this.phone_number = phone_number;
        this.phone_number_verified = phone_number_verified;
        this.address = address;
        this.updated_at = updated_at;
    }
    return UserProfile;
}());
exports.UserProfile = UserProfile;
//# sourceMappingURL=userProfile.model.js.map