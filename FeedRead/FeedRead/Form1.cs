using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace FeedRead
{
    public partial class Form1 : Form
    {
        private SyndicationFeed feed;
        public Form1()
        {
            InitializeComponent();

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

            foreach (SyndicationItem item in feed.Items)
            {
                var subject = item.Title.Text;
                foreach (var link in item.Links)
                {
                    var uri = link.Uri?.AbsoluteUri;
                    if (uri.Contains("mp3"))
                    {
                        var lvi = new ListViewItem(subject) { Text = subject };
                        
                        lvi.SubItems.Add(link.Uri?.AbsoluteUri);
                        listView1.Items.Add(lvi);
                    }
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
            
        }
        private void ListView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
         
             
            var sb = new StringBuilder();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                sb.AppendLine(item.SubItems[1].ToString());
            }
            richTextBox1.Text = sb.ToString();
            richTextBox1.Refresh();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
