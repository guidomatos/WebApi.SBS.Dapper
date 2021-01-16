using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.SBS.ApplicationCore.DTO
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}