import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseResponse, User } from '../entities';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `/api/User`; // ${environment.apiUrl}

  getAll(): Observable<BaseResponse<User[]>> {
    return this.http.get<BaseResponse<User[]>>(this.baseUrl);
  }
}
