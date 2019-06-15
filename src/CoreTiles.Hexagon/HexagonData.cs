using System;
using System.Collections.Concurrent;

namespace CoreTiles.Hexagon
{
    [Serializable]
    public class HexagonData
    {
        public HexagonData()
        {
            Data = new ConcurrentDictionary<string, object>();
        }

        public ConcurrentDictionary<string, object> Data { get; set; }


        public object this[string type]
        {
            get
            {
                object result;
                return Data.TryGetValue(type, out result) ? result : null;
            }
            set => Data[type] = value;
        }
    }

    public class HexagonDataFieldAttribute : Attribute
    {
    }
}