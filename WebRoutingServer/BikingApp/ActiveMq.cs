using Apache.NMS.ActiveMQ;
using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS.ActiveMQ.Commands;
using WebRoutingServer;

namespace BikingApp
{
    public class ActiveMq

        
    {
        private static ActiveMq instance;

        public void send(string message)
        {
            Uri connecturi = new Uri("activemq:tcp://localhost:61616");
            ConnectionFactory connectionFactory = new ConnectionFactory(connecturi);

            // Create a single Connection from the Connection Factory.
            IConnection connection = connectionFactory.CreateConnection();
            connection.Start();

            // Create a session from the Connection.
            ISession session = connection.CreateSession();

            // Use the session to target a queue.
            IDestination destination = session.GetQueue("test");

            // Create a Producer targetting the selected queue.
            IMessageProducer producer = session.CreateProducer(destination);

            ServiceItinerary s1 = new ServiceItinerary();
            //s1.getItinerary(string position, string destination, true);

            // You may configure everything to your needs, for instance:


            // Finally, to send messages:
            //string message1 = "";
            //foreach (Step s in s1)
            //{
            //  message1 = message1 + "\n " + s.instruction.ToString();
            //}

            //Console.WriteLine(message1);

            ITextMessage msg = session.CreateTextMessage(message);
            producer.Send(msg);








            Console.WriteLine("Message sent, check ActiveMQ web interface to confirm.");
            

            // Don't forget to close your session and connection when finished.
            session.Close();
            connection.Close();

            


            

        }
        
        public void send(List<Step> step)
        {
            String message1 = "";
            foreach (Step s in step)
            {
                message1 = message1 + "\n" + s.instruction.ToString();
            }
            send(message1);
        }

        public void sendMessageToQueue(List<Step> step)
        {
            foreach (Step s in step)
            {
                send(s.instruction.ToString());
            }







        }
        //getinstance
        public static ActiveMq getInstance()
        {
            if (instance == null)
            {
                instance = new ActiveMq();
            }
            return instance;
        }
    }
    


}

