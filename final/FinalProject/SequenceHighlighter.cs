using System;
using System.Collections.Generic;

namespace QpcrAnalyzer
{
    public class SequenceHighlighter
    {
        private const string Red   = "\u001b[31m";
        private const string Reset = "\u001b[0m";

        public string Highlight(Amplicon amplicon, DnaSequence feature)
        {
            string ampSeq = amplicon.Sequence;
            string bindingSeq = GetBindingSequence(feature);

            int index = ampSeq.IndexOf(bindingSeq, StringComparison.Ordinal);

            if (index == -1)
            {
                return $"[No binding site found for {feature.Name} on {amplicon.Name}]";
            }

            string highlighted =
                ampSeq.Substring(0, index) +
                Red + "[" +
                ampSeq.Substring(index, bindingSeq.Length) +
                "]" + Reset +
                ampSeq.Substring(index + bindingSeq.Length);

            return highlighted;
        }

        public string HighlightAll(Amplicon amplicon, Primer forward, Probe probe, ReversePrimer reverse)
        {
            string ampSeq = amplicon.Sequence;

            var regions = new List<(int start, int length)>
            {
                MakeRegion(ampSeq, forward),
                MakeRegion(ampSeq, probe),
                MakeRegion(ampSeq, reverse)
            };

            regions.RemoveAll(r => r.start == -1);

            if (regions.Count == 0)
                return "[No binding sites found on amplicon]";

            regions.Sort((a, b) => a.start.CompareTo(b.start));

            string result = "";
            int cursor = 0;

            foreach (var region in regions)
            {
                if (cursor < region.start)
                    result += ampSeq.Substring(cursor, region.start - cursor);

                result += Red + "[" + ampSeq.Substring(region.start, region.length) + "]" + Reset;

                cursor = region.start + region.length;
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

        private string GetBindingSequence(DnaSequence sequence)
        {
            if (sequence is Primer primer)
                return primer.GetBindingSequence();

            if (sequence is Probe probe)
                return probe.Sequence;

            return sequence.Sequence;
        }
    }
}