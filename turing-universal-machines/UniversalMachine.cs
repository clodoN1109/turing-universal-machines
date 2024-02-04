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
        internal UniversalMachine(int delay = 100)
        {

            numberOfOperations = 0;
            tape = new Tape();
            mconfig = "b";
            scanner = new Scanner(tape, delay, ref numberOfOperations);
            

        }

        internal int numberOfOperations;

        internal Tape tape;

        internal readonly Scanner scanner;

        internal string mconfig;
        internal void PrintResult()
        {
            string result = "";

            foreach (var item in tape.array)
            {
                if (item == "0" || item == "1")
                    result += item.ToString();
            }

            Console.WriteLine("\n\n\n RESULT: " + result + "\n\n");
            Thread.Sleep(2000);
        }

        internal void PrintMachineState(string instruction = "") {

            string parameters = "OPERATIONS: " + numberOfOperations.ToString("D6") + "  MCONFIG: " + mconfig.ToString() + "  TAPE:   ".ToString();

            string tapeState = "";
            foreach (var item in tape.array)
            {
                if (item != "")
                    tapeState += item.ToString();
            }

            Console.Write("\r" + new string(' ', 50));

            Console.Write("\r" + parameters + tapeState);

            int arrowPosition = parameters.Length + scanner.ScannedPosition;

            Console.SetCursorPosition(arrowPosition, Console.CursorTop + 1);
            Console.Write("\r" + new string(' ', arrowPosition) + "^" + new string(' ', Console.BufferWidth - Console.CursorLeft - 1));

            Console.SetCursorPosition(arrowPosition, Console.CursorTop + 1);
            Console.Write("\r" + new string(' ', arrowPosition - 4) + "SCANNER (" + instruction + ")" + new string(' ', Console.BufferWidth - Console.CursorLeft - 10));

            Console.SetCursorPosition(arrowPosition, Console.CursorTop - 2);

        }

        internal void PrintMachineDefinition() {

            Console.CursorVisible = false;

            Console.WriteLine("\n\n\n\n\n\n\n\n");
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

            PrintMachineState();
            Thread.Sleep(2000); 

        }
        internal string[] GetInstruction(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs, string scannedSymbol)
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

            internal void PlayBip(string instruction)
            {
#if WINDOWS
                switch (instruction)
                {
                    case "R": 
                    case "L":
                        Console.Beep(1000, 10);
                        break;

                    case "P0": 
                    case "P1":
                    case "Pe":
                    case "Px":
                        Console.Beep(2000, 10);
                        break;

                    case "E":
                        Console.Beep(200, 10);
                        break;
                }
#endif

            }

            internal void Operate(string instruction, ref int operations, int scannedPosition, int tapeLength) {

                if (scannedPosition >= tapeLength -1) 
                {
                    ScannedPosition++;
                    return; 
                }

                Thread.Sleep(Delay);
                PlayBip(instruction);
                switch (instruction)
                {
                    case "R":
                        ScannedPosition++;
                        break;
                    case "L":
                        ScannedPosition--;
                        break;

                    case "P0":
                        MountedTape.array[ScannedPosition] = "0";
                        break;

                    case "P1":
                        MountedTape.array[ScannedPosition] = "1";
                        break;
                    case "Pe":
                        MountedTape.array[ScannedPosition] = "e";
                        break;
                    case "Px":
                        MountedTape.array[ScannedPosition] = "x";
                        break;
                    case "E":
                        MountedTape.array[ScannedPosition] = "-";
                        break;
                }

                ScannedSymbol = MountedTape.array[ScannedPosition];
                operations++;

            }

        }

        internal void Run(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs)
        {

            PrintMachineDefinition();

            while (scanner.ScannedPosition < tape.array.Length)
            {

                foreach (var instruction in GetInstruction(mconfigs, scanner.ScannedSymbol))
                {

                    PrintMachineState(instruction);
                    scanner.Operate(instruction, ref numberOfOperations, scanner.ScannedPosition, tape.array.Length);

                }


            }

            PrintResult();

        }


    }
  
}
