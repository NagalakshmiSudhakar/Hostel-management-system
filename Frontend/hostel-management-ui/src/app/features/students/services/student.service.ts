
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Student } from '../../../core/models/student.model';
import { CreateStudentRequest } from '../../../core/models/create-student-request.model';
import { UpdateStudentRequest } from '../../../core/models/update-student-request.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7104/api/students';

  getAllStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.apiUrl);
  }
  createStudent(request: CreateStudentRequest): Observable<Student> {
    return this.http.post<Student>(this.apiUrl, request);
  }
  // GET: Retrieve one student by ID
getStudentById(studentId: number): Observable<Student> {
  return this.http.get<Student>(`${this.apiUrl}/${studentId}`);
}

// PUT: Update an existing student
updateStudent(
  studentId: number,
  request: UpdateStudentRequest
): Observable<Student> {
  return this.http.put<Student>(
    `${this.apiUrl}/${studentId}`,
    request
  );
}
}
