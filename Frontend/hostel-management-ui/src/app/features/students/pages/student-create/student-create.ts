
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink} from '@angular/router';

import { StudentService } from '../../services/student.service';
import { CreateStudentRequest } from '../../../../core/models/create-student-request.model';

@Component({
  selector: 'app-student-create',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './student-create.html',
  styleUrl: './student-create.scss'
})
export class StudentCreateComponent {

  private studentService = inject(StudentService);
  private router = inject(Router);

  student: CreateStudentRequest = {
    registerNumber: '',
    firstName: '',
    lastName: '',
    email: '',
    departmentName: ''
  };

  isSubmitting = false;
  errorMessage = '';

  onSubmit(): void {

    this.isSubmitting = true;
    this.errorMessage = '';

    this.studentService.createStudent(this.student).subscribe({

      next: (response) => {
        console.log('Student created:', response);

        this.router.navigate(['/admin/students']);
      },

      error: (error) => {
        console.error('Error creating student:', error);

        this.errorMessage = 'Unable to create student.';
        this.isSubmitting = false;
      }

    });
  }
}
