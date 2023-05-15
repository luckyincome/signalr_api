using Signalr_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signalr_API.DataStorage.Base;

namespace Signalr_API.DataStorage
{
    public class infoservices : Iinfoservices
    {
        public async Task<TwoDLiveResult> FindTowDLiveResultData(string objectId)
        {
            TwoDLiveResult info = new TwoDLiveResult();

            await Task.Run(() =>
            {
                List<ParameterInfo> parameters = new List<ParameterInfo>();
                parameters.Add(new ParameterInfo() { ParameterName = "objectId", ParameterValue = objectId });
                info = new SqlHelper().GetRecord<TwoDLiveResult>("pro_find_TwoDLiveResult", parameters);

            });

            return info;
        }


        public async Task<bool> UpdateTwoDLiveResult(TwoDLiveResult model)
        {
            bool result = false;
            await Task.Run(() =>
            {
                List<ParameterInfo> parameters = new List<ParameterInfo>();
                parameters.Add(new ParameterInfo() { ParameterName = "objectId", ParameterValue = model.objectId });

                parameters.Add(new ParameterInfo() { ParameterName = "Set", ParameterValue = model.Set });

                parameters.Add(new ParameterInfo() { ParameterName = "data", ParameterValue = model.data });

                parameters.Add(new ParameterInfo() { ParameterName = "backupData", ParameterValue = model.backupData });

                parameters.Add(new ParameterInfo() { ParameterName = "lastUpdateDate", ParameterValue = model.lastUpdateDate });

                parameters.Add(new ParameterInfo() { ParameterName = "updatedAt", ParameterValue = model.updatedAt });

                parameters.Add(new ParameterInfo() { ParameterName = "adminKey", ParameterValue = model.adminKey });

                parameters.Add(new ParameterInfo() { ParameterName = "Value", ParameterValue = model.Value });

                parameters.Add(new ParameterInfo() { ParameterName = "createdAt", ParameterValue = model.createdAt });

                parameters.Add(new ParameterInfo() { ParameterName = "result", ParameterValue = model.result });

                parameters.Add(new ParameterInfo() { ParameterName = "Key", ParameterValue = model.Key });

                parameters.Add(new ParameterInfo() { ParameterName = "bakData", ParameterValue = model.bakData });

                parameters.Add(new ParameterInfo() { ParameterName = "isService", ParameterValue = model.isService });

                result = new SqlHelper().ExecuteQuery("pro_update_TwoDLiveResultForAPI", parameters) > 0 ? true : false;
            });
            return result;
        }

        public List<Live2dLogInfo> Get2dlog(string section)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "section", ParameterValue = section });            
            return new SqlHelper().GetRecords<Live2dLogInfo>("pro_find_2dlog", parameters);
        }



        public async Task<Live2dLogInfo> FindLive2dLogByManual(string section, bool isReference)
        {
            Live2dLogInfo info = new Live2dLogInfo();
            await Task.Run(() =>
            {
                List<ParameterInfo> parameters = new List<ParameterInfo>();
                parameters.Add(new ParameterInfo() { ParameterName = "section", ParameterValue = section });
                parameters.Add(new ParameterInfo() { ParameterName = "isReference", ParameterValue = isReference });
                info = new SqlHelper().GetRecord<Live2dLogInfo>("pro_find_Live2dLog_By_Manual", parameters);

            });

            return info;
        }


        public async Task<bool> InsertLive2dLog(Live2dLogInfo model)
        {
            bool result = false;
            await Task.Run(() =>
            {
                List<ParameterInfo> parameters = new List<ParameterInfo>();

                parameters.Add(new ParameterInfo() { ParameterName = "isReference", ParameterValue = model.isReference });

                parameters.Add(new ParameterInfo() { ParameterName = "set", ParameterValue = model.set });

                parameters.Add(new ParameterInfo() { ParameterName = "section", ParameterValue = model.section });

                parameters.Add(new ParameterInfo() { ParameterName = "date", ParameterValue = model.date });

                parameters.Add(new ParameterInfo() { ParameterName = "isManual", ParameterValue = model.isManual });

                parameters.Add(new ParameterInfo() { ParameterName = "result", ParameterValue = model.result });

                parameters.Add(new ParameterInfo() { ParameterName = "value", ParameterValue = model.value });

                result = new SqlHelper().ExecuteQuery("pro_insert_Live2dLog", parameters) > 0 ? true : false;
            });
            return result;
        }

        public async Task<bool> UpdateLive2dLog(Live2dLogInfo model)
        {
            bool result = false;
            await Task.Run(() =>
            {
                List<ParameterInfo> parameters = new List<ParameterInfo>();
                parameters.Add(new ParameterInfo() { ParameterName = "objectId", ParameterValue = model.objectId });

                parameters.Add(new ParameterInfo() { ParameterName = "isReference", ParameterValue = model.isReference });

                parameters.Add(new ParameterInfo() { ParameterName = "set", ParameterValue = model.set });

                parameters.Add(new ParameterInfo() { ParameterName = "value", ParameterValue = model.value });

                parameters.Add(new ParameterInfo() { ParameterName = "section", ParameterValue = model.section });

                //parameters.Add(new ParameterInfo() { ParameterName = "updateAt", ParameterValue = model.updateAt });

                parameters.Add(new ParameterInfo() { ParameterName = "date", ParameterValue = model.date });

                parameters.Add(new ParameterInfo() { ParameterName = "isManual", ParameterValue = model.isManual });

                // parameters.Add(new ParameterInfo() { ParameterName = "createdAt", ParameterValue = model.createdAt });

                parameters.Add(new ParameterInfo() { ParameterName = "result", ParameterValue = model.result });

                result = new SqlHelper().ExecuteQuery("pro_update_Live2dLog", parameters) > 0 ? true : false;
            });
            return result;
        }




    }


   
}
