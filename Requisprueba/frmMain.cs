using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace Requisprueba
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregar forma = new FrmAgregar();
            forma.ShowDialog(this);
        }

        public void AddLvItem(ListViewItem lvItem)
        {
            listView1.Items.Add(lvItem);
        }

        public void EditLvItem(ListViewItem lvItem, int index)
        {
            listView1.Items[index].SubItems[5].Text = "Hola mundo!";
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecciona una requisición para modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FrmAgregar forma = new FrmAgregar();
            forma.SetLvItems(listView1.SelectedItems[0]);
            forma.ShowDialog(this);
        }

        private void BtnAutorizar_Click(object sender, EventArgs e)
        {
            int c = listView1.SelectedItems.Count;

            if (c == 0)
            {
                MessageBox.Show("Selecciona una o mas requisiciones", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < c; i++)
            {
                String fechaAut = listView1.SelectedItems[i].SubItems[3].Text;

                if ( fechaAut != "")
                {
                    MessageBox.Show("La requisición " + listView1.SelectedItems[i].SubItems[0].Text +
                        ", ya fue autorizada el " + fechaAut, "Error");

                    continue;
                }

                listView1.SelectedItems[i].SubItems[3].Text = "01/01/2020";
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");

            while(xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
                {
                    if(xmlReader.HasAttributes && xmlReader.GetAttribute("time") == null)
                    {
                        ListViewItem lvItem = new ListViewItem();
                        lvItem.Text = xmlReader.GetAttribute("currency");
                        lvItem.SubItems.Add(xmlReader.GetAttribute("rate"));
                        listView1.Items.Add(lvItem);
                    }
                }
            }
        }
    }
}
