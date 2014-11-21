using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tickets.ID
{
    public class Configurations
    {
        private static string _SecToken = string.Empty;
        private static string _URL = string.Empty;
        private static string _OutPut = string.Empty;

        public static string OutPut
        {
            get
            {
                _OutPut = "xml";
                return _OutPut;
            }
        }

        public static string SecToken
        {
            get
            {
                _SecToken = "0ebb4e62f00ee5b0229d441af2e26d31";
                return _SecToken;
            }
        }
        public static string URL
        {
            get
            {
                _URL = "http://api.master18.tiket.com/";
                return _URL;
            }
        }
    }
}
