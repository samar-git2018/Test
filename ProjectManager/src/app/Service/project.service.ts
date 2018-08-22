import { Injectable } from '@angular/core';
///import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { Project } from '../Model/project';

const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });


@Injectable()
export class ProjectService {
    
    selectedProject: Project;
    ProjectList: Project[];
    constructor(private http: HttpClient) { }

    postProject(project: Project) {
        var body = JSON.stringify(project);
        console.log(body);
        var headerOptions = new HttpHeaders({ 'Content-Type': 'application/json' });
       // var requestOptions = new RequestOptions({ method: RequestMethod.Post, headers: headerOptions });
        return this.http.post('http://localhost:51052/api/Project', body, { headers: headers }).subscribe();
    }

    putProject(id: string, project: string) {
        var body = JSON.stringify(project);
       // var headerOptions = new Headers({ 'Content-Type': 'application/json' });
        //var requestOptions = new RequestOptions({ method: RequestMethod.Put: headerOptions });
        return this.http.put('http://localhost:51052/api/Project/' + id,
            body).subscribe();
    }

    getProjectList() {       
        //this.http.get('http://localhost:51052/api/Project')
        //    .toPromise().then(res => {
        //        this.ProjectList = res.json().results;
        //    })
    }

    deleteProject(id: number) {
        return this.http.delete('http://localhost:51052/api/Project/' + id).subscribe();
    }
}