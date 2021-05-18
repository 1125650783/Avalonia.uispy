using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.VisualTree;

namespace AvaloniaUIHelp.Messages
{
    public class VisualMessage
    {
        public IVisual Visual;

        public VisualMessage(IVisual visual)
        {
            Visual = visual;
        }



    }
}
