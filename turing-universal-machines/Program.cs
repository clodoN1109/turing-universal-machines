using System;
using static turing_universal_machines.UniversalMachine;

namespace turing_universal_machines
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var myMachine = new UniversalMachine();

            myMachine.Run(MConfigs.C10);

        }
    }
}