
using Hostel_Management_System.Models;

namespace Hostel_Management_System.Features.Rooms.Repositories.Interfaces;

public interface IRoomRepository
{
    Task<List<Room>> GetAllRoomsAsync();

    Task<Room?> GetRoomByIdAsync(int roomId);

    Task AddRoomAsync(Room room);

    Task UpdateRoomAsync(Room room);
}
