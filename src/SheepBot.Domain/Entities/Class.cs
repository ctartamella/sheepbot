using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("class")]
[Index("Name", Name = "Index_class_name", IsUnique = true)]
public partial record Class : ModelBase
{
    [Column("class_id")]
    public int ClassId { get; init; }
    
    [Column("name")]
    [StringLength(50)]
    public string Name { get; init; } = null!;

    [InverseProperty("Class")]
    public virtual ICollection<CarClass> CarClasses { get; init; } = new List<CarClass>();

    [InverseProperty("Class")]
    public virtual ICollection<EventClass> EventClasses { get; init; } = new List<EventClass>();
    
    public static DataTable CreateDataTable(IEnumerable<Class> classes)
    {
        var table = new DataTable();
        table.Columns.Add("class_id", typeof(int));
        table.Columns.Add("name", typeof(string));

        foreach (var c in classes)
        {
            table.Rows.Add(c.ClassId, c.Name);
        }

        return table;
    }
}
