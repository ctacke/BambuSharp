using System.ComponentModel;
using Terminal.Gui;
using Terminal.Gui.Trees;

namespace BambuSharp.Terminal.UI;

/// <summary>
/// View for displaying printer status information with live updates and expandable AMS tree.
/// </summary>
public class PrinterStatusView : FrameView
{
    private readonly TreeView _statusTreeView;
    private LocalPrinter? _printer;
    private readonly List<StatusItem> _rootItems = new();
    private StatusItem? _amsRootItem;
    private bool _treeInitialized = false;

    public PrinterStatusView()
    {
        Title = "Printer Status";

        _statusTreeView = new TreeView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        _statusTreeView.TreeBuilder = new StatusItemTreeBuilder();

        Add(_statusTreeView);

        ShowNoSelection();
    }

    /// <summary>
    /// Sets the printer to display status for.
    /// </summary>
    public void SetPrinter(LocalPrinter? printer)
    {
        // Unsubscribe from previous printer
        if (_printer != null)
        {
            _printer.PropertyChanged -= OnPrinterPropertyChanged;
        }

        _printer = printer;

        // Subscribe to new printer
        if (_printer != null)
        {
            _printer.PropertyChanged += OnPrinterPropertyChanged;
        }

        // Reset tree when changing printers
        _treeInitialized = false;
        UpdateDisplay();
    }

