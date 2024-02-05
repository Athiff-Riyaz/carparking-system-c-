using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carpark
{
    public partial class Invoice : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Invoice()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            comboBoxcarno.DataSource = db.tblDepartures.ToList();
            comboBoxcarno.ValueMember = "Car_No";
            comboBoxcarno.DisplayMember = "Car_No";
        }

        private void comboBoxcarno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                tblDeparture obj = comboBoxcarno.SelectedItem as tblDeparture;
                if (obj != null)
                {
                    labeldname.Text = obj.Driver.ToString();
                    labeltype.Text = obj.Type.ToString();
                    labelentrytime.Text = obj.P_Time.ToString();
                    labelamount.Text = obj.Amount.ToString();
                    labelcarno.Text = obj.Car_No.ToString();
                    labeldtime.Text = obj.Departure_Time.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        Bitmap bitmap;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Graphics myg = this.CreateGraphics();
                bitmap = new Bitmap(this.Size.Width, this.Size.Height, myg);
                Graphics mg = Graphics.FromImage(bitmap);
                mg.CopyFromScreen(this.Location.X, Location.Y, 0, 0, this.Size);
                printPreviewDialog1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Welcomescreen w = new Welcomescreen();
            w.Show();
            this.Hide();
        }
    }
}
