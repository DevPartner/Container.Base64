import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})

export class PermissionsService {

  constructor(private router: Router, private authService: AuthService) { }

  canActivate(): boolean {
    const isAuthenticated = this.authService.isAuthenticated;

    if (!isAuthenticated) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
