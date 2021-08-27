import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services';
import { User } from './_models';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    currentUser: User;
    accessToken: string;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentUserObservable.subscribe(x => this.currentUser = x);
        this.authenticationService.accessTokenObservable.subscribe(x => this.accessToken = x);
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}