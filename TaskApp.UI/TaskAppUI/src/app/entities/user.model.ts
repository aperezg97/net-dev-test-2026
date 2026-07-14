import { Task } from './task.model';

export class User {
  username: string | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  password: string | undefined;
  email: string | undefined;

  tasks: Task[] = [];
}
