using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DGVPrinterHelper;

namespace carpark
{
    public partial class slotsForm : Form
    {
        DataClasses1DataContext db=new DataClasses1DataContext();
        public slotsForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Welcomescreen s=new Welcomescreen();
            s.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textsearch.Text=="")
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
              if(textsearch.Text!=null)
                {
                    string sk=textsearch.Text;
                    var chk =db.tblSlots.Where(o=>o.Slot_No==sk || o.Location==sk).ToList();
                    if(chk!=null)
                    {
                        dataGridView1.DataSource = chk;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void reset()
        {
            textsno.Text = "";
            textlocation.Text = "";
            labelid.Text = "";
        }
        public void load()
        {
            var lod = db.tblSlots.ToList();
            dataGridView1.DataSource = lod;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { 
                if(textsno.Text!=null & textlocation.Text!=null)
                {
                    string sno= textsno.Text;
                    var chk=db.tblSlots.Where(o=>o.Slot_No == sno).FirstOrDefault();
                    if (chk == null)
                    {


                        tblSlot s = new tblSlot();
                        s.Slot_No = textsno.Text;
                        s.Location = textlocation.Text;
                        db.tblSlots.InsertOnSubmit(s);
                        db.SubmitChanges();
                        MessageBox.Show("Data inserted");
                        reset();
                        load();
                    }
                    else
                    {
                        MessageBox.Show("with this name slot already added...");
                    }
                 }
                else
                {
                    MessageBox.Show("slot no or location box empty...try again");
                }
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message, "Error");
            }
        }

        private void slotsForm_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ir = e.RowIndex;
            labelid.Text = dataGridView1.Rows[ir].Cells[0].Value.ToString();
            textsno.Text = dataGridView1.Rows[ir].Cells[1].Value.ToString();
            textlocation.Text = dataGridView1.Rows[ir].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelid.Text!=null & textsno.Text != null & textlocation.Text != null)
                {


                    if (MessageBox.Show("Do you want to edit record?...", "edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        string sno = textsno.Text;
                        var chk = db.tblSlots.Where(o => o.Slot_No == sno).FirstOrDefault();
                        if (chk == null)
                        {
                            int st = Convert.ToInt32(labelid.Text);
                            var s= db.tblSlots.Where(o=>o.ID==st).FirstOrDefault();

                            s.Slot_No = textsno.Text;
                            s.Location = textlocation.Text;

                            db.SubmitChanges();
                            
                            MessageBox.Show("Data updated");
                            reset();
                            load();
                        }

                        else
                        {
                            MessageBox.Show("with this name slot already added...");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("slot no or location box empty...try again");
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
                            var s = db.tblSlots.Where(o => o.ID == st).FirstOrDefault();

                        db.tblSlots.DeleteOnSubmit(s);

                            db.SubmitChanges();

                            MessageBox.Show("Data Deleted");
                            reset();
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
            P.Title = "Total Cars capacity report";
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

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelid_Click(object sender, EventArgs e)
        {

        }

        private void textlocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textsno_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }
    }
}
