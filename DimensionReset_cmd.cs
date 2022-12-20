using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Runtime;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZWLibrary;

[assembly: CommandClass(typeof(DimensionReset.DimensionReset_cmd))]

namespace DimensionReset
{
    public class DimensionReset_cmd
    {
        [CommandMethod("dimspy")]
        static public void Run()
        {
            DimensionReset_cmd cmd = new DimensionReset_cmd();
            cmd.Evaluate();
        }

        DimensionReset_cmd()
        {
            service = new RealDimensionAppender();
        }

        RealDimensionFormater service;

        private void Evaluate()
        {

            while (true)
            {
                ErrorStatus result = AskForInput();
                if (result != ErrorStatus.OK)
                    return;
                service.Run();
            }
        }

        private ErrorStatus Initialize()
        {
            ErrorStatus status = AskForInput();
            return status;
        }

        private ErrorStatus AskForInput()
        {
            ObjectIdCollection result = SSGet.GetActive();
            if (result != null)
            {
                if (result.Count != 0)
                {
                    service.Items = result;
                    return ErrorStatus.OK;
                }                    
            }

            try
            {
                Document acDoc = Application.DocumentManager.MdiActiveDocument;
                PromptSelectionResult selectionResult = acDoc.Editor.GetSelection( PromptOptions );
                if (selectionResult.Status == PromptStatus.OK)
                {
                    service.Items = new ObjectIdCollection(selectionResult.Value.GetObjectIds());
                    return ErrorStatus.OK;
                }
                else
                {
                    return ErrorStatus.UserBreak;
                }
            }
            catch (KeyNotFoundException ex)
            {
                if (ex.Message == "Wszystkie")
                {
                    service.Items = SSGet.All();
                    return ErrorStatus.OK;
                }
                else if (ex.Message == "Usuń")
                {
                    ObjectIdCollection Items = service.Items;
                    service = new RealDimensionRemover(Items);
                }
            }
            catch ( System.Exception ex)
            {
                return ErrorStatus.InvalidInput;
            }

            return ErrorStatus.OK;
        }


        PromptSelectionOptions PromptOptions
        {
            get
            {
                PromptSelectionOptions result = new PromptSelectionOptions();
                result.Keywords.Add("Wszystkie");
                result.Keywords.Add("Usuń");
                result.MessageForAdding = "Wybierz wymiary[Wszystkie/Usuń]: ";
                result.KeywordInput += new SelectionTextInputEventHandler(OnKeywordInput);
                return result;
            }
        }


        static void OnKeywordInput(object sender, SelectionTextInputEventArgs e)
        {
            throw new KeyNotFoundException(e.Input);
        }
    }
}
