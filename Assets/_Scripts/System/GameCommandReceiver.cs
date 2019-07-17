using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandReceiver : MonoBehaviour
{
    Dictionary<GameCommandType, List<System.Action>> handlers = new Dictionary<GameCommandType, List<System.Action>>();

    //Receives message from 'SendGameCommand' and performs an interaction that was registered on awake
    public void Receive(GameCommandType e)
    {
        List<System.Action> callbacks = null;
        if (handlers.TryGetValue(e, out callbacks))
        {
            foreach (var i in callbacks) i();
        }
    }

    //Registers an interaction from 'GameCommandHandler' (On same script)
    public void Register(GameCommandType type, GameCommandHandler handler)
    {
        List<System.Action> callbacks = null;
        if (!handlers.TryGetValue(type, out callbacks))
        {
            callbacks = handlers[type] = new List<System.Action>();
        }
        callbacks.Add(handler.OnInteraction);
    }

    //Removes an interaction from the dictionary
    public void Remove(GameCommandType type, GameCommandHandler handler)
    {
        handlers[type].Remove(handler.OnInteraction);
    }
}
