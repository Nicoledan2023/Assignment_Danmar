using System.ComponentModel.DataAnnotations;

public class EventModel
{
    public uint EventModelId { get; set; }
    public string Title { get; set; } = string.Empty;

     public string Description { get; set; } = string.Empty;
     [Display(Name ="Event Time")]
    public DateTime ETime { get; set; }
   
}