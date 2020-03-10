using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OblServer
{
    public class Server
    {
        private static List<Book> books = new List<Book>()
        {
            new Book {Forfatter = "forfatter1", Isbn13 = "1234567891012", Sidetal = 40, Titel = "bog1"},
            new Book{Forfatter = "forfatter2", Isbn13 = "1234567891212", Sidetal = 40, Titel = "bog2"},
            new Book{Forfatter = "forfatter3", Isbn13 = "1234567821012", Sidetal = 40, Titel = "bog3"}
        };
        


        public void Start()
        {
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddress, 4646);

            server.Start();
            Console.WriteLine("Server has been launched");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client entered server");
                Task.Run(() =>
                {
                    TcpClient tempSocket = client;
                    DoClient(tempSocket);
                });
            }
            
        }
        public void DoClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();

            //Her definere vi en streamreader så vi kan læse fra serveren samt en streamwriter så vi kan skrive til serveren
            StreamReader sRead = new StreamReader(ns);
            StreamWriter sWrite = new StreamWriter(ns);
            sWrite.AutoFlush = true;

            string message = sRead.ReadLine();

            string answer = "";
            //While løkke til at sende og modtage beskeder
            while (message != null && message != "")
            {
                string[] messageArray = message.Split(' ');
                string param = message.Substring(message.IndexOf(' ') + 1);
                string command = messageArray[0];


                switch (command)
                {
                    case "GetAll":
                        sWrite.WriteLine(JsonConvert.SerializeObject(books));
                        break;
                    case "Get":
                        sWrite.WriteLine(books.Find(b => b.Isbn13 == param));
                        break;
                    case "Save":
                        Book saveBook = JsonConvert.DeserializeObject<Book>(param);
                        books.Add(saveBook);
                        break;
                }

                message = sRead.ReadLine();
            }
            //Slukker alt
            ns.Close();
        }
    }
}

