using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace carpark
{
    public partial class Arrival : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Arrival()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Welcomescreen w=new Welcomescreen();
            w.Show();
            this.Hide();
        }
        public void load()
        {
            var ld=db.tblArrivals.ToList();
            dataGridView1.DataSource= ld;
            labelid.Text = "";
            textdriver.Text = "";
            textcar.Text = "";
            textstime.Text = "";
            checkedListBox1.Text = "";

            var total= db.tblArrivals.Count();
            lbltotal.Text= total.ToString();
        }
        private void Arrival_Load(object sender, EventArgs e)
        {
            load();
            comboBox1.DataSource=db.tblSlots.ToList();
            comboBox1.ValueMember = "Slot_No";
            comboBox1.DisplayMember = "Slot_No";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textdriver.Text != null & textcar.Text != null & textstime.Text!=null & checkedListBox1.Text!=null & comboBox1.Text!=null )
                {
                    string sno = textcar.Text;
                    var chk = db.tblArrivals.Where(o => o.Car_No == sno).FirstOrDefault();
                    if (chk == null)
                    {


                        tblArrival s = new tblArrival();
                        s.Driver_Name = textdriver.Text;
                        s.Car_No = textcar.Text;
                        s.Category = checkedListBox1.Text;
                        s.Stay_Time = textstime.Text;
                        s.Selected_Slot=comboBox1.Text;
                        s.A_Time = DateTime.Now.ToString();

                        

                        db.tblArrivals.InsertOnSubmit(s);
                        db.SubmitChanges();
                        MessageBox.Show("Data parked succesfully");
                        
                        load();
                    }
                    else
                    {
                        MessageBox.Show("with this number car already parked...");
                    }
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ir = e.RowIndex;
            labelid.Text = dataGridView1.Rows[ir].Cells[0].Value.ToString();
            textdriver.Text = dataGridView1.Rows[ir].Cells[1].Value.ToString();
            textcar.Text = dataGridView1.Rows[ir].Cells[2].Value.ToString();
            //textcarno.Text = dataGridView1.Rows[ir].Cells[2].Value.ToString();
            textstime.Text = dataGridView1.Rows[ir].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[ir].Cells[4].Value.ToString();
            checkedListBox1.Text = dataGridView1.Rows[ir].Cells[5].Value.ToString();
            lblarrivaltm.Text = dataGridView1.Rows[ir].Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelid.Text!=null & textdriver.Text != null & textcar.Text != null & textstime.Text != null & checkedListBox1.Text != null & comboBox1.Text != null)
                {


                    if (MessageBox.Show("Do you want to edit record?...", "edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string sno = textcar.Text;
                        var chk = db.tblArrivals.Where(o => o.Car_No == sno).FirstOrDefault();
                        if (chk == null)
                        {
                            int st = Convert.ToInt32(labelid.Text);
                            var s = db.tblArrivals.Where(o => o.ID == st).FirstOrDefault();
                            
                            s.Driver_Name = textdriver.Text;
                            s.Car_No = textcar.Text;
                            s.Category = checkedListBox1.Text;
                            s.Stay_Time = textstime.Text;
                            s.Selected_Slot = comboBox1.Text;
                            s.A_Time = DateTime.Now.ToString();

                            db.SubmitChanges();

                            MessageBox.Show("Data updated");
                            
                            load();
                        }

                        else
                        {
                            MessageBox.Show("with this name slot already parked...");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("record not selected");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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

                        int st = Convert.ToInt32(labelid.Text);
                        var s = db.tblArrivals.Where(o => o.ID == st).FirstOrDefault();

                        db.tblArrivals.DeleteOnSubmit(s);

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
            P.Title = "Arrival report";
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
            if (textsearch.Text == "")
            {
                load();
            }
            else
            {
                searchdata();
            }
        }
        public void searchdata()
        {

            try
            {
                if (textsearch.Text != null)
                {
                    string sk = textsearch.Text;
                    var chk = db.tblArrivals.Where(o => o.Driver_Name == sk || o.Car_No == sk || o.Category==sk ).ToList();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblarrivaltm_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lbltotal_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lblcarno_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void labelid_Click(object sender, EventArgs e)
        {

        }

        private void textcar_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textdriver_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
