using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWindowsForm
{
    public class Tupel
    {
        string user { get; set; }
        string createdAt { get; set; }

        public Tupel(string user, string createdAt)
        {

            this.user = user;
            this.createdAt = createdAt;
        }

        public override string ToString()
        {

            return "\n Created by User: " + user + "\n Tweeted at Time: " + createdAt;
        }

    }
}
