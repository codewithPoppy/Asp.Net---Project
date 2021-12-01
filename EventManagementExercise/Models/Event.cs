using EventManagementExercise.Models.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementExercise.Models
{
  public class Event
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public DateTime Date { get; set; }

    [Required]
    [CollectionLength(2)]
    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
  }
}
