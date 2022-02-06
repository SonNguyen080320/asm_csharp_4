using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asm_rewrite.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [StringLength(maximumLength: 255, MinimumLength = 1, ErrorMessage = "Nhập tên từ 3 - 255 kí tự")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ")]
        [StringLength(maximumLength: 255, MinimumLength = 1, ErrorMessage = "Nhập họ 3 - 255 kí tự")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 255, MinimumLength = 10, ErrorMessage = "Nhập địa chỉ từ 10 - 255 kí tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(maximumLength: 15, MinimumLength = 10, ErrorMessage = "Nhập số điện thoại từ 10 - 15 kí tự")]
        public string Phone { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [StringLength(maximumLength: 255, MinimumLength = 10, ErrorMessage = "Nhập email từ 10 - 255 kí tự")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(maximumLength: 255, MinimumLength = 6, ErrorMessage = "Nhập mật khẩu từ 6 - 255 kí tự")]
        public string Password { get; set; }
    }
}
