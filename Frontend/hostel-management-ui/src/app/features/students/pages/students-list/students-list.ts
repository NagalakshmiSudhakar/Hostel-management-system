
import { Component, OnInit, inject } from '@angular/core';

import { StudentService } from '../../services/student.service';
import { Student } from '../../../../core/models/student.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-students-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './students-list.html',
  styleUrl: './students-list.scss'
})
export class StudentsListComponent implements OnInit {

  private studentService = inject(StudentService);

  students: Student[] = [];

  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadStudents();
  }

  // Get all students
  loadStudents(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.studentService.getAllStudents().subscribe({
      next: (data) => {
        console.log('Students received:', data);

        this.students = data;
        this.isLoading = false;

        console.log('Loading:', this.isLoading);
      },
      error: (error) => {
        console.error('API Error:', error);

        this.errorMessage = 'Unable to load students.';
        this.isLoading = false;
      },
      complete: () => {
        console.log('API request completed');
      }
    });
  }

  // Deactivate student (Soft Delete)
  deleteStudent(studentId: number): void {

    const confirmDelete = confirm(
      'Are you sure you want to deactivate this student?'
    );

    if (!confirmDelete) {
      return;
    }

    this.studentService.deactivateStudent(studentId).subscribe({
      next: (response) => {
        console.log('Student deactivated:', response);

        // Refresh the student list
        this.loadStudents();
      },
      error: (error) => {
        console.error('Error deactivating student:', error);

        this.errorMessage = 'Unable to deactivate student.';
      }
    });
  }
}
