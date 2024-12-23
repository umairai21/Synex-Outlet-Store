using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
    // Interface defining a method to handle state transitions

    public interface IState
    {
        void Handle(Context context);
    }
}
