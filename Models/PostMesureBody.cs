using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ServiceApiExample.Models
{
    public class PostMesureBody
    {
        // Part reference (Nom de la pièce)
        public string? PartName { get; set; }

        //UserFields (Champs utilisateur)
        public List<CustomField?> CustomFields { get; set; } = [];

        // CHaracteristics (Cotes)
        public List<Characteristic?> Characteristics { get; set; } = [];

        // Mesure status (GO / NO GO)
        public string? StepStatus { get; set; }

        // Device IP address
        public string? IpAddress { get; set; }

        // Device port 
        public int? IpPort { get; set; }

        // Mesure date
        public Date? Date { get; set; }

        // PresetFrame
        public bool? PresetFrame { get; set; }

        // PresetControle
        public bool? PresetControlFrame { get; set; }
    }

    // Date
    public class Date
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }
    }

    // UserField (Champ utilisateur)
    public class CustomField
    {
        // Number
        public int? Number { get; set; }

        // Name
        public string? Name { get; set; }

        // Value
        public string? Value { get; set; }

        // Is editable
        public bool? Editable { get; set; }

        // Is mandatory
        public bool? Obligatoire { get; set; }
    }

    // Characteristic (Cote)
    public class Characteristic
    {
        // Name
        public string? Name { set; get; }

        // Number
        public int? Number { get; set; }

        // Value
        public double? Value { set; get; }

        // Master (Etalon)
        public decimal? Master { get; set; }

        // Lower tolerance (Tolerance inf.)
        public decimal? InferiorTolerance { get; set; }

        // Nominal
        public decimal? Nominal { get; set; }

        // Upper Tolerance (Tolerance sup.)
        public decimal? SupperoirTolerance { get; set; }

        // Preset enabled (Etalonnable)
        public bool? CalibrationEnable { get; set; }

        //
        public bool? CalibrationControlEnable { get; set; }
    }
}

