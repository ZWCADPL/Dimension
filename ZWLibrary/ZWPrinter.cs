using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.ApplicationServices;

namespace ZWLibrary
{
    public class ZWPrinter 
    {
        
        public static void Print(ErrorStatus val)       {   
            Print(val.ToString());  
        }
        public static void Print(System.Exception val)  {  
            Print(val.ToString());  
        }
        internal static void NewLine()                  {   Print("\n") ;           }
        internal static void Print(int p)               {   Print(p.ToString());    }

        internal static void Print(double val) { Print(val.ToString()); }

        internal static void Print(object val) { Print(val.ToString()); }

        public void Display(string val)
        {
            Print(val);
        }
        public static void Print( string val )
        {
            try
            {
                Document Doc = ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                Doc.Editor.WriteMessage( val );    
            }
            catch ( System.Exception e)
            {
                Console.Write(e);
            }
            
        }
    }
}
