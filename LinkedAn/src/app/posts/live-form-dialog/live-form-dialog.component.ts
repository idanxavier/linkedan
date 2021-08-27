import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { timer } from 'rxjs';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-live-form-dialog',
  templateUrl: './live-form-dialog.component.html',
  styleUrls: ['./live-form-dialog.component.css']
})
export class LiveFormDialogComponent implements OnInit {
  public postForm: FormGroup
  constructor(
    public dialogRef: MatDialogRef<LiveFormDialogComponent>, 
    private fb: FormBuilder,
    private rest: PostService) { 
  }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      titulo: ['', [Validators.required]],
      conteudo: ['', [Validators.required]]
    });
  }

  newPost(): void {
    this.rest.newPost(this.postForm.value).subscribe(result => {});
    this.dialogRef.close();
    this.postForm.reset();
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }
  
  cancel(): void {
    this.dialogRef.close();
    this.postForm.reset();
  }

}
