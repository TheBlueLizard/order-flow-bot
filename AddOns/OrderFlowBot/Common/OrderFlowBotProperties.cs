﻿namespace NinjaTrader.Custom.AddOns.OrderFlowBot
{
    public struct OrderFlowBotPropertiesConfig
    {
        public double TickSize { get; set; }

        // DataBar
        public int LookBackBars { get; set; }
        public double ImbalanceRatio { get; set; }
        public int StackedImbalance { get; set; }
        public long ValidBidVolume { get; set; }
        public long ValidAskVolume { get; set; }
        public double ValidExhaustionRatio { get; set; }
        public double ValidAbsorptionRatio { get; set; }
    }

    public static class OrderFlowBotProperties
    {
        public static double TickSize { get; private set; }

        // DataBar
        public static int LookBackBars { get; private set; }
        public static double ImbalanceRatio { get; private set; }
        public static int StackedImbalance { get; private set; }
        public static long ValidBidVolume { get; private set; }
        public static long ValidAskVolume { get; private set; }
        public static double ValidExhaustionRatio { get; private set; }
        public static double ValidAbsorptionRatio { get; private set; }

        public static void Initialize(OrderFlowBotPropertiesConfig config)
        {
            TickSize = config.TickSize;

            LookBackBars = config.LookBackBars;
            ImbalanceRatio = config.ImbalanceRatio;
            StackedImbalance = config.StackedImbalance;
            ValidBidVolume = config.ValidBidVolume;
            ValidAskVolume = config.ValidAskVolume;
            ValidExhaustionRatio = config.ValidExhaustionRatio;
            ValidAbsorptionRatio = config.ValidAbsorptionRatio;
        }
    }
}
