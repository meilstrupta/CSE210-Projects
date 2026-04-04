namespace QpcrAnalyzer
{
    public class Amplicon : DnaSequence
    {
        private string _targetGene;

        public string TargetGene
        {
            get => _targetGene;
            set => _targetGene = value;
        }

        public Amplicon(string name, string sequence, double concentration, string targetGene)
            : base(name, sequence, concentration)
        {
            _targetGene = targetGene;
        }
    }
}