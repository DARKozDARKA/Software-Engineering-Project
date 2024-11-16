using UnityEngine;

namespace CodeBase.Services.Unity
{
    public class TimeCounter : ITimeCounter
    {
        private float _startTime;

        public void StartCountingTime()
        {
            _startTime = Time.time;
        }

        public float GetCurrentTimeDifference()
        {
            return Time.time - _startTime;
        }
    }
}