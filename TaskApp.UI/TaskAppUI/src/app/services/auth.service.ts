import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, of, tap } from 'rxjs';
import {
  BaseResponse,
  LoginRequest,
  LoginResponse,
  SignUpRequest,
  User
} from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly storageKey = 'taskapp.auth.token';

  readonly currentUser = signal<User | null>(null);
  readonly baseUrl = `/api/Auth`; // ${environment.apiUrl}

  getToken(): string | null {
    return localStorage.getItem(this.storageKey);
  }

  saveToken(token: string): void {
    localStorage.setItem(this.storageKey, token);
  }

  clearToken(): void {
    localStorage.removeItem(this.storageKey);
    this.currentUser.set(null);
  }

  login(payload: LoginRequest): Observable<BaseResponse<LoginResponse>> {
    return this.http.post<BaseResponse<LoginResponse>>(`${this.baseUrl}/login`, payload).pipe(
      tap((response) => {
        if (response.success && response.data?.token) {
          this.saveToken(response.data.token);
        }
      })
    );
  }

  signUp(payload: SignUpRequest): Observable<BaseResponse<User>> {
    return this.http.post<BaseResponse<User>>(`${this.baseUrl}/sign-up`, payload);
  }

  profile(): Observable<BaseResponse<User>> {
    return this.http.get<BaseResponse<User>>(`${this.baseUrl}/profile`).pipe(
      tap((response) => {
        if (response.success && response.data) {
          this.currentUser.set(response.data);
        }
      })
    );
  }

  validateSession(): Observable<boolean> {
    const token = this.getToken();
    if (!token) {
      return of(false);
    }

    return this.profile().pipe(
      map((response) => response.success && response.data !== null),
      tap((isValid) => {
        if (!isValid) {
          this.clearToken();
        }
      })
    );
  }
}
