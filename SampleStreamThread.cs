using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;
using Tweetinvi.Streaming;
using TwitterWindowsForm;

namespace MoMD
{
    public class SampleStreamThread
    {


        public ConcurrentBag<Tupel> samplingTupels { get; set; }
        public ConcurrentBag<Tupel> bloombergTupels { get; set; }
        public int samplingTweets { get; set; }
        public int samplingTweetsWithPlace { get; set; }
        public int bloombergTweets { get; set; }
        public int bloombergTweetsWithPlace { get; set; }
        public ISampleStream stream { get; set; }

        public SampleStreamThread() { samplingTweets = 0; samplingTweetsWithPlace = 0; }
        public void createSampleStreamForSampling()
        {
            samplingTupels = new ConcurrentBag<Tupel>();
            IAuthenticatedUser user = setAuthentication();

            stream = Stream.CreateSampleStream();

            stream.TweetReceived += (sender, args) =>
            {
                samplingTweets++;
                if (args.Tweet.Place != null)
                {
                    samplingTweetsWithPlace++;
                    if (hashString(args.Tweet.Place.Name) % 10 == 0)
                    {
                        samplingTupels.Add(new Tupel(args.Tweet.CreatedBy.Name, args.Tweet.CreatedAt.ToLongTimeString()));
                    }
                }
            };
            stream.StartStream();

        }


        public void createSampleStreamForBloombergFilter()
        {
            bloombergTupels = new ConcurrentBag<Tupel>();
            IAuthenticatedUser user = setAuthentication();

            stream = Stream.CreateSampleStream();
            Filter<string> bloomfilter = new Filter<string>(10000);
            bloomfilter.Add("United Stats");
            bloomfilter.Add("Italy");
            bloomfilter.Add("Trump");
            bloomfilter.Add("Hillary");
            bloomfilter.Add("Holiday");
            bloomfilter.Add("Beach");
            bloomfilter.Add("Election");
            bloomfilter.Add("trump");
            bloomfilter.Add("hillary");
            bloomfilter.Add("holiday");
            bloomfilter.Add("eeach");
            bloomfilter.Add("election");
            stream.TweetReceived += (sender, args) =>
            {
                bloombergTweets++;
                if (args.Tweet.Hashtags.Count > 0)
                {
                    bloombergTweetsWithPlace++;
                    //  if (bloomfilter.Contains(args.Tweet.Place.Country))
                    // {

                    foreach (IHashtagEntity hashtag in args.Tweet.Hashtags)
                    {

                        Console.WriteLine(hashtag.Text);
                        if (bloomfilter.Contains(hashtag.Text))
                        {
                            bloombergTupels.Add(new Tupel(args.Tweet.CreatedBy.Name, args.Tweet.CreatedAt.ToLongTimeString()));

                        }
                    }
                    //    }

                }
            };
            stream.StartStream();

        }

        /// <summary>
        /// Hashes a string using Bob Jenkin's "One At A Time" method from Dr. Dobbs (http://burtleburtle.net/bob/hash/doobs.html).
        /// Runtime is suggested to be 9x+9, where x = input.Length. 
        /// </summary>
        /// <param name="input">The string to hash.</param>
        /// <returns>The hashed result.</returns>
        private int hashString(string input)
        {
            string s = input as string;
            int hash = 0;

            for (int i = 0; i < s.Length; i++)
            {
                hash += s[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);
            return hash;
        }

        private IAuthenticatedUser setAuthentication()
        {
            Auth.SetUserCredentials(
                                    "HK6dYDYaZ91hGHA7uQoi1oWIO", //Consumerkey
                                    "8uMf9m1UTx2ST9bhzd0Ow9BLhLmqrU2GLRdnU2seWSYTKm2trH", //Consumer Secret
                                    "793479816209174528-eqb2G4BCyhTb32KtNh1BnLdArWcDgiU",//Access Token
                                    "oaO4hXOeWXhXuyxJYY2RkE7ffwrdh7N8W3CAmh93CVykw" //Acces Token Secret
                                    );

            IAuthenticatedUser user = User.GetAuthenticatedUser();
            return user;

        }

    }
}
