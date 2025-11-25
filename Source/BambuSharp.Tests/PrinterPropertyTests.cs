using System.ComponentModel;

namespace BambuSharp.Tests;

public class PrinterPropertyTests
{
    private const string TestPrinterIp = "192.168.4.75";
    private const string TestPrinterAccessCode = "8f5537d0";

    [Fact]
    public void NewPrinter_ShouldHaveDefaultPropertyValues()
    {
        // Arrange & Act
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);

        // Assert - Verify default values before any reports are received
        Assert.Equal(default, printer.BedTemperature);
        Assert.Equal(default, printer.NozzleTemperature);
        Assert.Equal(0, printer.PrintProgress);
        Assert.Equal(string.Empty, printer.CurrentFileName);
        Assert.Equal(PrinterState.Idle, printer.State);
    }

    //[Fact(Skip = "This test requires access to an actual, physical printer")]
    [Fact]
    public async Task ConnectedPrinter_ShouldUpdateProperties()
    {
        // Arrange
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);

        // Act - Connect to the printer and wait for first report
        await printer.Connect();
        await Task.Delay(1000); // Give time for reports to arrive

        // Assert - Properties should be populated from the printer reports
        // We can't assert exact values since we don't know printer state,
        // but we can verify they're reasonable values
        Assert.NotEqual(default, printer.BedTemperature);
        Assert.NotEqual(default, printer.NozzleTemperature);

        // Progress should be 0-100 range
        Assert.InRange(printer.PrintProgress, 0, 100);

        // State should be a valid enum value
        Assert.True(Enum.IsDefined(typeof(PrinterState), printer.State));

        Console.WriteLine($"Bed Temperature: {printer.BedTemperature}");
        Console.WriteLine($"Nozzle Temperature: {printer.NozzleTemperature}");
        Console.WriteLine($"Print Progress: {printer.PrintProgress}%");
        Console.WriteLine($"Current File: {printer.CurrentFileName}");
        Console.WriteLine($"State: {printer.State}");

        await printer.Disconnect();
        printer.Dispose();
    }

    //[Fact(Skip = "This test requires access to an actual, physical printer")]
    [Fact]
    public async Task ConnectedPrinter_PropertyChangeEvents_ShouldFire()
    {
        // Arrange
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);
        var propertyChangedEvents = new List<string>();

        printer.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != null)
            {
                propertyChangedEvents.Add(args.PropertyName);
            }
        };

        // Act - Connect and wait for reports
        await printer.Connect();
        await Task.Delay(2000); // Wait for multiple reports

        // Assert - Verify PropertyChanged events were raised
        Assert.Contains(nameof(LocalPrinter.BedTemperature), propertyChangedEvents);
        Assert.Contains(nameof(LocalPrinter.NozzleTemperature), propertyChangedEvents);
        Assert.Contains(nameof(LocalPrinter.State), propertyChangedEvents);

        Console.WriteLine($"PropertyChanged events fired: {string.Join(", ", propertyChangedEvents.Distinct())}");
        Console.WriteLine($"Total events: {propertyChangedEvents.Count}");

        await printer.Disconnect();
        printer.Dispose();
    }

    //[Fact(Skip = "This test requires access to an actual, physical printer")]
    [Fact]
    public async Task ConnectedPrinter_PropertiesMatchLastReport()
    {
        // Arrange
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);

        // Act - Connect and wait for reports
        await printer.Connect();
        await Task.Delay(1000);

#pragma warning disable CS0618 // Type or member is obsolete
        var report = printer.LastReport;
#pragma warning restore CS0618

        // Assert - Verify properties match the report values
        Assert.Equal(report.Print.BedTemperature, printer.BedTemperature);
        Assert.Equal(report.Print.NozzleTemperature, printer.NozzleTemperature);
        Assert.Equal(report.Print.Percent, printer.PrintProgress);
        Assert.Equal(report.Print.GcodeFile, printer.CurrentFileName);
        Assert.Equal((PrinterState)report.Print.State, printer.State);

        await printer.Disconnect();
        printer.Dispose();
    }

    [Fact]
    public void Printer_ImplementsINotifyPropertyChanged()
    {
        // Arrange & Act
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);

        // Assert
        Assert.IsAssignableFrom<INotifyPropertyChanged>(printer);
    }

    //[Fact(Skip = "This test requires access to an actual, physical printer")]
    [Fact]
    public async Task ConnectionTest()
    {
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);
        await printer.Connect();

#pragma warning disable CS0618 // Type or member is obsolete
        // just read some props to make sure they exist
        var bedTemp = printer.LastReport.Print.BedTemperature;
#pragma warning restore CS0618

        // Wait for a report to be received
    }
}