using MockPipelines.NamedPipeline.Helpers;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MockPipelines.NamedPipeline
{
    class Program
    {
        static readonly string[] MESSAGES =
        {
            "Insert Card",
            "Remove Card",
            "Enter Zip Code",
            "Enter PIN",
            "STATUS: APPROVED",
            "WELCOME"
        };

        static private ServerPipeline serverpipe;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server namedpipeline...");
            serverpipe = new ServerPipeline();
            if (serverpipe != null)
            {
                serverpipe.Start();
                serverpipe.ClientConnectedEvent += Serverpipe_ClientConnectedEvent;
                serverpipe.ClientDisconnectedEvent += Serverpipe_ClientDisconnectedEvent;
                serverpipe.MessageReceivedEvent += Serverpipe_MessageReceivedEvent;
                Thread.Sleep(10000000);
                serverpipe.Stop();
            }
        }

        private static void Serverpipe_MessageReceivedEvent(object sender, Interfaces.MessageReceivedEventArgs e)
        {
            string value = System.Text.RegularExpressions.Regex.Replace(e.Message.Trim('\"'), "[\\\\]+", string.Empty);
            DalActionResponseRoot request = JsonConvert.DeserializeObject<DalActionResponseRoot>(value);
            if (request != null)
            {
                string text = request.DALActionResponse.DeviceUIResponse.Command;
                switch (text)
                {
                    case "Card Inserted":
                    {
                        serverpipe.SendMessage($"{MESSAGES[1]}");
                        break;
                    }

                    case "Card Removed":
                    {
                        serverpipe.SendMessage($"{MESSAGES[2]}");
                        break;
                    }

                    case "ZIP":
                    {
                        serverpipe.SendMessage($"{MESSAGES[3]}");
                        break;
                    }

                    case "PIN":
                    {
                        serverpipe.SendMessage($"{MESSAGES[4]}");
                        break;
                    }

                    case "WELCOME":
                    {
                        serverpipe.SendMessage($"{MESSAGES[5]}");
                        Task.Run(() =>
                        {
                            Thread.Sleep(5000);
                            serverpipe.SendMessage($"{MESSAGES[0]}");
                        });
                        break;
                    }
                }
            }
        }

        private static void Serverpipe_ClientDisconnectedEvent(object sender, Interfaces.ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("server: client disconnected");
        }

        private static void Serverpipe_ClientConnectedEvent(object sender, Interfaces.ClientConnectedEventArgs e)
        {
            if (serverpipe.ClientConnected())
            {
                serverpipe.SendMessage($"{MESSAGES[0]}");
            }

        }
    }
}
