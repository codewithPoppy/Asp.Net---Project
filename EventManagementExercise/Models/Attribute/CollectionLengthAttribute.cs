using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace EventManagementExercise.Models.Attribute
{
  public class CollectionLengthAttribute : ValidationAttribute
  {
    private readonly int MinLength;

    public CollectionLengthAttribute(int minLength)
    {
      MinLength = minLength;
    }

    public override bool IsValid(object value)
    {
      var collection = value as ICollection;
      return collection?.Count >= MinLength;
    }
  }
}
