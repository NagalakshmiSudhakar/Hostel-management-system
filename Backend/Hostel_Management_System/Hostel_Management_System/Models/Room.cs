
namespace Hostel_Management_System.Models;

public class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int FloorNumber { get; set; }

    public string RoomType { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
