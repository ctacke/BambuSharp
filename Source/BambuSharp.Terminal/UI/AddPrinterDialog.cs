using BambuSharp.Terminal.Configuration;
using System.Net;
using Terminal.Gui;

namespace BambuSharp.Terminal.UI;

/// <summary>
/// Dialog for adding a new printer configuration.
/// </summary>
public class AddPrinterDialog : Dialog
{
    private readonly TextField _nameField;
    private readonly TextField _ipAddressField;
    private readonly TextField _accessCodeField;

    public PrinterConfig? Result { get; private set; }

    public AddPrinterDialog()
    {
        Title = "Add Printer";
        Width = 50;
        Height = 12;

        // Name field
        var nameLabel = new Label("Printer Name:")
        {
            X = 1,
            Y = 1
        };
        _nameField = new TextField("")
        {
            X = 1,
            Y = 2,
            Width = Dim.Fill() - 1
        };
        Add(nameLabel, _nameField);

        // IP Address field
        var ipLabel = new Label("IP Address:")
        {
            X = 1,
            Y = 4
        };
        _ipAddressField = new TextField("")
        {
            X = 1,
            Y = 5,
            Width = Dim.Fill() - 1
        };
        Add(ipLabel, _ipAddressField);

        // Access Code field
        var accessCodeLabel = new Label("Access Code:")
        {
            X = 1,
            Y = 7
        };
        _accessCodeField = new TextField("")
        {
            X = 1,
            Y = 8,
            Width = Dim.Fill() - 1,
            Secret = true  // Hide the access code input
        };
        Add(accessCodeLabel, _accessCodeField);

        // OK button
        var okButton = new Button("OK")
        {
            X = Pos.Center() - 10,
            Y = Pos.Bottom(this) - 4,
            IsDefault = true
        };
        okButton.Clicked += OnOkClicked;

        // Cancel button
        var cancelButton = new Button("Cancel")
        {
            X = Pos.Center() + 2,
            Y = Pos.Bottom(this) - 4
        };
        cancelButton.Clicked += OnCancelClicked;

        AddButton(okButton);
        AddButton(cancelButton);
    }

    private void OnOkClicked()
    {
        var name = _nameField.Text.ToString()?.Trim() ?? string.Empty;
        var ipAddress = _ipAddressField.Text.ToString()?.Trim() ?? string.Empty;
        var accessCode = _accessCodeField.Text.ToString()?.Trim() ?? string.Empty;

        // Validate inputs
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.ErrorQuery("Validation Error", "Printer name is required.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            MessageBox.ErrorQuery("Validation Error", "IP address is required.", "OK");
            return;
        }

        if (!IsValidIpAddress(ipAddress))
        {
            MessageBox.ErrorQuery("Validation Error", "Invalid IP address format.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(accessCode))
        {
            MessageBox.ErrorQuery("Validation Error", "Access code is required.", "OK");
            return;
        }

        // Create result
        Result = new PrinterConfig
        {
            Name = name,
            IpAddress = ipAddress,
            AccessCode = accessCode
        };

        Application.RequestStop();
    }

    private void OnCancelClicked()
    {
        Result = null;
        Application.RequestStop();
    }

    private static bool IsValidIpAddress(string ipAddress)
    {
        return IPAddress.TryParse(ipAddress, out _);
    }
}
