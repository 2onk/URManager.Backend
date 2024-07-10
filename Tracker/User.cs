using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace URManager.Backend.Tracker
{
    public class User
    {
        public static async Task TrackEvent()
        {
            var clientId = GenerateClientId();

            string serverUrl = "https://urmanager.de/index.php?option=com_eventtracker";
            using (HttpClient client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "uniqueId", clientId },
                    { "timestamp", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                try
                {
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync(serverUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Server notified successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to notify server.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to notify server.");
                }
            }
        }

        //public static async Task TrackEvent()
        //{
        //    var clientId = GenerateClientId();

        //    string serverUrl = "https://urmanager.de/index.php?option=com_eventtracker";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        var values = new Dictionary<string, string>
        //        {
        //        { "uniqueId", clientId },
        //        { "timestamp", DateTime.Now.ToString("o") }
        //    };

        //        try
        //        {
        //            var content = new FormUrlEncodedContent(values);
        //            var response = await client.PostAsync(serverUrl, content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine("Server notified successfully.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed to notify server.");
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine("Failed to notify server.");
        //        }

        //    }
        //}
        public static string GenerateClientId()
        {
            string result = "";
            ManagementClass manageClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection theCollectionOfResults = manageClass.GetInstances();

            foreach (System.Management.ManagementObject currentResult in theCollectionOfResults)
            {
                result = currentResult["ProcessorID"].ToString();
            }

            return result;
        }

    }
}
