import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { QuizListComponent } from './components/quiz-list/quiz-list.component';
import { QuizViewComponent } from './components/quiz-view/quiz-view.component';
import { QuizEditComponent } from './components/quiz-edit/quiz-edit.component';
import { FormsModule } from '@angular/forms';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';


@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        QuizListComponent,
        QuizViewComponent,
        QuizEditComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        JwtModule.forRoot({
            config: {
                tokenGetter: () => {
                    return localStorage.getItem('UserToken');
                },
                allowedDomains: ['https://localhost:7173/Login'],
            }
        })
    ],
    providers: [JwtHelperService],
    bootstrap: [AppComponent]
})
export class AppModule { }
