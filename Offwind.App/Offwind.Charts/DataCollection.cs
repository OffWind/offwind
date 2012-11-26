using System.Collections.Generic;

namespace Offwind.Charts
{
    public class DataCollection
    {
        private List<DataSeries> dataList;
        public DataCollection()
        {
            dataList = new List<DataSeries>();
        }
        public List<DataSeries> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }
    }
}
