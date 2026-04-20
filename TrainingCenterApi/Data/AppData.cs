using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data;

public static class AppData
{
    public static List<Room> Rooms { get; } =
    [
        new Room
        {
            Id = 1,
            Name = "Alpha Lab",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Beta Workshop",
            BuildingCode = "A",
            Floor = 2,
            Capacity = 18,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Gamma Hall",
            BuildingCode = "B",
            Floor = 0,
            Capacity = 40,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Delta Studio",
            BuildingCode = "B",
            Floor = 3,
            Capacity = 12,
            HasProjector = false,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Omega Classroom",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 30,
            HasProjector = true,
            IsActive = true
        }
    ];

    public static List<Reservation> Reservations { get; } =
    [
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "C# Basics",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "Confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Marek Zielinski",
            Topic = "Git Workshop",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "Pending"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Julia Nowak",
            Topic = "Agile Training",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(13, 0),
            Status = "Confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 1,
            OrganizerName = "Piotr Adamski",
            Topic = "API Design",
            Date = new DateOnly(2026, 6, 3),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 0),
            Status = "Confirmed"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "Katarzyna Lis",
            Topic = "Data Analysis",
            Date = new DateOnly(2026, 6, 7),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 30),
            Status = "Cancelled"
        },
        new Reservation
        {
            Id = 6,
            RoomId = 3,
            OrganizerName = "Tomasz Wrobel",
            Topic = "Future Leadership Session",
            Date = new DateOnly(2026, 7, 15),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "Confirmed"
        }
    ];
}
