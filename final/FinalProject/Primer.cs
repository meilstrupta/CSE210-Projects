namespace QpcrAnalyzer
{
    public class Primer : DnaSequence
    {
        private bool _isForward;

        public bool IsForward
        {
            get => _isForward;
            set => _isForward = value;
        }

        public Primer(string name, string sequence, double concentration, bool isForward)
            : base(name, sequence, concentration)
        {
            _isForward = isForward;
        }

        public virtual string GetBindingSequence()
        {
            return Sequence; // forward primer binds as written, reverse primer will override this to return the reverse complement
        }
    }
}