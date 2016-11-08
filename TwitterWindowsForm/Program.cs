using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitterWindowsForm;

namespace TwitterWindowsForm
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TwitterFeed feed = new TwitterFeed();

            //Filter<string> bloomfilter = new Filter<string>(100000,(float)0.01,null);
            


        }
    }
}
