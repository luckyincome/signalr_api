using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Signalr_API.Models
{
    public class TwoDResultData
    {
        public string set_1200 { get; set; }
        public string val_1200 { get; set; }
        public string set_430 { get; set; }
        public string val_430 { get; set; }
        public string result_1200 { get; set; }
        public string result_430 { get; set; }
        public string internet_930 { get; set; }
        public string modern_930 { get; set; }
        public string internet_200 { get; set; }
        public string modern_200 { get; set; }
        public string date { get; set; }
        public string time_1200 { get; set; }
        public string time_430 { get; set; }
        public string live { get; set; }
        public string live_set { get; set; }
        public string live_val { get; set; }
        public string status_1200 { get; set; }
        public string status_430 { get; set; }
        public string last_date { get; set; }
        public int is_close_day { get; set; }
        public string current_date { get; set; }
        public string current_time { get; set; }
    }

    public class TwoDResult
    {
        public int result { get; set; }
        public string message { get; set; }
        public TwoDResultData data { get; set; }
    }

    public class LiveData
    {
        public LiveData(String setion, string from, string to, String toDisplay, bool switchManualdata)
        {
            this.section = setion;
            this.from = from;
            this.to = to;
            this.toDisplay = toDisplay;
            set = "--";
            value = "--";
            result = "--";
            isManual = true;
            isDone = false;
            switchManual = switchManualdata;
        }
        public string section { get; set; }
        public string set { get; set; }
        public string value { get; set; }
        public string result { get; set; }
        public bool isManual { get; set; }
        public bool isDone { get; set; }
        public String from { get; set; }
        public String to { get; set; }
        public String toDisplay { get; set; }

        public DateTime fromDateTime
        {
            get { return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + this.from); }
        }

        public DateTime toDateTime
        {
            get { return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + this.to); }
        }

        public DateTime toDisplayDateTime
        {
            get { return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + this.toDisplay); }
        }

        /*XXX*/
        public bool switchManual
        {
            get; set;
        }

    }

    public class LiveDataApi
    {
        public LiveDataApi(string open_time)
        {
            set = "--";
            value = "--";
            this.open_time = open_time;
            twod = "--";
        }
        public string set { get; set; }
        public string value { get; set; }
        public string open_time { get; set; }
        public string twod { get; set; }
    }

    public class Back4app2DResults
    {
        public string SET_set { get; set; }
        public string SET_val { get; set; }
        public string latest_set { get; set; }
        public string latest_val { get; set; }
        public bool isDeleteOldDatas { get; set; }
        public string deleted_date { get; set; }
        public string resultNumber { get; set; }
        public string created_date { get; set; }
    }


    public class IndexIndustrySector
    {
        public string symbol { get; set; }
        public string nameEN { get; set; }
        public string nameTH { get; set; }
        public double prior { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double last { get; set; }
        public double change { get; set; }
        public double percentChange { get; set; }
        public double volume { get; set; }
        public double value { get; set; }
        public string querySymbol { get; set; }
        public string marketStatus { get; set; }
        public DateTime marketDateTime { get; set; }
        public string marketName { get; set; }
        public string industryName { get; set; }
        public string sectorName { get; set; }
        public string level { get; set; }
    }

    public class SetApiResult
    {
        public List<IndexIndustrySector> indexIndustrySectors { get; set; }
    }



    public class DResult
    {
        public int result { get; set; }        
        public TwoDLiveResult data { get; set; }
    }

    public class TwoDLiveResult
    {       
        public string objectId { get; set; }
        public string Set { get; set; }
        public string data { get; set; }
        public string backupData { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public DateTime updatedAt { get; set; }
        public string adminKey { get; set; }
        public string Value { get; set; }
        public DateTime createdAt { get; set; }
        public string result { get; set; }
        public string Key { get; set; }
        public string bakData { get; set; }

    }


    public class OutputJson
    {
        public TwoDLiveResult results { get; set; }
    }

    public class Live2dLogInfo
    {
        public string objectId { get; set; }

        public bool isReference { get; set; }

        public string set { get; set; }

        public string section { get; set; }

        public DateTime updateAt { get; set; }

        public DateTime date { get; set; }

        public bool isManual { get; set; }

        public string value { get; set; }

        public DateTime createdAt { get; set; }

        public string result { get; set; }
    }

}
