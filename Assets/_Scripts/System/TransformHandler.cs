using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandler : GameCommandHandler
{
    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public LoopType loopType;
    public float duration = 1;
    public AnimationCurve accelCurve;
    public AudioSource onStartAudio, onEndAudio;
    public GameCommandSender OnStartCommand, OnStopCommand;
    public bool activate = false;

    [Range(0, 1)]
    public float previewPosition;
    float time = 0f;
    float position = 0f;
    float direction = 1f;

    [ContextMenu("Test Start Audio")]
    void TestPlayAudio()
    {
        if (onStartAudio != null) onStartAudio.Play();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    public override void PerformInteraction()
    {
        activate = true;
        if (OnStartCommand != null) OnStartCommand.Send();
        if (onStartAudio != null) onStartAudio.Play();
    }

    public void FixedUpdate()
    {
        if (activate)
        {
            time = time + (direction * Time.deltaTime / duration);
            switch (loopType)
            {
                case LoopType.Once:
                    LoopOnce();
                    break;
                case LoopType.PingPong:
                    LoopPingPong();
                    break;
                case LoopType.Repeat:
                    LoopRepeat();
                    break;
            }
            //PerformTransform(position);
        }
    }

    //public virtual void PerformTransform(float position)
    //{

    //}

    void LoopPingPong()
    {
        position = Mathf.PingPong(time, 1f);
    }

    void LoopRepeat()
    {
        position = Mathf.Repeat(time, 1f);
    }

    void LoopOnce()
    {
        position = Mathf.Clamp01(time);
        if (position >= 1)
        {
            enabled = false;
            if (OnStopCommand != null) OnStopCommand.Send();
            direction *= -1;
        }
    }
}

