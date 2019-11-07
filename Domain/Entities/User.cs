using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int UserId { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        [Display(Name = "Логін")]
        public string UserName { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        [Display(Name = "Почта")]
        public string UserMail { get; set; }

        public bool IsAdmin { get; set; }

        [Display(Name = "Пароль")]
        public string UserPassword { get; set; }
    }
}
