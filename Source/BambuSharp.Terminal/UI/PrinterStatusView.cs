using System.ComponentModel;
using Terminal.Gui;

namespace BambuSharp.Terminal.UI;

/// <summary>
/// View for displaying printer status information with live updates.
/// </summary>
public class PrinterStatusView : FrameView
{
    private readonly TextView _statusTextView;
    private LocalPrinter? _printer;

    public PrinterStatusView()
    {
        Title = "Printer Status";

        _statusTextView = new TextView
        {
            X = 0,
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ReadOnly = true,
            WordWrap = true
        };

        Add(_statusTextView);

        UpdateDisplay();
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

        UpdateDisplay();
    }

    private void OnPrinterPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Update the display on the main thread
        Application.MainLoop.Invoke(UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        if (_printer == null)
        {
            _statusTextView.Text = "No printer selected.\n\nSelect a printer from the list above to view its status.";
            return;
        }

        try
        {
#pragma warning disable CS0618 // LastReport is obsolete
            // Check if we have received any data yet
            _ = _printer.LastReport;
#pragma warning restore CS0618

            var status = new System.Text.StringBuilder();
            status.AppendLine($"• State: {_printer.State}");
            status.AppendLine($"• Progress: {_printer.PrintProgress}%");
            status.AppendLine($"• File: {(_printer.CurrentFileName != string.Empty ? _printer.CurrentFileName : "None")}");
            status.AppendLine($"• Bed Temp: {_printer.BedTemperature.Celsius:F1}°C");
            status.AppendLine($"• Nozzle Temp: {_printer.NozzleTemperature.Celsius:F1}°C");

#pragma warning disable CS0618 // LastReport is obsolete
            var report = _printer.LastReport;
#pragma warning restore CS0618

            // Layer info
            if (report.Print.TotalLayerNum > 0)
            {
                status.AppendLine($"• Layer: {report.Print.LayerNum}/{report.Print.TotalLayerNum}");
            }
            else
            {
                status.AppendLine($"• Layer: N/A");
            }

            // Remaining time
            if (report.Print.RemainTime > 0)
            {
                var remainingTime = TimeSpan.FromMinutes(report.Print.RemainTime);
                status.AppendLine($"• Remaining Time: {remainingTime.Hours}h {remainingTime.Minutes}m");
            }
            else
            {
                status.AppendLine($"• Remaining Time: N/A");
            }

            // AMS status
            if (report.Print.Ams.AmsList.Count > 0)
            {
                status.AppendLine($"• AMS: {report.Print.Ams.AmsList.Count} unit(s) detected");
            }

            _statusTextView.Text = status.ToString();
        }
        catch (Exception)
        {
            // No report received yet
            _statusTextView.Text = "Waiting for printer data...\n\nConnecting to printer and waiting for status reports.";
        }
    }
}
