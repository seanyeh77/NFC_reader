using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFC_reader
{
    public static class util
    {
        public static void push_data(string msg, string page)
        {
            
            if (page == "selectcard")
            {
                selectcard.Current.userlog(msg);
            }
            else if (page == "deletecard")
            {
                deletecard.Current.showname(msg);
            }
            else if (page == "checkcard")
            {
                checkcard.Current.showdata(msg);
            }
            else if (page == "addcard")
            {
                addcard.Current.userlog(msg);
            }
        }
        public static void notcard(string msg,string page)
        {
            if (page == "selectcard")
            {
                selectcard.Current.notcard();
            }
            else if (page == "deletecard")
            {
                deletecard.Current.notcard();
            }
            else if (page == "checkcard")
            {
                checkcard.Current.notcard();
            }
            else if (page == "addcard")
            {
                addcard.Current.notcard();
            }
        }
    }
}
