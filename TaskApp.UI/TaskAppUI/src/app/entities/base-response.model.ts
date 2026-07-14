export class BaseResponse<T> {
  data: T | undefined;
  success: boolean | undefined;
  message: string | undefined;
}
