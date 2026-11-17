
import { Component, OnInit, inject } from '@angular/core';

import { StudentService } from '../../services/student.service';
import { Student } from '../../../../core/models/student.model';

@Component({
  selector: 'app-students-list',
  standalone: true,
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
}
