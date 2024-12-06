using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Singleton
    {
        private static Singleton _instance;
        public static Singleton Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
        public string serverName { get; set; }
        public string serverTitle { get; set; }
    }
}
