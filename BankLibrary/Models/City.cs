using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace BankLibrary
{
    public partial class City
    {
        public City()
        {
            Department = new HashSet<Department>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Назва не може бути порожньою")]
        [StringLength(50)]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        public virtual ICollection<Department> Department { get; set; }
    }
}
