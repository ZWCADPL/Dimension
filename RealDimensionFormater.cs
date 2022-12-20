using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace DimensionReset
{
    abstract class RealDimensionFormater
    {

        internal Transaction Tr;
        internal ObjectIdCollection Items;

        protected RealDimensionFormater()
        {
        }
        protected RealDimensionFormater(ObjectIdCollection items)
        {
            Items = items;
        }

        internal string Marker
        {
            get
            {
                return " {\\C1;[<>]}";
            }
        }            

        internal void Run()
        {
            if (Items == null)
                return;

            Initialize();
            Evaluate();
            Finalize();
     
        }
        private void Initialize()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            Tr = db.TransactionManager.StartTransaction();
        }

        private void Evaluate()
        {
            foreach (ObjectId item in Items)
            {
                Evaluate(item);
            }
        }

        private void Finalize()
        {
            Tr.Commit();
        }


        private void Evaluate(ObjectId item)
        {
            Dimension dim = Tr.GetObject(item, OpenMode.ForWrite, false) as Dimension;
            if (dim == null)
                return;
            if (NeedToEvaluate(dim))
                Format(dim);
        }

        protected abstract void Format(Dimension dim);

        protected abstract bool NeedToEvaluate(Dimension dim);

    }
}
