using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comubus_CAN
{
    public partial class Form1 : Form
    {
        int formnya = 0;
        string bawahnya = System.Environment.NewLine;
        public string bufftext;
        public string[] oo2;
        public int oo3;
        public int oo4;
        public int iter = 0;
        public int[] intbufftext;
        public string result3 = string.Empty;
        public Form1()
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            textBox1.ScrollBars = ScrollBars.Vertical;
            foreach (String oo in SerialPort.GetPortNames()) {
                comboBox1.Items.Add(oo);
            }
        }
        SerialPort seport;
        public void serial_port_connect(String port) {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();

            seport = new SerialPort(port);
        }
            /*try
            {
                label4.Text = "Connected to" + port;
                seport.DataReceived += new SerialDataReceivedEventHandler(seport_DataReceived);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        private void seport_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();
            textBox1.AppendText( dtn + "\t" + seport.ReadExisting() + "\n");
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            switch (ctrl.BackColor.Name) {
                case "LightGray":
                    ctrl.BackColor = Color.Gray;
                    formnya = 1;
                    label3.Visible = true;
                    label3.Text = "Data";
                    label2.Text = "";
                    textBox1.Text = result3;
                    button2.BackColor = Color.LightGray;
                    button3.BackColor = Color.LightGray;
                    break;
                case "Gray":
                    ctrl.BackColor = Color.LightGray;
                    formnya = 0;
                    label3.Visible = false;
                    textBox1.Text = "";
                    break;
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            switch (ctrl.BackColor.Name)
            {
                case "LightGray":
                    ctrl.BackColor = Color.Gray;
                    formnya = 2;
                    label3.Visible = true;
                    label2.Visible = true;
                    label3.Text = "Timestamp";
                    label2.Text = "Data";
                    button1.BackColor = Color.LightGray;
                    button3.BackColor = Color.LightGray;
                    break;
                case "Gray":
                    ctrl.BackColor = Color.LightGray;
                    formnya = 0;
                    label3.Visible = false;
                    label2.Visible = false;
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Control ctrl = ((Control)sender);
            switch (ctrl.BackColor.Name)
            {
                case "LightGray":
                    ctrl.BackColor = Color.Gray;
                    formnya = 3;
                    button1.BackColor = Color.LightGray;
                    button2.BackColor = Color.LightGray;
                    break;
                case "Gray":
                    ctrl.BackColor = Color.LightGray;
                    formnya = 0;
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void konversiabjadhexa1()
        {
            if (oo2[0] == "A") { oo3 = 10; }
            if (oo2[0] == "B") { oo3 = 11; }
            if (oo2[0] == "C") { oo3 = 12; }
            if (oo2[0] == "D") { oo3 = 13; }
            if (oo2[0] == "E") { oo3 = 14; }
            if (oo2[0] == "F") { oo3 = 15; }
        }
        public void konversiabjadhexa2()
        {
            if (oo2[1] == "A") { oo4 = 10; }
            if (oo2[1] == "B") { oo4 = 11; }
            if (oo2[1] == "C") { oo4 = 12; }
            if (oo2[1] == "D") { oo4 = 13; }
            if (oo2[1] == "E") { oo4 = 14; }
            if (oo2[1] == "F") { oo4 = 15; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string bufftext = textBox2.Text;
            string[] bufftexts = bufftext.Split(' ');
            string lung1 = bufftext.Replace(" ", "");
            int lung = (lung1.Length) / 2;
            intbufftext = new int[lung];

            foreach (string oo in bufftexts) {
                var oostringbuilder = new StringBuilder(oo);
                oostringbuilder.Insert(1, ".");
                string oo1 = oostringbuilder.ToString();

                oo2 = oo1.Split('.');
                if (oo2[0] == "A" || oo2[0] == "B" || oo2[0] == "C" || oo2[0] == "D" || oo2[0] == "E" || oo2[0] == "F")
                {
                    konversiabjadhexa1();
                }
                else {
                    oo3 = Convert.ToInt32(oo2[0]);
                }
                if (oo2[1] == "A" || oo2[1] == "B" || oo2[1] == "C" || oo2[1] == "D" || oo2[1] == "E" || oo2[1] == "F")
                {
                    konversiabjadhexa2();
                }
                else
                {
                    oo4 = Convert.ToInt32(oo2[1]);
                }
                int oo5 = (oo3 * 16) + oo4;
                intbufftext[iter] = oo5;
                iter = iter + 1;
            }
            backgroundWorker2.RunWorkerAsync();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Connect")
            {
                button5.Text = "Close";
                String port = comboBox1.Text;
                serial_port_connect(port);
                seport.Open();
                label4.Text = "Connected to "+port;
                backgroundWorker1.RunWorkerAsync();  //start receiving data in background
                backgroundWorker2.WorkerSupportsCancellation = true; // ability to cancel this thread
            }
            else
            {

                if (button5.Text == "Close") 
                {
                    seport.Close();
                    label4.Text = "not connected";
                    button5.Text = "Connect";
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (seport.IsOpen)
            {
                try
                {
                    // int transmitCount = transmittxtbox.TextLength;
                    int a = seport.ReadByte();
                    string strTemp = "";
                    string result2 = string.Empty;
                    String resultString = a.ToString("X2");
                    DateTime dt = DateTime.Now;
                    String dtn = dt.ToShortTimeString();

                    if (formnya == 0)
                    {
                        // this.textBox1.Invoke(new MethodInvoker(delegate () { textBox1.AppendText(resultString + " "); }));
                        result2 = resultString + " ";
                        result3 += result2;
                    }
                    if (formnya == 1)
                    {
                        this.textBox1.Invoke(new MethodInvoker(delegate () { textBox1.AppendText(resultString + " "); }));
                        result2 = resultString + " ";
                        result3 += result2;
                    }

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message.ToString());
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            
            {
               // seport.Write(bufftext);
            
                int count1 = 0;
                foreach (int a in intbufftext)
                {
                    if (seport.IsOpen)
                    {

                        //int a = STR.Read();
                        //String resultString = new string(a.ToString("X").ToCharArray());

                        byte[] bytCAT = BitConverter.GetBytes(intbufftext[count1]);
                        seport.Write(bytCAT, 0, 1);
                        count1 = count1 + 1;
                        iter = 0;
                        
                        string timestamps = DateTime.Now.ToString("HH:mm:ss");

                    }
                    else
                    {
                        MessageBox.Show("Send Failed");
                    }
                    backgroundWorker2.CancelAsync();
                }
            }
        }
    }
}
