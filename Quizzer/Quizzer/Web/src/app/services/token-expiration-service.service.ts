import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, timer } from 'rxjs';
import { map, filter, take } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class TokenExpirationService {
    private checkTimer: Observable<number>;

    constructor(private jwtHelper: JwtHelperService, private authService: AuthService) {
        this.checkTimer = timer(0, 60 * 1000);
    }

    checkTokenExpiration(): void {
        this.checkTimer
            .pipe(
                map(() => this.jwtHelper.getTokenExpirationDate()),
                filter(expirationDate => !!expirationDate),
                take(1)
            )
            .subscribe(async expirationDate => {
                const currentTime = new Date().getTime();
                const resolvedExpirationDate = await Promise.resolve(expirationDate);

                if (resolvedExpirationDate !== null) {
                    const expired = resolvedExpirationDate.getTime() < currentTime;
                
                    if (expired) {
                        this.authService.logout();
                    } 
                }
            });
    }

}
