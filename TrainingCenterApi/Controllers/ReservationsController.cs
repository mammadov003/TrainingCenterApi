using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = AppData.Reservations;

        if (date.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations.Where(reservation =>
                string.Equals(reservation.Status, status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.RoomId == roomId.Value);
        }

        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        var roomValidationResult = ValidateRoom(reservation.RoomId);
        if (roomValidationResult is not null)
        {
            return roomValidationResult;
        }

        if (HasOverlap(reservation))
        {
            return Conflict("The reservation overlaps with an existing reservation for the same room.");
        }

        reservation.Id = AppData.Reservations.Count == 0 ? 1 : AppData.Reservations.Max(r => r.Id) + 1;
        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation updatedReservation)
    {
        var existingReservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        var roomValidationResult = ValidateRoom(updatedReservation.RoomId);
        if (roomValidationResult is not null)
        {
            return roomValidationResult;
        }

        updatedReservation.Id = id;

        if (HasOverlap(updatedReservation, id))
        {
            return Conflict("The reservation overlaps with an existing reservation for the same room.");
        }

        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return Ok(existingReservation);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        AppData.Reservations.Remove(reservation);
        return NoContent();
    }

    private ActionResult? ValidateRoom(int roomId)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == roomId);

        if (room is null)
        {
            ModelState.AddModelError(nameof(Reservation.RoomId), "Room does not exist.");
            return ValidationProblem(ModelState);
        }

        if (!room.IsActive)
        {
            ModelState.AddModelError(nameof(Reservation.RoomId), "Room is inactive.");
            return ValidationProblem(ModelState);
        }

        return null;
    }

    private static bool HasOverlap(Reservation reservation, int? ignoredReservationId = null)
    {
        return AppData.Reservations.Any(existingReservation =>
            existingReservation.RoomId == reservation.RoomId &&
            existingReservation.Date == reservation.Date &&
            existingReservation.Id != ignoredReservationId &&
            reservation.StartTime < existingReservation.EndTime &&
            reservation.EndTime > existingReservation.StartTime);
    }
}
