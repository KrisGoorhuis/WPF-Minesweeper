using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Multisweeper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGameHost" in both code and config file together.
    [ServiceContract]
    public interface IGameHost
    {
        [OperationContract]
        void DoWork();
    }
}
