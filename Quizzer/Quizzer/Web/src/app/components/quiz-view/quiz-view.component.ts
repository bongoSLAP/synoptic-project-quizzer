import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionInfo } from 'src/app/models/question-info.model';
import { Quiz } from 'src/app/models/quiz.model';
import { Role } from 'src/app/models/role.model';
import { AuthService } from 'src/app/services/auth.service';
import { IndexService } from 'src/app/services/index.service';

@Component({
    selector: 'app-quiz-view',
    templateUrl: './quiz-view.component.html',
    styleUrls: ['./quiz-view.component.css']
})
export class QuizViewComponent implements OnInit {
    quiz: Quiz;
    role: Role | null;

    constructor(private route: ActivatedRoute, private router: Router, 
        private authService: AuthService, private indexService: IndexService) { }

    ngOnInit(): void {
        this.role = this.authService.getUserRole(); 
        console.log(this.role)

        this.quiz = history.state.quiz;

        if (this.quiz == undefined) {
            this.route.params.subscribe(params => {
                const quizId = params['quizId'];
            
                const quizJson = localStorage.getItem('quizzes');
                const quizzes = quizJson ? JSON.parse(quizJson) : [];
                this.quiz = quizzes.find((quiz: Quiz) => quiz.id === quizId);
            });
        }

        this.quiz.questions = this.indexService.sortQuestionsByIndexes(this.quiz.questions);

        this.quiz.questions.forEach(question => {
            question.answers = this.indexService.sortAnswersByIndexes(question.answers);
        });
    }

    Back(): void {
        this.router.navigate(['/quizzes']);
    }

    Edit(question: QuestionInfo): void {
        this.router.navigate(['/edit', this.quiz.id, question.id], { state: { question, quizId: this.quiz.id } });
    }
}
