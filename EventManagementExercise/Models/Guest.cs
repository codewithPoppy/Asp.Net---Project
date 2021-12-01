using EventManagementExercise.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementExercise.Models
{
  public class Guest
  {
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string NormalizedName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string NormalizedEmail { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DOB { get; set; }

    public virtual ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public Guest(string firstName, string lastName, string email)
    {
      FirstName = firstName.Trim();
      LastName = lastName.Trim();
      Email = email.Trim();
      Normalize();
    }
    public Guest(string firstName, string lastName, string email, DateTime dob)
    {
      FirstName = firstName.Trim();
      LastName = lastName.Trim();
      Email = email.Trim();
      Normalize();
    }

    public void FromDao(GuestDao guestDao)
    {
      FirstName = guestDao.FirstName.Trim();
      LastName = guestDao.LastName.Trim();
      Email = guestDao.Email.Trim();
      DOB = guestDao.DOB;
      Normalize();
    }

    private void Normalize()
    {
      NormalizedName = String.Concat(FirstName, " ", LastName).ToUpper();
      NormalizedEmail = Email.ToUpper();
    }
  }
}
