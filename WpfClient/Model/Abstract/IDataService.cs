using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClient.Model.Abstract
{
    public interface IDataService
    {
        void InsertRawData(string msg);
    }
}
