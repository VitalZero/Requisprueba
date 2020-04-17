using System;
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
            lvItem.SubItems.Add(string.Format("{0:c}", record.Monto));
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

            XmlNode childNode = doc.CreateElement("requi");
            XmlAttribute attribute = doc.CreateAttribute("num");
            attribute.Value = record.NumRequi.ToString();
            childNode.Attributes.Append(attribute);

            XmlNode dataNode = doc.CreateElement("elab");
            attribute = doc.CreateAttribute("fecha");
            attribute.Value = record.FechaElaboracion;
            dataNode.Attributes.Append(attribute);
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("sol");
            attribute = doc.CreateAttribute("fecha");
            attribute.Value = record.FechaSolicitud;
            dataNode.Attributes.Append(attribute);
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("aut");
            attribute = doc.CreateAttribute("val");
            attribute.Value = "0";
            dataNode.Attributes.Append(attribute);
            attribute = doc.CreateAttribute("fecha");
            attribute.Value = "";
            dataNode.Attributes.Append(attribute);
            childNode.AppendChild(dataNode);

            dataNode = doc.CreateElement("monto");
            dataNode.InnerText = record.Monto.ToString();
            attribute = doc.CreateAttribute("iva");
            attribute.Value = record.Iva.ToString();
            dataNode.Attributes.Append(attribute);
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
                string today = DateTime.Today.ToString("d");

                XmlNodeList nodeList = doc.GetElementsByTagName("requi");

                foreach(XmlNode node in nodeList)
                {
                    if(node.Attributes["num"].Value == listView1.SelectedItems[i].SubItems[0].Text)
                    {
                        node.ChildNodes[2].Attributes["val"].Value = "1";
                        node.ChildNodes[2].Attributes["fecha"].Value = today;
                        listView1.SelectedItems[i].SubItems[3].Text = today;
                        break;
                    }
                }
            }
            doc.Save("datos.xml");
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            doc = new XmlDocument();

            if (File.Exists("datos.xml"))
            {
                try
                {
                    doc.Load("datos.xml");

                    XmlNodeList nodeList = doc.GetElementsByTagName("requi");

                    foreach (XmlNode node in nodeList)
                    {
                        ListViewItem lvItem = new ListViewItem();

                        lvItem.Text = node.Attributes["num"].Value;
                        lvItem.SubItems.Add(node.ChildNodes[0].Attributes["fecha"].Value);
                        lvItem.SubItems.Add(node.ChildNodes[1].Attributes["fecha"].Value);
                        lvItem.SubItems.Add(node.ChildNodes[2].Attributes["fecha"].Value);

                        string formatted = string.Format("{0:c}", Convert.ToDouble(node.ChildNodes[3].InnerText));
                        lvItem.SubItems.Add(formatted);
                        lvItem.SubItems.Add(node.ChildNodes[4].InnerText);

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

        private void Salir_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
