using AdvancedScada.DriverBase;
using AdvancedScada.DriverBase.Devices;
using AdvancedScada.IBaseService;
using System;
using System.Collections.Generic;

namespace AdvancedScada.BaseService.Web
{
    public class ReadServiceWeb : IReadServiceWeb
    {
        public Dictionary<string, Tag> GetCollection()
        {
            try
            {
                return TagCollection.Tags;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
