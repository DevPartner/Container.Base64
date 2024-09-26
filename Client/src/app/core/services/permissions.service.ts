import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})

export class PermissionsService {

  constructor(private router: Router, private tokenService: TokenService) { }

  canActivate(): boolean {
    const isAuthenticated = this.tokenService.IsAuthenticated;

    if (!isAuthenticated) {
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
}
