"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var signup_model_1 = require("../_models/signup.model");
var auth_service_1 = require("../_services/auth.service");
var SignupComponent = /** @class */ (function () {
    function SignupComponent(auth) {
        this.auth = auth;
    }
    SignupComponent.prototype.ngOnInit = function () {
    };
    SignupComponent.prototype.signupUser = function (event) {
        event.preventDefault();
        var target = event.target;
        var user = new signup_model_1.Signup();
        user.name = target.querySelector('#name').value;
        user.familyName = target.querySelector('#familyName').value;
        user.email = target.querySelector('#email').value;
        user.password = target.querySelector('#password').value;
        user.nickName = target.querySelector('#nickName').value;
        user.location = target.querySelector('#location').value;
        this.auth.signupUser(user);
    };
    SignupComponent = __decorate([
        core_1.Component({
            selector: 'app-signup',
            templateUrl: './signup.component.html',
            styleUrls: ['./signup.component.scss']
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthService])
    ], SignupComponent);
    return SignupComponent;
}());
exports.SignupComponent = SignupComponent;
//# sourceMappingURL=signup.component.js.map