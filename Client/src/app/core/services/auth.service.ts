import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthToken } from '../models/AuthToken';
import { LoginDTO } from '../models/LoginDTO';
import { TokenService } from './token.service';
import { SignalRService } from './signal-r.service';
import { from, Observable, of, catchError, throwError, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  constructor(private http: HttpClient, private tokenService: TokenService, private signalRService: SignalRService) { }

  login(loginDto: LoginDTO): Observable<AuthToken> {
    const accessToken = 'Basic ' + btoa(`${loginDto.username}:${loginDto.password}`);

    const authToken: AuthToken = {
      accessToken: accessToken,
      expiresIn: this.setExpiresIn(3600) // Set expiresIn to 1 hour (3600 seconds)
    };

    return from(this.signalRService.startConnection(accessToken)).pipe(
      switchMap(() => {
        this.signalRService.stopConnection();
        this.tokenService.setToken(authToken);
        return of(authToken);
      }),
      catchError((error) => {
        console.error('Login or SignalR connection failed:', error);
        return throwError(() => new Error('Login failed.'));
      })
    );
  }

  private setExpiresIn(seconds: number): number {
    return Date.now() + seconds * 1000;
  }
}
