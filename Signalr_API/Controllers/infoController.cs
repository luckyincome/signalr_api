using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Signalr_API.DataStorage;
using Signalr_API.Hubconfig;
using Signalr_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signalr_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class infoController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly Iinfoservices _infoService;
        public infoController(IHubContext<ChatHub> hubContext, Iinfoservices infoService)
        {
            _hubContext = hubContext;
            _infoService = infoService;
        }


        [HttpGet]       
        public async Task<IActionResult> Get()
        {
            TwoDLiveResult result = await _infoService.FindTowDLiveResultData("HWIbmlsHHr");
            var objectWithFields = JsonConvert.SerializeObject(result);

            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(objectWithFields);

            JArray jsonArray = new JArray();
            jsonArray.Add(jsonObj);

            JObject jsonObjectWithKey = new JObject();
            jsonObjectWithKey["results"] = jsonArray;

            string jsonResult = jsonObjectWithKey.ToString();
           
            await _hubContext.Clients.All.SendAsync("transferchartdata", jsonResult);

            return Ok(new { Message = "Request Completed" });
        }


        [HttpPost]
        [Route("SentMsg")]
        public async Task<IActionResult> Post([FromBody] TwoDLiveResult message)
        {
            var objectWithFields = JsonConvert.SerializeObject(message);
            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(objectWithFields);
            JArray jsonArray = new JArray();
            jsonArray.Add(jsonObj);
            JObject jsonObjectWithKey = new JObject();
            jsonObjectWithKey["results"] = jsonArray;
            string jsonResult = jsonObjectWithKey.ToString();
            await _hubContext.Clients.All.SendAsync("transferchartdata", jsonResult);
            //await _hubContext.Clients.All.SendAsync("transferchartdata", message);
            return Ok();
        }


        [HttpGet]
        [Route("LastDataAsync")]
        public async Task<string> LastData()
        {
            TwoDLiveResult result = await _infoService.FindTowDLiveResultData("HWIbmlsHHr");
            var objectWithFields = JsonConvert.SerializeObject(result);

            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(objectWithFields);

            JArray jsonArray = new JArray();
            jsonArray.Add(jsonObj);

            JObject jsonObjectWithKey = new JObject();
            jsonObjectWithKey["results"] = jsonArray;

            string jsonResult = jsonObjectWithKey.ToString();

            return jsonResult;
        }


        [HttpGet]
        [Route("Gettest")]
        public async Task<IActionResult> Gettest()
        {         

            return Ok(new { Message = "Request Completed" });
        }


        [HttpGet]
        [Route("Get2dlog_Bysection")]
        public List<Live2dLogInfo> Get2dlog_Bysection(string section)
        {
            List<Live2dLogInfo> lst2dlog = _infoService.Get2dlog(section);
            return lst2dlog;

        }

    }
}
