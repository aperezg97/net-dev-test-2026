import { Component, EventEmitter, inject, Input, OnInit, Output, signal } from "@angular/core";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { StatusCatalogItem, Task, User } from "src/app/entities";
import { TaskService } from "src/app/services/task.service";
import { ToastService } from "src/app/services/toast.service";
import { UserService } from "src/app/services/user.service";

@Component({
  selector: 'app-task-form-modal',
  imports: [ReactiveFormsModule],
  templateUrl: './task-form-modal.component.html'
})
export class TaskFormModalComponent implements OnInit {

  protected readonly toastService = inject(ToastService);
  protected readonly formBuilder = inject(FormBuilder);
  protected readonly taskService = inject(TaskService);
  protected readonly userService = inject(UserService);

  @Input() statuses: StatusCatalogItem[] = [];
  @Output() onCloseEvt: EventEmitter<boolean> = new EventEmitter<boolean>();

  users: User[] = [];

  protected readonly isSubmitting = signal(false);
  protected createTaskForm: FormGroup | undefined;

  ngOnInit(): void {
    this.loadUsers();
    this.initForm();
  }

  public initForm() {
    this.createTaskForm = this.formBuilder.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    description: ['', [Validators.required, Validators.maxLength(250)]],
    dueDate: ['', Validators.required],
    statusId: [this.statuses ? this.statuses[0].id : '', Validators.required],
    assignedToId: ['', Validators.required],
  })
  }

  public closeCreateModal(success: boolean): void {
    this.resetForm();
    this.onCloseEvt.emit(success);
  }

  public resetForm() {
    this.createTaskForm!.reset({
      name: '',
      description: '',
      dueDate: '',
      statusId: this.statuses[0].id,
      assignedToId: '',
    });
  }

  public submitCreateTask(): void {
    if (this.createTaskForm!.invalid) {
      this.createTaskForm!.markAllAsTouched();
      return;
    }
    this.isSubmitting.set(true);
    const formValue = this.createTaskForm!.getRawValue();
    const dueDateValue = formValue.dueDate ?? '';
    const payload: Task = {
      name: formValue.name,
      description: formValue.description,
      dueDate: new Date(dueDateValue),
      statusId: formValue.statusId ?? this.statuses[0].id,
      assignedToId: formValue.assignedToId,
      isActive: true,
    } as Task;

    this.taskService.createTask(payload).subscribe({
      next: (response) => {
        this.isSubmitting.set(false);
        this.closeCreateModal(true);
        this.toastService.show(response.message ?? 'Task created successfully.', 'success');
      },
      error: () => {
        this.isSubmitting.set(false);
        this.toastService.show('Unable to create the task right now.', 'error');
      }
    });
  }

  private loadUsers() {
    this.userService.getAll().subscribe(
      (response) => {
        this.users = response.data || [];
      }
    );
  }

}
