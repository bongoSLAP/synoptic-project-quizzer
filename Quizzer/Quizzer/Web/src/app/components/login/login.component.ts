import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    outcome: string;
  
    constructor(
        private authService: AuthService,
        private router: Router
    ) { }

    ngOnInit(): void {
    }

    submit(data: any): void {
        if (data.username == "" || data.password == "") {
            this.outcome = 'One or more fields are empty.';
            return;
        }

        console.log(data)
    	this.authService.login(data)
    	.subscribe(
            (result) => {
              console.log('token: ', result);
              this.outcome = '';
              localStorage.setItem('UserToken', result);
              this.router.navigate(['/quizzes']); // Replace '/dashboard' with your desired route

            },
            (error) => {
                console.log(error);
                this.outcome = 'Username or password is unrecognised.';
            }
    	);   
    }
}