import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { BaseResponse, Task } from '../entities';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `/api/Task`; // ${environment.apiUrl}

  getAllTasks(onlyActives = true): Observable<BaseResponse<Task[]>> {
    return this.http.get<BaseResponse<Task[]>>(this.baseUrl, { params: { onlyActives } })
      .pipe(tap(x => {
        x.data = (x.data?.map(task => {
          task.statusId = task.statusId?.toLocaleLowerCase()
          return task;
        }) || []);
        return x;
      }));
  }

  getTasksByStatus(statusId: string): Observable<BaseResponse<Task[]>> {
    return this.http.get<BaseResponse<Task[]>>(`${this.baseUrl}/by-status/${statusId}`);
  }

  getTasksByUser(userId: string): Observable<BaseResponse<Task[]>> {
    return this.http.get<BaseResponse<Task[]>>(`${this.baseUrl}/by-user/${userId}`);
  }

  createTask(task: Task): Observable<BaseResponse<Task>> {
    return this.http.post<BaseResponse<Task>>(this.baseUrl, task);
  }

  updateTask(task: Task): Observable<BaseResponse<Task>> {
    return this.http.put<BaseResponse<Task>>(this.baseUrl, task);
  }

  updateTasksRange(tasks: Task[]): Observable<BaseResponse<Task>> {
    return this.http.put<BaseResponse<Task>>(`${this.baseUrl}/range`, tasks);
  }
}
