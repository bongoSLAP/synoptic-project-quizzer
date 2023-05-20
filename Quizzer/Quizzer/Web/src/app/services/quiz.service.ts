import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Quiz } from '../models/quiz.model';

@Injectable({
    providedIn: 'root'
})
export class QuizService {

    constructor(
        private http: HttpClient
    ) { }

    list(): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        
        return this.http.get('https://localhost:7173/Quiz/List', { headers })
    }
}
