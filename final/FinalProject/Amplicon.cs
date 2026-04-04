namespace QpcrAnalyzer
{
    public class Amplicon : DnaSequence
    {
        public string TargetGene { get; set; }

        public Amplicon(string name, string sequence, double concentration, string targetGene)
            : base(name, sequence, concentration)
        {
            TargetGene = targetGene;
        }

        // Amplicons rarely need special behavior, but this keeps the class extensible.
        public override string GetReverseComplement()
        {
            return base.GetReverseComplement();
        }
    }
}