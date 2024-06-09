﻿using NinjaTrader.Custom.AddOns.OrderFlowBot.DataBar;
using System;

namespace NinjaTrader.Custom.AddOns.OrderFlowBot.Strategies
{
    // This strategy is designed for trading pullbacks on a trend or larger price ranges.
    // Trade the structure with appropriate targets on higher volatility times.
    public class DeltaChaser : StrategyBase
    {
        public override string Name { get; set; }
        public override Direction ValidStrategyDirection { get; set; }

        public DeltaChaser(OrderFlowBotState orderFlowBotState, OrderFlowBotDataBars dataBars, string name)
        : base(orderFlowBotState, dataBars, name)
        {
        }

        public override void CheckStrategy()
        {
            if (IsValidLongDirection() && ValidStrategyDirection == Direction.Flat)
            {
                CheckLong();
            }

            if (IsValidShortDirection() && ValidStrategyDirection == Direction.Flat)
            {
                CheckShort();
            }
        }

        public override void CheckLong()
        {
            if (IsBullishBar() && IsOpenAboveTriggerStrikePrice() && IsBullishMinMaxDifference() && IsValidWithinTriggerStrikePriceRange())
            {
                ValidStrategyDirection = Direction.Long;
            }
        }

        public override void CheckShort()
        {
            if (IsBearishBar() && IsOpenBelowTriggerStrikePrice() && IsBearishMinMaxDifference() && IsValidWithinTriggerStrikePriceRange())
            {
                ValidStrategyDirection = Direction.Short;
            }
        }

        private bool IsOpenAboveTriggerStrikePrice()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return dataBars.Bar.Prices.Open > orderFlowBotState.TriggerStrikePrice;
        }

        private bool IsOpenBelowTriggerStrikePrice()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return dataBars.Bar.Prices.Open < orderFlowBotState.TriggerStrikePrice;
        }

        private bool IsBullishMinMaxDifference()
        {
            long maxDelta = Math.Abs(dataBars.Bar.Deltas.MaxDelta);
            long minDelta = Math.Abs(dataBars.Bar.Deltas.MinDelta);
            bool validMinDelta = dataBars.Bar.Deltas.MinDelta > OrderFlowBotAdvancedStrategiesConfig.DeltaChaserMinMaxDifferenceDelta * -1;

            return maxDelta >= OrderFlowBotAdvancedStrategiesConfig.DeltaChaserMinMaxDifferenceMultiplier * minDelta && validMinDelta && dataBars.Bar.Deltas.Delta > OrderFlowBotAdvancedStrategiesConfig.DeltaChaserDelta;
        }

        private bool IsBearishMinMaxDifference()
        {
            long maxDelta = Math.Abs(dataBars.Bar.Deltas.MaxDelta);
            long minDelta = Math.Abs(dataBars.Bar.Deltas.MinDelta);
            bool validMaxDelta = dataBars.Bar.Deltas.MaxDelta < OrderFlowBotAdvancedStrategiesConfig.DeltaChaserMinMaxDifferenceDelta;

            return minDelta >= OrderFlowBotAdvancedStrategiesConfig.DeltaChaserMinMaxDifferenceMultiplier * maxDelta && validMaxDelta && dataBars.Bar.Deltas.Delta < OrderFlowBotAdvancedStrategiesConfig.DeltaChaserDelta * -1;
        }

        private bool IsValidWithinTriggerStrikePriceRange()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return orderFlowBotState.TriggerStrikePrice - dataBars.Bar.Prices.Close <= OrderFlowBotAdvancedStrategiesConfig.DeltaChaserValidEntryTicks * OrderFlowBotDataBarConfig.TickSize;
        }

        private bool IsBullishBar()
        {
            return dataBars.Bar.BarType == BarType.Bullish;
        }

        private bool IsBearishBar()
        {
            return dataBars.Bar.BarType == BarType.Bearish;
        }
    }
}
