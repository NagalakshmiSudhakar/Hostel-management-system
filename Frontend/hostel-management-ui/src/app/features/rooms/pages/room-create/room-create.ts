
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { RoomService } from '../../services/room';
import { CreateRoomRequest } from '../../../../core/models/create-room-request.model';

@Component({
  selector: 'app-room-create',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './room-create.html',
  styleUrl: './room-create.scss',
})
export class RoomCreate {

  private roomService = inject(RoomService);
  private router = inject(Router);

  room: CreateRoomRequest = {
    roomNumber: '',
    floorNumber: 1,
    roomType: 'AC',
    capacity: 1
  };

  isSubmitting = false;
  errorMessage = '';

  onSubmit(): void {
    this.isSubmitting = true;
    this.errorMessage = '';

    this.roomService.createRoom(this.room).subscribe({
      next: (response) => {
        console.log('Room created:', response);

        this.router.navigate(['/admin/rooms']);
      },

      error: (error) => {
        console.error('Error creating room:', error);

        this.errorMessage = 'Unable to create room.';
        this.isSubmitting = false;
      }
    });
  }
}
