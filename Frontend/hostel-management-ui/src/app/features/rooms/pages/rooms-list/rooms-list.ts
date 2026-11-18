
import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';

import { RoomService } from '../../services/room';
import { Room } from '../../../../core/models/room.model';

@Component({
  selector: 'app-rooms-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './rooms-list.html',
  styleUrl: './rooms-list.scss'
})
export class RoomsListComponent implements OnInit {

  private roomService = inject(RoomService);

  rooms: Room[] = [];

  isLoading = false;
  errorMessage = '';

  ngOnInit(): void {
    this.loadRooms();
  }

  // Get all rooms
  loadRooms(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.roomService.getAllRooms().subscribe({
      next: (data) => {
        console.log('Rooms received:', data);

        this.rooms = data;
        this.isLoading = false;
      },

      error: (error) => {
        console.error('API Error:', error);

        this.errorMessage = 'Unable to load rooms.';
        this.isLoading = false;
      }
    });
  }

deleteRoom(roomId: number): void {
  const confirmed = confirm('Are you sure you want to deactivate this room?');

  if (!confirmed) {
    return;
  }

  this.roomService.deactivateRoom(roomId).subscribe({
    next: () => {
      alert('Room deactivated successfully!');
      this.loadRooms();
    },
    error: (error) => {
      console.error('Error deactivating room:', error);
      alert('Unable to deactivate room.');
    }
  });
}

}
