import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { User } from '../_models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUserObservable: Observable<User>;
    
    private accessTokenSubject: BehaviorSubject<string>;
    public accessTokenObservable: Observable<string>;
    
    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUserObservable = this.currentUserSubject.asObservable();

        this.accessTokenSubject = new BehaviorSubject<string>(JSON.parse(localStorage.getItem('accessToken')));
        this.accessTokenObservable = this.accessTokenSubject.asObservable();
    }
    

    public get currentUser(): User {
        return this.currentUserSubject.value;
    }

    public get accessToken(): string {
        return this.accessTokenSubject.value;
    }

    login(username: string, password: string) {
        return this.http.post<any>(`${environment.apiUrl}/api/auth/sign-in`, { username, password })
            .pipe(map(data => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(data.user));
                localStorage.setItem('accessToken', JSON.stringify(data.access_token));
                this.currentUserSubject.next(data.user);
                this.accessTokenSubject.next(data.access_token);
                return data;
            })) 
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        localStorage.removeItem('accessToken');
        this.currentUserSubject.next(null);
        this.accessTokenSubject.next(null);
    }
}