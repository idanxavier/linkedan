import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-update-form-dialog',
  templateUrl: './update-form-dialog.component.html',
  styleUrls: ['./update-form-dialog.component.css']
})
export class UpdateFormDialogComponent implements OnInit {
  public postForm: FormGroup
  constructor(
    public dialogRef: MatDialogRef<UpdateFormDialogComponent>, 
    private fb: FormBuilder,
    private rest: PostService,
    @Inject(MAT_DIALOG_DATA) public data: {postId: number}
    ) { 

  }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      titulo: ['', [Validators.required]],
      conteudo: ['', [Validators.required]]
    });
  }

  updatePost(): void {
    this.rest.updatePost(this.postForm.value, this.data.postId).subscribe(result => {});
    this.dialogRef.close();
    this.postForm.reset();
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }
  
  cancel(): void {
    this.dialogRef.close();
    this.postForm.reset();
  }

}
