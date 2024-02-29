using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement_frontEnd_.Models;

public  class AllergiesDetail
{
    [Key]
    public int Id { get; set; }

    public int? PatienId { get; set; }

    [Column("AllergiesID")]
    public int? AllergiesId { get; set; }
}
