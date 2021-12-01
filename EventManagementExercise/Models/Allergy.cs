using EventManagementExercise.Dao;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementExercise.Models
{
  public class Allergy
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public string NormalizedName { get; set; }

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public Allergy(string name)
    {
      Name = name.Trim();
      NormalizedName = Name.ToUpper();
    }

    public void FromDao(AllergyDao allergyDao)
    {
      Name = allergyDao.Name.Trim();
      NormalizedName = Name.ToUpper();
    }
  }
}
