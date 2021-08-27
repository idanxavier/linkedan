import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { first } from 'rxjs/operators';
import { LiveFormDialogComponent } from 'src/app/posts/live-form-dialog/live-form-dialog.component';
import { MessageFormDialogComponent } from 'src/app/posts/message-form-dialog/message-form-dialog.component';
import { UpdateFormDialogComponent } from 'src/app/posts/update-form-dialog/update-form-dialog.component';
import { Like } from 'src/app/_models/like';
import { Post } from 'src/app/_models/post';
import { User } from 'src/app/_models/user';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent {
  userPosts: Post[];
  userLikes: Like[];
  users: User[];
  posts: Post[];
  currentPost: any;
  currentLike: any;
  receivedMessages: any[];
  sendMessages: any[];

  constructor(private postService: PostService, public dialog: MatDialog) { }

  ngOnInit(): void {  
    this.postService.listUserPosts().pipe(first()).subscribe(userPosts => {
      this.userPosts = userPosts;
    });

    this.postService.listPosts().pipe(first()).subscribe(posts => {
      this.posts = posts;
    });

    this.postService.listUsers().pipe(first()).subscribe(users => {
      this.users = users;
    });
    
    this.postService.listUserLikes().pipe(first()).subscribe(userLikes => {
      this.userLikes = userLikes;
    });

    this.postService.listSendMessages().pipe(first()).subscribe(sendMessages => {
      this.sendMessages = sendMessages;
    });

    this.postService.listReceivedMessages().pipe(first()).subscribe(receivedMessages => {
      this.receivedMessages = receivedMessages;
    });

  }

  deletePost(id: number){
    this.postService.deletePost(id).subscribe(d =>{
    });
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }

  likePost(id: number): void {
    this.postService.newLike(id).subscribe(d =>{
    });
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }

  deleteLike(id: number){
    this.postService.deleteLike(id).subscribe(d =>{
    });
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }

  deleteMessage(id: number){
    this.postService.deleteMessage(id).subscribe(d =>{
    });
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }

  getLikedPost(id: number): void {
    this.postService.getLikedPost(id).subscribe(d =>{
      this.currentLike = d;
    },
    (error) => this.currentLike = undefined);
  }

  getPost(id: number): void {
    this.postService.getPost(id).subscribe(d =>{
    });
  }

   newPost(): void {
    const dialogRef = this.dialog.open(LiveFormDialogComponent, {
      minWidth: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  updatePost(postId: number): void {
    const dialogRef = this.dialog.open(UpdateFormDialogComponent, {
      data: { postId: postId},
      minWidth: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  sendMessage(userName: string): void {
    const dialogRef = this.dialog.open(MessageFormDialogComponent, {
      data: { userName: userName},
      minWidth: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
