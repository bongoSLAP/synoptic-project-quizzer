import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { Role } from '../models/role.model';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(
        private http: HttpClient,
        private jwt: JwtHelperService,
        private router: Router
    ) { }

    login(data: any): Observable<string> {
        return this.http.post('https://localhost:7173/Login', data, {responseType: 'text'})
    }

    logout(): void {
        localStorage.clear();
        this.router.navigate(['/login']);
    }

    isTokenExpired(token: string): boolean {
        return this.jwt.isTokenExpired(token);
    }

    getUserRole(): Role | null {
        const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        const token = localStorage.getItem('UserToken');

        if (token !== null) {
            const decodedRole = this.jwt.decodeToken(token);
            return decodedRole[roleClaim];
        }

        return null;
    }
}