<div align="center">
  
# Turing Universal Machines

## Implementation of Alan Turing's conception of a universal computing machine

</div>

<div align="center">
  <img src="https://github.com/clodoN1109/turing-universal-machines/assets/104923248/6b58f7a5-d8ed-4f4f-9e04-21abbde5028c" width="100%">
</div>

### Index
- The Project
- Source Code
- Running
- Requirements
- Roadmap

#
### The Project

  This programming essay is based on Alan Turing's 1936 essay "On Computable Numbers, with an Application to the Entscheidungsproblem" and consistis in a simulation of his
  conceptual universal machine. Simulating the machine might help experimenting feeding the machine with varied and complex sets of configurations.

  Link to the essay: https://www.cs.virginia.edu/~robins/Turing_Paper_1936.pdf
  
#
### Source Code

  The code, written in C#, is organized into three files:

  - UniversalMachine.cs : Defines the machine, its components ( tape and scanner ) and its behavior ( Run and GetInstruction methods ).

  - MachineConfig.cs : Defines the possible configurations to be fed to a created machine (for now, only C10 and C1011, examples Turing uses in his essay).

    Note: A set of configurations is just the analog of a program and its sequence of instructions.
    
  - Program.cs : Creates an instance of the machine and runs it using a selected configuration.

#
### Running

- Open the Program.cs file and configure the instantiation of the machine, setting up its delay parameter (in miliseconds). A delay around 200 miliseconds (default)
permits following the scanner in action step by step.

- Set the configuration set parameter for the Run method, using one of the members of the MachineConfigs class.

The example ahead uses delay of 1000 miliseconds and the C001 configuration set, which is intended to compute the infinite sequence 001011011101111... .

<div align="left">
  <img src="https://github.com/clodoN1109/turing-universal-machines/assets/104923248/fe2a526d-f08f-44d7-b48f-eaf8d3d0c9b8" width="70%">
</div>


- Run the program and observe in the terminal a representation of each state of the machine during the computations. 


<div align="center">
  
  <img src="https://github.com/clodoN1109/turing-universal-machines/assets/104923248/cfc247f4-0f44-4adb-88f8-3cd9ab37efe4" width="100%">
  
</div>

#
### Requirements

- Windows/Linux
- Microsoft Visual Studio
  
#
### Roadmap

  The plan is to expand and improve the code as I study the rest of the essay during the next 3 weeks (today is 02/02/2024, btw).
  One thing in my mind now is to create a simple web app where one could easily try out configurations and see the results.

#


<div align="center">
  <img src="https://github.com/clodoN1109/turing-universal-machines/assets/104923248/0e6d3437-c662-4450-8d61-1b7e3d433c6d" width="25%">
</div>
