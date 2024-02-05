using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace carpark
{
    public partial class Form1 : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (textPassword.Text == null | textEmail.Text == null)
                {

                    {
                        MessageBox.Show("Email or Password not valid.....! Try again");
                    }

                }

                else
                {
                    var item = db.tableAccounts.Where(s => s.Password == textPassword.Text & s.Email == textEmail.Text).FirstOrDefault();
                    if (item != null)
                    {
                        Welcomescreen wc = new Welcomescreen();
                        wc.Show();
                        this.Hide();
                    }
                    else
                    {
                        {
                            MessageBox.Show("your enter account information not exists....first create an account or try again");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            }
        
        }
    }


