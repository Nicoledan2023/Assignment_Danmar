using System.ComponentModel.DataAnnotations;

public class AnimalModel
{ 
    public int AnimalModelId { get; set; }
    public string Name { get; set; }=string.Empty;
    
    [Display(Name ="Picture")]
    public string? ImageName { get; set; }=string.Empty;
    public string Species { get; set; }=string.Empty;
    public int Age { get; set; } = 0;
    public string Gender { get; set; }=string.Empty;
    public string Location { get; set; }=string.Empty;
    public string Description { get; set; }=string.Empty;
    
}