using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.VisualTree;
using AvaloniaUIHelp.Core;
using AvaloniaUIHelp.DllImports;
using AvaloniaUIHelp.Messages;
using AvaloniaUIHelp.Models;
using AvaloniaUIHelp.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VisualExtensions = Avalonia.VisualTree.VisualExtensions;

namespace AvaloniaUIHelp.Views
{
    public partial class AppChoooser : Window
    {
        //private SnoobUI m_SnoobUI;

        /// <summary>
        /// 搜索
        /// </summary>
        private Border btnStartWindowsSearch;

        private CheckBox chkIsWindow;

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
            //m_ChoiceCursor = new Cursor(bitmap, new PixelPoint(0, 0));
            m_ChoiceCursor = new Cursor(StandardCursorType.Cross);
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
                   Point pi= e.GetPosition(null);
                  
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
                POINT scremPoint = new POINT(0, 0);
                NativeMethods.GetCursorPos(ref scremPoint);
                IntPtr itIntPtr = NativeMethods.GetWindowUnderMouse();
                if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    foreach (Window desktopWindow in desktop.Windows)
                    {
                        Thickness tk = desktopWindow.OffScreenMargin;
                        if (desktopWindow.PlatformImpl.Handle.Handle == itIntPtr)
                        {
                            //推送内容
                            SnoopViewModel snoopViewModel;
                            if (chkIsWindow.IsChecked.HasValue && chkIsWindow.IsChecked.Value)
                            {
                                snoopViewModel = new SnoopViewModel(desktopWindow);
                            }
                            else
                            {
                                
                                //Point pointTest = e.GetPosition(desktopWindow);
                                //Point point = new Point(scremPoint.X- desktopWindow.Position.X, scremPoint.Y- desktopWindow.Position.Y);
                              
                                //屏幕位置转换为Avalonia位置
                                Point p3 = desktopWindow.PointToClient(new PixelPoint(scremPoint.X, scremPoint.Y));

                                IVisual visual = VisualExtensions.GetVisualAt(desktopWindow, p3);
                                //IRenderRoot root = desktopWindow;
                                //desktopWindow.Renderer.HitTestFirst(point, visual, x => x.IsVisible);
                                //root.Renderer.HitTestFirst(point, visual, x=>x.IsVisible);
                                if (visual == null)
                                {
                                    snoopViewModel = new SnoopViewModel(desktopWindow);
                                }
                                else
                                {
                                    IVisual showVisual = visual;
                                    if (visual is Control control)
                                    {
                                        if (control.TemplatedParent!=null)
                                        {
                                            try
                                            {
                                                showVisual = (IVisual)control.TemplatedParent;
                                                if (showVisual==null)
                                                {
                                                    showVisual = visual;
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                               
                                            }
                                           
                                        }
                                    }

                                    snoopViewModel = new SnoopViewModel(showVisual);
                                    //var childPoint = desktopWindow.TranslatePoint(point, visual);
                                    //IVisual visual2 = VisualExtensions.GetVisualAt(visual, childPoint.Value);

                                }

                            }

                            SnoobUI snoobUi = new SnoobUI(snoopViewModel);
                            snoobUi.Show();
                            //MessageAggregator<VisualMessage>.Publish(new VisualMessage(desktopWindow));

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
            chkIsWindow = this.FindControl<CheckBox>("chkIsWindow");
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
