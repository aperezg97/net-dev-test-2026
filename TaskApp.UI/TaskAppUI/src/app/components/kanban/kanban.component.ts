import { DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { TaskService } from '../../services/task.service';
import { ToastService } from '../../services/toast.service';
import { StatusCatalogItem, Task } from 'src/app/entities';
import { Router } from '@angular/router';
import { CdkDragDrop, DragDropModule, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TaskFormModalComponent } from "../task-form-modal/task-form-modal.component";

@Component({
  selector: 'app-kanban',
  imports: [DatePipe, DragDropModule, ReactiveFormsModule, TaskFormModalComponent],
  templateUrl: './kanban.component.html',
  styleUrl: './kanban.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class KanbanComponent implements OnInit {
  protected readonly authService = inject(AuthService);
  protected readonly taskService = inject(TaskService);
  protected readonly toastService = inject(ToastService);
  protected readonly changeDetectorRef = inject(ChangeDetectorRef);
  protected readonly router = inject(Router);

  protected readonly statuses: StatusCatalogItem[] = [
    { id: 'f6e89617-063f-4296-9a9d-a1955931c3b4', name: 'Created' } as StatusCatalogItem,
    { id: '83f3b61a-8be3-4538-9857-c9525826aaf5', name: 'Pending' } as StatusCatalogItem,
    { id: 'f766a0c6-3019-44cf-ae30-5d4142c3d1e6', name: 'In Progress' } as StatusCatalogItem,
    { id: '48036c69-08c4-4214-af4c-790b3b6a5fed', name: 'Completed' } as StatusCatalogItem,
  ];

  protected tasks: Task[] = [];
  protected mappedTasks: { status: StatusCatalogItem, tasks: Task[] }[] = [];
  protected isLoading = false;
  protected errorMessage: string | null = null;
  protected isCreateModalOpen = false;

  ngOnInit(): void {
    this.loadTasks();
    this.changeDetectorRef.detectChanges();
  }

  public openCreateModal(): void {
    this.isCreateModalOpen = true;
    this.changeDetectorRef.markForCheck();
  }

  public closeCreateModal(success: boolean): void {
    this.isCreateModalOpen = false;
    if (success) {
      this.loadTasks();
    }
    this.changeDetectorRef.markForCheck();
  }

  public logout() {
    this.authService.clearToken();
    this.router.navigateByUrl('/');
  }

  public drop(event: CdkDragDrop<string[]>) {
    console.log({ event });
    // 1. Get the exact value/object being dragged
    const movedItem = event.item.data;

    // 2. Identify source and destination lists by their structural array reference
    const sourceArray = event.previousContainer.data;
    const destinationArray = event.container.data;

    // 3. Track index differences
    const originIndex = event.previousIndex;
    const finalIndex = event.currentIndex;

    console.log({
      movedItem,
      sourceArray,
      destinationArray,
      originIndex,
      finalIndex,
    });

    // Example Log: "Moved 'Fix bike brakes' from position 1 to position 3"
    console.log(`Moved '${JSON.stringify(destinationArray)}' from array ${sourceArray} position ${originIndex} to destinationArray ${destinationArray} position ${finalIndex}`);

    if (
      event.previousContainer === event.container &&
      event.previousIndex === event.currentIndex
    ) {
      return;
    }
    if (event.previousContainer === event.container) {
      moveItemInArray(
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );
    }
    this.detectTaskStatusUpdated();
  }

  protected loadTasks(): void {
    this.isLoading = true;
    this.errorMessage = null;

    this.taskService.getAllTasks(true).subscribe({
      next: (response) => {
        this.tasks = response.data ?? [];
        this.isLoading = false;
        this.mapTasks();
        this.changeDetectorRef.markForCheck();
      },
      error: () => {
        this.errorMessage = 'Unable to load tasks right now.';
        this.isLoading = false;
      }
    });
  }

  protected detectTaskStatusUpdated() {
    let updatedTasks: Task[] = [];
    for (const mappedTask of this.mappedTasks) {
      const updated = mappedTask.tasks.filter(x => x.statusId != mappedTask.status.id);
      if (updated && updated.length) {
        for (const updatedTask of updated) {
          console.log({
            updatedTask,
            prev: updatedTask.statusId,
            curr: mappedTask.status.id
          });
          updatedTask.statusId = mappedTask.status.id;
        }
        updatedTasks = updatedTasks.concat(updated);
      }
    }
    console.log({ updatedTasks });
    if (updatedTasks && updatedTasks.length) {
      this.updateTasks(updatedTasks);
    }
  }

  protected mapTasks() {
    this.mappedTasks = this.statuses.map((status) => {
      return {
        status,
        tasks: this.getTasksForStatus(status.id)
      }
    });
  }

  protected updateTasks(tasks: Task[]) {
    this.taskService.updateTasksRange(tasks).subscribe({
      next: (response) => {
        this.toastService.show('Task updated successfully.', 'success');
        this.loadTasks();
      },
      error: () => {
        this.errorMessage = 'Unable to update tasks right now.';
        this.isLoading = false;
      }
    });
  }

  protected getTasksForStatus(statusId: string | undefined): Task[] {
    return this.tasks.filter((task) => task.statusId?.toLocaleLowerCase() === statusId?.toLocaleLowerCase());
  }
}
