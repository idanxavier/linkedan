import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'message-form-dialog',
  templateUrl: './message-form-dialog.component.html',
  styleUrls: ['./message-form-dialog.component.css']
})
export class MessageFormDialogComponent implements OnInit {
  public postForm: FormGroup
  constructor(
    public dialogRef: MatDialogRef<MessageFormDialogComponent>, 
    private fb: FormBuilder,
    private rest: PostService,
    @Inject(MAT_DIALOG_DATA) public data: {userName: string}
    ) { 

  }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      conteudo: ['', [Validators.required]]
    });
  }

  sendMessage(): void {
    this.rest.sendMessage(this.postForm.value, this.data.userName).subscribe(result => {});
    this.dialogRef.close();
    this.postForm.reset();
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }
  
  cancel(): void {
    this.dialogRef.close();
    this.postForm.reset();
  }

}
