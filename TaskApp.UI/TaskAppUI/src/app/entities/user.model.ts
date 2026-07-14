import { Task } from './task.model';

export class User {
  id: string | undefined;
  username: string | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  password: string | undefined;
  email: string | undefined;

  tasks: Task[] = [];
}
