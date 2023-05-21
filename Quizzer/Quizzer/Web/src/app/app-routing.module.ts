import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { LoginComponent } from './components/login/login.component';
import { QuestionEditComponent } from './components/question-edit/question-edit.component';
import { QuizViewComponent } from './components/quiz-view/quiz-view.component';
import { QuestionAddComponent } from './components/question-add/question-add.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'quizzes', component: QuizListComponent },
    { path: 'quiz/:quizId', component: QuizViewComponent },
    { path: 'edit/:quizId/:questionId', component: QuestionEditComponent },
    { path: 'add/:quizId', component: QuestionAddComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
