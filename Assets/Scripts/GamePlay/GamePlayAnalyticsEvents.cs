using System;
using System.Linq;
using Analitics;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayAnalyticsEvents : MonoBehaviour
    {
        [SerializeField] private GameController _controller;

        private PropertyTimer sessionTimer; 
        private PropertyTimer turnTimer; 
        private PropertyTimer roundTimer; 
        
        private void OnEnable()
        {
            Debug.Log("Subscribe...");
            sessionTimer = new PropertyTimer(AnalyticsConstants.GAMESESSION_TIME);
            roundTimer = new PropertyTimer(AnalyticsConstants.ROUND_TIME);
            turnTimer = new PropertyTimer(AnalyticsConstants.TURN_TIME);
            
            _controller.OnStartGame += () =>
            {
                roundTimer.StartTimer();
                AnalyticsEvent.ExecuteProperty(
                    new AnalyticsEvent.PropertyUpdate(
                        AnalyticsConstants.RESTARTS, 1, AnalyticsEvent.BasicActions.SafeUpdateAdd));
            };
            _controller.OnGameEnded += (score) =>
            {
                roundTimer.StopWriteTimer();
                int gameSession = MyAnalytics.Instance.SafeGetProperty(AnalyticsConstants.GAMESESSIONS);
                AnalyticsEvent.ExecuteProperty(
                    new AnalyticsEvent.PropertyUpdate(
                        AnalyticsConstants.SCORES + "_" + gameSession, score, AnalyticsEvent.BasicActions.AddRecord));
            };
            _controller.OnCardDroped += (card) => AnalyticsEvent.ExecuteProperty(
                new AnalyticsEvent.PropertyUpdate(
                    AnalyticsConstants.CARD_DROP + "_" + card.ID,1,AnalyticsEvent.BasicActions.SafeUpdateAdd));
            _controller.OnCardPicked += (card) => AnalyticsEvent.ExecuteProperty(
                new AnalyticsEvent.PropertyUpdate(
                    AnalyticsConstants.CARD_PICK + "_" + card.ID,1,AnalyticsEvent.BasicActions.SafeUpdateAdd));
            _controller.OnCombinationCrieted += (cards) =>
            {
                int[] ids = cards.OrderBy(c=>c.ID).Select((c) => c.ID).ToArray();
                string strIds = string.Join("_",ids);
                AnalyticsEvent.ExecuteProperty(
                    new AnalyticsEvent.PropertyUpdate(
                        AnalyticsConstants.COMBOS + "_" + strIds, 1, AnalyticsEvent.BasicActions.SafeUpdateAdd));
                foreach (var card in cards)
                {
                    AnalyticsEvent.ExecuteProperty(
                        new AnalyticsEvent.PropertyUpdate(
                            AnalyticsConstants.CARD_COMBOS + "_" + card.ID, 1, AnalyticsEvent.BasicActions.SafeUpdateAdd));
                }
            };

            _controller.OnSessionsGameStarted += () =>
            {
                sessionTimer.StartTimer();
            };
            _controller.OnNextTurn += () =>
            {
                if(turnTimer.IsRunning)
                    turnTimer.StopWriteTimer();
                turnTimer.StartTimer();
            };
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                sessionTimer.StopWriteTimer();
                MyAnalytics.Instance.Save();
            }
        }

        private void OnApplicationQuit()
        {
            sessionTimer.StopWriteTimer();
            MyAnalytics.Instance.Save();
        }

    }
}