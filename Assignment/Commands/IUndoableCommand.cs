using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Commands
{
    // Interface for commands that can be undone
    public interface IUndoableCommand : ICommand
    {
        // Method to undo the command
        void Undo();
    }
}
