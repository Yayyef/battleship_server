using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace src
{

    /*
     * Classe du serveur
     * 
     * StartServer crée un socket et écoute sur le port spécifié
     * On défini ensuite des fonctions d'envoi et de réception des données
     */ 
	public class BS_Server
	{
        //socket passive pour écouter les connections
        //InterNetwork = on utilise ipv4
        //Stream = la connection est bidirectionnelle on envoie et recoit des données
        //Tcp = on utilise TCP
		private Socket passiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //socket qui se connecte au client
		private Socket clientSocket;

        //buffer pour envoyer les données
		private  byte[] data = new byte[1024];

        //numéro de port
		private static int port = 1000;

		public BS_Server()
        {
           
        }

        //Commence à écouter les connections
        public void StartServer()
        {

            //Pour pouvoir redémarrer le serveur rapidement
            passiveSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            //On se connecte à n'importe quelle adresse sur le num de port spécifié
            passiveSocket.Bind(new System.Net.IPEndPoint(IPAddress.Any, port));

            passiveSocket.Listen(1024);
            Console.WriteLine("Searching for players...");

            clientSocket = passiveSocket.Accept();
            IPEndPoint EP = (IPEndPoint)clientSocket.RemoteEndPoint;
            Console.WriteLine("A new challenger ! Connected to host " + EP.Address);


        }

        //Recoit les coordonnées du client et retourne la chaine (exemple : "b6")
        //En placant les coordonnées sous forme d'entiers dans outPos (ex : [1,5]) 
        public string GetPosition(int[] outPos)
        {
            int received = clientSocket.Receive(data);
            string msg = Encoding.ASCII.GetString(data,0,received);
            while (clientSocket.Available > 0)
            {
                received = clientSocket.Receive(data);
                msg += Encoding.ASCII.GetString(data,0,received);
            }
            Console.WriteLine(msg);
            if (!GetCoord(msg, outPos))
            {
                Console.WriteLine("Bad input from client. Aborting...");
                return "";
            }

            return msg;
        }

        //Se contente d'envoyer la chaine "message" codée en ASCII au client
        public void SendResponse(string message)
        {
            if (clientSocket == null)
            {
                Console.WriteLine("Start the server before sending positions stupid");
                return;
            }


            data = Encoding.ASCII.GetBytes(message);
            clientSocket.Send(data);
        }

        //Se contente de lire la réponse du client et la retourne
        public string GetResponse()
        {
            
            if (clientSocket == null)
            {
                Console.WriteLine("Start the server before sending positions stupid");
                return "" ;
            }

            int received = clientSocket.Receive(data);
            string msg = Encoding.ASCII.GetString(data,0 , received);

            while (clientSocket.Available > 0)
            {
                received = clientSocket.Receive(data);
                msg += Encoding.ASCII.GetString(data,0,received);
            }
            return msg;
        }

        //Finisseur de l'objet, on libère les ressources utilisées
        ~BS_Server()
        {

            if (passiveSocket != null)
            {
                passiveSocket.Close();
            }
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        //Fonction "helper" pour traduire une chaine "position" de type "b6" en coordonnées -> 1,5 dans "coord"
        //Retourne true ssi la chaine "position" est correctement formatée
        public bool GetCoord(string position, int [] coord)
        {
            if (position.Length < 2)
                return false;
            coord[0] = position[0] - 'a';
            coord[1] = Convert.ToInt32(position.Substring(1))-1;
            if (coord[0] < 0 || coord[0] > 9 || coord[1] < 0 || coord[1] > 9)
                return false;
            return true;
        }

    }
}

