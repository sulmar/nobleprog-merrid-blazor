using System.ComponentModel;

namespace Domain.Models;

public class Customer : BaseEntity
{
    [Description("Nazwa")]
    public string Name { get; set; }

    [Description("Opis")]
    public string Description { get; set; }

}
