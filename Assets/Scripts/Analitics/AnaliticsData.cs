using System;
using System.Collections.Generic;

namespace Analitics
{
    [Serializable]
    public class AnaliticsData
    {
        
        private Dictionary<string, int> _properties;
        private Dictionary<string, List<int>> _records;


        public Dictionary<string, int> Properties => _properties;
        public Dictionary<string, List<int>> Records => _records;

        public AnaliticsData()
        {
            _properties = new Dictionary<string, int>();
            _records = new Dictionary<string, List<int>>();
        }

        public AnaliticsData(Dictionary<string, int> properties, Dictionary<string, List<int>> records)
        {
            _properties = properties;
            _records = records;
        }

        public void Clear()
        {
            _properties.Clear();
            _records.Clear();
        }
    }
}