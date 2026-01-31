using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingApp.Core.Entities;

public class Reservation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty; 

    [Required]
    public int ParkingSpotId { get; set; }

    [Required]
    public string AccessCode { get; set; } = string.Empty; 
    public DateTime ReservedForDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;


    public virtual ParkingSpot? ParkingSpot { get; set; }
}