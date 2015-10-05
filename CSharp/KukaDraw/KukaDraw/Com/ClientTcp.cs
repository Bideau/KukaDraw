using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace KukaDraw.Com
{
    class ClientTcp
    {
        // Client socket
        private Socket client;

        // Server point
        private IPEndPoint serverRemote;

        // ManualResetEvent instances signal completion
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        private static ManualResetEvent disconnectDone =
            new ManualResetEvent(false);

        // Init configuration
        public void InitConfig(int iPort, string sAddress)
        {

            try
            {
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(sAddress);
                //IPAddress serverAddress = ipHostInfo.AddressList[0];

                IPAddress serverAddress = IPAddress.Parse(sAddress);
                this.serverRemote = new IPEndPoint(serverAddress, iPort);

                // Create a TCP/IP socket
                this.client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Connect
        public void Connect()
        {
            try
            {
                this.client.BeginConnect(this.serverRemote, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Connect callBack
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Send a message
        public void Send(String data)
        {
            Console.WriteLine("Trying to send {0}...", data);

            // Adding end of line
            data = data + '\n';

            // Convert the string to byte using ASCII
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            try
            {
                // Sending message
                this.client.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Send callback
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Disconnect
        public void Disconnect()
        {
            this.Send("DISCONNECTED");
            this.client.BeginDisconnect(false, new AsyncCallback(DisconnectCallback), client);
        }

        // Disconnect callback
        private static void DisconnectCallback(IAsyncResult ar)
        {
            Console.WriteLine("Disconnected from server");

            // Signal that the client is disconnected.
            disconnectDone.Set();
        }

        //Get Status
        public bool getStatus()
        {
            return this.client.Connected;
        }

        //Get connected to
        public string getConnectTo()
        {
            if (this.client == null)
            {
                return " ";
            }
            else
            {
                return this.client.RemoteEndPoint.ToString();
            }
            
        }
    }
}
