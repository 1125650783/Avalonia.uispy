using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AvaloniaUIHelp.Views
{
    public partial class AppChoooser : Window
    {
        private Button btnStartWindowsSearch;

        public AppChoooser()
        {
            
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
            PixelRect rect = this.Screens.Primary.WorkingArea;
            //todo  ���Ͻ���ʾ
            this.Position = new PixelPoint(50, 50);
            var loader = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var s = loader.Open(new Uri("avares://AvaloniaUIHelp/Assets/lineDragMove.png"));
            var bitmap = new Bitmap(s);
            this.btnStartWindowsSearch.Cursor = new Cursor(bitmap, new PixelPoint(0, 0));
#endif
        }

        #region ����
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            btnStartWindowsSearch = this.FindControl<Button>("btnStartWindowsSearch");
        }

        /// <summary>
        /// ʵ����ק
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleMove(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            this.BeginMoveDrag(e);
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void InputMin_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ��ʽ

        #endregion

    }
}
