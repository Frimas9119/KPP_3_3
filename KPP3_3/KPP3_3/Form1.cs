using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPP3_3
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;
        private Thread listenerThread;
        private Panel panel1;

        public Form1()
        {
            InitializeComponent();
            InitializeServer();
        }

        private void InitializeServer()
        {
            tcpListener = new TcpListener(IPAddress.Any, 12345);
            listenerThread = new Thread(new ThreadStart(ListenForClients));
            listenerThread.Start();
        }

        private void ListenForClients()
        {
            tcpListener.Start();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient tcpClient = (TcpClient)clientObj;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                string data = Encoding.ASCII.GetString(message, 0, bytesRead);
                ProcessData(data);
            }

            tcpClient.Close();
        }

        private void ProcessData(string data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ProcessData(data)));
            }
            else
            {
                switch (data)
                {
                    case "Black":
                        panel1.BackColor = Color.Coral;
                        break;

                    case "Red":
                        panel1.BackColor = Color.Red;
                        break;

                    case "Blue":
                        panel1.BackColor = Color.Blue;
                        break;

                    case "Yellow":
                        panel1.BackColor = Color.Yellow;
                        break;

                    case "HidePanel1":
                        panel1.Visible = false;
                        break;

                    case "ShowPanel1":
                        panel1.Visible = true;
                        break;

                }
            }
        }
    }
}
