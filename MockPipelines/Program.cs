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
            "Enter Card Type, 1) Debit, 2) Credit",
            "Enter PIN",
            "Enter Zip",
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

                    case "Debit":
                    {
                        serverpipe.SendMessage($"{MESSAGES[3]}");
                        break;
                    }

                    case "Credit":
                    {
                        serverpipe.SendMessage($"{MESSAGES[4]}");
                        break;
                    }

                    case "ZIP":
                    case "PIN":
                    {
                        serverpipe.SendMessage($"{MESSAGES[5]}");
                        break;
                    }

                    case "WELCOME":
                    {
                        serverpipe.SendMessage($"{MESSAGES[6]}");
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

    internal enum Messages
    {
        [System.ComponentModel.Description("Insert Card")]
        InsertCard = 1,
        [System.ComponentModel.Description("Card Inserted")]
        CardInserted = 2,
        [System.ComponentModel.Description("Remove Card")]
        RemoveCard = 3,
        [System.ComponentModel.Description("Card Removed")]
        CardRemoved = 4,
        [System.ComponentModel.Description("Enter Card Type\r\n1) Debit\r\n2) Credit")]
        CardType = 5,
        [System.ComponentModel.Description("Debit")]
        Debit = 6,
        [System.ComponentModel.Description("Credit")]
        Credit = 7,
        [System.ComponentModel.Description("Enter Zip Code")]
        EnterZip = 8,
        [System.ComponentModel.Description("ZIP")]
        ZipEntered = 9,
        [System.ComponentModel.Description("Enter PIN")]
        EnterPin = 10,
        [System.ComponentModel.Description("PIN")]
        PinEntered = 11,
        [System.ComponentModel.Description("STATUS: APPROVED")]
        StatusApproved = 12,
        [System.ComponentModel.Description("WELCOME")]
        Welcome = 13
    }
}
