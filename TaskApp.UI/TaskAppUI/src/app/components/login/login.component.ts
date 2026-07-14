import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Login } from 'src/app/entities/login.model';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  protected readonly isSubmitting = signal(false);
  protected readonly errorMessage = signal<string | null>(null);

  protected readonly loginForm = new FormGroup({
    username: new FormControl('admin', { nonNullable: true, validators: [Validators.required] }),
    password: new FormControl('Admin!123', { nonNullable: true, validators: [Validators.required] })
  });

  submit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set(null);

    const payload = new Login();
    payload.user = this.loginForm.controls.username.value;
    payload.pass = this.loginForm.controls.password.value;

    this.authService.login(payload).subscribe({
      next: (response) => {
        if (response.success && response.data?.token) {
          this.authService.saveToken(response.data.token);
          this.router.navigateByUrl('/tasks');
          return;
        }

        this.errorMessage.set(response.message ?? 'Unable to sign in right now.');
      },
      error: () => {
        this.isSubmitting.set(false);
        this.errorMessage.set('We could not reach the authentication server. Please try again.');
      },
      complete: () => {
        this.isSubmitting.set(false);
      }
    });
  }
}
