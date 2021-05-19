using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace AvaloniaUIHelp.ViewModels
{
    public class TreeModel : ViewModelBase
    {
        private string m_Name;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => m_Name;
            set
            {
                m_Name = value;
                this.RaisePropertyChanged(nameof(Name));
            }
        }

        private object m_ToolTip;

        /// <summary>
        /// 名称
        /// </summary>
        public object ToolTip
        {
            get => m_ToolTip;
            set
            {
                m_ToolTip = value;
                this.RaisePropertyChanged(nameof(ToolTip));
            }
        }

        private IBitmap m_Bitmap;

        public IBitmap Bitmap
        {
            get => m_Bitmap;
            set
            {
                m_Bitmap = value;
                this.RaisePropertyChanged(nameof(Bitmap));
            }
        }

        private VisualBrush m_VisualBrush;

        public VisualBrush VisualBrush
        {
            get => m_VisualBrush;
            set
            {
                m_VisualBrush = value;
                this.RaisePropertyChanged(nameof(VisualBrush));
            }
        }

        private double m_Width;

        public double Width
        {
            get => m_Width;
            set
            {
              
                m_Width = value;
                this.RaisePropertyChanged(nameof(Width));
            }
        }

        private double m_Height;

        public double Height
        {
            get => m_Height;
            set
            {
                m_Height = value;
                this.RaisePropertyChanged(nameof(Height));
            }
        }

        

        public object Object { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public ObservableCollection<TreeModel> Children { get; set; } = new ObservableCollection<TreeModel>();


    }
}
