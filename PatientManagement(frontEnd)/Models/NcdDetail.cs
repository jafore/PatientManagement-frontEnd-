using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement_frontEnd_.Models;

[Table("NCDDetails")]
public  class NcdDetail
{
    [Key]
    public int Id { get; set; }

    public int? PatientId { get; set; }

    [Column("NCDId")]
    public int? Ncdid { get; set; }
}
