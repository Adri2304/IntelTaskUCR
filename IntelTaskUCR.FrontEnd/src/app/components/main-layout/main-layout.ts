import { Component, inject } from '@angular/core';
import { Toolbar } from '../../shared/toolbar/toolbar';
import { Sidenav } from '../../shared/sidenav/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Authservice } from '../../services/AuthService/authservice';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-layout',
  imports: [Toolbar, Sidenav, MatButtonModule, MatIconModule],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css'
})
export class MainLayout {
  authService = inject(Authservice);
  router = inject(Router);

  exit(){
    this.authService.cerrarSesion();
    this.router.navigate(["/login"]);
  }
}
