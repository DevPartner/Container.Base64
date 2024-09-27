import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public connectionFailed: boolean = false;
  public message: string | null = null;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.login().pipe(
      catchError((error) => {
        console.error('Login failed:', error);
        this.connectionFailed = true;
        this.message = 'An error occurred. Please try again later.'; 
        return of(null);  // Return null on error
      }))
      .subscribe(result => {
        if (result && result.status === 'ok') { // Check if the login was successful.
          this.router.navigate(['encoder']);
        } else if (result && result.status === 'error') {
          this.connectionFailed = true; // Mark as failed
          this.message = result.message; // Set the error message
        }
      });
  }

  refreshPage() {
    location.reload();
  }
}
