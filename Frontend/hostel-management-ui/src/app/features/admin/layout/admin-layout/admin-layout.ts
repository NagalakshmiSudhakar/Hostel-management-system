
import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { AuthService } from '../../../auth/services/auth';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

@Component({
  selector: 'app-admin-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './admin-layout.html',
  styleUrl: './admin-layout.scss'
})
export class AdminLayoutComponent {
  private authService = inject(AuthService);
private router = inject(Router);

logout(): void {
  this.authService.logout();
  this.router.navigate(['/login']);
}
}
