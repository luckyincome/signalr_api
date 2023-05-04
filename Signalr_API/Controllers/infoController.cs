using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Signalr_API.Auth;
using Signalr_API.DataStorage;
using Signalr_API.Hubconfig;
using Signalr_API.Models;
using Signalr_API.Util;
using System;
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
        public static string secretKey;
        private readonly IConfiguration _config;
        public infoController(IHubContext<ChatHub> hubContext, Iinfoservices infoService, IConfiguration config)
        {
            _hubContext = hubContext;
            _infoService = infoService;
            this._config = config;
            secretKey = _config.GetValue<string>("apiSecretKey:secret");
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


        [HttpPost]
        [Route("saveresultout")]
        public async Task<IActionResult> saveresultout([FromBody] ResultOutModel model)
        {           

            string sectionname = string.Empty;
            int sectioncount = 0;

            if (MemoryCacheHelper.Exists("api2dresultout" + model.sectionId.ToString()))
            {
                return StatusCode(StatusCodes.Status301MovedPermanently, new Response { Status = "Error", Message = "you had published this section result before" });
            }

            DateTimeOffset expiration = DateTime.Now.AddHours(10);
            MemoryCacheHelper.Add("api2dresultout" + model.sectionId.ToString(), "saveresultout", expiration);

            DateTime editDate = DateTime.Now;
            if (editDate.Date != model.for_date_time.Date)
            {
                return StatusCode(StatusCodes.Status303SeeOther, new Response { Status = "Error", Message = "Please choose correct date" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string signature = Common.generateMD5ResultOut(model, secretKey);
            if (signature != model.signature)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new Response { Status = "Error", Message = "Invalid Signature" });
            }
            else
            {
                if (model.sectionId == 1) { sectionname = "10:30 AM"; sectioncount = 0; }
                else if (model.sectionId == 2) { sectionname = "12:01 PM"; sectioncount = 1; }
                else if (model.sectionId == 3) { sectionname = "02:30 PM"; sectioncount = 2; }
                else if (model.sectionId == 4) { sectionname = "04:30 PM"; sectioncount = 3; }

                var getLiveData = await _infoService.FindTowDLiveResultData("HWIbmlsHHr");
                string changeData = getLiveData.data;
                List<LiveData> _liveDatas = JsonConvert.DeserializeObject<List<LiveData>>(changeData);
                LiveData liveItem = _liveDatas.Find(s => (sectionname == s.section && DateTime.Now.Date == s.fromDateTime.Date));//find current section
                if (liveItem != null && liveItem.isDone == false)
                {
                    _liveDatas[sectioncount].set = model.set;
                    _liveDatas[sectioncount].value = model.setvalue;
                    _liveDatas[sectioncount].result = model.number;
                    _liveDatas[sectioncount].isDone = true;

                    var objectresult = JsonConvert.SerializeObject(_liveDatas); //Remark XXXX 

                    TwoDLiveResult twoDResults = new TwoDLiveResult();
                    twoDResults.data = objectresult; //from live data
                    twoDResults.result = model.number;
                    twoDResults.Set = model.set;
                    twoDResults.Value = model.setvalue;
                    twoDResults.updatedAt = DateTime.Now;
                    twoDResults.createdAt = DateTime.Now;
                    twoDResults.lastUpdateDate = DateTime.Now;
                    twoDResults.objectId = getLiveData.objectId;
                    twoDResults.adminKey = Guid.NewGuid().ToString();
                    twoDResults.bakData = getLiveData.bakData;
                    twoDResults.backupData = getLiveData.backupData;
                    twoDResults.Key = getLiveData.Key;
                    await _infoService.UpdateTwoDLiveResult(twoDResults);


                    Live2dLogInfo logmodel = new Live2dLogInfo();
                    logmodel = await _infoService.FindLive2dLogByManual(sectionname, true);                    

                    if (logmodel != null)
                    {
                        logmodel.set = model.set;
                        logmodel.value = model.setvalue;
                        logmodel.result = model.number.ToString();
                        logmodel.date = liveItem.toDisplayDateTime;

                        //updtae existing data
                        await _infoService.UpdateLive2dLog(logmodel);
                    }
                    ////manual-insert-beforeReference
                    if (logmodel == null)
                    {
                        logmodel = new Live2dLogInfo();
                        logmodel.set = model.set;
                        logmodel.value = model.setvalue;
                        logmodel.result = model.number.ToString();
                        logmodel.isReference = true;
                        logmodel.section = sectionname;
                        logmodel.date = liveItem.toDisplayDateTime;

                        //inser new row log
                        await _infoService.InsertLive2dLog(logmodel);
                    }

                    Get();


                    return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Result success!" });
                }

            }         

            return StatusCode(StatusCodes.Status304NotModified, new Response { Status = "Error", Message = "Result failed!" });
        }




        [HttpGet]
        [Route("resultbysection")]
        public async Task<List<ResultList>> ResultBySection(string section,DateTime date)
        {
            List<ResultList> lst = new List<ResultList>();

            var getLiveData = await _infoService.FindTowDLiveResultData("HWIbmlsHHr");
            String changeData = getLiveData.data;

            List<ResultList> _liveDatas = JsonConvert.DeserializeObject<List<ResultList>>(changeData);

            ResultList liveItem = _liveDatas.Find(s => (date.Date == s.fromDateTime.Date && section == s.section));
            if(liveItem != null) { lst.Add(liveItem); }           

            return lst;
        }

    }
}
