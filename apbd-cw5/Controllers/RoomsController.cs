using apbd_cw5.Data;
using apbd_cw5.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = DataStore.Rooms.AsQueryable();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly.HasValue && activeOnly.Value)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetRoomsByBuilding(string buildingCode)
    {
        var rooms = DataStore.Rooms.Where(r => r.BuildingCode == buildingCode).ToList();
        return Ok(rooms);
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room room)
    {
        room.Id = DataStore.NextRoomId++;
        DataStore.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room room)
    {
        var existing = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (existing == null)
            return NotFound();

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        var hasReservations = DataStore.Reservations.Any(r => r.RoomId == id);
        if (hasReservations)
            return Conflict("Cannot delete a room that has existing reservations.");

        DataStore.Rooms.Remove(room);
        return NoContent();
    }
}
