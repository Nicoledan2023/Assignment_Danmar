using System.ComponentModel.DataAnnotations;

public class UserModel
{
  public uint UserModelId { get; set; }
  public string Name { get; set; } = string.Empty;

  public string Role { get; set; } = string.Empty;

  public string email { get; set; } = string.Empty;

  public string PhoneNumber { get; set; } = string.Empty;

  public int Age { get; set; } = 0;




}