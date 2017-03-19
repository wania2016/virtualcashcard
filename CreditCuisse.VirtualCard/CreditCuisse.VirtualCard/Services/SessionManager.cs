using System.Collections.Generic;
using System.Linq;

namespace CreditCuisse.VirtualCard.Services
{
    /// <summary>
    /// This has to be a singleton class which can be eventually threadsafe,
    /// This should maintain all the requests to the sessions asking for Withdrawing the money
    /// I have kept it simple for the sake of limited time
    /// </summary>
    public static class SessionManager
    {
        private static List<string> CurrentList = new List<string>();

        public static void Start(string pin)
        {
            if (CurrentList.All(a => a != pin))
            {
                CurrentList.Add(pin);
            }
        }

        public static bool Exists(string pin)
        {
            return CurrentList.Any(a => a == pin);
        }
        public static void End(string pin)
        {
            if (CurrentList.Any(a => a == pin))
            {
                CurrentList.Remove(pin);
            }
        }

        public static void Clear()
        {
            CurrentList.Clear();
        }
    }
}