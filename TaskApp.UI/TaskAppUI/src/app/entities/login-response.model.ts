import { User } from "src/app/entities";

export class LoginResponse {
  token!: string;
  user!: User;
}
