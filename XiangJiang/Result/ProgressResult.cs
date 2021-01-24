using System;

namespace XiangJiang.Result
{
    public struct ProgressResult
    {
        public long Current { get; set; }
        public long Total { get; set; }

        public double Percent => Math.Round((double) (100 * Current) / Total, 2);
    }
}