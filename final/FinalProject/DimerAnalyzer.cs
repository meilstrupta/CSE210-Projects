namespace QpcrAnalyzer
{
    public class DimerAnalyzer
    {
        public bool CheckForDimer(DnaSequence sequenceA, DnaSequence sequenceB)
        {
            string seqA = sequenceA.Sequence;
            string rcB = sequenceB.GetReverseComplement();

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

        public string VisualizeDimer(DnaSequence sequenceA, DnaSequence sequenceB)
        {
            string seqA = sequenceA.Sequence;
            string rcB = sequenceB.GetReverseComplement();

            int maxLen = Math.Max(seqA.Length, rcB.Length);

            string paddedA = seqA.PadRight(maxLen);
            string paddedB = rcB.PadRight(maxLen);

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