namespace QpcrAnalyzer
{
    public class Primer : DnaSequence
    {
        public bool IsForward { get; set; }

        public Primer(string name, string sequence, double concentration, bool isForward)
            : base(name, sequence, concentration)
        {
            IsForward = isForward;
        }

        // Forward primers use the sequence as-is.
        // Reverse primers override this behavior in ReversePrimer.cs.
        public override string GetReverseComplement()
        {
            return base.GetReverseComplement();
        }

        // Placeholder for future primer-specific logic
        public virtual string GetBindingSequence()
        {
            // Forward primers bind directly; reverse primers override this.
            return Sequence;
        }
    }
}