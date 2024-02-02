using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static turing_universal_machines.UniversalMachine;

namespace turing_universal_machines
{
    internal class UniversalMachine
    {
        internal UniversalMachine(string initialConfig = "b", int delay = 100)
        {

            tape = new Tape();
            mconfig = initialConfig;
            scanner = new Scanner(tape, delay);

        }

        internal Tape tape;

        internal readonly Scanner scanner;

        internal string mconfig;
        internal string GetState
        {
            get
            {
                string state = "mconfig: ";

                state += mconfig.ToString();

                state += "  tape:   ".ToString();

                foreach (var item in tape.array)
                {
                    if (item != "")
                        state += item.ToString();
                }
                

                return  state;
            }
            set
            {
            }
        }

        internal string[] PassInstruction(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs, string scannedSymbol)
        {

            string[] operations = (string[])mconfigs[mconfig][scannedSymbol]["operations"];
            mconfig = (string)mconfigs[mconfig][scannedSymbol]["finalMConfig"];

            return operations;
        }

        internal class Tape
        {
            internal Tape()
            {
                array = new string[] { "-" };
            }

            internal string[] array;

            
        }

        internal class Scanner
        {
            internal Scanner(Tape tape, int delay)
            {

                ScannedPosition = 0;
                ScannedSymbol = "-";
                MountedTape = tape;
                Delay = delay; 

            }
            internal string ScannedSymbol{ get; set; }
            internal int ScannedPosition { get; set; }

            internal int Delay;

            internal Tape MountedTape { get; set; }

            internal void Operate(string[] instructions) {

                foreach (var instruction in instructions)
                {

                    switch (instruction)
                    {
                        case "R":
                            ScannedPosition++;

                            if (ScannedPosition >= MountedTape.array.Length)
                            {
                                MountedTape.array = MountedTape.array.Append("-").ToArray();
                            }
                            break;
                        case "L":
                            ScannedPosition--;
                            if (ScannedPosition < 0)
                            {
                                MountedTape.array = MountedTape.array.Prepend("-").ToArray();
                            }
                            break;

                        case "P0":
                            MountedTape.array[ScannedPosition] = "0";
                            Thread.Sleep(Delay);
                            break;

                        case "P1":
                            MountedTape.array[ScannedPosition] = "1";
                            Thread.Sleep(Delay);
                            break;
                        case "Pe":
                            MountedTape.array[ScannedPosition] = "e";
                            Thread.Sleep(Delay);
                            break;
                        case "Px":
                            MountedTape.array[ScannedPosition] = "x";
                            Thread.Sleep(Delay);
                            break;
                        case "E":
                            MountedTape.array[ScannedPosition] = "-";
                            break;
                    }

                    ScannedSymbol = MountedTape.array[ScannedPosition];

                }

            }

        }

        internal void Run(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs, int numberOfIterations = 1000)
        {

            Console.WriteLine("Running universal machine.");
            Console.WriteLine();

            for (int i = 0; i < numberOfIterations; i++)
            {

                scanner.Operate(

                    PassInstruction(mconfigs, scanner.ScannedSymbol)

                    );

                Console.Write("\r" + new string(' ', 50));
                Console.Write("\r" + GetState);

            }

            Console.WriteLine();


        }


    }
  
}
