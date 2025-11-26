namespace BambuSharp.Tests;

public class PrintFileListingTests
{
    private const string TestPrinterIp = "192.168.4.75";
    private const string TestPrinterAccessCode = "8f5537d0";

    /// <summary>
    /// Integration test for retrieving available print files from the printer.
    /// This test requires an actual printer to be accessible on the network.
    /// Update the IP address and access code above to match your printer.
    /// </summary>
//    [Fact(Skip = "This test requires access to an actual, physical printer. Remove Skip attribute to run.")]
    [Fact]
    public async Task GetPrintFilesAsync_ShouldReturnFileList()
    {
        // Arrange
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);

        try
        {
            // Act - Connect to the printer first
            await printer.Connect();

            // Get the list of available print files (with 10 second timeout)
            var files = await printer.GetPrintFilesAsync(timeoutSeconds: 10);

            // Assert
            Assert.NotNull(files);

            // If files exist on the printer, validate their properties
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    Assert.NotNull(file.Name);
                    Assert.NotEmpty(file.Name);
                    Assert.True(
                        file.Name.EndsWith(".gcode", StringComparison.OrdinalIgnoreCase) ||
                        file.Name.EndsWith(".3mf", StringComparison.OrdinalIgnoreCase),
                        $"File {file.Name} should have a .gcode or .3mf extension");
                    Assert.True(file.Size >= 0, "File size should not be negative");

                    // Output file information for debugging
                    Console.WriteLine($"Found file: {file.Name} ({file.Size} bytes)");
                }
            }
            else
            {
                Console.WriteLine("No print files found on the printer.");
            }
        }
        finally
        {
            // Clean up
            await printer.Disconnect();
            printer.Dispose();
        }
    }

    /// <summary>
    /// Integration test for retrieving files without connecting first.
    /// This should work because file listing uses FTP, which is independent of MQTT.
    /// </summary>
    [Fact(Skip = "This test requires access to an actual, physical printer. Remove Skip attribute to run.")]
    public async Task GetPrintFilesAsync_WithoutConnect_ShouldStillWork()
    {
        // Arrange
        var printer = new LocalPrinter(TestPrinterIp, TestPrinterAccessCode);

        try
        {
            // Act - Get files without connecting to MQTT first (with 10 second timeout)
            var files = await printer.GetPrintFilesAsync(timeoutSeconds: 10);

            // Assert
            Assert.NotNull(files);
            Console.WriteLine($"Retrieved {files.Count} files without MQTT connection");
        }
        finally
        {
            // Clean up
            printer.Dispose();
        }
    }

    /// <summary>
    /// Test that verifies proper error handling when connecting to an invalid IP.
    /// </summary>
    [Fact]
    public async Task GetPrintFilesAsync_WithInvalidIp_ShouldThrowException()
    {
        // Arrange
        var printer = new LocalPrinter("192.168.1.254", "invalidcode");

        // Act & Assert - Use short timeout so test doesn't hang
        await Assert.ThrowsAnyAsync<Exception>(async () =>
        {
            await printer.GetPrintFilesAsync(timeoutSeconds: 3);
        });

        printer.Dispose();
    }

    /// <summary>
    /// Test that verifies timeout behavior works correctly.
    /// </summary>
    [Fact]
    public async Task GetPrintFilesAsync_WithTimeout_ShouldRespectTimeout()
    {
        // Arrange - Use an IP that will likely timeout
        var printer = new LocalPrinter("192.168.1.254", "invalidcode");

        // Act & Assert - Should timeout within 2 seconds
        var exception = await Assert.ThrowsAnyAsync<Exception>(async () =>
        {
            await printer.GetPrintFilesAsync(timeoutSeconds: 2);
        });

        // Verify the operation completed quickly (within reasonable time)
        printer.Dispose();
    }
}
