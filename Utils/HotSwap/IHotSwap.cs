using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nl.flukeyfiddler.Utils
{
    public interface IHotSwap
    {
        // Used to check if upgrade needed
        int Version();

        // Used by old version before upgrade
        object GetState();

        // Old version passes this to new version
        void SetState(object prevState);
    }
}
