using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduce.Data.ViewModels
{
    public class ForumViewModel
    {
        public int ForumSeq { get; set; }

        [Required(ErrorMessage = "제목은 필수입력 항목입니다.")]
        [MinLength(2, ErrorMessage = "제목은 2자 이상 입력해주세요.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "내용은 필수입력 항목입니다.")]
        [MinLength(2, ErrorMessage = "내용은 2자 이상 입력해주세요.")]
        public string Content { get; set; }
    }
}
