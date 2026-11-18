
using Hostel_Management_System.Features.Rooms.DTOs;
using Hostel_Management_System.Features.Rooms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    // GET: api/rooms
    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _roomService.GetAllRoomsAsync();

        return Ok(rooms);
    }

    // GET: api/rooms/1
    [HttpGet("{roomId:int}")]
    public async Task<IActionResult> GetRoomById(int roomId)
    {
        var room = await _roomService.GetRoomByIdAsync(roomId);

        if (room is null)
        {
            return NotFound(new
            {
                message = $"Room with ID {roomId} was not found."
            });
        }

        return Ok(room);
    }

    // POST: api/rooms
    [HttpPost]
    public async Task<IActionResult> CreateRoom(
        [FromBody] CreateRoomRequest request)
    {
        var room = await _roomService.CreateRoomAsync(request);

        return StatusCode(StatusCodes.Status201Created, room);
    }

    // PUT: api/rooms/1
    [HttpPut("{roomId:int}")]
    public async Task<IActionResult> UpdateRoom(
        int roomId,
        [FromBody] UpdateRoomRequest request)
    {
        var room = await _roomService.UpdateRoomAsync(roomId, request);

        if (room is null)
        {
            return NotFound(new
            {
                message = $"Room with ID {roomId} was not found."
            });
        }

        return Ok(room);
    }

    // DELETE: api/rooms/1
    [HttpDelete("{roomId:int}")]
    public async Task<IActionResult> DeactivateRoom(int roomId)
    {
        var result = await _roomService.DeactivateRoomAsync(roomId);

        if (!result)
        {
            return NotFound(new
            {
                message = $"Room with ID {roomId} was not found."
            });
        }

        return Ok(new
        {
            message = "Room deactivated successfully."
        });
    }
}
