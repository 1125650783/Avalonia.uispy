using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaUIHelp.ViewModels
{
    public class TreeModel : ViewModelBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Reactive] public string Name { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Reactive] public object ToolTip { get; set; }

        public object Object { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<TreeModel> Children { get; set; } = new ObservableCollection<TreeModel>();


    }
}
