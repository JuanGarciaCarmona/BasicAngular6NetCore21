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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var rxjs_1 = require("rxjs");
var AuthService = /** @class */ (function () {
    function AuthService(router, http, baseUrl) {
        this.router = router;
        this.http = http;
        this.baseUrl = baseUrl;
        this.loggedIn$ = new rxjs_1.BehaviorSubject(this.loggedIn);
        this.loggedIn = !!localStorage.getItem('auth_token');
    }
    AuthService.prototype.signupUser = function (user) {
        var _this = this;
        return this.http.post(this.baseUrl + 'api/account', user)
            .subscribe(function (data) { return _this.router.navigate(['/login']); });
    };
    AuthService.prototype.login = function (user) {
        var _this = this;
        return this.http.post(this.baseUrl + 'api/auth/login', user)
            .subscribe(function (data) {
            _this.performLogin(data);
        });
    };
    AuthService.prototype.facebookLogin = function (accessToken) {
        var _this = this;
        return this.http.post(this.baseUrl + 'api/facebook/authenticate', JSON.stringify({ accessToken: accessToken }), { headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' }) })
            .subscribe(function (data) {
            _this.performLogin(data);
        });
    };
    AuthService.prototype.logout = function () {
        this.loggedIn = false;
        localStorage.removeItem("id_token");
        localStorage.removeItem("auth_token");
        localStorage.removeItem("expires_at");
    };
    AuthService.prototype.getUserProfile = function () {
        var _this = this;
        var authToken = localStorage.getItem("auth_token");
        if (this.userProfile == null) {
            this.http.get(this.baseUrl + 'api/profile', { headers: new http_1.HttpHeaders({ 'Authorization': 'Bearer ' + authToken }) })
                .subscribe(function (data) {
                _this.userProfile = data;
            });
        }
        return this.userProfile;
    };
    AuthService.prototype.isLoggedIn = function () {
        return this.loggedIn;
    };
    AuthService.prototype.performLogin = function (data) {
        this.setSession(data);
        this.loggedIn = true;
        this.router.navigate(['/profile']);
    };
    AuthService.prototype.setSession = function (authResult) {
        var expiresAt = authResult.expiresIn * 1000 + Date.now();
        localStorage.setItem('id_token', authResult.id);
        localStorage.setItem('auth_token', authResult.authToken);
        localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));
    };
    AuthService = __decorate([
        core_1.Injectable(),
        __param(2, core_1.Inject('BASE_URL')),
        __metadata("design:paramtypes", [router_1.Router,
            http_1.HttpClient, String])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map