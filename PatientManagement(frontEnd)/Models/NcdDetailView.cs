using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement_frontEnd_.Models;


public  class NcdDetailView
{
 
    public int Id { get; set; }

    public String? Patient { get; set; }


    public String? Ncd { get; set; }
}
