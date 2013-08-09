using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Model.Entities;

namespace WpfClient.Services
{
    public class FanService
    {
        public string GetFanMode(int workingFanNum, List<Door> doors)
        {
            var pattern = new StringBuilder();
            doors.ForEach(d => pattern.Append(d.StateId));

            if (workingFanNum == 1) {
                return pattern.ToString() == "332223332" ? "Норма" :
                       pattern.ToString() == "332232232" ? "Реверс" : "Авария";
            }
            if (workingFanNum == 2) {
                return pattern.ToString() == "223323323" ? "Норма" :
                       pattern.ToString() == "223332223" ? "Реверс" : "Авария";
            }

            return "Авария";
        }
    }
}
