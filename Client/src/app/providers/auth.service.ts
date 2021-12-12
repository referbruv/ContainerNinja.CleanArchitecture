import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { AuthToken } from '../models/AuthToken';
import { LoginDTO } from '../models/LoginDTO';
import { Config } from './config';
import { TokenService } from './token.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private http: HttpClient, private tokenService: TokenService) {
    }

    login(loginDto: LoginDTO) {
        return this.http.post(`${Config.api}/Auth/token`, loginDto)
            .pipe(map((response) => {
                let token = <AuthToken>response;
                this.tokenService.setToken(token);
                return token;
            }))
    }
}