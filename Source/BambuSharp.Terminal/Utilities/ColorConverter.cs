using Terminal.Gui;

namespace BambuSharp.UI.Utilities;

/// <summary>
/// Converts hex color strings to Terminal.Gui colors.
/// </summary>
public static class ColorConverter
{
    /// <summary>
    /// Gets a contrasting background color for a hex color string.
    /// </summary>
    /// <param name="hexColor">The hex color string (with or without # prefix).</param>
    /// <returns>Either Black or White, whichever provides better contrast.</returns>
    public static Color GetContrastingBackgroundForHex(string hexColor)
    {
        if (string.IsNullOrWhiteSpace(hexColor))
            return Color.Black;

        // Remove # prefix if present
        hexColor = hexColor.TrimStart('#');

        if (hexColor.Length < 6)
            return Color.Black;

        try
        {
            int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);

            return GetContrastingBackground(r, g, b);
        }
        catch
        {
            return Color.Black;
        }
    }

    /// <summary>
    /// Converts a hex color string (e.g., "#FF0000" or "FF0000") to the nearest Terminal.Gui Color.
    /// </summary>
    /// <param name="hexColor">The hex color string (with or without # prefix).</param>
    /// <returns>The nearest Terminal.Gui Color, or White if parsing fails.</returns>
    public static Color HexToTerminalColor(string hexColor)
    {
        if (string.IsNullOrWhiteSpace(hexColor))
            return Color.White;

        // Remove # prefix if present
        hexColor = hexColor.TrimStart('#');

        // Parse RGB values
        if (hexColor.Length < 6)
            return Color.White;

        try
        {
            int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);

            return RgbToTerminalColor(r, g, b);
        }
        catch
        {
            return Color.White;
        }
    }

    /// <summary>
    /// Determines if a color is "light" (high brightness) or "dark" (low brightness).
    /// </summary>
    /// <param name="r">Red component (0-255)</param>
    /// <param name="g">Green component (0-255)</param>
    /// <param name="b">Blue component (0-255)</param>
    /// <returns>True if the color is light, false if dark.</returns>
    private static bool IsLightColor(int r, int g, int b)
    {
        // Calculate perceived brightness using the standard formula
        // https://www.w3.org/TR/AERT/#color-contrast
        double brightness = (r * 0.299 + g * 0.587 + b * 0.114);
        return brightness > 128; // Threshold at middle brightness
    }

    /// <summary>
    /// Gets a contrasting background color for the given RGB values.
    /// </summary>
    public static Color GetContrastingBackground(int r, int g, int b)
    {
        return IsLightColor(r, g, b) ? Color.Black : Color.White;
    }

    /// <summary>
    /// Converts RGB values to the nearest Terminal.Gui Color using Euclidean distance.
    /// </summary>
    private static Color RgbToTerminalColor(int r, int g, int b)
    {
        // Terminal.Gui basic 16 colors with approximate RGB values
        var colorMap = new[]
        {
            (Color.Black, 0, 0, 0),
            (Color.Blue, 0, 0, 128),
            (Color.Green, 0, 128, 0),
            (Color.Cyan, 0, 128, 128),
            (Color.Red, 128, 0, 0),
            (Color.Magenta, 128, 0, 128),
            (Color.Brown, 128, 128, 0),
            (Color.Gray, 192, 192, 192),
            (Color.DarkGray, 128, 128, 128),
            (Color.BrightBlue, 0, 0, 255),
            (Color.BrightGreen, 0, 255, 0),
            (Color.BrightCyan, 0, 255, 255),
            (Color.BrightRed, 255, 0, 0),
            (Color.BrightMagenta, 255, 0, 255),
            (Color.BrightYellow, 255, 255, 0),
            (Color.White, 255, 255, 255)
        };

        // Find the closest color using Euclidean distance
        Color closestColor = Color.White;
        double minDistance = double.MaxValue;

        foreach (var (color, cr, cg, cb) in colorMap)
        {
            double distance = Math.Sqrt(
                Math.Pow(r - cr, 2) +
                Math.Pow(g - cg, 2) +
                Math.Pow(b - cb, 2)
            );

            if (distance < minDistance)
            {
                minDistance = distance;
                closestColor = color;
            }
        }

        return closestColor;
    }

    /// <summary>
    /// Gets the ANSI escape code for the specified Terminal.Gui color (foreground).
    /// </summary>
    private static string GetAnsiColorCode(Color color)
    {
        var code = color switch
        {
            Color.Black => "30",
            Color.Red => "31",
            Color.Green => "32",
            Color.Brown => "33",
            Color.Blue => "34",
            Color.Magenta => "35",
            Color.Cyan => "36",
            Color.Gray => "37",
            Color.DarkGray => "90",
            Color.BrightRed => "91",
            Color.BrightGreen => "92",
            Color.BrightYellow => "93",
            Color.BrightBlue => "94",
            Color.BrightMagenta => "95",
            Color.BrightCyan => "96",
            Color.White => "97",
            _ => "97" // Default to white
        };
        return code;
    }

    /// <summary>
    /// Gets a friendly color name for display.
    /// </summary>
    private static string GetColorName(Color color)
    {
        return color switch
        {
            Color.Black => "Black",
            Color.Red => "Red",
            Color.Green => "Green",
            Color.Brown => "Brown",
            Color.Blue => "Blue",
            Color.Magenta => "Magenta",
            Color.Cyan => "Cyan",
            Color.Gray => "Gray",
            Color.DarkGray => "DkGray",
            Color.BrightRed => "BrightRed",
            Color.BrightGreen => "BrightGreen",
            Color.BrightYellow => "Yellow",
            Color.BrightBlue => "BrightBlue",
            Color.BrightMagenta => "BrightMagenta",
            Color.BrightCyan => "BrightCyan",
            Color.White => "White",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a string with a block character and color name indicator.
    /// Note: Terminal.Gui TreeView doesn't support ANSI color codes, so we show the color name instead.
    /// </summary>
    /// <param name="hexColor">The hex color for the block.</param>
    /// <returns>A string containing a block character with color name.</returns>
    public static string GetColoredBlock(string hexColor)
    {
        if (string.IsNullOrWhiteSpace(hexColor))
            return "█ ";

        var color = HexToTerminalColor(hexColor);
        var colorName = GetColorName(color);

        // Return block with color name since TreeView doesn't support ANSI codes
        return $"█ [{colorName}] ";
    }
}
