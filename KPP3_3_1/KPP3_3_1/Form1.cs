using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace KPP3_3_1
{
    public partial class Form1 : Form
    {
        private TcpClient tcpClient;
        private NetworkStream clientStream;
        public Form1()
        {
            InitializeComponent();
            this.MouseClick += Form1_MouseClick;
            InitializeClient();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, new Point(e.X, e.Y));
            }
        }

        private void InitializeClient()
        {
            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 12345);
            clientStream = tcpClient.GetStream();
        }

        private void SendData(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        private void visibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!visibleToolStripMenuItem.Checked)
            {
                visibleToolStripMenuItem.Checked = true;
                SendData("ShowPanel1");
            }
            else
            {
                visibleToolStripMenuItem.Checked = false;
                SendData("HidePanel1");

            }
        }

        private void clWindowTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clWindowTextToolStripMenuItem.Checked = true;
            clRedToolStripMenuItem.Checked = false;
            clBlueToolStripMenuItem.Checked = false;
            clYellowToolStripMenuItem.Checked = false;
            SendData("Black");
        }

        private void clRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clWindowTextToolStripMenuItem.Checked = false;
            clRedToolStripMenuItem.Checked = true;
            clBlueToolStripMenuItem.Checked = false;
            clYellowToolStripMenuItem.Checked = false;
            SendData("Red");
        }

        private void clBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clWindowTextToolStripMenuItem.Checked = false;
            clRedToolStripMenuItem.Checked = false;
            clBlueToolStripMenuItem.Checked = true;
            clYellowToolStripMenuItem.Checked = false;
            SendData("Blue");
        }

        private void clYellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clWindowTextToolStripMenuItem.Checked = false;
            clRedToolStripMenuItem.Checked = false;
            clBlueToolStripMenuItem.Checked = false;
            clYellowToolStripMenuItem.Checked = true;
            SendData("Yellow");
        }
    }
}
