import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Authservice } from '../services/AuthService/authservice';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(Authservice);
  const router = inject(Router);

  const userData = authService.obtenerUsuario(); // suponiendo que tenés este método

  if (userData) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }
};
