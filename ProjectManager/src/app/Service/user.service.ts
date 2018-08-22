import { Injectable } from '@angular/core';
///import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, Subject, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'app/Model/user';

const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });


@Injectable()
export class UserService {

    selectedUser: User;
    UserList: User[];
    constructor(private http: HttpClient) { }

    postUser(User: User) {
        var body = JSON.stringify(User);
        return this.http.post('http://localhost:51052/api/User', body, { headers: headers }).subscribe();
    }

    putUser(id: string, User: string) {
        var body = JSON.stringify(User);
        return this.http.put('http://localhost:51052/api/User/' + id,
            body, { headers: headers }).subscribe();
    }

    getUserList() {
        this.http.request("GET", "http://localhost:51052/api/User/", { responseType: "json" }).subscribe(x=> this.UserList = x as User[]);
    }

    deleteUser(id: number) {
        return this.http.delete('http://localhost:51052/api/User/' + id).subscribe();
    }
}