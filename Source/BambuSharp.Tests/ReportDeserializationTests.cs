using System.Text.Json;

namespace BambuSharp.Tests;

/// <summary>
/// Tests for deserializing the Report object and its nested entities from JSON.
/// </summary>
public class ReportDeserializationTests
{
    private readonly ReportInternal _report;

    public ReportDeserializationTests()
    {
        // Load and deserialize the sample JSON once for all tests
        var jsonPath = Path.Combine("Inputs", "ReportSample.json");
        var json = File.ReadAllText(jsonPath);
        _report = JsonSerializer.Deserialize<ReportInternal>(json) ?? throw new InvalidOperationException("Failed to deserialize Report");
    }

    [Fact]
    public void Report_ShouldDeserialize()
    {
        Assert.NotNull(_report);
        Assert.NotNull(_report.Print);
    }

    [Fact]
    public void Print_ShouldHaveBasicProperties()
    {
        var print = _report.Print;

        Assert.Equal("push_status", print.Command);
        Assert.Equal(24.0, print.BedTemperature.Celsius);
        Assert.Equal(0.0, print.BedTargetTemperature.Celsius);
        Assert.Equal(28.0, print.NozzleTemperature.Celsius);
        Assert.Equal(0.0, print.NozzleTargetTemperature.Celsius);
        Assert.Equal("0.4", print.NozzleDiameter);
        Assert.Equal("HX01", print.NozzleType);
        Assert.Equal("-61dBm", print.WifiSignal);
        Assert.True(print.Sdcard);
    }

    [Fact]
    public void Print_ShouldHavePrintJobInformation()
    {
        var print = _report.Print;

        Assert.Equal(63, print.LayerNum);
        Assert.Equal(63, print.TotalLayerNum);
        Assert.Equal(100, print.Percent);
        Assert.Equal(6, print.State);
        Assert.Equal("FINISH", print.GcodeState);
        Assert.Equal("/data/Metadata/plate_1.gcode", print.GcodeFile);
        Assert.Equal("Rpi_4_Case_Bottom.stl + Rpi_4_Case_Top.stl", print.SubtaskName);
    }

    [Fact]
    public void Print3D_ShouldHaveLayerInformation()
    {
        var print3D = _report.Print.LayerInfo;

        Assert.NotNull(print3D);
        Assert.Equal(63, print3D.LayerNum);
        Assert.Equal(63, print3D.TotalLayerNum);
    }

    [Fact]
    public void AmsSystem_ShouldDeserialize()
    {
        var ams = _report.Print.Ams;

        Assert.NotNull(ams);
        Assert.Single(ams.AmsList);
        Assert.Equal("1", ams.AmsExistBits);
        Assert.True(ams.InsertFlag);
        Assert.False(ams.PowerOnFlag);
        Assert.Equal("f", ams.TrayExistBits);
    }

    [Fact]
    public void Ams_ShouldHavePropertiesAndTrays()
    {
        var amsUnit = _report.Print.Ams.AmsList.First();

        Assert.NotNull(amsUnit);
        Assert.Equal("0", amsUnit.Id);
        Assert.Equal("2", amsUnit.Humidity);
        Assert.Equal(27.9, amsUnit.Temperature.Celsius);
        Assert.Equal(4, amsUnit.Tray.Count);
    }

    [Fact]
    public void Tray_FirstTray_ShouldHaveCorrectProperties()
    {
        var firstTray = _report.Print.Ams.AmsList.First().Tray.First();

        Assert.NotNull(firstTray);
        Assert.Equal("0", firstTray.Id);
        Assert.Equal("PETG-CF", firstTray.TrayType);
        Assert.Equal("PETG-CF", firstTray.TraySubBrands);
        Assert.Equal("565656FF", firstTray.TrayColor);
        Assert.Equal("1.75", firstTray.TrayDiameter);
        Assert.Equal("G50-D6", firstTray.TrayIdName);
        Assert.Equal(240, firstTray.NozzleTempMin.Celsius);
        Assert.Equal(270, firstTray.NozzleTempMax.Celsius);
        Assert.Equal(11, firstTray.State);
        Assert.Equal(330000, firstTray.TotalLen);
    }

