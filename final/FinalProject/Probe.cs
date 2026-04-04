namespace QpcrAnalyzer
{
    public class Probe : DnaSequence
    {
        public string ReporterDye { get; set; }

        public Probe(string name, string sequence, double concentration, string reporterDye)
            : base(name, sequence, concentration)
        {
            ReporterDye = reporterDye;
        }
    }
}