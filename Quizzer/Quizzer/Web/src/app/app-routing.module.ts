import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { LoginComponent } from './components/login/login.component';
import { QuizEditComponent } from './components/quiz-edit/quiz-edit.component';
import { QuizViewComponent } from './components/quiz-view/quiz-view.component';

const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'quizzes', component: QuizListComponent },
    { path: 'quiz/:quizId', component: QuizViewComponent },
    { path: 'edit-quiz/:id', component: QuizEditComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
