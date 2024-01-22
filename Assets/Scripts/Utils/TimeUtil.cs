using UnityEngine;

namespace SensoryWorlds.Utils
{
    public static class TimeUtil
    {
        public static string GetTimestamp(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            var milliseconds = Mathf.FloorToInt(time % 1 * 1000);

            return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
        }
    }
}