import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginDTO } from 'src/app/models/LoginDTO';
import { AuthService } from 'src/app/providers/auth.service';
import { Location } from '@angular/common';
import { TokenService } from 'src/app/providers/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  isError: boolean = false;
  errMsg: string = "Some error has occured!";

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private location: Location, private tokenService: TokenService) {
    this.form = this.formBuilder.group({
      emailAddress: new FormControl([], [Validators.required, Validators.email]),
      password: new FormControl([], [Validators.required])
    })
  }

  ngOnInit(): void { 
    // clear off any tokens in cache
    // if user lands on login
    this.tokenService.clearToken();
  }

  login() {
    console.log(this.form.value);
    this.authService.login(<LoginDTO>this.form.value).subscribe({
      next: (response) => {
        this.router.navigate(['items']);
      },
      error: (err) => {
        if (err.status === 401) {
          this.form.reset();
          this.errMsg = "Authentication Failed!";
        }
        this.isError = true;
      }
    });
  }

  back() {
    this.location.back();
  }

}
