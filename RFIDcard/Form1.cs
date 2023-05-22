using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RFIDcard
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Program Files (x86)\ASSA ABLOY\Vision\pmsif.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int PMSifRegister(string szLicense, string szApplName);

        [DllImport(@"C:\Program Files (x86)\ASSA ABLOY\Vision\pmsif.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern IntPtr PMSifReturnKcdLcl(out char ff, byte[] Dta, bool Dbg, string szOpID, string szOpFirst, string szOpLast);
        //public static extern IntPtr PMSifReturnKcdLcl(byte[] ff, byte[] Dta, bool Dbg, string szOpID, string szOpFirst, string szOpLast);
        public static extern IntPtr PMSifReturnKcdLcl(out byte ff, byte[] Dta, bool Dbg, string szOpID, string szOpFirst, string szOpLast);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = PMSifRegister(textBox1.Text, textBox2.Text);
            textBox3.Text = result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte Cmd;
            //Cmd = new byte[1];// { 0x49 };

            string Tmp;
            string TmpDta = "";

            //string dta;
            byte[] dta;
            //dta = (new char[301]).ToString();
            dta = new byte[301];

            char[] dta_tmp;
            dta_tmp = new char[301];

            int i;

            if(textBox3.Text != "0")
            {
                MessageBox.Show("Not registered", "Error");
                return;
            }

            Tmp = CmdBox.Text;
            Cmd = Convert.ToByte(Tmp[0]); //Encoding.ASCII.GetBytes(Tmp);

            //TmpDta = BuildDataFrame(Cmd[0]);
            BuildDataFrame(Cmd, out TmpDta);

            if (TmpDta.Length > 0)
            {
                for(i = 0; i < TmpDta.Length; i++)
                {
                    //dta.
                    dta[i] = Convert.ToByte(TmpDta[i]);
                    //dta_tmp[i]= TmpDta.ToCharArray()[i];

                }

                for (i = TmpDta.Length; i < 301; i++)
                {
                    dta[i] = 0;
                    //dta_tmp[i] = '0';

                }
            }
            else
            {
                for (i = 0; i < 301; i++)
                {
                    dta[i] = 0;
                    //dta_tmp[i] = '0';
                }
            }

            //dta = dta_tmp[].ToString();
            //dta = new string(dta_tmp);

            //MessageBox.Show(Convert.ToString(dta.Length), "Report");

            //char a = 'I';
            //byte a;// = 73;
            //a = Convert.ToString(Cmd[0]);
            //a = Cmd[0];
            //PMSifReturnKcdLcl(Cmd, dta, false, "7289", "Jason", "Phillips");
            PMSifReturnKcdLcl(out Cmd, dta, false, "7289", "Jason", "Phillips");

            //textBox4.Text = ((char)Cmd[0]).ToString();
            //textBox4.Text = Convert.ToString(a);
            //textBox4.Text = ((char)a).ToString();
            textBox4.Text = ((char)Cmd).ToString();

            richTextBox1.Clear();
            
            for(i = 0; i < 301; i++)
            {
                richTextBox1.Text += ((char)dta[i]).ToString();
            }

        }

        private void BuildDataFrame(byte Command, out string Data)
        {
            //string Data;

            char code = (char)30;

            if ( R2Box.Text == "")
            {
                Data = code.ToString() + "R" + RBox.Text;
            }
            else
            {
                if(RBox.Text == "")
                {
                    Data = code.ToString() + "L" + R2Box.Text;
                }
                else
                {
                    Data = code.ToString() + "L" + RBox.Text + "," + R2Box.Text;
                }
            }

            if(TBox.Text != "")
            {
                Data = Data + code.ToString() + "T" + TBox.Text;
            }
            
            if(FBox.Text != "")
            {
                Data = Data + code.ToString() + "F" + FBox.Text;
            }

            if (NBox.Text != "")
            {
                Data = Data + code.ToString() + "N" + NBox.Text;
            }

            if (NBox.Text != "")
            {
                Data = Data + code.ToString() + "N" + NBox.Text;
            }

            if(textCardPMSId.Text != "")
            {
                Data = Data + code.ToString() + "P" + textCardPMSId.Text;
            }

            if(Command == 0x43)
            {
                if(RBox_XX.Text != "")
                {
                    Data = Data + code.ToString() + "R" + RBox_XX.Text;
                }

                if (TBox_XX.Text != "")
                {
                    Data = Data + code.ToString() + "T" + TBox_XX.Text;
                }

                if (FBox_XX.Text != "")
                {
                    Data = Data + code.ToString() + "F" + FBox_XX.Text;
                }

                if (NBox_XX.Text != "")
                {
                    Data = Data + code.ToString() + "N" + NBox_XX.Text;
                }
            }

            if (UBox.Text != "")
            {
                Data = Data + code.ToString() + "U" + UBox.Text;
            }

            if (StartBox.Text != "")
            {
                Data = Data + code.ToString() + "D" + StartBox.Text;
            }

            if(EndBox.Text != "")
            {
                Data = Data + code.ToString() + "O" + EndBox.Text;
            }

            if(textAddInfo.Text != "")
            {
                Data = Data + code.ToString() + textAddInfo.Text;
            }

            Data = Data + code.ToString() + "J1" + code.ToString() + "S048FCB924E6880" + code.ToString() + "VEB0D8F1F";

            //return Data;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
