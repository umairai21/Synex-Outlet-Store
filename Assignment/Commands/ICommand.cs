using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Commands
{
    // Interface for basic command execution
    public interface ICommand
    {
        // Method to execute the command
        void Execute();
    }
}
