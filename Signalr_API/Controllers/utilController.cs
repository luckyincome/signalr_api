using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signalr_API.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signalr_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class utilController : ControllerBase
    {
        [AllowAnonymous]
        [Route("c")] 
        [HttpGet]
        public async System.Threading.Tasks.Task<string> getpwd(String pwd)
        {
            if (pwd != null && pwd == "apiclearcacheall")
            {
                MemoryCacheHelper.Clear();
                return null;
            }
            if (pwd != null && pwd.Contains("apiclearcache_"))
            {
                string keyName = pwd.Replace("apiclearcache_", "");
                MemoryCacheHelper.Remove(keyName);
                return null;
            }

            return null;
        }
    }
}
