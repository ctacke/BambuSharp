using Meadow.Units;
using System.Text.Json.Serialization;

namespace BambuSharp;

/// <summary>
/// Contains comprehensive printer status information including hardware state, job progress, temperatures, and system configuration.
/// </summary>
public class Print
{
    /// <summary>
    /// Gets or sets the 3D printer status information.
    /// </summary>
    [JsonPropertyName("3D")]
    public PrintLayerInfo LayerInfo { get; set; } = new();

    /// <summary>
    /// Gets or sets the automatic material system (AMS) information.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: This property MUST be public for JSON deserialization to work.
    /// System.Text.Json only deserializes public properties by default.
    /// The type (AmsSystemInternal) is internal, which provides the encapsulation we want.
    /// </remarks>
    [JsonPropertyName("ams")]
    public AmsSystemInternal Ams { get; set; } = new();

    /// <summary>
    /// Gets or sets the AMS RFID status code.
    /// </summary>
    [JsonPropertyName("ams_rfid_status")]
    public int AmsRfidStatus { get; set; }

    /// <summary>
    /// Gets or sets the AMS status code.
    /// </summary>
    [JsonPropertyName("ams_status")]
    public int AmsStatus { get; set; }

    /// <summary>
    /// Gets or sets the access point error code.
    /// </summary>
    [JsonPropertyName("ap_err")]
    public int ApErr { get; set; }

    /// <summary>
    /// Gets or sets auxiliary printer information.
    /// </summary>
    [JsonPropertyName("aux")]
    public string Aux { get; set; } = "";

    /// <summary>
    /// Gets or sets a value indicating whether the auxiliary parts fan is active.
    /// </summary>
    [JsonPropertyName("aux_part_fan")]
    public bool AuxPartFan { get; set; }

    /// <summary>
    /// Gets or sets the batch identifier.
    /// </summary>
    [JsonPropertyName("batch_id")]
    public int BatchId { get; set; }

    /// <summary>
    /// Gets or sets the target heated bed temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("bed_target_temper")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature BedTargetTemperature { get; set; }

    /// <summary>
    /// Gets or sets the heated bed temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("bed_temper")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature BedTemperature { get; set; }

    /// <summary>
    /// Gets or sets the first large fan speed.
    /// </summary>
    [JsonPropertyName("big_fan1_speed")]
    public string BigFan1Speed { get; set; } = "";

    /// <summary>
    /// Gets or sets the second large fan speed.
    /// </summary>
    [JsonPropertyName("big_fan2_speed")]
    public string BigFan2Speed { get; set; } = "";

    /// <summary>
    /// Gets or sets the calibration version number.
    /// </summary>
    [JsonPropertyName("cali_version")]
    public int CaliVersion { get; set; }

    /// <summary>
    /// Gets or sets the canvas identifier.
    /// </summary>
    [JsonPropertyName("canvas_id")]
    public int CanvasId { get; set; }

    /// <summary>
    /// Gets or sets the collection of maintenance/care information.
    /// </summary>
    [JsonPropertyName("care")]
    public List<Care> Care { get; set; } = new();

    /// <summary>
    /// Gets or sets the configuration information.
    /// </summary>
    [JsonPropertyName("cfg")]
    public string Cfg { get; set; } = "";

    /// <summary>
    /// Gets or sets the command string.
    /// </summary>
    [JsonPropertyName("command")]
    public string Command { get; set; } = "";

    /// <summary>
    /// Gets or sets the cooling fan speed.
    /// </summary>
    [JsonPropertyName("cooling_fan_speed")]
    public string CoolingFanSpeed { get; set; } = "";

    /// <summary>
    /// Gets or sets the design identifier.
    /// </summary>
    [JsonPropertyName("design_id")]
    public string DesignId { get; set; } = "";

    /// <summary>
    /// Gets or sets the device information.
    /// </summary>
    [JsonPropertyName("device")]
    public Device Device { get; set; } = new();

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    [JsonPropertyName("err")]
    public string Err { get; set; } = "";

    /// <summary>
    /// Gets or sets the reason for print failure.
    /// </summary>
    [JsonPropertyName("fail_reason")]
    public string FailReason { get; set; } = "";

