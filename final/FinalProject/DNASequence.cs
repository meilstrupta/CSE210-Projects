namespace QpcrAnalyzer
{
    public abstract class DnaSequence
    {
        public string Name { get; set; }
        public string Sequence { get; set; }
        public double Concentration { get; set; }

        protected DnaSequence(string name, string sequence, double concentration)
        {
            Name = name;
            Sequence = sequence.ToUpper();
            Concentration = concentration;
        }

        public int GetLength()
        {
            return Sequence.Length;
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

            char[] rc = new char[Sequence.Length];

            for (int i = 0; i < Sequence.Length; i++)
            {
                rc[i] = Complement(Sequence[Sequence.Length - 1 - i]);
            }

            return new string(rc);
        }
    }
}