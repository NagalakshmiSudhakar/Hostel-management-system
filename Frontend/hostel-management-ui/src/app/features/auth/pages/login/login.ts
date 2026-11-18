
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth';
import { LoginRequest } from '../../../../core/models/auth/login-request.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {

  private authService = inject(AuthService);
  private router = inject(Router);

  credentials: LoginRequest = {
    email: '',
    password: ''
  };

  isSubmitting = false;
  errorMessage = '';

  onSubmit(): void {

    this.isSubmitting = true;
    this.errorMessage = '';

    this.authService.login(this.credentials).subscribe({

      next: (response) => {
        console.log('Login successful:', response);

        this.router.navigate(['/admin/dashboard']);
      },

      error: (error) => {
        console.error('Login failed:', error);

        this.errorMessage =
          error.status === 401
            ? 'Invalid email or password.'
            : 'Unable to login. Please try again.';

        this.isSubmitting = false;
      }

    });
  }
}
