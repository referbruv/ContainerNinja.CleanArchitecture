import { Injectable } from "@angular/core";
import { AuthToken } from "../models/AuthToken";

@Injectable({
    providedIn: 'root'
})
export class TokenService {
    constructor() { }

    getToken() {
        let tokenObj = localStorage.getItem('auth_token');
        if (tokenObj === null) return null;

        let parsed: AuthToken = JSON.parse(tokenObj);
        return parsed.accessToken;
    }

    setToken(authToken: AuthToken) {
        localStorage.setItem('auth_token', JSON.stringify(authToken));
    }

    clearToken() {
        localStorage.removeItem('auth_token');
    }

    public get IsAuthenticated(): boolean {
        return this.getToken() !== null;
    }
}