namespace QpcrAnalyzer
{
    public class ReversePrimer : Primer
    {
        public ReversePrimer(string name, string sequence, double concentration)
            : base(name, sequence, concentration, isForward: false)
        {
        }

        public override string GetBindingSequence()
        {
            return GetReverseComplement();
        }
    }
}