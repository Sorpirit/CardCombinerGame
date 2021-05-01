using UnityEngine;

namespace Analitics
{
    public struct PropertyTimer
    {
        private string key;
        private float startTime;
        private float endTime;

        public bool IsRunning { get; private set; }

        public float Timer => endTime - startTime;

        public PropertyTimer(string key)
        {
            this.key = key;
            startTime = 0;
            endTime = 0;
            IsRunning = false;
        }

        public void StartTimer()
        {
            startTime = Time.time;
            IsRunning = true;
        }

        public void StopWriteTimer()
        {
            endTime = Time.time;
            WriteTime();
            IsRunning = false;
        }
        private void WriteTime()
        {
            int time = Mathf.RoundToInt(endTime - startTime);
            AnalyticsEvent.ExecuteProperty(
                new AnalyticsEvent.PropertyUpdate(
                    key,time, AnalyticsEvent.BasicActions.AddRecord));
        }

        public string Key => key;
    }
}