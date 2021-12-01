using System;
using System.Collections.Generic;

namespace EventManagementExercise.Dao
{
  public class EventDao
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public IList<int> Guests { get; set; }
  }
}