
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Room } from '../../../core/models/room.model';
import { CreateRoomRequest } from '../../../core/models/create-room-request.model';
import { UpdateRoomRequest } from '../../../core/models/update-room-request.model';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7104/api/rooms';

  // Get all rooms
  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.apiUrl);
  }

  // Get room by ID
  getRoomById(roomId: number): Observable<Room> {
    return this.http.get<Room>(`${this.apiUrl}/${roomId}`);
  }

  // Create room
  createRoom(request: CreateRoomRequest): Observable<Room> {
    return this.http.post<Room>(this.apiUrl, request);
  }

  // Update room
  updateRoom(
    roomId: number,
    request: UpdateRoomRequest
  ): Observable<Room> {
    return this.http.put<Room>(
      `${this.apiUrl}/${roomId}`,
      request
    );
  }

  // Deactivate room
  deactivateRoom(roomId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${roomId}`);
  }
}
