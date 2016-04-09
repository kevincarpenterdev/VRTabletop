using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VRTabletop.Communications {
    class MessageResponse : Response {
        string Message;
        
        public MessageResponse(string m) {
            Message = m;
        }

         public override void Apply() {
            //Temp, Perhaps a "Toast" like message?
            Debug.Log(Message);
        }

    }
}
