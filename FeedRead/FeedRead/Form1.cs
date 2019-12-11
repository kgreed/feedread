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

            var x = "xxx";
            listView1.View = View.Details;
            listView1.Clear();
            foreach (SyndicationItem item in feed.Items)
            {
                var subject = item.Title.Text;
                //var lvi = new ListViewItem(subject) {Text = subject};
                //Debug.Print( $"text:{lvi.Text} name: {lvi.Name}  ");
                listView1.Items.Add(x);
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            
            var o = (ListView) sender;
            if (o.SelectedItems.Count != 1) return;
            var s = o.SelectedItems[0].Text;
            var item = feed.Items.FirstOrDefault(x => x.Title.Text == s);
            if (item?.Summary == null) return;

           

            richTextBox1.Text = item.Summary.Text;
            if (item.Links.Count == 0)
            {
                textBox1.Text = "";
                return;
            }


            textBox1.Text = item.Links[^1]?.Uri.AbsoluteUri;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
