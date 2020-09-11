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
        [Display(Name = "Номер відділення")]
        [Range(0,int.MaxValue,ErrorMessage = "Номер відділення повинен бути додатнім")]
        public int Number { get; set; }
        [Display(Name = "Місто")]
        public int CityId { get; set; }
        [Display(Name = "Інформація")]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }
        [Display(Name = "Кількість працівників")]
        [Required(ErrorMessage = "Кількість працівників не може бути порожньою")]
        [Range(1, int.MaxValue, ErrorMessage = "Кількість працівників повинна бути більша ніж 0")]
        public int NumberOfEmployers { get; set; }
        public string Photo { get; set; }

        public virtual Bank Bank { get; set; }
        [Display(Name = "Назва міста")]
        public virtual City City { get; set; }
    }
}
