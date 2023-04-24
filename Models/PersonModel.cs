using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Zoo.Models;

namespace Zoo.Models;
public class PersonModel
{
  [Key]
  public uint UserModelId { get; set; }
  public string Name { get; set; } = string.Empty;

  public string Role { get; set; } = string.Empty;

  public string email { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;

  public int Age { get; set; } = 0;




}