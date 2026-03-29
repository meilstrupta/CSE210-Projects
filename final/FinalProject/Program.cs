using System;
using System.Collections.Generic;

namespace QpcrAnalyzer
{
    // -----------------------------
    // Abstract Base Class
    // -----------------------------
    public abstract class DnaSequence
    {
        public string Name { get; set; }
        public string Sequence { get; set; }
        public double Concentration { get; set; }

        public DnaSequence(string name, string sequence, double concentration)
        {
            Name = name;
            Sequence = sequence;
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

    // -----------------------------
    // Primer
    // -----------------------------
    public class Primer : DnaSequence
    {
        public bool IsForward { get; set; }

        public Primer(string name, string sequence, double concentration, bool isForward)
            : base(name, sequence, concentration)
        {
            IsForward = isForward;
        }

        public override string GetReverseComplement()
        {
            // Reverse primers need reverse complement
            return base.GetReverseComplement();
        }
    }

    // -----------------------------
    // Probe
    // -----------------------------
    public class Probe : DnaSequence
    {
        public string Fluorophore { get; set; }
        public string Quencher { get; set; }

        public Probe(string name, string sequence, double concentration,
                     string fluorophore, string quencher)
            : base(name, sequence, concentration)
        {
            Fluorophore = fluorophore;
            Quencher = quencher;
        }
    }

    // -----------------------------
    // Amplicon
    // -----------------------------
    public class Amplicon : DnaSequence
    {
        public string TargetGene { get; set; }

        public Amplicon(string name, string sequence, double concentration, string targetGene)
            : base(name, sequence, concentration)
        {
            TargetGene = targetGene;
        }
    }

    // -----------------------------
    // Tm Calculator
    // -----------------------------
    public class TmCalculator
    {
        public double CalculateTm(DnaSequence seq)
        {
            // Placeholder formula
            return seq.GetLength() * 2.0;
        }
    }

    // -----------------------------
    // Dimer Analyzer
    // -----------------------------
    public class DimerAnalyzer
    {
        public bool CheckForDimer(DnaSequence a, DnaSequence b)
        {
            // Placeholder logic
            return false;
        }
    }

    // -----------------------------
    // Sequence Highlighter
    // -----------------------------
    public class SequenceHighlighter
    {
        public string Highlight(Amplicon amp, DnaSequence feature)
        {
            // Placeholder: just returns the amplicon
            return amp.Sequence;
        }
    }

    // -----------------------------
    // Program Interaction
    // -----------------------------
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the qPCR Primer Analyzer!");

            // Example interaction
            Amplicon amp = new Amplicon("MyAmplicon", "ATCGATCGATCG", 50, "GAPDH");
            Primer fwd = new Primer("Forward", "ATCGA", 20, true);
            Primer rev = new Primer("Reverse", "CGATC", 20, false);
            Probe probe = new Probe("Probe", "TCGAT", 10, "FAM", "BHQ1");

            TmCalculator tmCalc = new TmCalculator();
            Console.WriteLine($"Forward Tm: {tmCalc.CalculateTm(fwd)}");
            Console.WriteLine($"Reverse Tm: {tmCalc.CalculateTm(rev)}");
            Console.WriteLine($"Probe Tm: {tmCalc.CalculateTm(probe)}");

            Console.WriteLine("Reverse primer reverse complement:");
            Console.WriteLine(rev.GetReverseComplement());

            Console.WriteLine("Program finished successfully.");
        }
    }
}