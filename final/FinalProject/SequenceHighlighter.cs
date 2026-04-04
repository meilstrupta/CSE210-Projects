using System;
using System.Collections.Generic;

namespace QpcrAnalyzer
{
    public class SequenceHighlighter
    {
        // ANSI escape codes for red + reset
        private const string RED   = "\u001b[31m";
        private const string RESET = "\u001b[0m";

        // Highlight a single feature (keeps your old behavior, now with color)
        public string Highlight(Amplicon amp, DnaSequence feature)
        {
            string ampSeq = amp.Sequence;
            string bindingSeq = GetBindingSequence(feature);

            int index = ampSeq.IndexOf(bindingSeq, StringComparison.Ordinal);

            if (index == -1)
            {
                return $"[No binding site found for {feature.Name} on {amp.Name}]";
            }

            string highlighted =
                ampSeq.Substring(0, index) +
                RED + "[" +
                ampSeq.Substring(index, bindingSeq.Length) +
                "]" + RESET +
                ampSeq.Substring(index + bindingSeq.Length);

            return highlighted;
        }

        // Highlight forward, probe, and reverse all on the same amplicon
        public string HighlightAll(Amplicon amp, Primer forward, Probe probe, ReversePrimer reverse)
        {
            string ampSeq = amp.Sequence;

            var regions = new List<(int start, int length)>
            {
                MakeRegion(ampSeq, forward),
                MakeRegion(ampSeq, probe),
                MakeRegion(ampSeq, reverse)
            };

            // Remove any that weren't found
            regions.RemoveAll(r => r.start == -1);

            if (regions.Count == 0)
                return "[No binding sites found on amplicon]";

            // Sort left → right
            regions.Sort((a, b) => a.start.CompareTo(b.start));

            string result = "";
            int cursor = 0;

            foreach (var r in regions)
            {
                if (cursor < r.start)
                    result += ampSeq.Substring(cursor, r.start - cursor);

                result += RED + "[" + ampSeq.Substring(r.start, r.length) + "]" + RESET;

                cursor = r.start + r.length;
            }

            if (cursor < ampSeq.Length)
                result += ampSeq.Substring(cursor);

            return result;
        }

        private (int start, int length) MakeRegion(string ampSeq, DnaSequence feature)
        {
            string bindingSeq = GetBindingSequence(feature);
            int index = ampSeq.IndexOf(bindingSeq, StringComparison.Ordinal);
            return (index, bindingSeq.Length);
        }

        // Centralized binding logic (this is the key fix)
        private string GetBindingSequence(DnaSequence seq)
        {
            if (seq is Primer primer)
            {
                // Forward primer: Sequence
                // Reverse primer: override returns reverse complement
                return primer.GetBindingSequence();
            }

            if (seq is Probe probe)
            {
                return probe.Sequence;
            }

            return seq.Sequence;
        }
    }
}