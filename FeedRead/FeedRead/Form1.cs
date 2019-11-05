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
        private void button1_Click(object sender, EventArgs e)
        {
            //LoadList();
            if (listView1.SelectedItems.Count != 1) return;
            var li = listView1.SelectedItems[0];
            var subject = li.Text;
            var item = feed.Items.FirstOrDefault(x => x.Summary.Text == subject);
            if (item == null) return;

            Debug.Print(item.BaseUri.AbsoluteUri);

        }

        private void LoadList()
        {
            const string url = "https://feeds.simplecast.com/JGE3yC0V";
            var reader = XmlReader.Create(url);
            feed = SyndicationFeed.Load(reader);
            reader.Close();
            listView1.View = View.Details;
            var i = 0;
            foreach (SyndicationItem item in feed.Items)
            {
                i++;
                var subject = item.Title.Text;
                var summary = item.Summary.Text;
                var lvi = new ListViewItem(subject);
                //var lvi = new ListViewItem
                //{
                //    Text = subject,
                //    Name = subject,
                //    ImageKey = $"key{i}"
                //};
                listView1.Items.Add(lvi);
            }

            // MessageBox.Show($"There are {i} items");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadList();
            //listView1.Refresh();
        }
    }
}
