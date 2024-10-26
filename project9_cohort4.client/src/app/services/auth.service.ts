import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../../interfaces/auth';
import iziToast from 'izitoast';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5011/api/Auth';
  private jwtHelper = new JwtHelperService();
  private currentUserSubject = new BehaviorSubject<User | null>(null);

  constructor(private http: HttpClient, private router: Router) {
    const token = localStorage.getItem('token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      this.currentUserSubject.next({
        id: decodedToken.userId,
        username: decodedToken.unique_name,
        email: decodedToken.email,
        role: decodedToken.role,
      });
    }
  }

  get currentUser(): Observable<User | null> {
    return this.currentUserSubject.asObservable();
  }

  register(data: any) {
    this.http.post(`${this.apiUrl}/register`, data).subscribe(
      (response: any) => {
        response.token ? this.storeToken(response.token) : null;
        iziToast.success({
          title: 'Success',
          message: 'Register Successful',
          position: 'topCenter',
          timeout: 3000,
        });
        this.router.navigate(['/']);
      }, error => {
        console.log(error);
        iziToast.error({
          title: 'Error',
          message: 'Email already exists',
          position: 'topCenter',
          timeout: 3000,
        });
      }
    );

  }

  login(data: any) {
    this.http.post(`${this.apiUrl}/login`, data).subscribe(
      (response: any) => {
        response.token ? this.storeToken(response.token) : null;
        iziToast.success({
          title: 'Success',
          message: 'Login Successful',
          position: 'topCenter',
          timeout: 3000,
        });
        this.router.navigate(['/']);
      }, error => {
        console.log(error);
        iziToast.error({
          title: 'Error',
          message: 'Invalid email or password',
          position: 'topCenter',
          timeout: 3000,
        });
      }
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
  }

  private storeToken(token: string): void {
    localStorage.setItem('token', token);
    const decodedToken = this.jwtHelper.decodeToken(token);
    this.currentUserSubject.next({
      id: decodedToken.userId,
      username: decodedToken.unique_name,
      email: decodedToken.email,
      role: decodedToken.role,
    });
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }
}
