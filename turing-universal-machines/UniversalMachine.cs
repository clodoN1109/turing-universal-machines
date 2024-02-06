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


namespace turing_universal_machines
{
    /// <summary>
    /// An implementation of Alan Turing's conception of a universal computing machine.
    /// </summary>
    internal class UniversalMachine
    {
        /// <summary>
        /// Creates a machine instance along with its scanner and tape components, and sets variables for the
        /// number of operations and the initial configuration.
        /// </summary>
        /// <param name="delay">Determines the duration of each machine operation.</param>
        internal UniversalMachine(int delay = 100)
        {

            numberOfOperations = 0;
            tape = new Tape();
            mconfig = "b";
            scanner = new Scanner(tape, delay);
            

        }
        /// <summary>
        /// Counts the total number of operations performed by the machine.
        /// </summary>
        internal int numberOfOperations;
        /// <summary>
        /// Represents Turing's conception of a tape that stores both input and output information.
        /// </summary>
        internal Tape tape;
        /// <summary>
        /// Represents the instrument capable of processing the information stores in the tape. 
        /// It carries, at each iteration, exactly one symbol from the tape and one instruction
        /// given by the machine configuration set.
        /// </summary>
        internal readonly Scanner scanner;

        internal string mconfig;
        /// <summary>
        /// Writes to the console the state of the tape but ignoring the auxiliary symbols at intermidiate
        /// positions.
        /// </summary>
        internal void PrintResult()
        {
            var result = new StringBuilder();
            foreach (string symbol in tape.array)
            {
                if (symbol == "0" || symbol == "1") result.Append(symbol);
            }

            Console.WriteLine($"\n\n\n RESULT: { result } \n\n");
            Thread.Sleep(2000);
        }
        /// <summary>
        /// Writes to the console the complete state of the tape.
        /// </summary>
        /// <param name="instruction"></param>
        internal void PrintMachineState(string instruction = "") {

            string parameters = $"OPERATIONS: {numberOfOperations:D6} MCONFIG: {mconfig} TAPE: ";

            var tapeState = new StringBuilder();
            foreach (string symbol in tape.array)
            {
                if (symbol != "") tapeState.Append(symbol);
            }

            Console.Write($"\r{new string(' ', 50)}");
            Console.Write($"\r{parameters}{tapeState}");
            int arrowPosition = parameters.Length + scanner.ScannedPosition;
            Console.SetCursorPosition(arrowPosition, Console.CursorTop + 1);
            Console.Write($"\r{new string(' ', arrowPosition)}^{new string(' ', Console.BufferWidth - Console.CursorLeft - 1)}");
            Console.SetCursorPosition(arrowPosition, Console.CursorTop + 1);
            Console.Write($"\r{new string(' ', arrowPosition - 4)}SCANNER ({instruction}){new string(' ', Console.BufferWidth - Console.CursorLeft - 10)}");

#if WINDOWS

            Console.SetCursorPosition(arrowPosition, Console.CursorTop - 2);

#else 

            Console.SetCursorPosition(arrowPosition, Console.CursorTop - 3);

#endif

        }
        /// <summary>
        /// Print to the console captions for the scanner instructions.
        /// </summary>
        internal void PrintMachineDefinition() {

            Console.CursorVisible = false;

            Console.WriteLine("\n");
            Console.WriteLine("L = Move left once");
            Console.WriteLine("R = Move right once");
            Console.WriteLine("P0 = Print 0");
            Console.WriteLine("P1 = Print 1");
            Console.WriteLine("Px = Print x");
            Console.WriteLine("Pe = Print e");
            Console.WriteLine("E = Erase symbol");
            Console.WriteLine();

        }
        /// <summary>
        /// Retrieves the sequence of instructions to be passed to the scanner that correspond to the current scanned symbol
        /// and to the selected configuration set. 
        /// </summary>
        /// <param name="mconfigs">The configuration set selected to run the universal machine.</param>
        /// <param name="scannedSymbol">The last scanned symbol.</param>
        /// <returns></returns>
        internal string[] GetInstruction(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs, string scannedSymbol)
        {

            string[] operations = (string[])mconfigs[mconfig][scannedSymbol]["operations"];
            mconfig = (string)mconfigs[mconfig][scannedSymbol]["finalMConfig"];

            return operations;
        }
        /// <summary>
        /// Represents Turing's conception of a tape that stores both input and output information.
        /// </summary>
        internal class Tape
        {
            internal Tape()
            {

                array = Enumerable.Repeat("-", 100).ToArray();

            }

            internal string[] array;

            
        }
        /// <summary>
        /// Represents the instrument capable of processing the information stores in the tape. 
        /// It carries, at each iteration, exactly one symbol from the tape and one instruction
        /// given by the machine configuration set.
        /// </summary>
        internal class Scanner
        {
            internal Scanner(Tape tape, int delay)
            {

                ScannedPosition = 15;
                ScannedSymbol = "-";
                MountedTape = tape;
                Delay = delay; 

            }
            /// <summary>
            /// The last scanned symbol.
            /// </summary>
            internal string ScannedSymbol{ get; set; }
            /// <summary>
            /// The current scanner's position.
            /// </summary>
            internal int ScannedPosition { get; set; }
            /// <summary>
            /// An increment for the duration of each universal machine operation. 
            /// </summary>
            internal int Delay;

            internal Tape MountedTape { get; set; }
            /// <summary>
            /// Defines a specific sound effect for the execution of each type of operation.
            /// </summary>
            /// <param name="instruction">Each instruction to be executed by the scanner.</param>
            internal void PlayBeep(string instruction)
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
            /// <summary>
            /// Perform the operations to be executed over the tape.
            /// </summary>
            /// <param name="instruction">Each instruction to be executed by the scanner.</param>
            /// <param name="operations">The count of the number of operations performed.</param>
            /// <param name="scannedPosition">The current scanner's position.</param>
            /// <param name="tapeLength">Used to set a limit for the execution of the Operate method.</param>
            internal void Operate(string instruction, ref int operations, int scannedPosition, int tapeLength) {

                if (scannedPosition >= tapeLength -1) 
                {
                    ScannedPosition++;
                    return; 
                }

                Thread.Sleep(Delay);
                //PlayBeep(instruction);
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
        /// <summary>
        /// Simulates the execution the of the created universal machine and presents in the terminal 
        /// each step of the process.
        /// </summary>
        /// <param name="mconfigs">The selected set of configurations from the MachineConfigs class.</param>
        internal void Run(Dictionary<string, Dictionary<string, Dictionary<string, object>>> mconfigs)
        {

            Console.Clear();

            PrintMachineDefinition();

            Console.WriteLine("UNIVERSAL MACHINE RUNNING WITH PROGRAM C001:");
            Console.WriteLine();

            PrintMachineState();

            //#if WINDOWS
            //            Console.Beep(400, 2000);
            //#endif
            Thread.Sleep(2000);

            while (scanner.ScannedPosition < tape.array.Length)
            {

                foreach (string instruction in GetInstruction(mconfigs, scanner.ScannedSymbol))
                {

                    PrintMachineState(instruction);
                    scanner.Operate(instruction, ref numberOfOperations, scanner.ScannedPosition, tape.array.Length);

                }


            }

            PrintResult();

        }


    }
  
}
