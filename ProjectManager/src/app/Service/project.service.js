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
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
require("rxjs/add/operator/map");
require("rxjs/add/operator/toPromise");
var ProjectService = (function () {
    function ProjectService(http) {
        this.http = http;
    }
    ProjectService.prototype.postProject = function (emp) {
        var body = JSON.stringify(emp);
        var headerOptions = new http_1.Headers({ 'Content-Type': 'application/json' });
        var requestOptions = new http_1.RequestOptions({ method: http_1.RequestMethod.Post, headers: headerOptions });
        return this.http.post('http://localhost:51052/api/Project', body, requestOptions).map(function (x) { return x.json(); });
    };
    ProjectService.prototype.putProject = function (id, project) {
        var body = JSON.stringify(project);
        var headerOptions = new http_1.Headers({ 'Content-Type': 'application/json' });
        var requestOptions = new http_1.RequestOptions({ method: http_1.RequestMethod.Put, headers: headerOptions });
        return this.http.put('http://localhost:51052/api/Project/' + id, body, requestOptions).map(function (res) { return res.json(); });
    };
    ProjectService.prototype.getProjectList = function () {
        var _this = this;
        this.http.get('http://localhost:51052/api/Project')
            .map(function (data) {
            return data.json();
        }).toPromise().then(function (x) {
            _this.ProjectList = x;
        });
    };
    ProjectService.prototype.deleteProject = function (id) {
        return this.http.delete('http://localhost:51052/api/Project/' + id).map(function (res) { return res.json(); });
    };
    return ProjectService;
}());
ProjectService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ProjectService);
exports.ProjectService = ProjectService;
//# sourceMappingURL=project.service.js.map