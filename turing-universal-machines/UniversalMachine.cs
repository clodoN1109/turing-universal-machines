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

            operations = 0;
            tape = new Tape();
            mconfig = initialConfig;
            scanner = new Scanner(tape, delay, ref operations);
            

        }

        internal int operations;

        internal Tape tape;

        internal readonly Scanner scanner;

        internal string mconfig;
        private string[] GetState
        {
            get
            {
                string parameters = "OPERATIONS: " + operations.ToString("D6") + "  MCONFIG: " + mconfig.ToString() + "  TAPE:   ".ToString();

                string state = "";
                foreach (var item in tape.array)
                {
                    if (item != "")
                        state += item.ToString();
                }
                

                return  new string[] { parameters, state };
            }
            set
            {
            }
        }

        internal string GetResult
        {
            get
            {
                string state = "";

                foreach (var item in tape.array)
                {
                    if (item == "0" || item == "1")
                        state += item.ToString();
                }


                return state;
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

                array = Enumerable.Repeat("-", 100).ToArray();

            }

            internal string[] array;

            
        }

        internal class Scanner
        {
            internal Scanner(Tape tape, int delay, ref int operations)
            {

                ScannedPosition = 15;
                ScannedSymbol = "-";
                MountedTape = tape;
                Delay = delay; 

            }
            internal string ScannedSymbol{ get; set; }
            internal int ScannedPosition { get; set; }

            internal int Delay;

            internal Tape MountedTape { get; set; }

            internal void Operate(string instruction, ref int operations) {

                Thread.Sleep(Delay);
                switch (instruction)
                {
                    case "R":

                        Console.Beep(1000, 10);

                        ScannedPosition++;

                        if (ScannedPosition >= MountedTape.array.Length)
                        {
                            MountedTape.array = MountedTape.array.Append("-").ToArray();
                        }
                        break;
                    case "L":

                        Console.Beep(1000, 10);

                        ScannedPosition--;
                        if (ScannedPosition < 0)
                        {
                            MountedTape.array = MountedTape.array.Prepend("-").ToArray();
                        }
                        break;

                    case "P0":
                        Console.Beep(2000, 10);
                        MountedTape.array[ScannedPosition] = "0";
                        
                        break;

                    case "P1":
                        Console.Beep(2000, 10);
                        MountedTape.array[ScannedPosition] = "1";

                        break;
                    case "Pe":
                        Console.Beep(2000, 10);
                        MountedTape.array[ScannedPosition] = "e";

                        break;
                    case "Px":
                        Console.Beep(2000, 10);
                        MountedTape.array[ScannedPosition] = "x";

                        break;
                    case "E":
                        Console.Beep(40, 10);
                        MountedTape.array[ScannedPosition] = "-";
                        break;
                }

                ScannedSymbol = MountedTape.array[ScannedPosition];
                operations++;

            }

        }

        internal void Run(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs, int numberOfIterations = 1000)
        {
            Console.CursorVisible = false;

            Console.WriteLine();
            Console.WriteLine("L = Move left once");
            Console.WriteLine("R = Move right once");
            Console.WriteLine("P0 = Print 0");
            Console.WriteLine("P1 = Print 1");
            Console.WriteLine("Px = Print x");
            Console.WriteLine("Pe = Print e");
            Console.WriteLine("E = Erase symbol");
            Console.WriteLine();

            Console.WriteLine("UNIVERSAL MACHINE RUNNING WITH PROGRAM C001:");
            Console.WriteLine();

            for (int i = 0; i < numberOfIterations; i++)
            {

                var instructions = PassInstruction(mconfigs, scanner.ScannedSymbol);

                foreach (var instruction in instructions)
                {

                    Console.Write("\r" + new string(' ', 50));

                    var state = GetState;
                    Console.Write("\r" + state[0] + state[1]);

                    int arrowPosition = state[0].Length + scanner.ScannedPosition;

                    Console.SetCursorPosition(arrowPosition, Console.CursorTop + 1);
                    Console.Write("\r" + new string(' ', arrowPosition) + "^" + new string(' ', Console.BufferWidth - Console.CursorLeft - 1));
                    
                    Console.SetCursorPosition(arrowPosition, Console.CursorTop + 1);
                    Console.Write("\r" + new string(' ', arrowPosition - 4) + "SCANNER(" + instruction + ")"+ new string(' ', Console.BufferWidth - Console.CursorLeft - 10));

                    Console.SetCursorPosition(arrowPosition, Console.CursorTop - 2);

                    scanner.Operate(instruction, ref operations);

                }

               


            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("RESULT: " + GetResult);
            Console.WriteLine();

        }


    }
  
}
