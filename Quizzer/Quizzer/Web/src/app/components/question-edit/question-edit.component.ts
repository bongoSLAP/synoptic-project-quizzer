import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QuestionInfo } from 'src/app/models/question-info.model';
import { Quiz } from 'src/app/models/quiz.model';
import { IndexService } from 'src/app/services/index.service';
import { QuestionService } from 'src/app/services/question.service';
import { AnswerService } from 'src/app/services/answer.service';
import { AnswerInfo } from 'src/app/models/answer-info.model';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
    selector: 'app-question-edit',
    templateUrl: './question-edit.component.html',
    styleUrls: ['./question-edit.component.css']
})
export class QuestionEditComponent implements OnInit {
    question: QuestionInfo;
    quizId: string;
    outcome: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private indexService: IndexService,
        private quizService: QuizService,
        private questionService: QuestionService,
        private answerService: AnswerService
    ) { }

    ngOnInit(): void {
        this.quizId = history.state.quizId;

        if (this.quizId == undefined) {
            this.route.params.subscribe(params => {
                this.quizId = params['quizId']
            });
        }

        this.question = history.state.question;
        
        if (this.question == undefined) {
            this.route.params.subscribe(params => {
                const questionId = params['questionId'];
            
                const quizJson = localStorage.getItem('quizzes');
                const quizzes = quizJson ? JSON.parse(quizJson) : [];
                const quiz = quizzes.find((quiz: Quiz) => quiz.id === this.quizId);
                this.question = quiz.questions.find((question: QuestionInfo) => question.id === questionId);
            });
        }

        this.question.answers = this.indexService.sortAnswersByIndexes(this.question.answers);
    }

    saveAnswerChanges(answer: AnswerInfo): void {
        if (this.isAnswerAlreadyExist(answer)) {
            this.answerService.edit(answer).subscribe(
                () => {
                    this.outcome = 'Answer successfully saved.'
                    this.updateQuizList()
                },
                (response: any) => {
                    this.outcome = `Something went wrong: ${this.readResponse(response)}`;
                }
            );
        } else {
            this.answerService.add({ answerInfo: answer, questionId: this.question.id }).subscribe(
                () => {
                    this.outcome = 'New answer successfully added.';
                    this.updateQuizList()
                },
                (response: any) => {
                    this.outcome = `Something went wrong: ${this.readResponse(response)}`;
                }
            );
        }
    }

    saveQuestionChanges(question: QuestionInfo): void {
        this.questionService.edit(question).subscribe(
            () => {
                this.outcome = 'Question successfully saved.'
                this.updateQuizList()
            },
            (response: any) => {
                this.outcome = `Something went wrong: ${this.readResponse(response)}`;
            }
        );
    }

    deleteAnswer(answer: AnswerInfo): void {
        const confirmed = window.confirm('Are you sure you want to delete this answer?');
        if (confirmed) {
            if (this.isAnswerAlreadyExist(answer)) {
                this.answerService.delete(answer.id as string).subscribe(
                    () => {
                        this.outcome = 'Answer successfully deleted.';
                        this.updateQuizList();
                        this.deleteAnswerField(answer.id as string);
                    },
                    (response: any) => {
                        this.outcome = `Something went wrong: ${this.readResponse(response)}`;
                    }
                );
            } 
            else {
                this.deleteAnswerField(answer.id as string);
            }
        }
    }

    addAnswerField(): void {
        const newAnswer: AnswerInfo = {
            answerIndex: this.question.answers.length, 
            text: '' 
        };

        this.question.answers.push(newAnswer);
    }

    deleteAnswerField(answerId: string): void {
        const index = this.question.answers.findIndex(answer => answer.id === answerId);
        this.question.answers.splice(index, 1);
    }

    isAnswerAlreadyExist(answer: AnswerInfo): boolean {
        return !!answer.id;
    }

    readResponse(response: any): string {
        if (Array.isArray(response.error)) {
            return response.error[0].errorMessage;
        }

        return response.error;
    }

    updateQuizList(): void {
        this.quizService.list().subscribe(
            (quizzes) => {
                localStorage.setItem('quizzes', JSON.stringify(quizzes));
                const quiz = quizzes.find((quiz: Quiz) => quiz.id === this.quizId);
                this.question = quiz.questions.find((question: QuestionInfo) => question.id === this.question.id);
                this.question.answers = this.indexService.sortAnswersByIndexes(this.question.answers);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    back(): void {
        this.router.navigate([`/quiz/${this.quizId}`]);
    }
}
