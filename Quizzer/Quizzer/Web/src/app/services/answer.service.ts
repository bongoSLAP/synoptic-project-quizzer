import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AnswerAddInfo } from '../models/answer-add-info.model';
import { Observable } from 'rxjs';
import { AnswerInfo } from '../models/answer-info.model';

@Injectable({
  providedIn: 'root'
})
export class AnswerService {

    constructor(
        private http: HttpClient
    ) { }
    
    add(answer: AnswerAddInfo): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        return this.http.post('https://localhost:7173/Answer/Add', answer, { headers });
    }

    delete(answerId: string): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        return this.http.post('https://localhost:7173/Answer/Delete', { id: answerId }, { headers });
    }

    edit(answer: AnswerInfo): Observable<any> {
        const token = localStorage.getItem('UserToken');
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

        return this.http.post('https://localhost:7173/Answer/Edit', answer, { headers });
    }
}
