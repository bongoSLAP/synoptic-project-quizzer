import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { QuestionInfo } from '../models/question-info.model';
import { QuestionAddInfo } from '../models/question-add-info.model';

@Injectable({
    providedIn: 'root'
})
export class QuestionService {

    constructor(
        private http: HttpClient
    ) { }
    
    add(question: QuestionAddInfo): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        return this.http.post('https://localhost:7173/Question/Add', question, { headers });
    }

    delete(questionId: string): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        return this.http.post('https://localhost:7173/Question/Delete', { id: questionId }, { headers });
    }

    edit(question: QuestionInfo): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        return this.http.post('https://localhost:7173/Question/Edit', question, { headers });
    }
}
