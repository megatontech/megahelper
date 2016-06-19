using System;
using System.Collections.Generic;
using System.Text;

namespace BaseUtils
{
    internal class config
    {
        public static string DatabaseFile = "";

        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DatabaseFile);
            }
        }
    }
}