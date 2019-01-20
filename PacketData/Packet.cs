using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PacketData
{
    [Serializable]
    public class Packet
    {
        public List<string> GData;
        public List<Orar> orar;
        public int packetInt;
        public bool packetBool;
        public string senderID;
        public string senderName;
        public PacketType packetType;
        public Packet(PacketType packetType, string senderID)
        {
            GData = new List<string>();
            this.senderID = senderID;
            this.packetType = packetType;
        }
        public Packet(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);

            Packet p = (Packet)bf.Deserialize(ms);
            ms.Close();
            this.GData = p.GData;
            this.orar = p.orar;
            this.packetInt = p.packetInt;
            this.packetBool = p.packetBool;
            this.senderID = p.senderID;
            this.packetType = p.packetType;
        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, this);
            byte[] bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }
        public static string GetIP4Address()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress i in ips)
            {
                if (i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return i.ToString();
            }
            return "127.0.0.1";
        }
    }
    [Serializable]
    public class Orar
    {
        public DateTime Zi { get; set; }
        public int Nr_Row { get; set; }
        public int Modul { get; set; }
        public string Nume { get; set; }
        public string Denumire { get; set; }
        public Orar(DateTime zi,int nr_row,int modul,string nume,string denumire)
        {
            this.Zi = zi;
            this.Nr_Row = nr_row;
            this.Modul = modul;
            this.Nume = nume;
            this.Denumire = denumire;
        }
    }
    public enum PacketType
    {
        Connecting,
        Chat,
        Login,
        Register,
        GenerareOrar
    }
}
