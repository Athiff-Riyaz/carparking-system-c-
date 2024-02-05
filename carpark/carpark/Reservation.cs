using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace carpark
{
    public partial class Reservation : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Reservation()
        {
            InitializeComponent();
        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            var ld = db.tblDepartures.ToList();
            dataGridView1.DataSource = ld;
            display();

        }

        public void display()
        {
            int sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);

            }
            lblamount.Text = sum.ToString();

            var slot = db.tblSlots.Count();
            labelcp.Text = slot.ToString();
            var pca = db.tblArrivals.Count();
            labelarrive.Text = pca.ToString();

            var pca1 = db.tblDepartures.Count();
            lbltotald.Text = pca1.ToString();

        }

        private void lbldtime_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            var ld = db.tblDepartures.ToList();
            dataGridView1.DataSource = ld;
        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {


            try
            {
                if (textsearch.Text == "")
                {

                    load();
                }
                else { 

                    var chk = db.tblDepartures.Where(s => s.Car_No == textsearch.Text || s.Driver == textsearch.Text).ToList();
               
                
                    if (chk != null)
                    {
                        dataGridView1.DataSource = chk;
                        display();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void load()
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DGVPrinter P = new DGVPrinter();
            P.printDocument = printDocument1;
            P.Title = "Whole report";
            P.SubTitle = String.Format("Date:{0}", DateTime.Now);
            P.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            P.printDocument = printDocument1;
            P.PageNumberInHeader = true;
            P.PorportionalColumns = true;
            P.HeaderCellAlignment = StringAlignment.Near;
            P.Footer = "Car parking system";
            P.FooterSpacing = 15;
            P.PrintDataGridView(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Invoice i= new Invoice();
            i.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Welcomescreen w = new Welcomescreen();
            w.Show();
            this.Hide();
        }
    }

    }

