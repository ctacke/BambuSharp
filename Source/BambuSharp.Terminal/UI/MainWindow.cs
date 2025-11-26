using Terminal.Gui;

namespace BambuSharp.UI;

/// <summary>
/// Main application window for the Bambu Sharp CLI.
/// </summary>
public class MainWindow : Window
{
    private readonly PrinterManager _printerManager;
    private readonly ListView _printerListView;
    private readonly PrinterStatusView _statusView;
    private LocalPrinter? _selectedPrinter;

    public MainWindow(PrinterManager printerManager)
    {
        _printerManager = printerManager;

        Title = "BambuSharp Terminal (Ctrl+Q to Quit)";

        // Create top button bar
        var addButton = new Button("Add Printer")
        {
            X = 1,
            Y = 0
        };
        addButton.Clicked += OnAddPrinterClicked;

        var refreshButton = new Button("Refresh")
        {
            X = Pos.Right(addButton) + 2,
            Y = 0
        };
        refreshButton.Clicked += OnRefreshClicked;

        var quitButton = new Button("Quit")
        {
            X = Pos.AnchorEnd() - 9,  // "Quit" button width
            Y = 0
        };
        quitButton.Clicked += () => Application.RequestStop();

        Add(addButton, refreshButton, quitButton);

        // Create printers label
        var printersLabel = new Label("Configured Printers:")
        {
            X = 1,
            Y = 2
        };
        Add(printersLabel);

        // Create printer list view
        _printerListView = new ListView
        {
            X = 1,
            Y = 3,
            Width = Dim.Fill() - 1,
            Height = 5  // Reduced from 8 to give more space to status view
        };
        _printerListView.SelectedItemChanged += OnPrinterSelected;
        Add(_printerListView);

        // Create printer status view
        _statusView = new PrinterStatusView
        {
            X = 1,
            Y = Pos.Bottom(_printerListView) + 1,
            Width = Dim.Fill() - 1,
            Height = Dim.Fill() - 1
        };
        Add(_statusView);

        // Load initial data
        RefreshPrinterList();

        // Connect to all printers asynchronously
        Task.Run(async () =>
        {
            await _printerManager.ConnectAllAsync();
            Application.MainLoop.Invoke(RefreshPrinterList);
        });
    }

    private void OnAddPrinterClicked()
    {
        var dialog = new AddPrinterDialog();
        Application.Run(dialog);

        if (dialog.Result != null)
        {
            Task.Run(async () =>
            {
                try
                {
                    var printer = await _printerManager.AddPrinterAsync(dialog.Result);
                    await printer.Connect();

                    Application.MainLoop.Invoke(() =>
                    {
                        RefreshPrinterList();
                        MessageBox.Query("Success", $"Printer '{dialog.Result.Name}' added successfully!", "OK");
                    });
                }
                catch (Exception ex)
                {
                    Application.MainLoop.Invoke(() =>
                    {
                        MessageBox.ErrorQuery("Error", $"Failed to add printer: {ex.Message}", "OK");
                    });
                }
            });
        }
    }

    private void OnRefreshClicked()
    {
        RefreshPrinterList();
    }

    private void RefreshPrinterList()
    {
        var printers = _printerManager.ConfiguredPrinters
            .Select(p =>
            {
                var printer = _printerManager.GetPrinter(p.IpAddress);
                var status = printer != null ? "Connected" : "Disconnected";
                return $"{p.Name} ({p.IpAddress}) - [{status}]";
            })
            .ToList();

        _printerListView.SetSource(printers);

        // If we had a selected printer, try to maintain selection
        if (_selectedPrinter != null)
        {
            var index = _printerManager.ConfiguredPrinters
                .ToList()
                .FindIndex(p => p.IpAddress == _selectedPrinter.IpAddress);

            if (index >= 0)
            {
                _printerListView.SelectedItem = index;
            }
        }
    }

    private void OnPrinterSelected(ListViewItemEventArgs args)
    {
        if (args.Item < 0 || args.Item >= _printerManager.ConfiguredPrinters.Count)
        {
            _statusView.SetPrinter(null);
            _selectedPrinter = null;
            return;
        }

        var config = _printerManager.ConfiguredPrinters[args.Item];
        _selectedPrinter = _printerManager.GetPrinter(config.IpAddress);
        _statusView.SetPrinter(_selectedPrinter);
    }
}
