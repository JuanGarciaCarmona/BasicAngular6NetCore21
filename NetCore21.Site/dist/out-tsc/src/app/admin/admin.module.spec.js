"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var admin_module_1 = require("./admin.module");
describe('AdminModule', function () {
    var adminModule;
    beforeEach(function () {
        adminModule = new admin_module_1.AdminModule();
    });
    it('should create an instance', function () {
        expect(adminModule).toBeTruthy();
    });
});
//# sourceMappingURL=admin.module.spec.js.map