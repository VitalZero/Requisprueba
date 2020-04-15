﻿using System;
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
    public partial class frmAgregar : Form
    {
        private int lvIndex;
        private bool isModifying = false;
        private ListViewItem lvTmpItem;

        public frmAgregar()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!isModifying)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = txtRequi.Text;
                lvItem.SubItems.Add(dtElaboracion.Text);
                lvItem.SubItems.Add(dtSolicitud.Text);
                lvItem.SubItems.Add("");
                lvItem.SubItems.Add(txtMonto.Text);
                lvItem.SubItems.Add(txtNotas.Text);
                ((frmMain)this.Owner).AddLvItem(lvItem);
            }
            else
            {
                ((frmMain)this.Owner).EditLvItem(lvTmpItem, lvIndex);
            }

            this.Close();
        }

        public void SetLvItems(ListViewItem lvItem)
        {
            this.Text = "Modificar Requisición";
            isModifying = true;
            lvIndex = lvItem.Index;
            lvTmpItem = lvItem;
            txtRequi.Text = lvTmpItem.SubItems[0].Text;
        }
    }
}