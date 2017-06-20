using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model.Core
{
    public interface IModel
    {
        [Key]
        int Id { get; set; }
    }
}
