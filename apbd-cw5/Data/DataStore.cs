using apbd_cw5.Models;

namespace apbd_cw5.Data;

public static class DataStore
{
    public static int NextRoomId = 6;
    public static int NextReservationId = 7;

    public static List<Room> Rooms = new List<Room>
    {
        new Room { Id = 1, Name = "Pudge's Pit", BuildingCode = "A", Floor = 1, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Rick's Lab", BuildingCode = "A", Floor = 2, Capacity = 20, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "Invoker's Tower", BuildingCode = "B", Floor = 3, Capacity = 15, HasProjector = true, IsActive = true },
        new Room { Id = 4, Name = "Land of Meeseeks", BuildingCode = "B", Floor = 1, Capacity = 50, HasProjector = true, IsActive = true },
        new Room { Id = 5, Name = "Dimension C-137", BuildingCode = "C", Floor = 0, Capacity = 10, HasProjector = false, IsActive = false }
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Pudge", Topic = "Hook Anatomy: Throw and Pull Mechanics", Date = new DateOnly(2026, 5, 5), StartTime = new TimeOnly(8, 0), EndTime = new TimeOnly(10, 0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 1, OrganizerName = "Crystal Maiden", Topic = "Frost and Crystal Mechanics", Date = new DateOnly(2026, 5, 5), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(13, 0), Status = "planned" },
        new Reservation { Id = 3, RoomId = 2, OrganizerName = "Rick Sanchez", Topic = "Interdimensional Portals: Basics and Applications", Date = new DateOnly(2026, 5, 6), StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(11, 30), Status = "confirmed" },
        new Reservation { Id = 4, RoomId = 3, OrganizerName = "Invoker", Topic = "Exort, Quas, Wex: Advanced Spellcasting", Date = new DateOnly(2026, 5, 7), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(16, 0), Status = "planned" },
        new Reservation { Id = 5, RoomId = 4, OrganizerName = "Mr. Meeseeks", Topic = "Existentialism and the Pain of Being 101", Date = new DateOnly(2026, 5, 8), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12, 0), Status = "cancelled" },
        new Reservation { Id = 6, RoomId = 2, OrganizerName = "Morty Smith", Topic = "Surviving Adventures with Grandpa: A Case Study", Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "confirmed" }
    };
}
