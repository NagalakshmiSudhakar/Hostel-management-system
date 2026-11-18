
using Hostel_Management_System.Data;
using Hostel_Management_System.Features.Rooms.Repositories.Interfaces;
using Hostel_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hostel_Management_System.Features.Rooms.Repositories.Implementations;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all rooms
    public async Task<List<Room>> GetAllRoomsAsync()
    {
        return await _context.Rooms
            .AsNoTracking()
            .ToListAsync();
    }

    // Get room by ID
    public async Task<Room?> GetRoomByIdAsync(int roomId)
    {
        return await _context.Rooms
            .FirstOrDefaultAsync(r => r.RoomId == roomId);
    }

    // Add a new room
    public async Task AddRoomAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    // Update existing room
    public async Task UpdateRoomAsync(Room room)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
    }
}

