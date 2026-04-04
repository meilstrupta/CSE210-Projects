namespace QpcrAnalyzer
{
    public class Probe : DnaSequence
    {
        private string _reporterDye;

        public string ReporterDye
        {
            get => _reporterDye;
            set => _reporterDye = value;
        }

        public Probe(string name, string sequence, double concentration, string reporterDye)
            : base(name, sequence, concentration)
        {
            _reporterDye = reporterDye;
        }
    }
}