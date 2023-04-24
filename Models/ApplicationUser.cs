using Microsoft.AspNetCore.Identity;
using Zoo.Models;

public class ApplicationUser : IdentityUser
{
  public string FirstName { get; set; } = default!;
  public string LastName { get; set; } = default!;

  public static explicit operator ApplicationUser(PersonModel v)
  {
    throw new NotImplementedException();
  }
}