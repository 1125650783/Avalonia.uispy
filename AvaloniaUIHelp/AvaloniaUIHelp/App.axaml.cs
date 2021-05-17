using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaUIHelp.ViewModels;
using AvaloniaUIHelp.Views;

namespace AvaloniaUIHelp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //desktop.MainWindow = new AppChoooser
                //{
                //    DataContext = new MainWindowViewModel(),
                //};
                desktop.MainWindow = new AppChoooser();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
