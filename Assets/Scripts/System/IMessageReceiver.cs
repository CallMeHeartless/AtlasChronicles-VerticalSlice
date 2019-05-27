using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessageSystem {
    public enum MessageType {
        eDamageMessage
    }

    public interface IMessageReceiver {
        void OnReceiveMessage(MessageType _eMessageType, object _Message);
    }
}

