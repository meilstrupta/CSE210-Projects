namespace QpcrAnalyzer
{
    public abstract class DnaSequence
    {
        // Private attributes
        private string _name;
        private string _sequence;
        private double _concentration;

        // Public properties
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Sequence
        {
            get => _sequence;
            set => _sequence = value.ToUpper();
        }

        public double Concentration
        {
            get => _concentration;
            set => _concentration = value;
        }

        // Constructor
        protected DnaSequence(string name, string sequence, double concentration)
        {
            _name = name;
            _sequence = sequence.ToUpper();
            _concentration = concentration;
        }

        // Behaviors
        public int GetLength()
        {
            return _sequence.Length;
        }

        public virtual string GetReverseComplement()
        {
            char Complement(char b) => b switch
            {
                'A' => 'T',
                'T' => 'A',
                'C' => 'G',
                'G' => 'C',
                _ => 'N'
            };

            char[] rc = new char[_sequence.Length];

            for (int i = 0; i < _sequence.Length; i++)
            {
                rc[i] = Complement(_sequence[_sequence.Length - 1 - i]);
            }

            return new string(rc);
        }
    }
}