using System;

namespace QpcrAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== qPCR Primer Analyzer ===\n");

            // -----------------------------
            // USER INPUT SECTION
            // -----------------------------

            // Forward primer
            Console.Write("Enter forward primer name: ");
            string fwdName = Console.ReadLine();

            Console.Write("Enter forward primer sequence: ");
            string fwdSeq = Console.ReadLine();

            Console.Write("Enter forward primer concentration (uM): ");
            double fwdConc = double.Parse(Console.ReadLine());

            // Reverse primer
            Console.Write("\nEnter reverse primer name: ");
            string revName = Console.ReadLine();

            Console.Write("Enter reverse primer sequence: ");
            string revSeq = Console.ReadLine();

            Console.Write("Enter reverse primer concentration (uM): ");
            double revConc = double.Parse(Console.ReadLine());

            // Amplicon
            Console.Write("\nEnter amplicon name: ");
            string ampName = Console.ReadLine();

            Console.Write("Enter amplicon sequence: ");
            string ampSeq = Console.ReadLine();

            Console.Write("Enter amplicon concentration (# of initial copies): ");
            double ampConc = double.Parse(Console.ReadLine());

            Console.Write("Enter target gene name: ");
            string geneName = Console.ReadLine();

            // Probe
            Console.Write("\nEnter probe name: ");
            string probeName = Console.ReadLine();

            Console.Write("Enter probe sequence: ");
            string probeSeq = Console.ReadLine();

            Console.Write("Enter probe concentration (uM): ");
            double probeConc = double.Parse(Console.ReadLine());

            Console.Write("Enter reporter dye (e.g., FAM, HEX, Cy5): ");
            string reporter = Console.ReadLine();

            Console.WriteLine("\n--- Constructing objects... ---\n");

            // -----------------------------
            // OBJECT CREATION
            // -----------------------------

            Primer forward = new Primer(fwdName, fwdSeq, fwdConc, isForward: true);
            ReversePrimer reverse = new ReversePrimer(revName, revSeq, revConc);
            Amplicon amp = new Amplicon(ampName, ampSeq, ampConc, geneName);
            Probe probe = new Probe(probeName, probeSeq, probeConc, reporter);

            TmCalculator tmCalc = new TmCalculator();
            DimerAnalyzer dimer = new DimerAnalyzer();
            SequenceHighlighter highlighter = new SequenceHighlighter();

            // -----------------------------
            // OUTPUT SECTION
            // -----------------------------

            Console.WriteLine("=== RESULTS ===\n");

            // Tm values
            Console.WriteLine("Melting Temperatures:");
            Console.WriteLine($"Forward Primer Tm: {tmCalc.CalculateTm(forward)} °C");
            Console.WriteLine($"Reverse Primer Tm: {tmCalc.CalculateTm(reverse)} °C");
            Console.WriteLine($"Probe Tm: {tmCalc.CalculateTm(probe)} °C\n");

            // Reverse complement
            Console.WriteLine("Reverse Primer Binding Sequence:");
            Console.WriteLine(reverse.GetBindingSequence() + "\n");

            // Highlighting
            Console.WriteLine("Binding Site Highlighting on Amplicon:\n");
            Console.WriteLine(highlighter.HighlightAll(amp, forward, probe, reverse) + "\n");

            // -----------------------------
            // FULL DIMER TESTING
            // -----------------------------
            Console.WriteLine("=== DIMER TESTING ===");

            TestPair("Forward vs Forward", forward, forward, dimer);
            TestPair("Reverse vs Reverse", reverse, reverse, dimer);
            TestPair("Probe vs Probe", probe, probe, dimer);

            TestPair("Forward vs Reverse", forward, reverse, dimer);
            TestPair("Forward vs Probe", forward, probe, dimer);
            TestPair("Reverse vs Probe", reverse, probe, dimer);

            Console.WriteLine("\nAnalysis complete. Press Enter to exit.");
            Console.ReadLine();
        }

        // -----------------------------
        // DIMER TESTING METHOD
        // -----------------------------
        static void TestPair(string label, DnaSequence a, DnaSequence b, DimerAnalyzer dimer)
        {
            Console.WriteLine($"\n--- {label} ---");

            if (dimer.CheckForDimer(a, b))
            {
                Console.WriteLine(dimer.VisualizeDimer(a, b));
            }
            else
            {
                Console.WriteLine($"[No dimer detected between {a.Name} and {b.Name}]");
            }
        }
    }
}