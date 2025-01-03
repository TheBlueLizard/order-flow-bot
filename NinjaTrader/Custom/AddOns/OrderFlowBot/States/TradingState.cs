﻿using NinjaTrader.Custom.AddOns.OrderFlowBot.Configs;
using NinjaTrader.Custom.AddOns.OrderFlowBot.Models.Strategies;
using System.Collections.Generic;

namespace NinjaTrader.Custom.AddOns.OrderFlowBot.States
{
    public class TradingState : IReadOnlyTradingState
    {
        private readonly dynamic _initialTriggeredState;
        private readonly dynamic _initialTradeDirectionState;
        public string TriggeredName { get; private set; }
        public bool StrategyTriggered { get; private set; }
        public Direction TriggeredDirection { get; private set; }
        public Direction SelectedTradeDirection { get; set; }
        public Direction StandardInverse { get; set; }
        public bool IsBacktestEnabled { get; set; }
        public string BacktestStrategyName { get; set; }
        public bool IsTradingEnabled { get; set; }
        public bool IsAutoTradeEnabled { get; set; }
        public bool IsAlertEnabled { get; set; }
        public double TriggerStrikePrice { get; set; }
        public List<string> SelectedStrategies { get; set; }
        public int LastTradedBarNumber { get; set; }
        public int CurrentBarNumber { get; set; }
        public bool HasMarketPosition { get; set; }

        public TradingState(IBacktestData backtestData)
        {
            IsBacktestEnabled = backtestData.IsBacktestEnabled;
            BacktestStrategyName = backtestData.Name;

            _initialTriggeredState = new
            {
                TriggeredName = "None",
                StrategyTriggered = false,
                TriggeredDirection = Direction.Flat,
            };

            _initialTradeDirectionState = new
            {
                TriggerStrikePrice = 0,
                StandardInverse = Direction.Standard,
                SelectedTradeDirection = IsBacktestEnabled ? Direction.Any : Direction.Flat
            };

            IsTradingEnabled = true;
            IsAutoTradeEnabled = false;
            IsAlertEnabled = false;
            SelectedStrategies = new List<string>();
            LastTradedBarNumber = 0;
            CurrentBarNumber = 0;
            HasMarketPosition = false;

            SetInitialTriggeredState();
            SetInitialTradeDirection();
        }

        public void SetTriggeredTradingState(
            string name,
            bool strategyTriggered,
            Direction triggeredDirection
        )
        {
            TriggeredName = name;
            StrategyTriggered = strategyTriggered;
            TriggeredDirection = triggeredDirection;
        }

        public void SetInitialTriggeredState()
        {
            TriggeredName = _initialTriggeredState.TriggeredName;
            StrategyTriggered = _initialTriggeredState.StrategyTriggered;
            TriggeredDirection = _initialTriggeredState.TriggeredDirection;
        }

        public void SetInitialTradeDirection()
        {
            TriggerStrikePrice = _initialTradeDirectionState.TriggerStrikePrice;
            StandardInverse = _initialTradeDirectionState.StandardInverse;
            SelectedTradeDirection = _initialTradeDirectionState.SelectedTradeDirection;
        }

        public void AddStrategyByName(string name)
        {
            if (!SelectedStrategies.Contains(name))
            {
                SelectedStrategies.Add(name);
            }
        }

        public void RemoveStrategyByName(string name)
        {
            SelectedStrategies.RemoveAll(s => s == name);
        }

        public void RemoveAllSelectedStrategies()
        {
            SelectedStrategies.Clear();
        }

        public void ResetTriggerStrikePrice()
        {
            TriggerStrikePrice = 0;
        }

        public void ResetSelectedTradeDirection()
        {
            SelectedTradeDirection = Direction.Flat;
        }
    }
}
