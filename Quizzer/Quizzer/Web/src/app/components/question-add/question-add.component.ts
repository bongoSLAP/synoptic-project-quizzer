import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AnswerInfo } from 'src/app/models/answer-info.model';
import { QuestionInfo } from 'src/app/models/question-info.model';
import { IndexService } from 'src/app/services/index.service';
import { QuestionService } from 'src/app/services/question.service';
import { QuizService } from 'src/app/services/quiz.service';

@Component({
    selector: 'app-question-add',
    templateUrl: './question-add.component.html',
    styleUrls: ['./question-add.component.css']
})
export class QuestionAddComponent implements OnInit {
    quizId: string;
    question: QuestionInfo = new QuestionInfo();
    outcome: string;

    constructor(private router: Router, private route: ActivatedRoute, 
        private indexService: IndexService, private questionService: QuestionService,
        private quizService: QuizService) { }

    ngOnInit(): void {
        this.question.answers = []
        this.addAnswerField()

        this.route.params.subscribe(params => {      
            this.quizId = params['quizId'];  
        });
    }

    back(): void {
        this.router.navigate([`/quiz/${this.quizId}`]);
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

    addQuestion(): void {
        this.questionService.add({ questionInfo: this.question, quizId: this.quizId}).subscribe(
            () => {
                this.updateQuizList()
            },
            (response: any) => {
                this.outcome = `Something went wrong: ${this.readResponse(response)}`;
            }
        );
    }

    updateQuizList(): void {
        this.quizService.list().subscribe(
            (quizzes) => {
                localStorage.setItem('quizzes', JSON.stringify(quizzes));
                this.back();
            },
            (error) => {
                console.log(error);
            }
        );
    }

    readResponse(response: any): string {
        if (Array.isArray(response.error)) {
            return response.error[0].errorMessage;
        }

        return response.error;
    }
}
