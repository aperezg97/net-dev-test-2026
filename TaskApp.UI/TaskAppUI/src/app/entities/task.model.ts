import { BaseModel, StatusCatalogItem, User } from "src/app/entities";

export class Task extends BaseModel {
  name: string | undefined;
  description: string | undefined;
  dueDate: Date | undefined;
  assignedToId: string | undefined;
  assignedTo: User | undefined;
  statusId: string | undefined;
  status: StatusCatalogItem | undefined;
}
