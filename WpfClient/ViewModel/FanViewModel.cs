using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClient.ViewModel
{
    class FanViewModel
    {
        public string FanName { get; set; }
        public List<PropertyValue> PropertyValues { get; set; }
    }

    class PropertyValue
    {
        public string Property { get; set; }
        public int Value { get; set; }
    }
}
