using apbd_cw5.Data;
using apbd_cw5.Models;
using Microsoft.AspNetCore.Mvc;

namespace apbd_cw5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetReservations(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var reservations = DataStore.Reservations.AsQueryable();

        if (date.HasValue)
            reservations = reservations.Where(r => r.Date == date.Value);

        if (!string.IsNullOrEmpty(status))
            reservations = reservations.Where(r => r.Status == status);

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);

        return Ok(reservations.ToList());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
            return NotFound("Room not found.");

        if (!room.IsActive)
            return BadRequest("Cannot reserve an inactive room.");

        var overlap = DataStore.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.StartTime < reservation.EndTime &&
            reservation.StartTime < r.EndTime);

        if (overlap)
            return Conflict("The reservation overlaps with an existing one for this room.");

        reservation.Id = DataStore.NextReservationId++;
        DataStore.Reservations.Add(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateReservation(int id, [FromBody] Reservation reservation)
    {
        var existing = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (existing == null)
            return NotFound();

        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
            return NotFound("Room not found.");

        if (!room.IsActive)
            return BadRequest("Cannot reserve an inactive room.");

        var overlap = DataStore.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.StartTime < reservation.EndTime &&
            reservation.StartTime < r.EndTime);

        if (overlap)
            return Conflict("The reservation overlaps with an existing one for this room.");

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return Ok(existing);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound();

        DataStore.Reservations.Remove(reservation);
        return NoContent();
    }
}
