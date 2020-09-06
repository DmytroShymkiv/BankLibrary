using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankLibrary
{
    public partial class Department
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        [Required(ErrorMessage = "Номер відділення не може бути порожнім")]
        [StringLength(50)]
        [Display(Name = "Номер відділення")]
        public int Number { get; set; }
        public int CityId { get; set; }
        [Display(Name = "Інформація")]
        public string Info { get; set; }
        [Display(Name = "Кількість працівників")]
        [Required(ErrorMessage = "Кількість працівників не може бути порожньою")]
        public int NumberOfEmployers { get; set; }
        public string Photo { get; set; }

        public virtual Bank Bank { get; set; }
        [Display(Name = "Назва міста")]
        public virtual City City { get; set; }
    }
}
