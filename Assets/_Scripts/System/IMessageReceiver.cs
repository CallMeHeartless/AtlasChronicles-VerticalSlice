using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MessageSystem {
    public enum MessageType { // Add more message types here
        eDamageMessage,
        eGroundSlam,
        eReset,
        eActivate,
        eOn,
        eOff
    }

    public interface IMessageReceiver {
        void OnReceiveMessage(MessageType _eMessageType, object _Message);
    }

    // Example message 
    public class SwitchMessage : MonoBehaviour {
        public GameObject source;
        public float timer;
    }
}

