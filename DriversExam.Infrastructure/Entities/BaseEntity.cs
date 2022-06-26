using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DriversExam.Infrastructure.Entities;

public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime DateOfCreation { get; set; }
    public DateTime DateOfUpdate { get; set; }
}