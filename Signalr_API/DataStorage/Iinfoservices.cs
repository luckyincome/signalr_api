using Signalr_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signalr_API.DataStorage.Base;


namespace Signalr_API.DataStorage
{
    public interface Iinfoservices
    {
        Task<TwoDLiveResult> FindTowDLiveResultData(string objectId);


        Task<bool> UpdateTwoDLiveResult(TwoDLiveResult model);

        List<Live2dLogInfo> Get2dlog(string section);

    }
}
