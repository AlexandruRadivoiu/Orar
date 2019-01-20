using PacketData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{

    class CClient
    {
        
        public static Socket master;
        public static string id;
        public static List<Orar> orar;
        public static bool hasOrar = false;
        public static Packet packet;
        public static string name;
        public static string rank;
        public static string parola;
        public static bool gate = false;
        public static string result;

        public static void connect()
        {
        //// Console.WriteLine("Enter your name: ");
        //name = Console.ReadLine();
        A: Console.Write("Enter host IP Address: ");
            // string ip = Console.ReadLine();
            string ip = "192.168.56.1";

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);

            try
            {
                master.Connect(ipe);
            }
            catch
            {
                Console.WriteLine("Could not connect to host!");
                Thread.Sleep(1000);
                goto A;
            }
            Thread t = new Thread(Data_IN);
            t.Start();
            /*   for (; ; )
           {

           Console.WriteLine("::>");
             string input = Console.ReadLine();

             Packet p = new Packet(PacketType.Chat, id);
             p.GData.Add(name);
             p.GData.Add(input);
             master.Send(p.ToBytes());
           }    */
        }
      
        public static void Login()
        {
            Packet p = new Packet(PacketType.Login, id);
       
            p.GData.Add(name);
            p.GData.Add(parola);
            master.Send(p.ToBytes());
        }
        public static void Register(string nume,string prenume,string nickname,string parola ,string email,string rank)
        {
            Packet p = new Packet(PacketType.Register, id);
            p.GData.Add(nume);
            p.GData.Add(prenume);
            p.GData.Add(nickname);
            p.GData.Add(parola);
            p.GData.Add(email);
            p.GData.Add(rank);
            master.Send(p.ToBytes());

        }
        public static void RequestOrar()
        {
            Packet p = new Packet(PacketType.GenerareOrar, id);
            p.GData.Add(name);
            p.GData.Add(rank);
            master.Send(p.ToBytes());
        }
        static void Data_IN()
        {
            byte[] Buffer;
            int readBytes;
            for (; ; )
            {
                try
                {

                    Buffer = new byte[master.SendBufferSize];
                    readBytes = master.Receive(Buffer);
                    if (readBytes > 0)
                    {
                        DataManager(new Packet(Buffer));
                    }

                }
                catch (SocketException ex)
                {
                    Console.WriteLine("The server has disconnected");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }
        static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Connecting:
                    id = p.GData[0];
                    Console.WriteLine(p.GData[1]);
                    break;
                case PacketType.Chat:
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(p.GData[0] + ":" + p.GData[1]);
                    Console.ForegroundColor = c;
                    break;
                case PacketType.Login:


                    ///aici se intoarce
                    Console.WriteLine(p.GData[0]);
                    result = p.GData[0];
                    rank = p.GData[1];
                    if (!p.GData[2].Equals("None"))
                    {
                        hasOrar = true;
                        orar = p.orar;
                    }
                    if (result.Equals("succes"))
                        gate = true;
                       break;
                case PacketType.Register:
                    Console.WriteLine(p.GData[0]);
                    result = p.GData[0];
                    if (result.Equals("succes"))
                        gate = true;
                    break;
                  
                case PacketType.GenerareOrar:
                    orar = p.orar;
                    hasOrar = true;
                    result = p.GData[0];
                    break;
            }
        }
    }
}

