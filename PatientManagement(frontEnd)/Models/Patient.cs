using System.ComponentModel.DataAnnotations;

namespace PatientManagement_frontEnd_.Models;

public  class Patient
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }
    public int? DiseasesId { get; set; }
    public bool? Epilepsy { get; set; }
}
