import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Like } from '../_models/like';
import { Post } from '../_models/post';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})

export class PostService {

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  listUserPosts() {
    return this.http.get<Post[]>(`${environment.apiUrl}/api/Post/list-user-posts`);
  }

  listUsers() {
    return this.http.get<User[]>(`${environment.apiUrl}/api/Auth/list-users`);
  }

  listUserLikes() {
    return this.http.get<Like[]>(`${environment.apiUrl}/api/Post/list-user-likes`);
  }

  newPost(post: any): Observable<Post>{
    return this.http.post<any>(`${environment.apiUrl}/api/Post/create-post`, post, this.httpOptions);
  }

  newAccount(user: any): Observable<any>{
    return this.http.post<any>(`${environment.apiUrl}/api/Auth/sign-up`, user, this.httpOptions);
  }

  deletePost(id: number): Observable<number> {
    return this.http.delete<number>(`${environment.apiUrl}/api/Post/delete-post?postId=`+ id);
  }

  deleteLike(id: number): Observable<number> {
    return this.http.delete<number>(`${environment.apiUrl}/api/Post/delete-like?likeId=`+ id);
  }

  deleteMessage(id: number): Observable<number> {
    return this.http.delete<number>(`${environment.apiUrl}/api/Message/delete-message?messageId=`+ id);
  }

  newLike(id: number): Observable<number> {
    return this.http.post<number>(`${environment.apiUrl}/api/Post/new-like`, id, this.httpOptions);
  }

  getLikedPost(postId: number): Observable<number> {
    return this.http.get<number>(`${environment.apiUrl}/api/Post/get-like-by-user-and-post?postId=`+ postId);
  }

  getPost(postId: number): Observable<number> {
    return this.http.get<number>(`${environment.apiUrl}/api/Post/get-post?postId=`+ postId);
  }

  updatePost(post: any, postId: number): Observable<number> {
    return this.http.post<number>(`${environment.apiUrl}/api/Post/update-post?postId=`+ postId, post, this.httpOptions);
  }

  sendMessage(post: any, userName: string): Observable<string> {
    return this.http.post<string>(`${environment.apiUrl}/api/Message/new-message?userName=`+ userName, post, this.httpOptions);
  }

  listPosts() {
    return this.http.get<Post[]>(`${environment.apiUrl}/api/Post/list-posts`);
  }

  
  listSendMessages() {
    return this.http.get<Post[]>(`${environment.apiUrl}/api/Message/list-user-send-messages`);
  }

  
  listReceivedMessages() {
    return this.http.get<Post[]>(`${environment.apiUrl}/api/Message/list-user-received-messages`);
  }
}
