using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;

namespace MoMD
{
    class Program
    {
        static void Main(string[] args)
        {

            ConcurrentBag<String> hashtags = new ConcurrentBag<string>();



            SampleStreamThread thread1 =  new SampleStreamThread();
            //Thread thr = new Thread(new ThreadStart(thread1.createSampleStream));
            //thread1.createSampleStream();

            var t = new Thread(() =>
            {
                thread1.createSampleStream();
            });

            t.Start();



            //onsole.WriteLine("Example");

            
            while(true)
            {
                //thread1.stream.PauseStream();

                hashtags = thread1.getHashtags();
                Console.Clear();

                foreach (string hashtag in hashtags)
                {
                    
                    Console.WriteLine(hashtag);
                    }
                //thread1.restartSampleStream();


                Thread.Sleep(1000);
            }
            
        }
    }
}
