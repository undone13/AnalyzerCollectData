using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for PaginaPrincipala.xaml
    /// </summary>
    public partial class PaginaPrincipala : Window
    {
        public static string filename = "AnalyzerCollectData.xml";

        public PaginaPrincipala()
        {
            InitializeComponent();
            Loaded += Load;
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            if(File.Exists(filename) == false)
            {
                CreateXMLFile();
            }
        }

        private void buton_Setari_Click(object sender, RoutedEventArgs e)
        {
            PaginaSetari paginaSetari = new PaginaSetari();
            paginaSetari.Show();
        }

        static void CreateXMLFile()
        {
            XmlDocument document = new XmlDocument();

            XmlNode db = document.CreateElement("Database");
            document.AppendChild(db);

            XmlAttribute type = document.CreateAttribute("type");
            type.InnerText = "Microsoft SQL Server";
            db.Attributes.Append(type);

            XmlAttribute security = document.CreateAttribute("integratedSecurity");
            security.InnerText = "True";
            db.Attributes.Append(security);

            XmlNode server = document.CreateElement("server");
            XmlNode dbname = document.CreateElement("dbName");
            XmlNode username = document.CreateElement("username");
            XmlNode password = document.CreateElement("password");

            db.AppendChild(server);
            db.AppendChild(dbname);
            db.AppendChild(username);
            db.AppendChild(password);

            document.Save(filename);
        }

    }
}
