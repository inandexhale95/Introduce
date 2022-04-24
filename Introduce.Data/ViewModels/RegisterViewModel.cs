using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "아이디는 필수입력 항목 입니다.")]
        [MinLength(6, ErrorMessage = "아이디는 6자 이상 입력해야 합니다.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "이름은 필수입력 항목 입니다.")]
        [MinLength(2, ErrorMessage = "이름은 2자 이상 입력해야 합니다.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "이메일은 필수입력 항목 입니다.")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "비밀번호는 필수입력 항목 입니다.")]
        [MinLength(6, ErrorMessage = "비밀번호는 6자 이상 입력해야 합니다.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
