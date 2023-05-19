import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(
        private http: HttpClient
    ) { }

    login(data: any): Observable<string> {
        return this.http.post('https://localhost:7173/Login', data, {responseType: 'text'})
    }
}