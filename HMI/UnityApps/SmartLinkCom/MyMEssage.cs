using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLinkCom
{
    [Serializable]
    class MyMessage
    {
        public int RoomId;
        public int FixId;
        public int Level;

        public override string ToString()
        {
            return "Msg - "+ RoomId+"-"+FixId+"-"+Level;
        }
    }
}
