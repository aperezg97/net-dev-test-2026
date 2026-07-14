import { DatePipe } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { Task } from "src/app/entities";

@Component({
  selector: 'app-kanban-item',
  imports: [DatePipe],
  templateUrl: './kanban-item.component.html',
})
export class KanbanItemComponent {
  @Input('task') task!: Task;
  @Output() onEditEvt: EventEmitter<Task> = new EventEmitter();

  public editTask() {
    this.onEditEvt.emit(this.task);
  }
}
