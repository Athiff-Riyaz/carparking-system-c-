using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace carpark
{
    public partial class Departure : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Departure()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Welcomescreen w = new Welcomescreen();
            w.Show();
            this.Hide();
        }

        private void Departure_Load(object sender, EventArgs e)
        {
            try
            {
                var dblod = db.tblDepartures.ToList();
                dataGridView1.DataSource = dblod;
                comboBoxcarno.DataSource = db.tblArrivals.ToList();
                comboBoxcarno.ValueMember = "Car_No";
                comboBoxcarno.DisplayMember = "Car_No";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxcarno.Text != null & labeldname.Text != null & labelptime.Text != null & labelptime.Text != null)
                {



                    tblDeparture s = new tblDeparture();
                    s.Car_No = comboBoxcarno.Text;
                    s.Driver = labeldname.Text;
                    s.Type = labelptype.Text;
                    s.P_Time = labelptime.Text;

                    decimal str = Convert.ToDecimal(labelptime.Text);
                    decimal amt = Convert.ToDecimal(textpamount.Text);
                    decimal amttotal = str * amt;

                    s.Amount = amttotal;
                    s.Departure_Time = str;



                    db.tblDepartures.InsertOnSubmit(s);
                    db.SubmitChanges();
                    MessageBox.Show("Departured succesfully");

                    load();
                }


                else
                {
                    MessageBox.Show("Box empty...try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void load()
        {
            var dblod = db.tblDepartures.ToList();
            dataGridView1.DataSource = dblod;
            comboBoxcarno.DataSource = db.tblArrivals.ToList();
            comboBoxcarno.ValueMember = "Car_No";
            comboBoxcarno.DisplayMember = "Car_No";
        }

        private void comboBoxcarno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                tblArrival obj = comboBoxcarno.SelectedItem as tblArrival;
                if (obj != null)
                {
                    labeldname.Text = obj.Driver_Name.ToString();
                    labelptype.Text = obj.Category.ToString();
                    labelptime.Text = obj.Stay_Time.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int indexrow = e.RowIndex;
            labelid1.Text = dataGridView1.Rows[indexrow].Cells[0].Value.ToString();
            lbldtime.Text = dataGridView1.Rows[indexrow].Cells[6].Value.ToString();
            lblpfee.Text = dataGridView1.Rows[indexrow].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelid.Text != null & labeldname.Text != null & labelptime.Text != null & labelptime.Text != null)
                {


                    if (MessageBox.Show("Do you want to edit record?...", "edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {

                        int st = Convert.ToInt32(labelid1.Text);
                        var s = db.tblDepartures.Where(o => o.ID == st).FirstOrDefault();

                        s.Car_No = comboBoxcarno.Text;
                        s.Driver = labeldname.Text;
                        s.Type = labelptype.Text;
                        s.P_Time = labelptime.Text;

                        decimal str = Convert.ToDecimal(labelptime.Text);
                        decimal amt = Convert.ToDecimal(textpamount.Text);
                        decimal amttotal = str * amt;

                        s.Amount = amttotal;
                        s.Departure_Time = str;

                        db.SubmitChanges();

                        MessageBox.Show("Data updated");

                        load();



                    }
                }
                else
                {
                    MessageBox.Show("record not selected");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Amount not entered");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelid.Text != null)
                {


                    if (MessageBox.Show("Do you want to delete record?...", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {

                        int st = Convert.ToInt32(labelid1.Text);
                        var s = db.tblDepartures.Where(o => o.ID == st).FirstOrDefault();

                        db.tblDepartures.DeleteOnSubmit(s);

                        db.SubmitChanges();

                        MessageBox.Show("Data Deleted");

                        load();

                    }
                }
                else
                {
                    MessageBox.Show("record not selected...! please select and continue.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DGVPrinter P = new DGVPrinter();
            P.printDocument = printDocument1;
            P.Title = "Departure report";
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

        private void textsearch_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (textsearch.Text != null)
                {
                    load();
                }
                else
                {



                    var chk = db.tblDepartures.Where( s=> s.Car_No==textsearch.Text|| s.Driver == textsearch.Text).ToList();
                    if (chk != null)
                    {
                        dataGridView1.DataSource = chk;
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

    
