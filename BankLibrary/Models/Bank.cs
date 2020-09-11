using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLibrary
{
    public partial class Bank
    {
        public Bank()
        {
            Department = new HashSet<Department>();
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Назва не може бути порожньою")]
        [StringLength(50)]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Інформація")]
        public string Info { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Department> Department { get; set; }
    }
}
