import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { timer } from 'rxjs';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-register-form-dialog',
  templateUrl: './register-form-dialog.component.html',
  styleUrls: ['./register-form-dialog.component.css']
})
export class RegisterFormDialogComponent implements OnInit {
  public postForm: FormGroup
  constructor(
    public dialogRef: MatDialogRef<RegisterFormDialogComponent>, 
    private fb: FormBuilder,
    private rest: PostService) { 
  }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      Username: ['', [Validators.required]],
      Email: ['', [Validators.required]],
      Password: ['', [Validators.required]],
      PasswordConfirm: ['', [Validators.required]]
    });
  }

  newAccount(): void {
    this.rest.newAccount(this.postForm.value).subscribe(result => {});
    this.dialogRef.close();
    this.postForm.reset();
    setTimeout(function(){  this.document.location.reload(); }, 1000); 
  }
  
  cancel(): void {
    this.dialogRef.close();
    this.postForm.reset();
  }

}
