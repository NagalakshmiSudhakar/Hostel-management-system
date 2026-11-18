
namespace Hostel_Management_System.Features.Rooms.DTOs;

public class UpdateRoomRequest
{
    public string RoomNumber { get; set; } = string.Empty;

    public int FloorNumber { get; set; }

    public string RoomType { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public bool IsActive { get; set; }
}

