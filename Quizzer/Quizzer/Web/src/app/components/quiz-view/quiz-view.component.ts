import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Quiz } from 'src/app/models/quiz.model';
import { Role } from 'src/app/models/role.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-quiz-view',
    templateUrl: './quiz-view.component.html',
    styleUrls: ['./quiz-view.component.css']
})
export class QuizViewComponent implements OnInit {
    quiz: Quiz;
    role: Role | null;

    constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService) { }

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

        this.sortByIndexes();
    }

    sortByIndexes(): void {
        this.quiz.questions.sort((a, b) => a.questionIndex - b.questionIndex)
        this.quiz.questions.forEach(question => {
            question.answers.sort((a, b) => a.answerIndex - b.answerIndex);
        });
    }

    mapToAlphabet(index: number): string {
        const alphabet = 'ABCDE';
        const charIndex = index;
        return alphabet.charAt(charIndex);
    }

    Back(): void {
        this.router.navigate(['/quizzes']);
    }
}
