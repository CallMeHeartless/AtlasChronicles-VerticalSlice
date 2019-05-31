using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandSender : MonoBehaviour
{
    [Tooltip("The type of command to send.")]
    [SerializeField] GameCommandType interactionType;
    [Tooltip("This object will receive the command.")]
    [SerializeField] public GameCommandReceiver receivingObject;
    [Tooltip("If set to true, this command will only be sent once.")]
    [SerializeField] public bool oneShot = false;
    [Tooltip("How many seconds must pass before the command is sent again.")]
    [SerializeField] public float coolDown = 1;
    [Tooltip("If not null, this audio source will be played when the command is sent.")]
    [SerializeField] public AudioSource onSendAudio;
    [Tooltip("If onSendAudio is not null, it will play after this time has passed.")]
    [SerializeField] public float audioDelay;

    float lastSendTime;
    bool isTriggered = false;

    [ContextMenu("Send Interaction")]
    public void Send()
    {
        // If sending once and is already activated, dont send message.
        if (oneShot && isTriggered)
            return;

        // If cooldown is not yet complete, dont send message.
        if (Time.time - lastSendTime < coolDown)
            return;

        //Message has been sent
        isTriggered = true;

        //Update last time the message was sent
        lastSendTime = Time.time;

        // Make the receiving object recieve this message
        receivingObject.Receive(interactionType);

        //Play audio if not null
        if (onSendAudio) onSendAudio.PlayDelayed(audioDelay); 
    }

    protected virtual void Reset()
    {
        receivingObject = GetComponent<GameCommandReceiver>();
    }
}
