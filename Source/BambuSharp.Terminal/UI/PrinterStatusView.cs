using BambuSharp.UI.Utilities;
using System.ComponentModel;
using Terminal.Gui;
using Terminal.Gui.Trees;

namespace BambuSharp.UI;

/// <summary>
/// View for displaying printer status information with live updates and expandable AMS tree.
/// </summary>
public class PrinterStatusView : FrameView
{
    private readonly TreeView _statusTreeView;
    private LocalPrinter? _printer;
    private readonly List<StatusItem> _rootItems = new();
    private StatusItem? _amsRootItem;
    private StatusItem? _extruderRootItem;
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

        // Set up color getter to apply custom colors to items
        _statusTreeView.ColorGetter = (obj) =>
        {
            if (obj is StatusItem item && item.ForegroundColor.HasValue)
            {
                var bgColor = item.BackgroundColor ?? Color.Black;
                var colorScheme = new ColorScheme
                {
                    Normal = new Terminal.Gui.Attribute(item.ForegroundColor.Value, bgColor),
                    Focus = new Terminal.Gui.Attribute(item.ForegroundColor.Value, Color.DarkGray),
                    HotNormal = new Terminal.Gui.Attribute(item.ForegroundColor.Value, bgColor),
                    HotFocus = new Terminal.Gui.Attribute(item.ForegroundColor.Value, Color.DarkGray)
                };
                return colorScheme;
            }
            return null;
        };

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
            // Try to access a property to check if data is available
            _ = _printer.State;

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

        // Create Extruder root item
        _extruderRootItem = new StatusItem("Extruder");
        _rootItems.Add(_extruderRootItem);

        // Add extruder details as children
        _extruderRootItem.Children.Add(new StatusItem("Current Temp: "));
        _extruderRootItem.Children.Add(new StatusItem("Target Temp: "));
        _extruderRootItem.Children.Add(new StatusItem("Filament Temp: "));
        _extruderRootItem.Children.Add(new StatusItem("Status: "));

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

        // Update basic status items
        var idx = 0;
        _rootItems[idx++].Text = $"State: {_printer.State}";
        _rootItems[idx++].Text = $"Progress: {_printer.PrintProgress}%";
        _rootItems[idx++].Text = $"File: {(_printer.CurrentFileName != string.Empty ? _printer.CurrentFileName : "None")}";
        _rootItems[idx++].Text = $"Bed Temp: {_printer.BedTemperature.Celsius:F1}°C";
        _rootItems[idx++].Text = $"Nozzle Temp: {_printer.NozzleTemperature.Celsius:F1}°C";

        // Layer info
        if (_printer.TotalLayers > 0)
        {
            _rootItems[idx++].Text = $"Layer: {_printer.CurrentLayer}/{_printer.TotalLayers}";
        }
        else
        {
            _rootItems[idx++].Text = "Layer: N/A";
        }

        // Remaining time
        if (_printer.RemainingMinutes > 0)
        {
            var remainingTime = TimeSpan.FromMinutes(_printer.RemainingMinutes);
            _rootItems[idx++].Text = $"Remaining Time: {remainingTime.Hours}h {remainingTime.Minutes}m";
        }
        else
        {
            _rootItems[idx++].Text = "Remaining Time: N/A";
        }

        // Skip extruder root item in main list (it's handled separately below)
        idx++;

        // Update Extruder data
        if (_extruderRootItem != null)
        {
            if (_printer.Extruder != null)
            {
                _extruderRootItem.Text = $"Extruder (ID: {_printer.Extruder.Id})";

                // Update extruder details
                if (_extruderRootItem.Children.Count >= 4)
                {
                    _extruderRootItem.Children[0].Text = $"Current Temp: {_printer.Extruder.CurrentTemperature.Celsius:F1}°C";
                    _extruderRootItem.Children[1].Text = $"Target Temp: {_printer.Extruder.TargetTemperature.Celsius:F1}°C";
                    _extruderRootItem.Children[2].Text = $"Filament Temp: {_printer.Extruder.FilamentTemperature.Celsius:F1}°C";
                    _extruderRootItem.Children[3].Text = $"Status: {_printer.Extruder.Status}";
                }
            }
            else
            {
                _extruderRootItem.Text = "Extruder: Not detected";
                // Clear children if no extruder data
                if (_extruderRootItem.Children.Count >= 4)
                {
                    _extruderRootItem.Children[0].Text = "Current Temp: N/A";
                    _extruderRootItem.Children[1].Text = "Target Temp: N/A";
                    _extruderRootItem.Children[2].Text = "Filament Temp: N/A";
                    _extruderRootItem.Children[3].Text = "Status: N/A";
                }
            }
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
                        // Add colored block if color is available
                        var colorBlock = !string.IsNullOrEmpty(tray.Color)
                            ? ColorConverter.GetColoredBlock(tray.Color)
                            : "";

                        // Get the terminal colors for this tray
                        Color? terminalColor = null;
                        Color? backgroundColor = null;

                        if (!string.IsNullOrEmpty(tray.Color))
                        {
                            terminalColor = ColorConverter.HexToTerminalColor(tray.Color);
                            backgroundColor = ColorConverter.GetContrastingBackgroundForHex(tray.Color);
                        }

                        var trayText = $"{colorBlock}Tray {tray.Id}: {tray.FilamentType}";
                        if (!string.IsNullOrEmpty(tray.Color))
                        {
                            trayText += $" ({tray.Color})";
                        }
                        trayText += $" - {tray.RemainingPercent}% remaining ({tray.RemainingLength}mm)";
                        trayItem.Text = trayText;
                        trayItem.ForegroundColor = terminalColor;
                        trayItem.BackgroundColor = backgroundColor;

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
        public Color? ForegroundColor { get; set; }
        public Color? BackgroundColor { get; set; }

        // ITreeNode implementation
        public object? Tag { get; set; }
        IList<ITreeNode> ITreeNode.Children => Children.Cast<ITreeNode>().ToList();

        public StatusItem(string text, Color? foregroundColor = null, Color? backgroundColor = null)
        {
            Text = text;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public override string ToString() => Text;
    }
}
