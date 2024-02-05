using System;
using static turing_universal_machines.UniversalMachine;
using turing_universal_machines;

namespace turing_universal_machines
{
    /// <summary>
    /// Defines the program's entry point.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Instantiates the UniversalMachine class using a user given delay value and runs it with a chosen configuration option.
        /// </summary>
        /// <param name="args">The <paramref name="args" /> might receive a value for the delay parameter (in miliseconds)</param>
        static void Main(string[] args)
        {
            int delay;
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("Enter a value for the machine's delay: ");
                    delay = int.Parse(Console.ReadLine());
                    break;
                default:
                    delay = int.Parse(args[0]);
                    break;
            }

            var myMachine = new UniversalMachine(delay);

            myMachine.Run(MachineConfigs.C001);

        }
    }
}