    [Fact]
    public void Tray_SecondTray_ShouldBePETG()
    {
        var secondTray = _report.Print.Ams.AmsList.First().Tray[1];

        Assert.Equal("1", secondTray.Id);
        Assert.Equal("PETG", secondTray.TrayType);
        Assert.Equal("PETG HF", secondTray.TraySubBrands);
        Assert.Equal("1F79E5FF", secondTray.TrayColor);
        Assert.Equal(230, secondTray.NozzleTempMin.Celsius);
        Assert.Equal(260, secondTray.NozzleTempMax.Celsius);
    }

    [Fact]
    public void Device_ShouldDeserialize()
    {
        var device = _report.Print.Device;

        Assert.NotNull(device);
        Assert.Equal(1, device.Type);
        Assert.Equal(24, device.BedTemperature.Celsius);
    }

    [Fact]
    public void BedDevice_ShouldHaveTemperatureAndState()
    {
        var bed = _report.Print.Device.Bed;

        Assert.NotNull(bed);
        Assert.Equal(0, bed.State);
        Assert.NotNull(bed.Info);
        Assert.Equal(24, bed.Info.Temperature.Celsius);
    }

    [Fact]
    public void ExtruderDevice_ShouldHaveExtruderInfo()
    {
        var extruder = _report.Print.Device.Extruder;

        Assert.NotNull(extruder);
        Assert.Equal(1, extruder.State);
        Assert.Single(extruder.Info);

        var extruderInfo = extruder.Info.First();
        Assert.Equal(0, extruderInfo.Id);
        Assert.Equal(28, extruderInfo.Temperature.Celsius);
        Assert.Equal(0, extruderInfo.Htar);
        Assert.Equal(0, extruderInfo.Hnow);
    }

    [Fact]
    public void NozzleDevice_ShouldHaveNozzleInfo()
    {
        var nozzle = _report.Print.Device.Nozzle;

        Assert.NotNull(nozzle);
        Assert.Equal(1, nozzle.Exist);
        Assert.Equal(0, nozzle.State);
        Assert.Single(nozzle.Info);

        var nozzleInfo = nozzle.Info.First();
        Assert.Equal(0, nozzleInfo.Id);
        Assert.Equal(0.4, nozzleInfo.Diameter);
        Assert.Equal("HX01", nozzleInfo.Type);
        Assert.Equal(0, nozzleInfo.Wear);
    }

    [Fact]
    public void Job_ShouldHaveStageInformation()
    {
        var job = _report.Print.Job;

        Assert.NotNull(job);
        Assert.NotNull(job.CurStage);
        Assert.Equal(0, job.CurStage.Idx);
        Assert.Equal(0, job.CurStage.State);

        Assert.Single(job.Stage);
        var stage = job.Stage.First();
        Assert.Equal(0, stage.Idx);
        Assert.Equal(2, stage.Type);
        Assert.Single(stage.Tool);
        Assert.Equal("HS01", stage.Tool.First());
    }

    [Fact]
    public void IpCam_ShouldHaveConfiguration()
    {
        var ipcam = _report.Print.IpCam;

        Assert.NotNull(ipcam);
        Assert.Equal("disable", ipcam.AgoraService);
        Assert.Equal("enable", ipcam.BrtcService);
        Assert.Equal("enable", ipcam.IpcamRecord);
        Assert.Equal("720p", ipcam.Resolution);
        Assert.Equal("disable", ipcam.Timelapse);
    }

