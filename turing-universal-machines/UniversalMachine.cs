using System;
using System.Buffers;
using System.Collections.Generic;
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
        internal UniversalMachine()
        {

            tape = new Tape();
            mconfig = "b";
            scanner = new Scanner(tape);

        }

        internal Tape tape;

        internal readonly Scanner scanner;

        internal string mconfig;
        internal string GetState
        {
            get
            {
                string state = "";
                foreach (var item in tape.array)
                {
                    if (item == "0" || item == "1")
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
                array = new string[] { "None" };
            }

            internal string[] array;

            
        }

        internal class Scanner
        {
            internal Scanner(Tape tape)
            {

                ScannedPosition = 0;
                ScannedSymbol = "None";
                MountedTape = tape;

            }
            internal string ScannedSymbol{ get; set; }
            internal int ScannedPosition { get; set; }

            internal Tape MountedTape { get; set; }

            internal void Operate(string[] instructions) {

                foreach (var item in instructions)
                {
                    Console.WriteLine("Instruction: " + item);
                    Console.WriteLine("Scanned Symbol: " + ScannedSymbol);
                    Console.WriteLine("Scanned Position: " + ScannedPosition);
                    Console.WriteLine("-------------------------------------------------------------");

                    switch (item)
                    {
                        case "R":
                            ScannedPosition++;

                            if (ScannedPosition >= MountedTape.array.Length)
                            {
                                MountedTape.array = MountedTape.array.Append("None").ToArray();
                            }
                            break;
                        case "L":
                            ScannedPosition--;
                            if (ScannedPosition < 0)
                            {
                                MountedTape.array = MountedTape.array.Prepend("None").ToArray();
                            }
                            break;

                        case "P0":
                            MountedTape.array[ScannedPosition] = "0";
                            break;

                        case "P1":
                            MountedTape.array[ScannedPosition] = "1";
                            break;
                    }

                }


                ScannedSymbol = MountedTape.array[ScannedPosition];

            }

        }

        internal static class MConfigs 
        {

            internal static Dictionary<string, Dictionary<string, Dictionary<string, object>>> C10 = 
                
                new Dictionary<string, Dictionary<string, Dictionary<string, object>>> {

                    {"b", 
                        new Dictionary<string, Dictionary<string, object>> {
                            { "None",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "P0", "R" } },
                                    { "finalMConfig", "c" }
                                }
                            }
                        }
                    },

                    {"c",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "None",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R" } },
                                    { "finalMConfig", "e" }
                                }
                            }
                        }
                    },                    
                    
                    {"e",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "None",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "P1", "R" } },
                                    { "finalMConfig", "f" }
                                }
                            }
                        }
                    },                    
                    
                    {"f",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "None",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R" } },
                                    { "finalMConfig", "b" }
                                }
                            }
                        }
                    }

                };

        }

        internal void Run(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs)
        {

            for (int i = 0; i<80; i++)
            {
                scanner.Operate(

                    PassInstruction(mconfigs, scanner.ScannedSymbol)

                    );
            }

            
            Console.WriteLine("Result: ");
            Console.WriteLine();
            Console.WriteLine(GetState);

        }


    }
  
}
