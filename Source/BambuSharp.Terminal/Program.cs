using Terminal.Gui;

namespace BambuSharp.UI;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Initialize Terminal.Gui
        Application.Init();

        try
        {
            // Create printer manager
            var printerManager = new PrinterManager();
            printerManager.LoadConfigurationsAsync();

            // Create and run main window
            var mainWindow = new MainWindow(printerManager);
            Application.Run(mainWindow);

            // Cleanup on exit
            printerManager.Dispose();
        }
        finally
        {
            Application.Shutdown();
        }
    }
}