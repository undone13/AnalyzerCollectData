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
            try
            {

                string connectionString = "Data Source=" + textBox_DatabaseServer.Text + ";Initial Catalog=" + textBox_DatabaseName.Text + ";Integrated Security=True";
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

            XmlAttribute type = document.CreateAttribute("type");
            root.Attributes.Append(type);

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
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");
            XmlNode server = root.SelectSingleNode("server");
            server.InnerText = textBox_DatabaseServer.Text;

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }

        private void textBox_DatabaseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");
            XmlNode dbname = root.SelectSingleNode("dbName");
            dbname.InnerText = textBox_DatabaseName.Text;

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }
        private void textBox_DatabaseUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(AnalyzerCollectData.PaginaPrincipala.filename);
            XmlNode root = document.SelectSingleNode("Database");
            XmlNode username = root.SelectSingleNode("username");
            username.InnerText = textBox_DatabaseUsername.Text;

            document.Save(AnalyzerCollectData.PaginaPrincipala.filename);
        }

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
    }
}
