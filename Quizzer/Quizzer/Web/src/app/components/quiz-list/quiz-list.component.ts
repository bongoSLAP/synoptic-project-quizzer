import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Quiz } from 'src/app/models/quiz.model';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
    selector: 'app-quiz-list',
    templateUrl: './quiz-list.component.html',
    styleUrls: ['./quiz-list.component.css']
})
export class QuizListComponent implements OnInit {
    quizzes: Quiz[];

    constructor(private quizService: QuizService, private router: Router) { }

    ngOnInit(): void {
        this.fetchQuizList();
    }

    fetchQuizList(): void {
        this.quizService.list().subscribe(
            (quizzes) => {
                this.quizzes = quizzes;
                localStorage.setItem('quizzes', JSON.stringify(quizzes));
            },
            (error) => {
                console.log(error);
            }
        );
    }

    navigateToQuizView(quiz: Quiz): void {
        this.router.navigate(['/quiz/', quiz.id], { state: { quiz }});
    }
}
