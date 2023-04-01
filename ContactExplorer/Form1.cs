using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace ContactExplorer
{
    public partial class Form1 : Form
    {
        DataTable dt;
        SqlDataAdapter adapter;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetData("select * from Contact");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData($"select * from Contact where Name = '{textBox1.Text}'");
        }

        public void GetData(string sql)
        {
            using (DataBase db = new DataBase())
            {
                dt = new DataTable();
                adapter = new SqlDataAdapter(sql, db.Connection);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetData("select * from Contact");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();
                adapter.Update(dt);
            }
            catch 
            {
                MessageBox.Show("Что-то пошло не так, повторите попытку еще раз");
            }
        }
    }
}
