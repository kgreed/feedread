using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace FeedRead
{
   
    public partial class Form1 : Form
    {
        private SyndicationFeed feed;
        private FeedReadConfiguration configuration;
      
        public Form1()
        {
            InitializeComponent();
            configuration = Helper.GetApplicationConfiguration( );


        }
       

       

        private void LoadList()
        {
            var url = textBox1.Text;
            var reader = XmlReader.Create(url);
            feed = SyndicationFeed.Load(reader);
            reader.Close();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Clear();
            listView1.Columns.Add("subject");
            listView1.Columns.Add("uri");
            listView1.Columns[0].Width = configuration.Col1Width;
            listView1.Columns[1].Width = 300;


            foreach (SyndicationItem item in feed.Items)
            {
                var subject = item.Title.Text;
                foreach (var link in item.Links)
                {
                    var uri = link.Uri?.AbsoluteUri;
                    if (!uri.Contains("mp3")) continue;
                    var lvi = new ListViewItem(subject) { Text = subject };
                    lvi.SubItems.Add(link.Uri?.AbsoluteUri);
                    listView1.Items.Add(lvi);
                }
            }
            listView1.Refresh();
        }
        
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //const string urlStart = "https://feeds.simplecast.com/JGE3yC0V";
           // const string urlStart ="https://anchor.fm/s/3fab060/podcast/rss";
            const string urlStart = "https://changelog.com/practicalai/feed";
        
            textBox1.Text = urlStart;
            LoadList();

           // var prop =GetOrInitProperty(kCol1Width, listView1.Columns[0].Width);

        }

        private int GetInt(string key, int defaultValue)
        {
            var pair = GetOrInitProperty(key, defaultValue);
            return Convert.ToInt32(pair.Value);
        }

        private KeyValuePair<string, object> GetOrInitProperty(string propName, object defaultValue)
        {
           // configuration.GetSection()
            //foreach (var prop in configuration.)
            //{
            //    if (prop.Key == propName)
            //    {
            //        return prop;
            //    }
            //}
        
            //configuration.Properties.Add(propName,defaultValue);
            //foreach (var prop in configuration.Properties)
            //{
            //    if (prop.Key == propName)
            //    {
            //        return prop;
            //    }
            //}

            throw  new Exception("Unexpected cant find property");
        }

        //private void PutValuePair(string propName, object value)
        //{
        //    var pair = GetOrInitProperty(propName, value);
        //    if (pair.Value == value) return;
        //    configuration.Properties.Remove(propName);
        //    pair = GetOrInitProperty(propName, value);
        //    SetAppSettingValue(propName,value.ToString());

        //}
        public static void SetAppSettingValue(string key, string value, string appSettingsJsonFilePath = null)
        {
            if (appSettingsJsonFilePath == null)
            {
                appSettingsJsonFilePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "appsettings.json");
            }

            var json = System.IO.File.ReadAllText(appSettingsJsonFilePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

            jsonObj[key] = value;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(appSettingsJsonFilePath, output);
        }
        private void ListView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
         
             
            var sb = new StringBuilder();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                sb.AppendLine(item.SubItems[1].ToString());
            }
            
            
            var tokens =sb.ToString().Split(" ");
            var s = "";
            foreach (var token in tokens)
            {
                s = token;
                if (!s.Contains("mp3")) continue;
             
                if (s.StartsWith("{"))
                {
                    s = s.Remove(0, 1);
                }

                var p = s.IndexOf(".mp3");
                s = s.Substring(0, p + 4);
                 
                richTextBox1.Text = s;
            }
          
            richTextBox1.Refresh();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //PutValuePair(kCol1Width, listView1.Columns[0].Width);
           
            //var extn = configuration as IConfigurationBuilder; // not sure about
            //var provider = extn.GetFileProvider();
            //var info = provider.GetFileInfo(subpath: kAppSettings);
            
            //System.Diagnostics.Debug.Print("Hi");
           // var info =provider.GetFileInfo()
                                                                                               // how do i save
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
