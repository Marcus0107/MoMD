using MoMD;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TwitterWindowsForm
{
    public partial class TwitterFeed : Form
    {


        ConcurrentBag<Tupel> hashtagBag;
        SampleStreamThread sampleStream = new SampleStreamThread();

        public TwitterFeed()
        {

            InitializeComponent();
            button1 = new Button();

            textBox1.ScrollBars = ScrollBars.Both;
            ShowDialog();
        }
        //Get Hashtags
        private void button1_Click_1(object sender, EventArgs e)
        {
            hashtagBag = sampleStream.samplingTupels;

            foreach (Tupel hashtag in hashtagBag)
            {
                textBox1.AppendText(hashtag.ToString() + "\u000D\u000A");

            }

            textBox2.AppendText("\u000D\u000ATweets gesammt: " + sampleStream.samplingTweets.ToString() + " with place " + sampleStream.samplingTweetsWithPlace + " in Sample: " + hashtagBag.Count.ToString());
        }

        //Reset Textbox 1
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        //Start Sampling
        private void button3_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() =>
            {
                sampleStream.createSampleStreamForSampling();
            });

            t.Start();
        }

        //Stop Sampling
        private void button4_Click(object sender, EventArgs e)
        {
            sampleStream.stream.StopStream();
        }

        //Start Bloomberg Filter Thread
        private void button5_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() =>
            {
                sampleStream.createSampleStreamForBloombergFilter();
            });

            t.Start();
        }

        //Get Bloomberg Filter Tupel
        private void button6_Click(object sender, EventArgs e)
        {

            hashtagBag = sampleStream.bloombergTupels;

            foreach (Tupel tupel in hashtagBag)
            {
                textBox1.AppendText(tupel.ToString() + "\u000D\u000A");

            }

            textBox2.AppendText("\u000D\u000ATweets gesammt: " + sampleStream.bloombergTweets.ToString() + " with place " + sampleStream.bloombergTweetsWithPlace + " in Sample: " + hashtagBag.Count.ToString());

        }
    }
}