    private void OnPrinterPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Update the display on the main thread
        Application.MainLoop.Invoke(UpdateDisplay);
    }

    private void ShowNoSelection()
    {
        _rootItems.Clear();
        _statusTreeView.ClearObjects();
        _rootItems.Add(new StatusItem("No printer selected"));
        _statusTreeView.AddObjects(_rootItems);
        _statusTreeView.SetNeedsDisplay();
        _treeInitialized = true;
    }

    private void ShowWaiting()
    {
        _rootItems.Clear();
        _statusTreeView.ClearObjects();
        _rootItems.Add(new StatusItem("Waiting for printer data..."));
        _statusTreeView.AddObjects(_rootItems);
        _statusTreeView.SetNeedsDisplay();
        _treeInitialized = true;
    }

    private void UpdateDisplay()
    {
        if (_printer == null)
        {
            ShowNoSelection();
            return;
        }

        try
        {
#pragma warning disable CS0618 // LastReport is obsolete
            _ = _printer.LastReport;
            var report = _printer.LastReport;
#pragma warning restore CS0618

            if (!_treeInitialized)
            {
                // First time initialization - build the tree structure
                BuildTree();
                _treeInitialized = true;
            }

            // Update existing nodes without rebuilding
            UpdateTreeData();

            _statusTreeView.SetNeedsDisplay();
        }
        catch (Exception)
        {
            ShowWaiting();
        }
    }

    private void BuildTree()
    {
        _rootItems.Clear();
        _statusTreeView.ClearObjects();

        // Create basic status items
        _rootItems.Add(new StatusItem("State: "));
        _rootItems.Add(new StatusItem("Progress: "));
        _rootItems.Add(new StatusItem("File: "));
        _rootItems.Add(new StatusItem("Bed Temp: "));
        _rootItems.Add(new StatusItem("Nozzle Temp: "));
        _rootItems.Add(new StatusItem("Layer: "));
        _rootItems.Add(new StatusItem("Remaining Time: "));

        // Create AMS root item
        _amsRootItem = new StatusItem("AMS Units");
        _rootItems.Add(_amsRootItem);

        // Build AMS structure based on current printer state
        if (_printer?.AmsUnits != null)
        {
            foreach (var ams in _printer.AmsUnits)
            {
                var amsItem = new StatusItem($"Unit {ams.Id}");
                _amsRootItem.Children.Add(amsItem);

                foreach (var tray in ams.Trays)
                {
                    var trayItem = new StatusItem($"Tray {tray.Id}");
                    amsItem.Children.Add(trayItem);

                    // Add tray details as children
                    trayItem.Children.Add(new StatusItem("Nozzle Temp: "));
                    trayItem.Children.Add(new StatusItem("Brand: "));
                }
            }
        }

        _statusTreeView.AddObjects(_rootItems);
    }

    private void UpdateTreeData()
    {
        if (_printer == null) return;

#pragma warning disable CS0618 // LastReport is obsolete
        var report = _printer.LastReport;
#pragma warning restore CS0618

        // Update basic status items
        var idx = 0;
        _rootItems[idx++].Text = $"State: {_printer.State}";
        _rootItems[idx++].Text = $"Progress: {_printer.PrintProgress}%";
        _rootItems[idx++].Text = $"File: {(_printer.CurrentFileName != string.Empty ? _printer.CurrentFileName : "None")}";
        _rootItems[idx++].Text = $"Bed Temp: {_printer.BedTemperature.Celsius:F1}°C";
        _rootItems[idx++].Text = $"Nozzle Temp: {_printer.NozzleTemperature.Celsius:F1}°C";

        // Layer info
        if (report.Print.TotalLayerNum > 0)
        {
            _rootItems[idx++].Text = $"Layer: {report.Print.LayerNum}/{report.Print.TotalLayerNum}";
        }
        else
        {
            _rootItems[idx++].Text = "Layer: N/A";
        }

        // Remaining time
        if (report.Print.RemainTime > 0)
        {
            var remainingTime = TimeSpan.FromMinutes(report.Print.RemainTime);
            _rootItems[idx++].Text = $"Remaining Time: {remainingTime.Hours}h {remainingTime.Minutes}m";
        }
        else
        {
            _rootItems[idx++].Text = "Remaining Time: N/A";
        }

        // Update AMS data
        if (_amsRootItem != null && _printer.AmsUnits?.Count > 0)
        {
            _amsRootItem.Text = $"AMS Units ({_printer.AmsUnits.Count})";

            // Check if AMS structure changed (number of units or trays)
            if (StructureChanged())
            {
                // Rebuild tree if structure changed
                _treeInitialized = false;
                BuildTree();
                return;
            }

            // Update AMS unit data
            for (int i = 0; i < _printer.AmsUnits.Count && i < _amsRootItem.Children.Count; i++)
            {
                var ams = _printer.AmsUnits[i];
                var amsItem = _amsRootItem.Children[i];

                amsItem.Text = $"Unit {ams.Id}: {ams.Temperature.Celsius:F1}°C, {ams.Humidity}% humidity";

                // Update tray data
                for (int j = 0; j < ams.Trays.Count && j < amsItem.Children.Count; j++)
                {
                    var tray = ams.Trays[j];
                    var trayItem = amsItem.Children[j];

                    if (!string.IsNullOrEmpty(tray.FilamentType))
                    {
                        var trayText = $"Tray {tray.Id}: {tray.FilamentType}";
                        if (!string.IsNullOrEmpty(tray.Color))
                        {
                            trayText += $" ({tray.Color})";
                        }
                        trayText += $" - {tray.RemainingPercent}% remaining ({tray.RemainingLength}mm)";
                        trayItem.Text = trayText;

                        // Update tray details
                        if (trayItem.Children.Count >= 2)
                        {
                            if (tray.NozzleTempMin.Celsius > 0 || tray.NozzleTempMax.Celsius > 0)
                            {
                                trayItem.Children[0].Text = $"Nozzle Temp: {tray.NozzleTempMin.Celsius:F0}-{tray.NozzleTempMax.Celsius:F0}°C";
                            }
                            else
                            {
                                trayItem.Children[0].Text = "Nozzle Temp: N/A";
                            }

                            if (!string.IsNullOrEmpty(tray.Brand))
                            {
                                trayItem.Children[1].Text = $"Brand: {tray.Brand}";
                            }
                            else
                            {
                                trayItem.Children[1].Text = "Brand: N/A";
                            }
                        }
                    }
                    else
                    {
                        trayItem.Text = $"Tray {tray.Id}: Empty";
                    }
                }
            }
        }
        else if (_amsRootItem != null)
        {
            _amsRootItem.Text = "AMS: Not detected";
            _amsRootItem.Children.Clear();
        }
    }

    private bool StructureChanged()
    {
        if (_printer?.AmsUnits == null || _amsRootItem == null)
            return false;

        // Check if number of AMS units changed
        if (_printer.AmsUnits.Count != _amsRootItem.Children.Count)
            return true;

        // Check if number of trays in each unit changed
        for (int i = 0; i < _printer.AmsUnits.Count && i < _amsRootItem.Children.Count; i++)
        {
            if (_printer.AmsUnits[i].Trays.Count != _amsRootItem.Children[i].Children.Count)
                return true;
        }

        return false;
    }

    private class StatusItemTreeBuilder : ITreeBuilder<ITreeNode>
    {
        public bool SupportsCanExpand => true;

        public bool CanExpand(ITreeNode toExpand)
        {
            return toExpand is StatusItem item && item.Children.Count > 0;
        }

        public IEnumerable<ITreeNode> GetChildren(ITreeNode forObject)
        {
            return forObject is StatusItem item ? item.Children : Enumerable.Empty<ITreeNode>();
        }

        public string GetText(ITreeNode model)
        {
            return model is StatusItem item ? item.Text : string.Empty;
        }
    }

    private class StatusItem : ITreeNode
    {
        public string Text { get; set; }
        public List<StatusItem> Children { get; } = new();

        // ITreeNode implementation
        public object? Tag { get; set; }
        IList<ITreeNode> ITreeNode.Children => Children.Cast<ITreeNode>().ToList();

        public StatusItem(string text)
        {
            Text = text;
        }

        public override string ToString() => Text;
    }
}
