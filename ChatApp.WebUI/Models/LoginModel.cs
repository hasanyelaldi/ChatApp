using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatApp.WebUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz.")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz.")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

    }
}