import { Component } from '@angular/core';
import { TokenExpirationService } from './services/token-expiration-service.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'Quizzer';

    constructor(private tokenExpirationService: TokenExpirationService) {}

    ngOnInit(): void {
        this.tokenExpirationService.checkTokenExpiration();
    }
}
