using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using AvaloniaUIHelp.Core;
using AvaloniaUIHelp.Messages;
using AvaloniaUIHelp.ViewModels;

namespace AvaloniaUIHelp.Views
{
    public partial class SnoobUI : Window
    {

        private SnoopViewModel m_SnoopViewModel;

        #region 构造

        #endregion

        static SnoobUI()
        {
            SubMessages();
        }

        public SnoobUI()
        {
            InitializeComponent();
            this.AttachDevTools();
            m_SnoopViewModel = new SnoopViewModel();
            this.DataContext = m_SnoopViewModel;

        }

        public SnoobUI(SnoopViewModel snoopViewModel)
        {
            InitializeComponent();
            this.AttachDevTools();
            m_SnoopViewModel = snoopViewModel;
            this.DataContext = m_SnoopViewModel;
            TreeModel treeModel = m_SnoopViewModel.TreeDataSource.FirstOrDefault();
            m_SnoopViewModel.AllVisualProperties =
                SnoopViewModel.Create(treeModel.Object, treeModel.Object.GetType());

        }

        public static void Init()
        {

        }

        #region 消息
        private static void SubMessages()
        {
            MessageAggregator<TypeMessage>.Subscribe(OnTypeMessage);
            MessageAggregator<VisualMessage>.Subscribe(OnVisualMessage);
        }

        private static void OnTypeMessage(TypeMessage typeMessage)
        {
            SnoopViewModel snoopViewModel = new SnoopViewModel(typeMessage.Obj, typeMessage.Type);
            ShowWindow(snoopViewModel);
        }

        private static void OnVisualMessage(VisualMessage visualMessage)
        {
            SnoopViewModel snoopViewModel = new SnoopViewModel(visualMessage.Visual);
            ShowWindow(snoopViewModel);
        }

        private static void ShowWindow(SnoopViewModel snoopViewModel)
        {
            SnoobUI snoobUi = new SnoobUI(snoopViewModel);
            snoobUi.Show();
        }

        #endregion

        /// <summary>
        /// 按下Enter变更属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                TextBox textBox = sender as TextBox;
                VisualProperty visualProperty = textBox.DataContext as VisualProperty;
                if (textBox.Text != visualProperty.StringValue)
                {
                    visualProperty.StringValue = textBox.Text;
                }
            }
            base.OnKeyDown(e);
        }

        private void TreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count == 0)
                {
                    return;
                }
                TreeModel treeModel = e.AddedItems[0] as TreeModel;
                if (treeModel == null)
                {
                    return;
                }

                if (treeModel.Object == null)
                {
                    return;
                }

              
                
               
                m_SnoopViewModel.AllVisualProperties =
                    SnoopViewModel.Create(treeModel.Object, treeModel.Object.GetType());
               
            }
            catch (Exception exception)
            {

            }
        }

        private void SnoobUI_Closed(object? sender, System.EventArgs e)
        {
            m_SnoopViewModel.Dispose();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.Closed += SnoobUI_Closed;
        }

     


      
    }
}
