using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Core
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name cannot contain numbers.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(50, ErrorMessage = "Surname can't be longer than 50 characters.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Surname cannot contain numbers.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Cellphone is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Cellphone number can't be longer than 15 characters.")]
        [RegularExpression(@"^\+?[0-9]{7,15}$", ErrorMessage = "Cellphone must be a valid number.")]
        public string Cellphone { get; set; }
    }
}
