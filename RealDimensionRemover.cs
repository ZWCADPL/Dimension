using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace DimensionReset
{
    class RealDimensionRemover : RealDimensionFormater
    {
        public RealDimensionRemover( ObjectIdCollection Items)
            : base( Items)
        {
        }

        protected override void Format(Dimension dim)
        {
            dim.DimensionText = dim.DimensionText.Replace(Marker , "") ;
        }

        protected override bool NeedToEvaluate(Dimension dim)
        {
            string txt = dim.DimensionText;
            return txt.Contains(Marker) ;
        }
    }
}
