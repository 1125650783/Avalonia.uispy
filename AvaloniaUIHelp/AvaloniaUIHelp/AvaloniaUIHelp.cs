using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaUIHelp.Views;

namespace AvaloniaUIHelp
{
   public class AvaloniaUIHelp
    {
        public static void Init(out string error)
        {
            error = "";
            try
            {
                AppChoooser appChoooser = new AppChoooser();
                appChoooser.Show();
                SnoobUI.Init();
            }
            catch (Exception e)
            {
                error = e.Message;
            }
        }
    }
}
