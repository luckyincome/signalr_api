using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signalr_API.Auth
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class FailCountResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int failCount { get; set; }
    }
}
