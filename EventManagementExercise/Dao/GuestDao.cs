using System;
using System.Collections.Generic;

namespace EventManagementExercise.Dao
{
  public class GuestDao
  {
    public int? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DOB { get; set; }
    public IList<AllergyDao> Allergies { get; set; }
  }
}