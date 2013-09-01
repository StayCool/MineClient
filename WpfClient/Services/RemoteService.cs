using System;

namespace WpfClient.Services
{
    public static class RemoteService
    {
        public static DateTime LastRecieve { get; private set; }

        public static void onRecieve(string str)
        {
            LastRecieve = DateTime.Now;
        }
    }
}
