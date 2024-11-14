using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.DTO
{
    public class CheckInDTO
    {
        public string CheckInState { get; set; } = string.Empty;
        public List<IFormFile> CheckInPictures { get; set; } = [];
    }
}
