import { Injectable } from '@angular/core';
import { Config } from "./config";
import { Observable, of, catchError } from 'rxjs';

interface AuthResponse {
  status: 'ok' | 'error';
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  login(): Observable<AuthResponse> {
    return new Observable<AuthResponse>(observer => {
      fetch(`${Config.api}/api/health`, {
        method: 'GET',
      })
        .then(response => {
          if (response.ok) {
            const successMessage = 'Authentication successful!';
            console.log(successMessage);
            this.updateConnectionStatus('ok', successMessage);
            observer.next({ status: 'ok', message: successMessage });
          } else if (response.status === 401) {
            const errorMessage = 'Authentication failed. Incorrect login.';
            this.updateConnectionStatus('error', errorMessage);
            observer.next({ status: 'error', message: errorMessage });
          } else {
            const unexpectedMessage = 'Unexpected response from server.';
            console.error(unexpectedMessage);
            this.updateConnectionStatus('error', unexpectedMessage);
            observer.next({ status: 'error', message: unexpectedMessage });
          }
          observer.complete(); // Indicate that the observable is complete
        })
        .catch(error => {
          const networkErrorMessage = 'Network error occurred.';
          console.error(networkErrorMessage, error);
          this.updateConnectionStatus('error', networkErrorMessage);
          observer.next({ status: 'error', message: networkErrorMessage });
          observer.complete(); // Indicate that the observable is complete
        });
    });
  }

  private updateConnectionStatus(status: 'ok' | 'error', message: string): void {
    localStorage.setItem('connectionStatus', JSON.stringify({ status, message }));
  }

  isAuthenticated(): boolean {
    const connectionStatus = localStorage.getItem('connectionStatus');
    if (connectionStatus) {
      const parsedStatus = JSON.parse(connectionStatus);
      return parsedStatus.status === 'ok';
    }
    return false; // Default to false if no connection status is found
  }
}
