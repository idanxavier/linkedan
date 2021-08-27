import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { LoginComponent } from './login';
import { PostsComponent } from './post/posts/posts.component';
import {MatTabsModule} from '@angular/material/tabs';
import {BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { LocalDateTimePipe } from './shared/pipe/local-date-time.pipe';
import {MatChipsModule} from '@angular/material/chips';
import {MatDialogModule} from '@angular/material/dialog';
import { LiveFormDialogComponent } from './posts/live-form-dialog/live-form-dialog.component';
import { MatNativeDateModule } from '@angular/material/core';
import {MatTreeModule} from '@angular/material/tree';
import {MatMenuModule} from '@angular/material/menu';
import { UpdateFormDialogComponent } from './posts/update-form-dialog/update-form-dialog.component';
import { MessageFormDialogComponent } from './posts/message-form-dialog/message-form-dialog.component';
import { RegisterFormDialogComponent } from './posts/register-form-dialog/register-form-dialog.component';

@NgModule({
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        HttpClientModule,
        AppRoutingModule,
        MatTabsModule,
        BrowserAnimationsModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatFormFieldModule,
        MatChipsModule,
        MatDialogModule,
        MatNativeDateModule,
        FormsModule,
        ReactiveFormsModule,
        MatTreeModule,
        MatMenuModule
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        LoginComponent,
        PostsComponent,
        LocalDateTimePipe,
        LiveFormDialogComponent,
        UpdateFormDialogComponent,
        MessageFormDialogComponent,
        RegisterFormDialogComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
        LocalDateTimePipe
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }