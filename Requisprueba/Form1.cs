using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Requisprueba
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar forma = new frmAgregar();
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

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            frmAgregar forma = new frmAgregar();
            forma.SetLvItems(listView1.SelectedItems[0]);
            forma.ShowDialog(this);
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            int c = listView1.SelectedItems.Count;

            if (c == 0)
            {
                MessageBox.Show("Selecciona una o mas requisiciones", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < c; i++)
            {
                listView1.SelectedItems[i].SubItems[3].Text = "01/01/2020";
            }
        }
    }
}
