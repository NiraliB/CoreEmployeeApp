using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpAppCoreEF_Self.Models
{
    public class EmployeeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required]
        public string EmpName { get; set; }

        [Required]
        public string EmpSurname { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required]
        public int DeptId { get; set; }
        public DepartmentModel DepartmentModel { get; set; }

        [NotMapped]
        public SelectList DepartmentList { get; set; }
    }
    
}
