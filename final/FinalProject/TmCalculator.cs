namespace QpcrAnalyzer
{
    public class TmCalculator
    {
        // Wallace rule: Tm = 2*(A/T) + 4*(G/C)
        public double CalculateTm(DnaSequence sequence)
        {
            int atCount = 0;
            int gcCount = 0;

            foreach (char nucleotide in sequence.Sequence)
            {
                if (nucleotide == 'A' || nucleotide == 'T')
                    atCount++;
                else if (nucleotide == 'G' || nucleotide == 'C')
                    gcCount++;
            }

            return (2 * atCount) + (4 * gcCount);
        }
    }
}