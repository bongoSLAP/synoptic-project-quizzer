import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class QuizService {

    constructor(
        private http: HttpClient
    ) { }

    list(data: any): Observable<string> {
        return this.http.post('https://localhost:7173/Quiz/List', data, {responseType: 'text'})
    }
}
