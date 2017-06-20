using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model.Core
{
    public class User : IModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz.")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz.")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen resim ekleyiniz.")]
        [DisplayName("Resim")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Lütfen yetkilendirme ekleyiniz.")]
        [DisplayName("Yönetici")]
        public bool IsAdmin { get; set; }
    }
}
