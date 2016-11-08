using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Streaming;

namespace MoMD
{
    class UserStreamThread
    {
        public int i { get; set; }
        ConcurrentBag<string> hashtags = new ConcurrentBag<string>();
        public ISampleStream stream { get; set; }

        public void createUserStreams()
        {

            Auth.SetUserCredentials("HK6dYDYaZ91hGHA7uQoi1oWIO", //Consumerkey
    "8uMf9m1UTx2ST9bhzd0Ow9BLhLmqrU2GLRdnU2seWSYTKm2trH", //Consumer Secret
    "793479816209174528-eqb2G4BCyhTb32KtNh1BnLdArWcDgiU",//Access Token
    "oaO4hXOeWXhXuyxJYY2RkE7ffwrdh7N8W3CAmh93CVykw" //Acces Token Secret
    );
            var user = User.GetAuthenticatedUser();

            var stream = Stream.CreateUserStream();
            stream.TweetCreatedByAnyone += (sender, args) =>
            {
                
                Console.WriteLine("Test " + args.Tweet.Hashtags[0].Text);
            };
            stream.StartStream();

        }

        public ConcurrentBag<string> getHashtags()
        {

            return this.hashtags;
        }
    }
}
