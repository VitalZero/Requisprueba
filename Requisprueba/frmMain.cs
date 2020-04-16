﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Requisprueba
{
    public partial class FrmMain : Form
    {
        private XmlDocument doc;

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

        public void AddRecord(Record record)
        {
            ListViewItem lvItem = new ListViewItem();

            lvItem.Text = record.NumRequi.ToString();
            lvItem.SubItems.Add(record.FechaElaboracion);
            lvItem.SubItems.Add(record.FechaSolicitud);
            lvItem.SubItems.Add(record.FechaAutorizacion);
            lvItem.SubItems.Add(record.Monto.ToString());
            lvItem.SubItems.Add(record.Notas);

            listView1.Items.Add(lvItem);

            //XmlWriter xmlWriter = XmlWriter.Create("datos.xml");
            //xmlWriter.WriteStartDocument();
            //xmlWriter.WriteStartElement("requisiciones");
            //xmlWriter.WriteStartElement("requisicion");
            //xmlWriter.WriteAttributeString("numero", "22315");
            //xmlWriter.WriteEndElement();
            //xmlWriter.WriteEndElement();
            //xmlWriter.WriteEndDocument();
            //xmlWriter.Close();
            XmlNode rootNode = doc.GetElementsByTagName("requisiciones")[0];

            XmlNode childNode = doc.CreateElement("requisicion");
            XmlAttribute attribute = doc.CreateAttribute("num");
            attribute.Value = record.NumRequi.ToString();
            childNode.Attributes.Append(attribute);

            XmlNode dataNode = doc.CreateElement("elaboracion");
            dataNode.InnerText = record.FechaElaboracion;
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("solicitud");
            dataNode.InnerText = record.FechaSolicitud;
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("autorización");
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("monto");
            dataNode.InnerText = record.Monto.ToString();
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("notas");
            dataNode.InnerText = record.Notas;
            childNode.AppendChild(dataNode);

            rootNode.AppendChild(childNode);

            doc.Save("datos.xml");
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
            doc = new XmlDocument();

            if (File.Exists("datos.xml"))
            {
                try
                {
                    doc.Load("datos.xml");

                    XmlNodeList nodeList = doc.GetElementsByTagName("requisicion");

                    foreach (XmlNode node in nodeList)
                    {
                        ListViewItem lvItem = new ListViewItem();

                        lvItem.Text = node.Attributes["num"].Value;
                        lvItem.SubItems.Add(node.ChildNodes[0].InnerText);
                        lvItem.SubItems.Add(node.ChildNodes[1].InnerText);

                        listView1.Items.Add(lvItem);
                    }
                }
                catch (XmlException ex)
                {
                }
            }
            else
            {
                XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                doc.AppendChild(decl);

                XmlNode rootNode = doc.CreateElement("requisiciones");
                doc.AppendChild(rootNode);
                doc.Save("datos.xml");
            }
            
            //XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");

            //while (xmlReader.Read())
            //{
            //    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
            //    {
            //        if (xmlReader.HasAttributes && xmlReader.GetAttribute("time") == null)
            //        {
            //            ListViewItem lvItem = new ListViewItem();
            //            lvItem.Text = xmlReader.GetAttribute("currency");
            //            lvItem.SubItems.Add(xmlReader.GetAttribute("rate"));
            //            listView1.Items.Add(lvItem);
            //        }
            //    }
            //}
        }
    }
}
