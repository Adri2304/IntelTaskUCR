import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Authservice } from '../../services/AuthService/authservice';
import { IAuth } from '../../models/iauth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [MatCardModule, MatFormFieldModule, MatInputModule, MatIconModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  loginForm: FormGroup;
  authService = inject(Authservice);
  router = inject(Router);

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      correo: ['', Validators.required],
      password: ['', Validators.required,]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { correo, password } = this.loginForm.value;

      const data: IAuth = {
        userEmail: correo, 
        password: password
      }

      console.log(data);
      
      this.authService.autenticarUsuario(data).subscribe({
        next: (res) => {
          console.log(res);
          this.authService.guardarUsuarioEnLocalStorage(res);
          this.router.navigate(['/task']);
        },
        error: (err) => {
          console.log(err);
          console.log("No se pudo");
        }
      })
    }
  }
}
