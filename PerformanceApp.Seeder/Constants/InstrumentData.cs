namespace PerformanceApp.Seeder.Constants;

public static class InstrumentData
{
    public static readonly string SsabB = "SSAB B";
    public static readonly string AstraZeneca = "Astra Zeneca";
    public static readonly string Statsobligation1046 = "Statsobligation 1046";
    public static readonly string Omx30 = "OMX30";
    public static readonly string OmrXtBond = "OMRXTBOND";

    public static readonly List<string> _instruments = [SsabB, AstraZeneca, Statsobligation1046, Omx30, OmrXtBond];

    public static List<string> Instruments => _instruments.OrderBy(i => i).ToList();
}