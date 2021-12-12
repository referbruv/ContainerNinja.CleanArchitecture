import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginDTO } from 'src/app/models/LoginDTO';
import { AuthService } from 'src/app/providers/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.form = this.formBuilder.group({
      emailAddress: new FormControl([], [Validators.required, Validators.email]),
      password: new FormControl([], [Validators.required])
    })
  }

  ngOnInit(): void { }

  login() {
    console.log(this.form.value);
    this.authService.login(<LoginDTO>this.form.value).subscribe((response) => {
      this.router.navigate(['items']);
    });
  }

}
