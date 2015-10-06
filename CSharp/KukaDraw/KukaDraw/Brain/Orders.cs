using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using KukaDraw.Com;

namespace KukaDraw.Brain
{
    class Orders
    {
        private ArrayList orders = new ArrayList();
        private ClientTcp myClient;

        public void giveOrders(ClientTcp remoteClient)
        {
            this.myClient = remoteClient;

            // Send all orders
            this.myClient.Send("START");
            foreach (string order in orders)
            {
                this.myClient.Send(order);
                Console.WriteLine(order);
            }
            this.myClient.Send("STOP");


        }

        // Send all orders to the server
        public void giveOrders(int iPort, string sAddress)
        {
            // Create to client and configurate it
            this.myClient = new ClientTcp();
            this.myClient.InitConfig(iPort, sAddress);

            // Connect to server
            this.myClient.Connect();

            // Send all orders
            this.myClient.Send("START");
            foreach (string order in orders)
            {
                this.myClient.Send(order);
            }
            this.myClient.Send("STOP");

            // Disconnect from server
            this.myClient.Disconnect();

            // Clean orders
            this.orders.Clear();
        }

        // Link the following points and add them to the orders buffer
        public void addOrder(PointF[] points)
        {
            string frame = "";
            PointF ptn1, ptn2;

            //Construct all orders
            for (int i = 0; i <= points.Length - 2; i++)
            {
                ptn1 = points[i];
                ptn2 = points[i + 1];
                frame = "LINE:" + ptn1.X + ";" + ptn1.Y + ":" + ptn2.X + ";" + ptn2.Y;
                frame = frame.Replace(",", ".");
                this.orders.Add(frame);
            }

        }

        // Same ad addOrder(Pointf[]) but with lists
        public void addOrder(List<PointF> listPoints)
        {
            // Transform the list to an array
            PointF[] tabPoints = listPoints.ToArray();

            // Call the array method
            this.addOrder(tabPoints);
        }

        // Print all orders in the console
        public void showOrders()
        {
            int index = 1;
            foreach (string order in orders)
            {
                Console.WriteLine("Order {0}: {1}", index, order);
                index++;
            }
        }
    }
}
