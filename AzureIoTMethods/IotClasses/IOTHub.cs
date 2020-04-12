using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AzureIoTMethods.IotClasses
{
    public class IOTHub
    {
        private readonly static string s_connectionString = "HostName=Berksiothub.azure-devices.net;DeviceId=berkdevice1;SharedAccessKey=ZLR28pssmORj1eE0ERWjVTr9lDeV3VUxSkEw1eFjxfE=";
        private static DeviceClient s_deviceClient;
        public void ListenMethod()
        {

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            // Create a handler for the direct method call
            s_deviceClient.SetMethodHandlerAsync("GetLocalIPAddress", GetLocalIPAddress, null).Wait();
        }

        private static Task<MethodResponse> GetLocalIPAddress(MethodRequest methodRequest, object userContext)
        {
            var data = Encoding.UTF8.GetString(methodRequest.Data);

            // Check the payload is a single integer value
            if (!string.IsNullOrEmpty(data))
            {

                string result = "{\"LocalIPAddress\":\"" + GetLocalIPAddress() + "\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 200));

            }
            else
            {
                // Acknowlege the direct method call with a 400 error message
                string result = "{\"result\":\"Invalid parameter\"}";
                return Task.FromResult(new MethodResponse(Encoding.UTF8.GetBytes(result), 400));
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }



        public  string SendMessage(string messages)
        {
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);

            // Create JSON message
            var Message = new
            {
                msg = messages
            };
            var messageString = JsonConvert.SerializeObject(Message);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));


            // Send the telemetry message
              s_deviceClient.SendEventAsync(message);
              Task.Delay(1000);
 
            return "Message Sent" + DateTime.Now + messageString;

        }
    }
}
