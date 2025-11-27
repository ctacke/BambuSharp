# BambuSharp

![](./Docs/bambu-sharp-logo.png)

An open source .NET API for Bambu Lab printers.

## Overview

BambuSharp is a comprehensive .NET library for communicating with Bambu Lab 3D printers over the local network. It provides real-time monitoring of printer status, temperatures, print progress, AMS (Automatic Material System) units, and much more through an easy-to-use API with full `INotifyPropertyChanged` support.

## Features

- **Real-time Printer Monitoring** - Live updates via MQTT connection
- **Comprehensive Status Information**
  - Print progress, current file, layer information
  - Bed and nozzle temperatures
  - Extruder details with accurate temperature conversion
  - Nozzle specifications (diameter, type, wear percentage)
  - AMS units with filament information (type, color, remaining length)
  - IP camera settings and resolution
  - Light status (chamber light, work light, etc.)
  - Network interfaces and IP addresses
  - AI features (spaghetti detector, first layer inspector, etc.)
  - File upload status and progress
  - Cloud connectivity status
- **Terminal UI** - Cross-platform terminal-based monitoring interface with colored filament display
- **INotifyPropertyChanged Support** - Easy integration with MVVM applications
- **Type-Safe API** - Strongly-typed properties with units (using Meadow.Units for temperatures)

## Installation

```bash
dotnet add package BambuSharp
```

## Quick Start

### Basic Connection and Monitoring

```csharp
using BambuSharp;

// Create a printer instance with IP address and access code
var printer = new LocalPrinter("192.168.1.100", "your-access-code");

// Subscribe to property changes
printer.PropertyChanged += (sender, e) =>
{
    Console.WriteLine($"Property changed: {e.PropertyName}");
};

// Connect to the printer
await printer.Connect();

// Access printer status
Console.WriteLine($"State: {printer.State}");
Console.WriteLine($"Progress: {printer.PrintProgress}%");
Console.WriteLine($"Current File: {printer.CurrentFileName}");
Console.WriteLine($"Bed Temperature: {printer.BedTemperature.Celsius:F1}°C");
Console.WriteLine($"Nozzle Temperature: {printer.NozzleTemperature.Celsius:F1}°C");
Console.WriteLine($"Layer: {printer.CurrentLayer}/{printer.TotalLayers}");

// Access AMS information
foreach (var ams in printer.AmsUnits)
{
    Console.WriteLine($"AMS Unit {ams.Id}: {ams.Humidity}% humidity");
    foreach (var tray in ams.Trays)
    {
        Console.WriteLine($"  Tray {tray.Id}: {tray.FilamentType} - {tray.Color}");
        Console.WriteLine($"    Remaining: {tray.Remaining}mm of {tray.TotalLength}mm");
    }
}

// Disconnect when done
await printer.Disconnect();
printer.Dispose();
```

## Available Properties

### Core Status
- `State` - Current printer state (Idle, Printing, Paused, etc.)
- `PrintProgress` - Print completion percentage (0-100)
- `CurrentFileName` - Name of the currently loaded/printing file
- `CurrentLayer` / `TotalLayers` - Layer progress information
- `RemainingMinutes` - Estimated time remaining

### Temperature Monitoring
- `BedTemperature` - Current heated bed temperature
- `NozzleTemperature` - Current nozzle temperature
- `Extruder` - Detailed extruder information including:
  - `CurrentTemperature` - Current nozzle temperature (device-level)
  - `TargetTemperature` - Target nozzle temperature
  - `FilamentTemperature` - Filament temperature
  - `Status` - Extruder status code

### Hardware Information
- `Nozzle` - Nozzle specifications:
  - `Diameter` - Nozzle diameter (e.g., 0.4mm)
  - `Type` - Nozzle type (e.g., "HX01")
  - `WearPercent` - Nozzle wear percentage
- `IpCamera` - Camera settings:
  - `Resolution` - Camera resolution
  - `RecordingSetting` - Recording configuration
- `Lights` - List of lights (chamber, work, etc.) with on/off status
- `Networks` - Network interfaces with IP addresses and subnet masks

### AMS (Automatic Material System)
- `AmsUnits` - List of AMS units, each containing:
  - Humidity and temperature
  - Trays with filament information (type, color, remaining length, temperature settings)

### Advanced Features
- `AI` - AI camera features:
  - `PrintingMonitor` - AI monitoring enabled/disabled
  - `FirstLayerInspector` - First layer inspection
  - `SpaghettiDetector` - Spaghetti failure detection
  - `BuildPlateMarkerDetector` - Build plate marker detection
  - `PrintHalt` - Automatic print halt on detection
  - `HaltPrintSensitivity` - Sensitivity level
  - `AllowSkipParts` - Allow skipping detected issues
- `UploadStatus` - File upload progress:
  - `Status` - Upload status
  - `Progress` - Upload percentage
  - `Speed` - Upload speed
  - `FileSize` / `FinishSize` - File size information
  - `TimeRemaining` - Estimated upload time remaining
- `Files` - List of available print files on printer storage
- `CloudStatus` - Cloud connectivity:
  - `BambuLabCloudConnected` - Bambu Lab cloud connection status
  - `ExternalCloudConnected` - External cloud connection status
  - `Version` - Protocol version

## Terminal UI

BambuSharp includes a cross-platform terminal-based monitoring interface built with Terminal.Gui:

- Real-time expandable tree view of all printer properties
- Colored filament display for AMS trays with high-contrast backgrounds
- Live updates without page refreshes
- Navigate with keyboard (arrow keys to expand/collapse)

![Terminal UI showing printer status, temperatures, AMS units with colored filaments, and more](bambu-sharp.gif)

## Technical Details

### MQTT Communication

BambuSharp communicates with Bambu Lab printers using MQTT over the local network:
- Real-time status updates (~1 second intervals)
- Automatic JSON deserialization with custom converters
- Reliable connection management

## Requirements

- .NET 8.0 or later
- Network access to Bambu Lab printer
- Printer access code (found in printer settings)

## Acknowledgments

- Built with [Meadow.Units](https://github.com/WildernessLabs/Meadow.Units) for type-safe unit handling
- Terminal UI powered by [Terminal.Gui](https://github.com/gui-cs/Terminal.Gui)
- MQTT communication via [MQTTnet](https://github.com/dotnet/MQTTnet)
