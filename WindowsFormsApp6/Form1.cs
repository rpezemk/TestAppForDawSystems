using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        private List<Row> rows;
        private List<Header> headers;
        private HeaderFilter hfilter;
        private RowFilter rfilter;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Header h1 = new Header() { ID = 1, Numer = 1, Rok = 2019, Seria = "test", Kontrahent = "Gumisie Company we wodzie" };
            Header h2 = new Header() { ID = 2, Numer = 13, Rok = 2021, Seria = "test", Kontrahent = "Nerd Stream xD" };
            headers = new List<Header> { h1, h2 };
            dataGridView1.DataSource = headers;
            dataGridView1.Columns["PelnyNumer"].ReadOnly = true;


            Row p1 = new Row() { ID = 1, Numer = 1, Lp = 1, Towar = "Zapałki Czechowickie", Cena = 0.49M, Ilosc = 100 };
            Row p2 = new Row() { ID = 1, Numer = 1, Lp = 2, Towar = "Wódka Barmańska", Cena = 29M, Ilosc = 10 };
            Row p3 = new Row() { ID = 1, Numer = 1, Lp = 3, Towar = "Papierosy Route66", Cena = 13.55M, Ilosc = 5 };

            Row q1 = new Row() { ID = 2, Numer = 13, Lp = 1, Towar = "Korkociąg", Cena = 5M, Ilosc = 2 };
            Row q2 = new Row() { ID = 2, Numer = 13, Lp = 2, Towar = "Chateau de ...", Cena = 130M, Ilosc = 3 };

            rows = new List<Row> { p1, p2, p3, q1, q2 };
            dataGridView2.DataSource = rows;
            dataGridView2.Columns["ID"].Visible = false;
            dataGridView2.Columns["Numer"].Visible = false;
            dataGridView2.Columns["Rok"].Visible = false;
            dataGridView2.Columns["Seria"].Visible = false;

            rfilter = new RowFilter();
            hfilter = new HeaderFilter();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            Stream stream;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = saveFileDialog1.OpenFile()) != null)
                    {
                        IFormatter formatter = new BinaryFormatter();
                        List<object> list = new List<object>();
                        list.Add(headers);
                        list.Add(rows);
                        formatter.Serialize(stream, list);
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd, nie można zapisać pliku!: " + ex.Message);
                }

            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Stream stream = null;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
            openDialog.FilterIndex = 1;
            openDialog.RestoreDirectory = true;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((stream = openDialog.OpenFile()) != null)
                    {
                        using (stream)
                        {
                            IFormatter formatter = new BinaryFormatter();
                            List<object> objects = (List<object>)formatter.Deserialize(stream);
                            List<Header> hs = (List<Header>)objects[0];
                            List<Row> rs = (List<Row>)objects[1];

                            headers = hs;
                            rows = rs;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd, nie można odczytać pliku!" + ex.Message);
                }
            }
            refresh();
        }

        private void HeaderClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(dataGridView1.SelectedRows.Count == 0)
                {
                    dataGridView1.CurrentRow.Selected = true;
                }
                var H = dataGridView1.SelectedRows[0].DataBoundItem as Header;

                //dataGridView2.DataSource = rows.FindAll(r => (r.ID == H.ID) & (r.Numer == H.Numer));
                rfilter.ID = H.ID;
                rfilter.Numer = H.Numer;
                dataGridView2.DataSource = rfilter.View(rows);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message + " - " + ex.Source);
            }
        }

        private List<Header> ViewFiltered()
        {

            return new List<Header>();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            rfilter.EnableFilter = checkBox2.Checked;
            refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            rfilter.Towar = textBox1.Text;
            refresh();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if((textBox2.Text == "") | (textBox2.Text == null))
            {
                rfilter.Cena = 0;
            }
            else
            {
                rfilter.Cena = Convert.ToDecimal(textBox2.Text);
            }
            refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            hfilter.EnableFilter = checkBox1.Checked;
            refresh();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if ((textBox3.Text == "") | (textBox3.Text == null)) hfilter.Numer = 0;
            else hfilter.Numer = Convert.ToInt32(textBox3.Text);
            refresh();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if ((textBox5.Text == "") | (textBox5.Text == null)) hfilter.Rok = 0;
            else hfilter.Rok = Convert.ToInt32(textBox5.Text);
            refresh();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            hfilter.Kontrahent = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            hfilter.Seria = textBox7.Text;
            refresh();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if ((textBox4.Text == "") | (textBox4.Text == null)) hfilter.Data = DateTime.MinValue;
            else
            {
                try
                {
                    hfilter.Data = DateTime.Parse(textBox4.Text);
                }
                catch (Exception ex)
                {

                }
            }

            refresh();
        }

        private void HApprovedFilterBox_CheckedChanged(object sender, EventArgs e)
        {
            hfilter.Zatwierdzony = Convert.ToBoolean(HApprovedFilterBox.Checked);
            refresh();
        }

        private void refresh()
        {
            dataGridView1.DataSource = hfilter.View(headers);
            dataGridView2.DataSource = rfilter.View(rows);
        }

        private void ButtonNewHeader_Click(object sender, EventArgs e)
        {
            headers.Add(new Header());
            MessageBox.Show(headers.Count.ToString());
            refresh();
        }

        private void ButtonDelHeader_Click(object sender, EventArgs e)
        {
            try
            {
                headers.Remove(dataGridView1.SelectedRows[0].DataBoundItem as Header);
            }
            catch
            {
                MessageBox.Show("Zaznacz wiersz!");
            }
            refresh();
        }
    
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
