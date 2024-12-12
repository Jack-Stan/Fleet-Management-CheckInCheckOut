using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO
{
    public class CheckOutDTO
    {
        public string CheckOutState { get; set; } = string.Empty;
        public List<IFormFile> CheckOutPictures { get; set; } = [];
    }
}
