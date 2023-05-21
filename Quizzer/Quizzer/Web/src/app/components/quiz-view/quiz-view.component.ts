import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionInfo } from 'src/app/models/question-info.model';
import { Quiz } from 'src/app/models/quiz.model';
import { Role } from 'src/app/models/role.model';
import { AuthService } from 'src/app/services/auth.service';
import { IndexService } from 'src/app/services/index.service';
import { QuestionService } from 'src/app/services/question.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
    selector: 'app-quiz-view',
    templateUrl: './quiz-view.component.html',
    styleUrls: ['./quiz-view.component.css']
})
export class QuizViewComponent implements OnInit {
    quiz: Quiz;
    role: Role | null;

    constructor(private route: ActivatedRoute, private router: Router, 
        private authService: AuthService, private indexService: IndexService,
        private questionService: QuestionService, private quizService: QuizService) { }

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

    back(): void {
        this.router.navigate(['/quizzes']);
    }

    edit(question: QuestionInfo): void {
        this.router.navigate(['/edit', this.quiz.id, question.id], { state: { question, quizId: this.quiz.id } });
    }

    Delete(question: QuestionInfo): void {
        const confirmed = window.confirm('Are you sure you want to delete this question?');
        if (confirmed) {
            this.questionService.delete(question.id as string).subscribe(
                () => {
                    this.updateQuizList(question.id as string);
                },
                (response: any) => {
                    alert(`Something went wrong: ${this.readResponse(response)}`);
                }
            );
        }
    }

    updateQuizList(questionId: string): void {
        this.deleteQuestionField(questionId);

        this.quizService.list().subscribe(
            (quizzes) => {
                localStorage.setItem('quizzes', JSON.stringify(quizzes));
                const quiz = quizzes.find((quiz: Quiz) => quiz.id === this.quiz.id);
                this.quiz = quiz;
                this.quiz.questions = this.indexService.sortQuestionsByIndexes(this.quiz.questions);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    deleteQuestionField(questionId: string) {
        const index = this.quiz.questions.findIndex(question => question.id === questionId);
        this.quiz.questions.splice(index, 1);
    }

    readResponse(response: any): string {
        if (Array.isArray(response.error)) {
            return response.error[0].errorMessage;
        }

        return response.error;
    }

    add(): void {
        this.router.navigate(['/add', this.quiz.id]);
    }
}