    [Fact]
    public void XCam_ShouldHaveAIFeatures()
    {
        var xcam = _report.Print.XCam;

        Assert.NotNull(xcam);
        Assert.True(xcam.SpaghettiDetector);
        Assert.True(xcam.FirstLayerInspector);
        Assert.True(xcam.PrintHalt);
        Assert.True(xcam.BuildplateMarkerDetector);
        Assert.True(xcam.PrintingMonitor);
        Assert.Equal("medium", xcam.HaltPrintSensitivity);
        Assert.False(xcam.AllowSkipParts);
    }

    [Fact]
    public void LightsReport_ShouldHaveTwoLights()
    {
        var lights = _report.Print.LightsReport;

        Assert.NotNull(lights);
        Assert.Equal(2, lights.Count);

        var chamberLight = lights.First(l => l.Node == "chamber_light");
        Assert.Equal("on", chamberLight.Mode);

        var workLight = lights.First(l => l.Node == "work_light");
        Assert.Equal("flashing", workLight.Mode);
    }

    [Fact]
    public void Hms_ShouldHaveErrorCodes()
    {
        var hms = _report.Print.Hms;

        Assert.NotNull(hms);
        Assert.Single(hms);

        var error = hms.First();
        Assert.Equal(131073, error.Code);
        Assert.Equal(201327360, error.Attr);
    }

    [Fact]
    public void Care_ShouldHaveMaintenanceInfo()
    {
        var care = _report.Print.Care;

        Assert.NotNull(care);
        Assert.Equal(2, care.Count);

        var cr = care.First(c => c.Id == "cr");
        Assert.Equal("20631064", cr.Info);

        var ls = care.First(c => c.Id == "ls");
        Assert.Equal("2061918", ls.Info);
    }

    [Fact]
    public void Network_ShouldHaveNetworkInfo()
    {
        var net = _report.Print.Net;

        Assert.NotNull(net);
        Assert.Equal(16, net.Conf);
        Assert.Equal(2, net.Info.Count);
    }

    [Fact]
    public void NetworkInfo_ShouldConvertToIPAddress()
    {
        var net = _report.Print.Net;
        var firstInterface = net.Info.First();

        // The JSON value 1258596544 should be converted to an IPAddress
        Assert.NotNull(firstInterface.IpAddress);
        Assert.NotNull(firstInterface.SubnetMask);

        // Verify the conversion worked
        Assert.NotEqual(System.Net.IPAddress.None, firstInterface.IpAddress);

        // Verify it's a valid IPv4 address
        var ipString = firstInterface.IpAddress.ToString();
        Assert.NotEmpty(ipString);
        Assert.Contains(".", ipString);

        // The IP should have 4 octets
        var parts = ipString.Split('.');
        Assert.Equal(4, parts.Length);
    }

    [Fact]
    public void NetworkInfo_ZeroIP_ShouldBeNull()
    {
        var net = _report.Print.Net;
        var secondInterface = net.Info[1];

        // When IP and mask are 0 in JSON, they should deserialize as null
        Assert.Null(secondInterface.IpAddress);
        Assert.Null(secondInterface.SubnetMask);
    }

    [Fact]
    public void Network_IsConnected_ShouldReturnTrue()
    {
        var net = _report.Print.Net;

        Assert.True(net.IsConnected());
    }

    [Fact]
    public void Network_GetActiveInterfaces_ShouldReturnOne()
    {
        var net = _report.Print.Net;
        var activeInterfaces = net.GetActiveInterfaces().ToList();

        Assert.Single(activeInterfaces);
        Assert.True(activeInterfaces[0].IsConfigured());
    }

    [Fact]
    public void NetworkInfo_ToString_ShouldFormatCorrectly()
    {
        var net = _report.Print.Net;
        var firstInterface = net.Info.First();

        var toString = firstInterface.ToString();
        Assert.NotNull(toString);
        Assert.Contains("/", toString);
    }

    [Fact]
    public void Online_ShouldHaveCloudStatus()
    {
        var online = _report.Print.Online;

        Assert.NotNull(online);
        Assert.False(online.Ahb);
        Assert.False(online.Ext);
        Assert.Equal(4, online.Version);
    }

