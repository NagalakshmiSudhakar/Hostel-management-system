
import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { RoomService } from '../../services/room';
import { Room } from '../../../../core/models/room.model';
import { UpdateRoomRequest } from '../../../../core/models/update-room-request.model';

@Component({
  selector: 'app-room-edit',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './room-edit.html',
  styleUrl: './room-edit.scss',
})
export class RoomEdit implements OnInit {

  private roomService = inject(RoomService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private cdr = inject(ChangeDetectorRef);

  roomId!: number;

  room: UpdateRoomRequest = {
    roomNumber: '',
    floorNumber: 1,
    roomType: 'AC',
    capacity: 1,
    isActive: true
  };

  isLoading = true;
  isSubmitting = false;
  errorMessage = '';

  ngOnInit(): void {
    this.roomId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadRoom();
  }

  loadRoom(): void {
  console.log('Loading room ID:', this.roomId);

  this.roomService.getRoomById(this.roomId).subscribe({
    next: (response: Room) => {
      console.log('ROOM API DATA:', JSON.stringify(response, null, 2));
      console.log('API Response:', response);

      this.room = {
        roomNumber: response.roomNumber,
        floorNumber: response.floorNumber,
        roomType: response.roomType,
        capacity: response.capacity,
        isActive: response.isActive
      };

      console.log('Updated room object:', this.room);

      this.isLoading = false;
      this.cdr.detectChanges();
    },
    error: (error) => {
      console.error('Error loading room:', error);
      this.errorMessage = 'Unable to load room details.';
      this.isLoading = false;
      this.cdr.detectChanges();
    }
  });
}

  onSubmit(): void {
    this.isSubmitting = true;
    this.errorMessage = '';

    this.roomService.updateRoom(this.roomId, this.room).subscribe({
      next: () => {
        this.router.navigate(['/admin/rooms']);
      },
      error: (error) => {
        console.error('Error updating room:', error);
        this.errorMessage = 'Unable to update room.';
        this.isSubmitting = false;
      }
    });
  }
}