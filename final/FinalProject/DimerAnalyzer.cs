namespace QpcrAnalyzer
{
    public class DimerAnalyzer
    {
        // Simple 4-base complement check (your original logic)
        public bool CheckForDimer(DnaSequence a, DnaSequence b)
        {
            string seqA = a.Sequence;
            string rcB = b.GetReverseComplement();

            for (int i = 0; i < seqA.Length - 3; i++)
            {
                string windowA = seqA.Substring(i, 4);

                for (int j = 0; j < rcB.Length - 3; j++)
                {
                    string windowB = rcB.Substring(j, 4);

                    if (IsComplement(windowA, windowB))
                        return true;
                }
            }

            return false;
        }

        // Simple visualization: align sequences at position 0
        public string VisualizeDimer(DnaSequence a, DnaSequence b)
        {
            string seqA = a.Sequence;
            string rcB = b.GetReverseComplement();

            // Pad both sequences to the same length
            int maxLen = Math.Max(seqA.Length, rcB.Length);

            string paddedA = seqA.PadRight(maxLen);
            string paddedB = rcB.PadRight(maxLen);

            // Build match line
            char[] match = new char[maxLen];
            for (int i = 0; i < maxLen; i++)
            {
                if (i < seqA.Length && i < rcB.Length && IsBaseComplement(seqA[i], rcB[i]))
                    match[i] = '|';
                else
                    match[i] = ' ';
            }

            string matchLine = new string(match);

            return
                $"5'-{paddedA}-3'\n" +
                $"   {matchLine}\n" +
                $"3'-{paddedB}-5'";
        }

        private bool IsComplement(string a, string b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!IsBaseComplement(a[i], b[i]))
                    return false;
            }
            return true;
        }

        private bool IsBaseComplement(char x, char y)
        {
            return (x == 'A' && y == 'T') ||
                   (x == 'T' && y == 'A') ||
                   (x == 'C' && y == 'G') ||
                   (x == 'G' && y == 'C');
        }
    }
}