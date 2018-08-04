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
    public partial class Form2 : Form
    {
        OleDbConnection conn = new OleDbConnection("provider=Microsoft.Jet.Oledb.4.0;data source=hastacafe.mdb");
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataSet ds = new DataSet();
      
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            da.InsertCommand = new OleDbCommand("INSERT INTO ürünekle(ürün_adı,fiyat,adet) VALUES(@ürün_adı,@fiyat,@adet)", conn);
            da.InsertCommand.Parameters.Add("@ürün_adı", OleDbType.Char).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add("@fiyat", OleDbType.Integer).Value = textBox2.Text;
            da.InsertCommand.Parameters.Add("@adet", OleDbType.Integer).Value = textBox3.Text;

            conn.Open();

            da.InsertCommand.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ürün Eklendi");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            da.SelectCommand = new OleDbCommand("Select * From ürünekle",conn);
            ds.Clear();
            da.Fill(ds);
            dg.DataSource = ds.Tables[0];

        }

        private void button3_Click(object sender, EventArgs e)
        {

            OleDbCommand cmd = new OleDbCommand("SELECT ürün_adı,fiyat,adet FROM ürünekle WHERE ürün_adı=@ürün_adı", conn);
            cmd.Parameters.Add("@ürün_adı", OleDbType.Char).Value = textBox4.Text;

            conn.Open();

            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read()) 
            {
                textBox7.Text = dr["ürün_adı"].ToString();
                textBox6.Text = dr["fiyat"].ToString();
                textBox5.Text = dr["adet"].ToString();

            }

            conn.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x;

            da.UpdateCommand = new OleDbCommand("UPDATE ürünekle SET ürün_adı=@yürün_adı,fiyat=@fiyat,adet=@adet WHERE ürün_adı=@ürün_adı", conn);

            da.UpdateCommand.Parameters.Add("@yürün_adı", OleDbType.Char).Value = textBox7.Text;
            da.UpdateCommand.Parameters.Add("@fiyat", OleDbType.Char).Value = textBox6.Text;
            da.UpdateCommand.Parameters.Add("@adet", OleDbType.Char).Value = textBox5.Text;
            da.UpdateCommand.Parameters.Add("@ürün_adı", OleDbType.Char).Value = textBox4.Text;
            conn.Open();

            x = da.UpdateCommand.ExecuteNonQuery();


            conn.Close();
            if (x >= 1)
                MessageBox.Show("Düzenleme  işlemi tamamlanmıştır");

            textBox7.Clear();
            textBox6.Clear();
            textBox5.Clear();
            textBox4.Clear();
        

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();

            OleDbCommand cmd1 = new OleDbCommand("SELECT ürün_adı FROM ürünekle",conn);

            conn.Open();
            OleDbDataReader dr1 = cmd1.ExecuteReader();

            while (dr1.Read())
            {

                listBox1.Items.Add(dr1["ürün_adı"].ToString());

            }

            conn.Close();

            OleDbCommand cmd2 = new OleDbCommand("SELECT tarih FROM hesap", conn);

            conn.Open();
            OleDbDataReader dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {

                listBox2.Items.Add(dr2["tarih"].ToString());

            }

            conn.Close();
           

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            OleDbCommand cmd1 = new OleDbCommand("SELECT fiyat,adet FROM ürünekle WHERE ürün_adı=@ürün_adı", conn);
            cmd1.Parameters.Add("@ürün_adı", OleDbType.Char).Value = listBox1.SelectedItem.ToString();
            
            conn.Open();
            OleDbDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {

                label15.Text = dr1["fiyat"].ToString();
                label16.Text = dr1["adet"].ToString();

            }

            conn.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int x=1;
            int a=int.Parse(label16.Text);
            int b = int.Parse(textBox8.Text);
            int c = a - b;
            int tutar;
            int t = int.Parse(label15.Text);
            tutar = t * b;


            da.InsertCommand = new OleDbCommand("INSERT INTO hesap(saat,tarih,ürün,tutar,satılanadet) VALUES (@saat,@tarih,@ürün,@tutar,@satılanadet)", conn);
            da.InsertCommand.Parameters.Add("@saat", OleDbType.Char).Value = label18.Text;
            da.InsertCommand.Parameters.Add("@tarih", OleDbType.Char).Value = label17.Text;
            da.InsertCommand.Parameters.Add("@ürün", OleDbType.Char).Value = listBox1.SelectedItem.ToString();
            da.InsertCommand.Parameters.Add("@tutar", OleDbType.Integer).Value = tutar;
            da.InsertCommand.Parameters.Add("@satılanadet", OleDbType.Integer).Value = textBox8.Text;

            conn.Open();

            da.InsertCommand.ExecuteNonQuery();
            conn.Close();


            da.UpdateCommand = new OleDbCommand("UPDATE ürünekle SET adet=@adet WHERE ürün_adı=@ürün_adı", conn);
            da.UpdateCommand.Parameters.Add("@adet", OleDbType.Char).Value = c;
            da.UpdateCommand.Parameters.Add("@ürün_adı", OleDbType.Char).Value =listBox1.SelectedItem.ToString();

            conn.Open();

            x=da.UpdateCommand.ExecuteNonQuery();
            if (x >= 1)
                MessageBox.Show("Satış işlemi başarı ile gerçekleşti\n Kalan Adet:  "+c+" Tutar  "+tutar+" TL");
            else
                MessageBox.Show("HATA");
            conn.Close();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                label17.Text = DateTime.Now.ToShortDateString();
                label18.Text = DateTime.Now.ToLongTimeString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new OleDbCommand("Select saat,ürün,tutar,satılanadet From hesap Where tarih=@tarih",conn);
            da.SelectCommand.Parameters.Add("@tarih",OleDbType.Char).Value=listBox2.SelectedItem.ToString();

            conn.Open();

            ds.Clear();
            da.SelectCommand.ExecuteNonQuery();
            da.Fill(ds, "hesap");
            dg2.DataSource = ds.Tables["hesap"];

            conn.Close();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OleDbCommand cmdx = new OleDbCommand("Select tutar From hesap Where tarih=@tarih", conn);
            cmdx.Parameters.Add("@tarih", OleDbType.Char).Value = listBox2.SelectedItem.ToString();
            
            conn.Open();
            
            OleDbDataReader drx = cmdx.ExecuteReader();

            while (drx.Read())
            {

                listBox3.Items.Add(drx["tutar"].ToString());

            }
            conn.Close();


          
            int toplam = 0;

            for (int k = 0; k < listBox3.Items.Count; k++)
            {
                toplam += Convert.ToInt32(listBox3.Items[k].ToString());
            } 

            MessageBox.Show("Seçilen Tarihte ki Hasılat   " + toplam + "   TL");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu özellik şu an kullanılabilir durumda değil");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu özellik şu an kullanılabilir durumda değil");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu özellik şu an kullanılabilir durumda değil");
        }

    }
}
