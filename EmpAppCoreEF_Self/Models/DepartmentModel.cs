using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Models
{
    public class DepartmentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeptId { get; set; }
        [Required]
        public string DeptName { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<EmployeeModel> EmployeeModels { get; set; }
    }
}
