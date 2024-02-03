using System;
using static turing_universal_machines.UniversalMachine;
using turing_universal_machines;

namespace turing_universal_machines
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var myMachine = new UniversalMachine("b", 500);

            myMachine.Run(MachineConfigs.C010, 600);

        }
    }
}