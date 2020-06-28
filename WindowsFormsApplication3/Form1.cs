using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Security.Permissions;
using MetroFramework;
using MetroFramework.Forms;
using SAMP;

namespace WindowsFormsApplication3
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        int NpcCount = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;

            // RCON query example
            
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send("exit");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }

            MessageBox.Show(output);
            Application.Exit();
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@"samp-server.exe", "");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;

            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send("gmx");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;

            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send("loadfs Anti0RS");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            metroButton13.Enabled = false;
            metroButton14.Enabled = true;
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void metroButton15_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;

            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send("loadfs AntiFlo0D");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            } 
            metroButton15.Enabled = false;
            metroButton16.Enabled = true;
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);

        }

        private void metroButton14_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "";
            string Rcon_Pass = metroTextBox1.Text;

            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send("unloadfs Anti0RS");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            metroButton14.Enabled = false;
            metroButton13.Enabled = true;

            MessageBox.Show(output);
            richTextBox1.Text += output;
            richTextBox1.Text += "";
        }

        private void metroButton16_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;

            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text,Int16.Parse(metroTextBox3.Text), Rcon_Pass, true))
            {
                rq.Send("unloadfs AntiFlo0D");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            metroButton15.Enabled = true;
            metroButton16.Enabled = false;
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroButton13.Enabled = true;
            metroButton14.Enabled = false;
            metroButton15.Enabled = true;
            metroButton16.Enabled = false;
            string output = "";
            // Server info query example
            using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = "";
            string Host_Name = "hostname ";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;
            Host_Name += metroTextBox4.Text;
            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send(Host_Name);

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            metroButton14.Enabled = false;
            metroButton13.Enabled = true;
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = "";
            string SV_Pass = "password ";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;
            SV_Pass += metroTextBox5.Text;
            // RCON query example
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send(SV_Pass);

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string output = "";
            // Server info query example
            /*using (Query q = new Query(metroTextBox2.Text, 7777))
            {
                q.Send('i');

                foreach (string str in q.Store(q.Recieve()))
                    output += str + '\n';
            }*/

            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send("password 0");

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://t.me/NimA_BastaniW");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string output = "";

            string FS_Name = "loadfs ";

            output += "Rcon :";
            FS_Name += metroTextBox6.Text;
            string Rcon_Pass = metroTextBox1.Text;
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send(FS_Name);

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string output = "";

            string FS_Name = "unloadfs ";

            output += "Rcon :";
            FS_Name += metroTextBox6.Text;
            string Rcon_Pass = metroTextBox1.Text;
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send(FS_Name);

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string output = "";

            string URL_Site = "weburl ";

            output += "Rcon :";
            URL_Site += metroTextBox7.Text;
            string Rcon_Pass = metroTextBox1.Text;
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send(URL_Site);

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            NpcCount ++;
            string npcc = NpcCount.ToString();
            metroTextBox8.Text = npcc;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (NpcCount != 0)
            {
                NpcCount--;
                string npcc = NpcCount.ToString();
                metroTextBox8.Text = npcc;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string output = "";

            string Max_NPCS = "maxnpc ";
            
            string npcc = NpcCount.ToString();

            output += "Rcon :";
            Max_NPCS += npcc;
            string Rcon_Pass = metroTextBox1.Text;
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                rq.Send(Max_NPCS);

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string output = "";
            output += "Rcon :";
            string Rcon_Pass = metroTextBox1.Text;
            using (RCONQuery rq = new RCONQuery(metroTextBox2.Text, 7777, Rcon_Pass, true))
            {
                if (metroCheckBox2.Checked) { rq.Send("db_logging 1"); } else { rq.Send("db_logging 0"); }
                if (metroCheckBox1.Checked) { rq.Send("logqueries 1"); } else { rq.Send("logqueries 0"); }
                if (metroCheckBox3.Checked) { rq.Send("db_log_queries 1"); } else { rq.Send("db_log_queries 0"); }
                if (metroCheckBox4.Checked) { rq.Send("chatlogging 1"); } else { rq.Send("chatlogging 0"); }
                if (metroCheckBox5.Checked) { rq.Send("timestamp 1"); } else { rq.Send("timestamp 0"); }

                foreach (string str in rq.Store(rq.Recieve()))
                    output += str + '\n';
            }
            richTextBox1.Text += output;
            richTextBox1.Text += "";

            MessageBox.Show(output);
        }

        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
                richTextBox1.Text = "";
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://t.me/NimA_BastaniW");
        }
    }
}