    /// <summary>
    /// Gets or sets the fan gear level.
    /// </summary>
    [JsonPropertyName("fan_gear")]
    public int FanGear { get; set; }

    /// <summary>
    /// Gets or sets the file name or path.
    /// </summary>
    [JsonPropertyName("file")]
    public string File { get; set; } = "";

    /// <summary>
    /// Gets or sets a value indicating whether a firmware upgrade is mandatory.
    /// </summary>
    [JsonPropertyName("force_upgrade")]
    public bool ForceUpgrade { get; set; }

    /// <summary>
    /// Gets or sets function-related information.
    /// </summary>
    [JsonPropertyName("fun")]
    public string Fun { get; set; } = "";

    /// <summary>
    /// Gets or sets the path to the G-code file being printed.
    /// </summary>
    [JsonPropertyName("gcode_file")]
    public string GcodeFile { get; set; } = "";

    /// <summary>
    /// Gets or sets the G-code file preparation completion percentage.
    /// </summary>
    [JsonPropertyName("gcode_file_prepare_percent")]
    public string GcodeFilePreparePercent { get; set; } = "";

    /// <summary>
    /// Gets or sets the G-code execution state.
    /// </summary>
    [JsonPropertyName("gcode_state")]
    public string GcodeState { get; set; } = "";

    /// <summary>
    /// Gets or sets the heatbreak fan speed.
    /// </summary>
    [JsonPropertyName("heatbreak_fan_speed")]
    public string HeatbreakFanSpeed { get; set; } = "";

    /// <summary>
    /// Gets or sets the collection of health management system (HMS) error records.
    /// </summary>
    [JsonPropertyName("hms")]
    public List<Hms> Hms { get; set; } = new();

    /// <summary>
    /// Gets or sets the home flag indicating homing status.
    /// </summary>
    [JsonPropertyName("home_flag")]
    public long HomeFlag { get; set; }

    /// <summary>
    /// Gets or sets the hardware switch state.
    /// </summary>
    [JsonPropertyName("hw_switch_state")]
    public int HwSwitchState { get; set; }

    /// <summary>
    /// Gets or sets the temperature information.
    /// </summary>
    [JsonPropertyName("info")]
    public TemperatureInfo Info { get; set; } = new();

    /// <summary>
    /// Gets or sets the IP camera information.
    /// </summary>
    [JsonPropertyName("ipcam")]
    public IpCam IpCam { get; set; } = new();

    /// <summary>
    /// Gets or sets the print job information.
    /// </summary>
    [JsonPropertyName("job")]
    public Job Job { get; set; } = new();

    /// <summary>
    /// Gets or sets the job attribute code.
    /// </summary>
    [JsonPropertyName("job_attr")]
    public int JobAttr { get; set; }

    /// <summary>
    /// Gets or sets the job identifier.
    /// </summary>
    [JsonPropertyName("job_id")]
    public string JobId { get; set; } = "";

    /// <summary>
    /// Gets or sets the current layer number being printed.
    /// </summary>
    [JsonPropertyName("layer_num")]
    public int LayerNum { get; set; }

    /// <summary>
    /// Gets or sets the collection of light status reports.
    /// </summary>
    [JsonPropertyName("lights_report")]
    public List<LightReport> LightsReport { get; set; } = new();

    /// <summary>
    /// Gets or sets the mapping configuration list.
    /// </summary>
    [JsonPropertyName("mapping")]
    public List<int> Mapping { get; set; } = new();

    /// <summary>
    /// Gets or sets the machine controller action code.
    /// </summary>
    [JsonPropertyName("mc_action")]
    public int McAction { get; set; }

    /// <summary>
    /// Gets or sets the machine controller error code.
    /// </summary>
    [JsonPropertyName("mc_err")]
    public int McErr { get; set; }

    /// <summary>
    /// Gets or sets the machine controller operation completion percentage.
    /// </summary>
    [JsonPropertyName("mc_percent")]
    public int McPercent { get; set; }

    /// <summary>
    /// Gets or sets the machine controller print error code.
    /// </summary>
    [JsonPropertyName("mc_print_error_code")]
    public string McPrintErrorCode { get; set; } = "";

    /// <summary>
    /// Gets or sets the machine controller print stage description.
    /// </summary>
    [JsonPropertyName("mc_print_stage")]
    public string McPrintStage { get; set; } = "";

