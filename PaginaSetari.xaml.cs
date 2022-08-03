using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace AnalyzerCollectData
{
    /// <summary>
    /// Interaction logic for PaginaSetari.xaml
    /// </summary>
    public partial class PaginaSetari : Window
    {
        public PaginaSetari()
        {
            InitializeComponent();
            Loaded += Load;
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            CollectXMLData();
        }

        private void buton_testConexiune_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "";
            try
            {
                if(checkBox_DatabaseIntegratedSecurity.IsChecked == true)
                {
                    connectionString = "Data Source=" + textBox_DatabaseServer.Text + ";Initial Catalog=" + textBox_DatabaseName.Text + ";Integrated Security=True";
                }
                else
                {
                    connectionString = "Data Source =" + textBox_DatabaseServer.Text + "; Initial Catalog =" + textBox_DatabaseName.Text + ";user ID=" + textBox_DatabaseUsername.Text + ";Password =" + textBox_DatabasePassword.Password + ";";
                }

                SqlConnection sql = new SqlConnection(connectionString);
                sql.Open();

                MessageBox.Show("Baza de date deschisa cu success!");
            }
            catch
            {
                MessageBox.Show("Eroare la deschiderea bazei de date!");
            }
        }

        private void CollectXMLData()
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");


            //Selectare TIP Server
            if (root.Attributes[0].Value == "Microsoft SQL Server")
                combobox_tip.SelectedIndex = 0;
            else
                combobox_tip.SelectedIndex = 1;

            //Selectare Integrated Security
            if (root.Attributes[1].Value == "true")
            {
                textBox_DatabasePassword.IsEnabled = false;
                textBox_DatabaseUsername.IsEnabled = false;
                checkBox_DatabaseIntegratedSecurity.IsChecked = true;
            }
            else
            {
                textBox_DatabasePassword.IsEnabled = true;
                textBox_DatabaseUsername.IsEnabled = true;
                checkBox_DatabaseIntegratedSecurity.IsChecked = false;  
            }

            XmlNode server = root.SelectSingleNode("server");
            textBox_DatabaseServer.Text = server.InnerText;
            XmlNode dbname = root.SelectSingleNode("dbName");
            textBox_DatabaseName.Text = dbname.InnerText;
            XmlNode username = root.SelectSingleNode("username");
            textBox_DatabaseUsername.Text = username.InnerText;
            XmlNode password = root.SelectSingleNode("password");
            textBox_DatabasePassword.Password = password.InnerText;
        }

        private void textBox_DatabaseServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataChangeTextbox("server", textBox_DatabaseServer);
        }

        private void textBox_DatabaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataChangeTextbox("dbName", textBox_DatabaseName);
        }
        private void textBox_DatabaseUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataChangeTextbox("username", textBox_DatabaseUsername);
        }


        //PASSWORDBOX
        private void textBox_DatabasePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");
            XmlNode password = root.SelectSingleNode("password");
            password.InnerText = textBox_DatabasePassword.Password;

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }

        private void combobox_tip_DropDownClosed(object sender, EventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");
            root.Attributes[0].Value = combobox_tip.Text;

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }

        //CHECKBOX
        private void checkBox_DatabaseIntegratedSecurity_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");
            if (checkBox_DatabaseIntegratedSecurity.IsChecked == true)
            {
                textBox_DatabasePassword.IsEnabled = false;
                textBox_DatabaseUsername.IsEnabled = false;
                root.Attributes[1].Value = "true";
            }
            else
            {
                textBox_DatabasePassword.IsEnabled = true;
                textBox_DatabaseUsername.IsEnabled = true;
                root.Attributes[1].Value = "false";
            }

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }

        private void dataChangeTextbox(string xmlNodeName, TextBox textbox)
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");

            XmlNode nodeName = root.SelectSingleNode(xmlNodeName);
            nodeName.InnerText = textbox.Text;

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }
    }
}
