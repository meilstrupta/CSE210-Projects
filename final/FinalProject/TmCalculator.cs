namespace QpcrAnalyzer
{
    public class TmCalculator
    {
        // Basic melting temperature formula:
        // Tm = 2°C * (# of A/T) + 4°C * (# of G/C)
        // This is the classic Wallace rule. Will not be perfectly accurate.
        public double CalculateTm(DnaSequence seq)
        {
            int atCount = 0;
            int gcCount = 0;

            foreach (char n in seq.Sequence)
            {
                if (n == 'A' || n == 'T')
                    atCount++;
                else if (n == 'G' || n == 'C')
                    gcCount++;
            }

            return (2 * atCount) + (4 * gcCount);
        }
    }
}