    /// <summary>
    /// Gets or sets the machine controller print sub-stage code.
    /// </summary>
    [JsonPropertyName("mc_print_sub_stage")]
    public int McPrintSubStage { get; set; }

    /// <summary>
    /// Gets or sets the machine controller remaining time in minutes.
    /// </summary>
    [JsonPropertyName("mc_remaining_time")]
    public int McRemainingTime { get; set; }

    /// <summary>
    /// Gets or sets the machine controller stage code.
    /// </summary>
    [JsonPropertyName("mc_stage")]
    public int McStage { get; set; }

    /// <summary>
    /// Gets or sets the printer model identifier.
    /// </summary>
    [JsonPropertyName("model_id")]
    public string ModelId { get; set; } = "";

    /// <summary>
    /// Gets or sets the network connectivity information.
    /// </summary>
    [JsonPropertyName("net")]
    public Network Net { get; set; } = new();

    /// <summary>
    /// Gets or sets the nozzle diameter specification.
    /// </summary>
    [JsonPropertyName("nozzle_diameter")]
    public string NozzleDiameter { get; set; } = "";

    /// <summary>
    /// Gets or sets the target nozzle temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("nozzle_target_temper")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature NozzleTargetTemperature { get; set; }

    /// <summary>
    /// Gets or sets the nozzle temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("nozzle_temper")]
    [JsonConverter(typeof(TemperatureJsonConverter))]
    public Temperature NozzleTemperature { get; set; }

    /// <summary>
    /// Gets or sets the nozzle type.
    /// </summary>
    [JsonPropertyName("nozzle_type")]
    public string NozzleType { get; set; } = "";

    /// <summary>
    /// Gets or sets the online status information.
    /// </summary>
    [JsonPropertyName("online")]
    public Online Online { get; set; } = new();

    /// <summary>
    /// Gets or sets the print job completion percentage.
    /// </summary>
    [JsonPropertyName("percent")]
    public int Percent { get; set; }

    /// <summary>
    /// Gets or sets the number of available plates.
    /// </summary>
    [JsonPropertyName("plate_cnt")]
    public int PlateCnt { get; set; }

    /// <summary>
    /// Gets or sets the current plate identifier.
    /// </summary>
    [JsonPropertyName("plate_id")]
    public int PlateId { get; set; }

    /// <summary>
    /// Gets or sets the current plate index.
    /// </summary>
    [JsonPropertyName("plate_idx")]
    public int PlateIdx { get; set; }

    /// <summary>
    /// Gets or sets the print preparation completion percentage.
    /// </summary>
    [JsonPropertyName("prepare_per")]
    public int PreparePer { get; set; }

    /// <summary>
    /// Gets or sets the print error code.
    /// </summary>
    [JsonPropertyName("print_error")]
    public int PrintError { get; set; }

    /// <summary>
    /// Gets or sets the print G-code action code.
    /// </summary>
    [JsonPropertyName("print_gcode_action")]
    public int PrintGcodeAction { get; set; }

    /// <summary>
    /// Gets or sets the actual print action code.
    /// </summary>
    [JsonPropertyName("print_real_action")]
    public int PrintRealAction { get; set; }

    /// <summary>
    /// Gets or sets the print type.
    /// </summary>
    [JsonPropertyName("print_type")]
    public string PrintType { get; set; } = "";

    /// <summary>
    /// Gets or sets the printing profile identifier.
    /// </summary>
    [JsonPropertyName("profile_id")]
    public string ProfileId { get; set; } = "";

    /// <summary>
    /// Gets or sets the project identifier.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = "";

    /// <summary>
    /// Gets or sets the print queue position.
    /// </summary>
    [JsonPropertyName("queue")]
    public int Queue { get; set; }

    /// <summary>
    /// Gets or sets the estimated queue wait time in seconds.
    /// </summary>
    [JsonPropertyName("queue_est")]
    public int QueueEst { get; set; }

    /// <summary>
    /// Gets or sets the queue number.
    /// </summary>
    [JsonPropertyName("queue_number")]
    public int QueueNumber { get; set; }

    /// <summary>
    /// Gets or sets the queue status code.
    /// </summary>
    [JsonPropertyName("queue_sts")]
    public int QueueSts { get; set; }