    [Fact]
    public void UpgradeState_ShouldHaveFirmwareInfo()
    {
        var upgrade = _report.Print.UpgradeState;

        Assert.NotNull(upgrade);
        Assert.Equal("IDLE", upgrade.Status);
        Assert.Equal("00M09D552901316", upgrade.Sn);
        Assert.Equal(2, upgrade.NewVersionState);
        Assert.False(upgrade.ForceUpgrade);
        Assert.Equal("0", upgrade.Progress);
    }

    [Fact]
    public void Upload_ShouldHaveUploadStatus()
    {
        var upload = _report.Print.Upload;

        Assert.NotNull(upload);
        Assert.Equal("idle", upload.Status);
        Assert.Equal("Good", upload.Message);
        Assert.Equal(0, upload.FileSize);
        Assert.Equal(0, upload.FinishSize);
        Assert.Equal(0, upload.Progress);
    }

    [Fact]
    public void VirtualTray_ShouldDeserialize()
    {
        var vtTray = _report.Print.CurrentVirtualTray;

        Assert.NotNull(vtTray);
        Assert.Equal("255", vtTray.Id);
        Assert.Equal("00000000", vtTray.TrayColor);
        Assert.Equal("1.75", vtTray.TrayDiameter);
    }

    [Fact]
    public void VirtualSlot_ShouldHaveOneTray()
    {
        var virSlot = _report.Print.VirtualTrays;

        Assert.NotNull(virSlot);
        Assert.Single(virSlot);

        var slot = virSlot.First();
        Assert.Equal("255", slot.Id);
        Assert.Equal("1.75", slot.TrayDiameter);
    }

    [Fact]
    public void FanSpeeds_ShouldBeStrings()
    {
        var print = _report.Print;

        Assert.Equal("0", print.BigFan1Speed);
        Assert.Equal("0", print.BigFan2Speed);
        Assert.Equal("0", print.CoolingFanSpeed);
        Assert.Equal("0", print.HeatbreakFanSpeed);
    }

    [Fact]
    public void PlateDevice_ShouldHavePlateInfo()
    {
        var plate = _report.Print.Device.Plate;

        Assert.NotNull(plate);
        Assert.Equal(1, plate.Base);
        Assert.Equal(1, plate.Material);
        Assert.Equal("", plate.CurId);
        Assert.Equal("", plate.TarId);
    }

    #region Enum Tests

    [Fact]
    public void GCodeState_ShouldParseToEnum()
    {
        var gcodeState = _report.Print.GetGCodeState();

        Assert.Equal(GCodeState.Finish, gcodeState);
    }

    [Fact]
    public void PrinterState_ShouldConvertToEnum()
    {
        var printerState = _report.Print.GetPrinterState();

        Assert.NotNull(printerState);
        Assert.Equal(PrinterState.Finished, printerState.Value);
    }

    [Fact]
    public void SpeedProfile_ShouldConvertToEnum()
    {
        var speedProfile = _report.Print.GetSpeedProfile();

        Assert.NotNull(speedProfile);
        Assert.Equal(SpeedProfile.Standard, speedProfile.Value);
    }

    [Fact]
    public void Print_IsComplete_ShouldReturnTrue()
    {
        Assert.True(_report.Print.IsComplete());
        Assert.False(_report.Print.IsPrinting());
        Assert.False(_report.Print.IsIdle());
    }

    [Fact]
    public void EnumHelpers_GCodeState_ShouldParseAllStates()
    {
        Assert.Equal(GCodeState.Idle, EnumHelpers.ParseGCodeState("IDLE"));
        Assert.Equal(GCodeState.Running, EnumHelpers.ParseGCodeState("RUNNING"));
        Assert.Equal(GCodeState.Pause, EnumHelpers.ParseGCodeState("PAUSE"));
        Assert.Equal(GCodeState.Finish, EnumHelpers.ParseGCodeState("FINISH"));
        Assert.Equal(GCodeState.Failed, EnumHelpers.ParseGCodeState("FAILED"));
        Assert.Equal(GCodeState.Prepare, EnumHelpers.ParseGCodeState("PREPARE"));
        Assert.Equal(GCodeState.Unknown, EnumHelpers.ParseGCodeState("INVALID"));
        Assert.Equal(GCodeState.Unknown, EnumHelpers.ParseGCodeState(null));
    }

