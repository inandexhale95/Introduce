using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.ViewModels
{
    public class FreeBoardViewModel
    {
        [Required(ErrorMessage = "이름은 필수입력 항목입니다.")]
        [MinLength(2, ErrorMessage = "이름은 2자리 이상 입력해주세요.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "비밀번호는 필수입력 항목입니다.")]
        [MinLength(4, ErrorMessage = "비밀번호는 4자리 이상 입력해주세요.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "내용은 필수입력 항목입니다.")]
        public string Content { get; set; }
    }
}
