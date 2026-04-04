namespace QpcrAnalyzer
{
    public class ReversePrimer : Primer
    {
        public ReversePrimer(string name, string sequence, double concentration)
            : base(name, sequence, concentration, isForward: false)
        {
        }

        // Reverse primers ALWAYS bind using the reverse complement.
        public override string GetBindingSequence()
        {
            return GetReverseComplement();
        }

        // You can override this too, but it's optional since Primer already calls base.
        public override string GetReverseComplement()
        {
            return base.GetReverseComplement();
        }
    }
}