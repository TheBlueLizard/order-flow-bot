﻿using NinjaTrader.Custom.AddOns.OrderFlowBot.Models.DataBars.Base;

namespace NinjaTrader.Custom.AddOns.OrderFlowBot.Models.DataBars
{
    public class DataBarDataProvider : IDataBarDataProvider
    {
        public ICustomVolumetricBar VolumetricBar { get; set; }
        public int Time { get; set; }
        public int Day { get; set; }
        public int CurrentBar { get; set; }
        public int BarsAgo { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public CumulativeDeltaBar CumulativeDeltaBar { get; set; }

        public DataBarDataProvider()
        {
            VolumetricBar = null;
            Time = 0;
            Day = 0;
            CurrentBar = 0;
            BarsAgo = 0;
            High = 0;
            Low = 0;
            Open = 0;
            Close = 0;
            CumulativeDeltaBar = null;
        }
    }
}
