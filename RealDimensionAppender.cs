using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using ZwSoft.ZwCAD.DatabaseServices;

namespace DimensionReset
{
    internal class RealDimensionAppender : RealDimensionFormater
    {

        public RealDimensionAppender()
            : base()
        {
        }
        public RealDimensionAppender( ObjectIdCollection Items)
            : base( Items)
        {
        }
        protected override void Format(Dimension dim)
        {
            dim.DimensionText = dim.DimensionText + Marker;
        }

        protected override bool NeedToEvaluate(Dimension dim)
        {
            string txt = dim.DimensionText;
            if (string.IsNullOrEmpty(txt))
                return false;
            if (txt.Contains("<>"))
                return false;

            // string format = stringformatOf(dim);
            // string dimvalue = string.Format(format, dim.Measurement );

            IFormatProvider format = formatOf(dim);
            string dimvalue = dim.Measurement.ToString(format);

            dimvalue = dim.Prefix + dimvalue + dim.Suffix;
            if (dimvalue == txt)
                return false;
            return true;
        }

        private string stringformatOf(Dimension dim)
        {
            string result = dim.Prefix;
            string decseparator = ".";
            result += "{" + "0:"+ "0" + decseparator + new string ('#', dimdec(dim)) + "}";
            result += dim.Suffix;
            return result;
        }

        private IFormatProvider formatOf(Dimension dim)
        {
            NumberFormatInfo curent = CultureInfo.CurrentCulture.NumberFormat;

            NumberFormatInfo result = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
                       
            // result.NumberDecimalDigits = dimdec(dim);
            result.NumberDecimalDigits = 4;  // 
            result.NumberDecimalSeparator = dim.Dimdsep.ToString();
            result.NumberGroupSeparator = "";
            
            return result;
        }

        int dimdec(Dimension dim)
        {
            int result = dim.Dimdec;
            if (result == 0)
                result = HostApplicationServices.WorkingDatabase.Dimdec;
            return result;
        }
        
    }
}
