
using Hostel_Management_System.Features.Rooms.DTOs;
using Hostel_Management_System.Features.Rooms.Repositories.Interfaces;
using Hostel_Management_System.Features.Rooms.Services.Interfaces;
using Hostel_Management_System.Models;

namespace Hostel_Management_System.Features.Rooms.Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    // Get all rooms
    public async Task<List<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _roomRepository.GetAllRoomsAsync();

        return rooms.Select(room => new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            RoomType = room.RoomType,
            Capacity = room.Capacity,
            IsActive = room.IsActive
        }).ToList();
    }

    // Get room by ID
    public async Task<RoomDto?> GetRoomByIdAsync(int roomId)
    {
        var room = await _roomRepository.GetRoomByIdAsync(roomId);

        if (room is null)
        {
            return null;
        }

        return new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            RoomType = room.RoomType,
            Capacity = room.Capacity,
            IsActive = room.IsActive
        };
    }

    // Create room
    public async Task<RoomDto> CreateRoomAsync(CreateRoomRequest request)
    {
        var room = new Room
        {
            RoomNumber = request.RoomNumber,
            FloorNumber = request.FloorNumber,
            RoomType = request.RoomType,
            Capacity = request.Capacity,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _roomRepository.AddRoomAsync(room);

        return new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            RoomType = room.RoomType,
            Capacity = room.Capacity,
            IsActive = room.IsActive
        };
    }

    // Update room
    public async Task<RoomDto?> UpdateRoomAsync(
        int roomId,
        UpdateRoomRequest request)
    {
        var room = await _roomRepository.GetRoomByIdAsync(roomId);

        if (room is null)
        {
            return null;
        }

        room.RoomNumber = request.RoomNumber;
        room.FloorNumber = request.FloorNumber;
        room.RoomType = request.RoomType;
        room.Capacity = request.Capacity;
        room.IsActive = request.IsActive;

        await _roomRepository.UpdateRoomAsync(room);

        return new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            FloorNumber = room.FloorNumber,
            RoomType = room.RoomType,
            Capacity = room.Capacity,
            IsActive = room.IsActive
        };
    }

    // Deactivate room (Soft Delete)
    public async Task<bool> DeactivateRoomAsync(int roomId)
    {
        var room = await _roomRepository.GetRoomByIdAsync(roomId);

        if (room is null)
        {
            return false;
        }

        room.IsActive = false;

        await _roomRepository.UpdateRoomAsync(room);

        return true;
    }
}
