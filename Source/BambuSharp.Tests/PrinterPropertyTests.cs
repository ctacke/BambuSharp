namespace BambuSharp.Tests;

public class PrinterPropertyTests
{
    private const string TestPrinterIp = "192.168.4.75";
    private const string TestPrinterAccessCode = "8f5537d0";

    //[Fact(Skip = "This test requires access to an actual, physical printer")]
    [Fact]
    public async Task ConnectionTest()
    {
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);
        await printer.Connect();

        // just read some props to make sure they exist
        var bedTemp = printer.LastReport.Print.BedTemperature;

        // Wait for a report to be received
    }
}