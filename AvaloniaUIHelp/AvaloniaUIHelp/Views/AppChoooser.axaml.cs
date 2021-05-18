using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.VisualTree;
using AvaloniaUIHelp.Core;
using AvaloniaUIHelp.DllImports;
using AvaloniaUIHelp.Messages;
using AvaloniaUIHelp.Models;
using AvaloniaUIHelp.ViewModels;

namespace AvaloniaUIHelp.Views
{
    public partial class AppChoooser : Window
    {
        //private SnoobUI m_SnoobUI;

        /// <summary>
        /// 搜索
        /// </summary>
        private Border btnStartWindowsSearch;

        /// <summary>
        /// 搜索鼠标
        /// </summary>
        private Cursor m_ChoiceCursor;

        public AppChoooser()
        {
            
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
            PixelRect rect = this.Screens.Primary.WorkingArea;
            //todo  右上角显示
            this.Position = new PixelPoint(50, 50);
            var loader = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var s = loader.Open(new Uri("avares://AvaloniaUIHelp/Assets/shut.png"));
            var bitmap = new Bitmap(s);
            m_ChoiceCursor = new Cursor(bitmap, new PixelPoint(0, 0));
            this.btnStartWindowsSearch.PointerPressed += BtnStartWindowsSearch_PointerPressed;
            this.btnStartWindowsSearch.PointerReleased += BtnStartWindowsSearch_PointerReleased;
            SnoobUI.Init();
#endif
        }

        private void BtnStartWindowsSearch_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            try
            {
                if (e.GetCurrentPoint(sender as IVisual).Properties.IsLeftButtonPressed)
                {
                    this.btnStartWindowsSearch.Cursor = m_ChoiceCursor;
                }
            }
            catch (Exception exception)
            {
               
            }
            
        }

      

        private void BtnStartWindowsSearch_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            try
            {
                //释放 拉取控件
                this.btnStartWindowsSearch.Cursor = null;
                IntPtr itIntPtr = NativeMethods.GetWindowUnderMouse();
                if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    foreach (Window desktopWindow in desktop.Windows)
                    {
                        if (desktopWindow.PlatformImpl.Handle.Handle== itIntPtr)
                        {
                            //推送内容
                            //SnoobUI snoobUi = new SnoobUI();
                            //snoobUi.Show();
                            MessageAggregator<VisualMessage>.Publish(new VisualMessage(desktopWindow));

                            break;
                        }
                    }
                }

            }
            catch (Exception exception)
            {
              
            }
          

        }

     

      

        #region 窗体
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            btnStartWindowsSearch = this.FindControl<Border>("btnStartWindowsSearch");
        }

        /// <summary>
        /// 实现拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleMove(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            this.BeginMoveDrag(e);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void InputMin_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 样式

        #endregion

    }
}
