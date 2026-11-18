
using Hostel_Management_System.Features.Rooms.DTOs;

namespace Hostel_Management_System.Features.Rooms.Services.Interfaces;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllRoomsAsync();

    Task<RoomDto?> GetRoomByIdAsync(int roomId);

    Task<RoomDto> CreateRoomAsync(CreateRoomRequest request);

    Task<RoomDto?> UpdateRoomAsync(
        int roomId,
        UpdateRoomRequest request);

    Task<bool> DeactivateRoomAsync(int roomId);
}
