using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace hastacafe
{
    public partial class Form1 : Form
    {
        public static string id, pw;
        OleDbConnection conn = new OleDbConnection("provider=Microsoft.Jet.Oledb.4.0;data source=hastacafe.mdb");
        OleDbDataAdapter da = new OleDbDataAdapter();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT kadi,sifre FROM admin", conn);

            conn.Open();

            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                id = dr["kadi"].ToString();
                pw = dr["sifre"].ToString();
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == textBox1.Text && pw == textBox2.Text)
            {
                Form2 frm2 = new Form2();
                frm2.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
        }



    }
}
