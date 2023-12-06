using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SheepBot.Domain.Base;
namespace SheepBot.Domain.Entities;

[Table("car")]
[Index("Name", Name = "Index_car_name", IsUnique = true)]
public partial record Car : ModelBase
{
    [Column("car_id")]
    public int CarId { get; init; }
    
    [Column("name")]
    [StringLength(50)]
    public string Name { get; init; } = null!;

    [Column("is_free")]
    public bool IsFree { get; init; }

    [Column("is_legacy")]
    public bool IsLegacy { get; init; }

    [InverseProperty("Car")]
    public virtual ICollection<CarClass> CarClasses { get; } = new List<CarClass>();

    public static DataTable CreateDataTable(IEnumerable<Car> cars)
    {
        var table = new DataTable();
        table.Columns.Add("car_id", typeof(int));
        table.Columns.Add("name", typeof(string));
        table.Columns.Add("is_free", typeof(bool));
        table.Columns.Add("is_legacy", typeof(bool));

        foreach (var car in cars)
        {
            table.Rows.Add(
                car.CarId,
                car.Name,
                car.IsFree,
                car.IsLegacy);
        }
        
        return table;
    }
}
