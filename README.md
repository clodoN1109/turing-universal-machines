<div align="center">
  
# Turing Universal Machines

## Implementation of the conception of Alan Turing from a universal computing machine

</div>


  
<div align="center">
  <img src="https://github.com/clodoN1109/turing-universal-machines/assets/104923248/bd707c6b-20c2-4326-a209-0f7fff7cbcf6" width="100%">
</div>

### Index
- The Project
- Source Code
- Requirements
- Roadmap

#
### The Project

  This programming essay is based on Alan Turing's 1936 essay "On Computable Numbers, with an Application to the Entscheidungsproblem" and consistis in a simulation of his
  conceptual universal machine. Simulating the machine might help experimenting feeding the machine with varied and complex set of configurations.

  Link to the essay: https://www.cs.virginia.edu/~robins/Turing_Paper_1936.pdf
  
#
### Source Code

  The code, written in C#, is organized into three files:

  - UniversalMachine.cs : Defines the machine, its components ( tape and scanner ) and its behavior ( Run and PassInstruction methods ).

  - MachineConfig.cs : Defines the possible configurations to be fed to a created machine (for now, only C10 and C1011, examples Turing uses in his essay).
    Note: A set of configurations is analogous to the concept of algorithm.
    
  - Program.cs : Creates an instance of the machine and runs it using a selected configuration.
    
#
### Requirements

- Windows/Linux/macOS
- Microsoft Visual Studio
  
#
### Roadmap

  The plan is to expand and improve the code as I study the rest of the essay during the next 3 weeks (today is 02/02/2024, btw).
  One thing in my mind now is to create a simple web app where one could easily try out configurations and see the results.

#


<div align="center">
  <img src="https://github.com/clodoN1109/turing-universal-machines/assets/104923248/0e6d3437-c662-4450-8d61-1b7e3d433c6d" width="25%">
</div>
