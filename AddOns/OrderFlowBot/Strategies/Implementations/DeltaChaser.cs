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
            bool validMinDelta = minDelta < 100;

            return maxDelta >= 2.5 * minDelta && validMinDelta && dataBars.Bar.Deltas.Delta > 150;
        }

        private bool IsBearishMinMaxDifference()
        {
            long maxDelta = Math.Abs(dataBars.Bar.Deltas.MaxDelta);
            long minDelta = Math.Abs(dataBars.Bar.Deltas.MinDelta);
            bool validMaxDelta = maxDelta < 100;

            return minDelta >= 2.5 * maxDelta && validMaxDelta && dataBars.Bar.Deltas.Delta < -150;
        }

        private bool IsValidWithinTriggerStrikePriceRange()
        {
            if (orderFlowBotState.TriggerStrikePrice == 0)
            {
                return true;
            }

            return orderFlowBotState.TriggerStrikePrice - dataBars.Bar.Prices.Close <= 3;
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