    /// <summary>
    /// Gets or sets the total number of items in the queue.
    /// </summary>
    [JsonPropertyName("queue_total")]
    public int QueueTotal { get; set; }

    /// <summary>
    /// Gets or sets the remaining time in minutes.
    /// </summary>
    [JsonPropertyName("remain_time")]
    public int RemainTime { get; set; }

    /// <summary>
    /// Gets or sets the collection of status objects.
    /// </summary>
    [JsonPropertyName("s_obj")]
    public List<object> SObj { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether an SD card is present.
    /// </summary>
    [JsonPropertyName("sdcard")]
    public bool Sdcard { get; set; }

    /// <summary>
    /// Gets or sets the sequence identifier.
    /// </summary>
    [JsonPropertyName("sequence_id")]
    public string SequenceId { get; set; } = "";

    /// <summary>
    /// Gets or sets the speed level setting.
    /// </summary>
    [JsonPropertyName("spd_lvl")]
    public int SpdLvl { get; set; }

    /// <summary>
    /// Gets or sets the speed magnitude setting.
    /// </summary>
    [JsonPropertyName("spd_mag")]
    public int SpdMag { get; set; }

    /// <summary>
    /// Gets or sets the printer status string.
    /// </summary>
    [JsonPropertyName("stat")]
    public string Stat { get; set; } = "";

    /// <summary>
    /// Gets or sets the printer state code.
    /// </summary>
    [JsonPropertyName("state")]
    public int State { get; set; }

    /// <summary>
    /// Gets or sets the stage configuration list.
    /// </summary>
    [JsonPropertyName("stg")]
    public List<int> Stg { get; set; } = new();

    /// <summary>
    /// Gets or sets the current stage code.
    /// </summary>
    [JsonPropertyName("stg_cur")]
    public int StgCur { get; set; }

    /// <summary>
    /// Gets or sets the subtask identifier.
    /// </summary>
    [JsonPropertyName("subtask_id")]
    public string SubtaskId { get; set; } = "";

    /// <summary>
    /// Gets or sets the subtask name.
    /// </summary>
    [JsonPropertyName("subtask_name")]
    public string SubtaskName { get; set; } = "";

    /// <summary>
    /// Gets or sets the task identifier.
    /// </summary>
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; } = "";

    /// <summary>
    /// Gets or sets the total number of layers in the print job.
    /// </summary>
    [JsonPropertyName("total_layer_num")]
    public int TotalLayerNum { get; set; }

    /// <summary>
    /// Gets or sets the firmware upgrade state information.
    /// </summary>
    [JsonPropertyName("upgrade_state")]
    public UpgradeState UpgradeState { get; set; } = new();

    /// <summary>
    /// Gets or sets the file upload information.
    /// </summary>
    [JsonPropertyName("upload")]
    public Upload Upload { get; set; } = new();

    /// <summary>
    /// Gets or sets the firmware version string.
    /// </summary>
    [JsonPropertyName("ver")]
    public string Ver { get; set; } = "";

    /// <summary>
    /// Gets or sets the collection of virtual material trays.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: This property MUST be public for JSON deserialization to work.
    /// System.Text.Json only deserializes public properties by default.
    /// </remarks>
    [JsonPropertyName("vir_slot")]
    public List<TrayInternal> VirtualTrays { get; set; } = new();

    /// <summary>
    /// Gets or sets the current virtual tray information.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: This property MUST be public for JSON deserialization to work.
    /// System.Text.Json only deserializes public properties by default.
    /// </remarks>
    [JsonPropertyName("vt_tray")]
    public TrayInternal CurrentVirtualTray { get; set; } = new();

    /// <summary>
    /// Gets or sets the WiFi signal strength.
    /// </summary>
    [JsonPropertyName("wifi_signal")]
    public string WifiSignal { get; set; } = "";

    /// <summary>
    /// Gets or sets the X camera information.
    /// </summary>
    [JsonPropertyName("xcam")]
    public XCam XCam { get; set; } = new();

    /// <summary>
    /// Gets or sets the X camera status.
    /// </summary>
    [JsonPropertyName("xcam_status")]
    public string XCamStatus { get; set; } = "";
}