    [Fact]
    public void EnumHelpers_SpeedProfile_ShouldMapCorrectly()
    {
        Assert.Equal(SpeedProfile.Silent, EnumHelpers.GetSpeedProfile(1));
        Assert.Equal(SpeedProfile.Standard, EnumHelpers.GetSpeedProfile(2));
        Assert.Equal(SpeedProfile.Sport, EnumHelpers.GetSpeedProfile(3));
        Assert.Equal(SpeedProfile.Ludicrous, EnumHelpers.GetSpeedProfile(4));
        Assert.Null(EnumHelpers.GetSpeedProfile(99));
    }

    [Fact]
    public void EnumHelpers_HmsSeverity_ShouldMapCorrectly()
    {
        Assert.Equal(HmsSeverity.Fatal, EnumHelpers.GetHmsSeverity(1));
        Assert.Equal(HmsSeverity.Serious, EnumHelpers.GetHmsSeverity(2));
        Assert.Equal(HmsSeverity.Common, EnumHelpers.GetHmsSeverity(3));
        Assert.Equal(HmsSeverity.Info, EnumHelpers.GetHmsSeverity(4));
        Assert.Null(EnumHelpers.GetHmsSeverity(99));
    }

    [Fact]
    public void EnumHelpers_HmsModule_ShouldMapCorrectly()
    {
        Assert.Equal(HmsModule.MotionController, EnumHelpers.GetHmsModule(0x03));
        Assert.Equal(HmsModule.Mainboard, EnumHelpers.GetHmsModule(0x05));
        Assert.Equal(HmsModule.Ams, EnumHelpers.GetHmsModule(0x07));
        Assert.Equal(HmsModule.Toolhead, EnumHelpers.GetHmsModule(0x08));
        Assert.Equal(HmsModule.XCam, EnumHelpers.GetHmsModule(0x0C));
        Assert.Null(EnumHelpers.GetHmsModule(0xFF));
    }

    #endregion

    #region Temperature Tests

    [Fact]
    public void Temperature_ShouldDeserializeFromJson()
    {
        var print = _report.Print;

        // Verify temperatures are deserialized correctly
        Assert.NotEqual(default, print.BedTemperature);
        Assert.NotEqual(default, print.NozzleTemperature);
    }

    [Fact]
    public void Temperature_ShouldSupportMultipleUnits()
    {
        var print = _report.Print;

        // Access temperature in Celsius
        var bedTempC = print.BedTemperature.Celsius;
        Assert.Equal(24.0, bedTempC);

        // Access temperature in Fahrenheit
        var bedTempF = print.BedTemperature.Fahrenheit;
        Assert.Equal(75.2, bedTempF, 0.1); // Allow small tolerance for floating point

        // Access temperature in Kelvin
        var bedTempK = print.BedTemperature.Kelvin;
        Assert.Equal(297.15, bedTempK, 0.1);
    }

    [Fact]
    public void Temperature_ExtruderInfo_ShouldWork()
    {
        var extruderInfo = _report.Print.Device.Extruder.Info.First();

        Assert.Equal(28, extruderInfo.Temperature.Celsius);
        Assert.Equal(82.4, extruderInfo.Temperature.Fahrenheit, 0.1);
    }

    [Fact]
    public void Temperature_DeviceBed_ShouldWork()
    {
        var device = _report.Print.Device;

        Assert.Equal(24, device.BedTemperature.Celsius);
    }

    #endregion
}
