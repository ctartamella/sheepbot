using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;

namespace SheepBot.Domain.Entities;

[Table("car_class")]
[Index("CarId", "ClassId", Name = "Index_class_car_pair", IsUnique = true)]
public partial record CarClass : ModelBase
{
    [Column("class_id")]
    public long ClassId { get; init; }

    [Column("car_id")]
    public long CarId { get; init; }

    [ForeignKey("CarId")]
    [InverseProperty("CarClasses")]
    public virtual Car Car { get; init; } = null!;

    [ForeignKey("ClassId")]
    [InverseProperty("CarClasses")]
    public virtual Class Class { get; init; } = null!;

    public static DataTable CreateDataTable(IEnumerable<CarClass> carClasses)
    {
        var table = new DataTable();
        table.Columns.Add("car_id", typeof(int));
        table.Columns.Add("class_id", typeof(int));;

        foreach (var c in carClasses)
        {
            table.Rows.Add(c.CarId, c.ClassId);
        }
        
        return table;
    }
}
