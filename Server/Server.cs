using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Net;
using PacketData;
namespace Server
{
    class Server
    {
        static Socket listenerSocket;
        static List<Orar> orar;
        static bool isOrar = false;
        static List<ClientData> _clients;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on " + Packet.GetIP4Address());
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<ClientData>();

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(Packet.GetIP4Address()), 4242);
            listenerSocket.Bind(ip);

            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();

        }


        static void ListenThread()
        {
            for (; ; )
            {
                listenerSocket.Listen(0);
                _clients.Add(new ClientData(listenerSocket.Accept()));
            }
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        private static void Insert(DateTime zi,int nr_row,int modul,int grupaID,int profesorMaterieID)
        {
            using (var context = new FacultateEntities())
            {
                var newRow = new Orar()
                {
                    Zi = zi,
                    Nr_Row = nr_row,
                    Modul = modul,
                    GrupaID = grupaID,
                    ProfesorMaterieID = profesorMaterieID
                 
                    
                };
                context.Orars.Add(newRow);
                context.SaveChanges();
            }
            Console.WriteLine("Row in Orar inserted!");
        }
        public static void genereazaOrar()
        {
            isOrar = true;
            using (var context = new FacultateEntities())
            {
                var grupe = context.Grupes.ToList();
                var profesori = context.Profesoris.ToList();
                var materii = context.Materiis.ToList();
                var profesorMaterie = context.ProfesorMateries.ToList();

             

                int[,] nr_ore = new int[grupe.Count()+1, materii.Count()+1];
                foreach(var grupa in grupe)
                foreach (var materie in materii)
                {
                    nr_ore[grupa.ID,materie.ID] = materie.Nr_Ore;
                }
                int nr_row = 1;
                foreach (DateTime day in EachDay(new DateTime(2009, 3, 10),new DateTime(2009, 3, 16)))
                {
                    nr_row++;
                    for (int modul = 0; modul < 3;modul++)
                    {
                        bool[] ocupat = Enumerable.Repeat(false, 17).ToArray();
                       foreach(var grupa in grupe)
                        {

                            foreach(var materie in materii)
                            {
                                if (!ocupat[materie.ID])
                                {
                                    if (nr_ore[grupa.ID,materie.ID] > 0)
                                    {
                                        foreach (var x in profesorMaterie)
                                        {
                                            if (materie.ID == x.MaterieID)
                                            {
                                                nr_ore[grupa.ID,materie.ID]--;
                                                Insert(day,nr_row, modul, grupa.ID, x.ID);
                                                break;
                                            }
                                        }
                                        ocupat[materie.ID] = true;
                                        break;
                                    }
                                }
                            }

                        }

                    }
                }
            }
        }
        public static void Data_IN(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;
            byte[] Buffer;
            int readBytes;
            for (; ; )
            {
                try
                {

                    Buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(Buffer);
                    if (readBytes > 0)
                    {
                        Packet packet = new Packet(Buffer);
                        DataManager(packet);
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("A client disconnected");
                    return;
                }

            }
        }

        public static Packet GetOrar(int grupaId,Packet packet)
        {
            using (var context = new FacultateEntities())
            {
                var orar = context.getOrarByGrupaId(grupaId);
                packet.orar = new List<PacketData.Orar>();
                foreach (getOrarByGrupaId_Result cs in orar)
                {
                    packet.orar.Add(new PacketData.Orar(cs.Zi, cs.Nr_Row, cs.Modul, cs.Nume, cs.Denumire));
                }
                return packet; 
            }
            return packet;
        }
        
        public static Packet GetOrar(string nickname, Packet packet)
        {          
                using (var context = new FacultateEntities())
                {
                    var orar = context.getOrarByProfesor(nickname);
                    packet.orar = new List<PacketData.Orar>();
                    foreach (getOrarByProfesor_Result cs in orar)
                    {
                        packet.orar.Add(new PacketData.Orar(cs.Zi, cs.Nr_Row, cs.Modul,cs.Nume, cs.Denumire));
                    }
                    return packet;
                }
            return packet;
        }
        
        public static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Chat:
                    foreach (ClientData c in _clients)
                    {
                        Console.WriteLine("Send to " + p.senderID);
                        
                        c.clientSocket.Send(p.ToBytes());
                    }
                    break;
                case PacketType.Login:
                    foreach (ClientData c in _clients)
                    { //gaseste conexiunea clientului in lista de clienti
                        if (p.senderID.Equals(c.id))
                        { //daca clientul e inca conectat
                            
                            Packet packet = new Packet(PacketType.Login, c.id); //packetul de trimis
                            
                            using (var context = new FacultateEntities())
                            {
                                var studenti = context.Studentis.ToList();
                                var profesori = context.Profesoris.ToList();
                             
                                packet.GData.Add("Inregistrati-va pentru conectare!");
                                packet.GData.Add("None");
                                packet.GData.Add("None");
                                packet.orar = new List<PacketData.Orar>();

                                foreach (Studenti student in studenti)
                                     {
                                         if (student.Nickname.Equals(p.GData[0]))
                                         {
                                             if (student.Parola.Equals(p.GData[1])) { 
                                             c.name = p.GData[0];
                                            if (isOrar)
                                            {
                                                packet = GetOrar(student.GrupaID, packet);
                                                packet.GData[2] = "Orar";
                                            }
                                            packet.GData[0] = "succes";
                                            packet.GData[1] = "Student";
                                            c.clientSocket.Send(packet.ToBytes());
                                             return;
                                             }
                                             else
                                             {
                                                 packet.GData[0] = "Parola este gresita!";
                                                 c.clientSocket.Send(packet.ToBytes());
                                                 return;
                                             }
                                         }
                                     }
                                 foreach(Profesori profesor in profesori)
                                 {
                                    if(profesor.Nickname!=null)
                                     if (profesor.Nickname.Equals(p.GData[0]))
                                     {
                                         if (profesor.Parola.Equals(p.GData[1]))
                                         {
                                             c.name = p.GData[0];
                                                if (isOrar)
                                                { packet = GetOrar(profesor.Nume, packet);
                                                    packet.GData[2] = "Orar";
                                                }
                                             packet.GData[0] = "succes";
                                             packet.GData[1] = "Profesor";

                                                c.clientSocket.Send(packet.ToBytes());
                                             return;
                                         }
                                         else
                                         {
                                             packet.GData[0] = "Parola este gresita!";
                                             c.clientSocket.Send(packet.ToBytes());
                                             return;
                                         }
                                     }
                                 }
                            }
                            c.clientSocket.Send(packet.ToBytes());
                        }
                    }
                  
                
                    break;
                case PacketType.Register:
                    foreach (ClientData c in _clients)
                    { //gaseste conexiunea clientului in lista de clienti
                        if (p.senderID.Equals(c.id))
                        { //daca clientul e inca conectat
                            Packet packet = new Packet(PacketType.Register, c.id); //packetul de trimis
                            packet.GData.Add("Nu sunteti in baza de date!");
                            using (var context = new FacultateEntities())
                            {
                                if (p.GData[5].Equals("Student"))
                                {
                                    var studenti = context.Studentis.ToList();
                                    foreach(Studenti student in studenti)
                                    {
                                        if (student.Nume.Equals(p.GData[0]))
                                        {
                                            if (student.Prenume.Equals(p.GData[1]))
                                            {
                                                var studentnou = (from c1 in context.Studentis
                                                                   where c1.Nume.Equals(p.GData[0]) && c1.Prenume.Equals(p.GData[1])
                                                                   select c1).First();
                                                studentnou.Nickname = p.GData[2];
                                                studentnou.Parola = p.GData[3];
                                                context.SaveChanges();
                                                packet.GData[0] = "succes";
                                                c.clientSocket.Send(packet.ToBytes());
                                                return;
                                            }
                                            else
                                            {
                                                packet.GData[0] = student.Nume + " nu are prenumele " + p.GData[1];
                                                c.clientSocket.Send(packet.ToBytes());
                                                return;
                                            }
                                        }
                                    }
                                       
                                }
                                else
                                {
                                    var profesori = context.Profesoris.ToList();
                                    foreach(Profesori profesor in profesori)
                                    {
                                        if(profesor.Nume.Equals(p.GData[0]))
                                        {
                                            if (profesor.Prenume.Equals(p.GData[1]))
                                            {
                                                string x1 = p.GData[0];
                                                string x2 = p.GData[1];
                                                var profesornou = (from c1 in context.Profesoris
                                                                   where c1.Nume.Equals(x1) && c1.Prenume.Equals(x2)
                                                                      select c1).First();
                                                profesornou.Nickname = p.GData[2];
                                                profesornou.Parola = p.GData[3];
                                                 context.SaveChanges(); 
                                      
                                                packet.GData[0] = "succes";
                                                c.clientSocket.Send(packet.ToBytes());
                                                return;
                                            }
                                            else
                                            {
                                                packet.GData[0] = profesor.Nume + " nu are prenumele " + p.GData[1];
                                                c.clientSocket.Send(packet.ToBytes());
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            c.clientSocket.Send(packet.ToBytes());
                        }
                    }
                            break;
                case PacketType.GenerareOrar:
                    if(!isOrar)
                    genereazaOrar();
                    foreach (ClientData c in _clients)
                    {
                        if (p.senderID.Equals(c.id))
                        {
                             Packet packet = new Packet(PacketType.GenerareOrar, c.id);
                            packet.orar = new List<PacketData.Orar>();
                            using (var context = new FacultateEntities())
                            {
                               
                                var studenti = context.Studentis.ToList();
                                var profesori = context.Profesoris.ToList();
                                packet.orar = new List<PacketData.Orar>();
                          
                                if (p.GData[1].Equals("Student"))
                                {
                                    foreach (Studenti student in studenti)
                                    {
                                        if (student.Nickname.Equals(p.GData[0]))                
                                                packet = GetOrar(student.GrupaID, packet);
                                                packet.GData.Add(student.Nume);
                                    }
                                }
                                else
                                {
                                    foreach (Profesori profesor in profesori)
                                    {
                                        if (profesor.Nickname != null)
                                            if (profesor.Nickname.Equals(p.GData[0]))                                        
                                                        packet = GetOrar(profesor.Nume, packet);
                                        packet.GData.Add(profesor.Nume);
                                    }
                                }
                            }
                            c.clientSocket.Send(packet.ToBytes());
                        }
                    }
                    break;
            }
        }
    }
    class ClientData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;
        public string name;

        public ClientData()
        {
            this.id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            DidConnection();
        }
        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            this.id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            DidConnection();
        }

        public void DidConnection()
        {
            Packet p = new Packet(PacketType.Connecting, "server");
            p.GData.Add(id);
            p.GData.Add("succes");
            clientSocket.Send(p.ToBytes());
        }
    }

}