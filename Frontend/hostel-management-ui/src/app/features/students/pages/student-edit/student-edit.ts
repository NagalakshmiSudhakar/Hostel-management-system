import {
  Component,
  OnInit,
  inject,
  ChangeDetectorRef
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { StudentService } from '../../services/student.service';
import { UpdateStudentRequest } from '../../../../core/models/update-student-request.model';

@Component({
  selector: 'app-student-edit',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './student-edit.html',
  styleUrl: './student-edit.scss'
})
export class StudentEditComponent implements OnInit {

  private studentService = inject(StudentService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  studentId!: number;

  student: UpdateStudentRequest = {
    registerNumber: '',
    firstName: '',
    lastName: '',
    email: '',
    departmentName: '',
    isActive: true
  };

  isLoading = false;
  isSubmitting = false;
  errorMessage = '';

  ngOnInit(): void {
    this.studentId = Number(this.route.snapshot.paramMap.get('id'));
     console.log('Student ID:', this.studentId);
    this.loadStudent();
  }

  loadStudent(): void {
  this.isLoading = true;

  this.studentService.getStudentById(this.studentId).subscribe({
    next: (data) => {
      console.log('API SUCCESS:', data);

      this.student = {
        registerNumber: data.registerNumber,
        firstName: data.firstName,
        lastName: data.lastName,
        email: data.email,
        departmentName: data.departmentName,
        isActive: data.isActive
      };

      this.isLoading = false;
      this.cdr.markForCheck();
    },

    error: (error) => {
      console.error('API ERROR:', error);

      this.errorMessage = 'Unable to load student.';
      this.isLoading = false;
      this.cdr.markForCheck();
    }
  });
}

  onSubmit(): void {
    this.isSubmitting = true;
    this.errorMessage = '';

    this.studentService.updateStudent(this.studentId, this.student).subscribe({
      next: (response) => {
        console.log('Student updated:', response);
        this.router.navigate(['/admin/students']);
      },
      error: (error) => {
        console.error('Error updating student:', error);
        this.errorMessage = 'Unable to update student.';
        this.isSubmitting = false;
      }
    });
  }
}
