import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';
import { QuizService } from '../services/quiz.service';

@Injectable({
    providedIn: 'root'
})
export class QuizResolver implements Resolve<any> {

    constructor(private quizService: QuizService) { }

    resolve(): Observable<any> {
        return this.quizService.list();
    }
}