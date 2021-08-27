import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models';
import { UserService, AuthenticationService } from '../_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    user: User;

    constructor(private userService: UserService, private authService: AuthenticationService) {}

    ngOnInit() {
        this.loading = true;        
        if (this.user = this.authService.currentUser) {
            this.loading = false;
        };
    }
}