import { Injectable, signal } from '@angular/core';

export interface ToastMessage {
  id: number;
  message: string;
  type: 'success' | 'error' | 'info';
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  readonly toasts = signal<ToastMessage[]>([]);

  show(message: string, type: ToastMessage['type'] = 'info'): void {
    const toast: ToastMessage = {
      id: Date.now() + Math.floor(Math.random() * 1000),
      message,
      type
    };

    this.toasts.update((current) => [...current, toast]);

    window.setTimeout(() => this.dismiss(toast.id), 4000);
  }

  dismiss(id: number): void {
    this.toasts.update((current) => current.filter((toast) => toast.id !== id));
  }
}
