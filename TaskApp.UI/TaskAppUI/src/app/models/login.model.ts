export class LoginRequest {
  constructor(
    public user: string,
    public pass: string
  ) {}
}

export interface SignUpRequest {
  username: string;
  firstName: string;
  lastName: string;
  password: string;
  email: string;
}

export interface BaseResponse<T> {
  success: boolean;
  data: T | null;
  message: string | null;
}

export interface User {
  id: number;
  username: string;
  firstName: string;
  lastName: string;
  email: string | null;
}

export interface LoginResponse {
  token: string;
  user: User;
}
