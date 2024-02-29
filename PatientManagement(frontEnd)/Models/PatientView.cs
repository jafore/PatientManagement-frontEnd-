using System.ComponentModel.DataAnnotations;

namespace PatientManagement_frontEnd_.Models;

public  class PatientView
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }
    public string? Diseases { get; set; }
    public bool? Epilepsy { get; set; }
}
