using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public SignalAssetEventPair[] signalAssetEventPairs;

    [Serializable]
    public class SignalAssetEventPair
    {
        public SignalAsset signalAsset;
        public ParameterizedEvent events;

        [Serializable]
        public class ParameterizedEvent : UnityEvent<string>
        {}
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is DialogSignalEmitter dialogEmitter)
        {
            var matches = signalAssetEventPairs.Where(x => ReferenceEquals(x.signalAsset, dialogEmitter.asset));
            foreach (var m in matches)
            {
                m.events.Invoke(dialogEmitter.Dialog);
            }
        }
    }

    // public Why events;

}

[Serializable]
public class Why : UnityEvent<BaseEventData>
{}

public class DialogSignalEmitter : SignalEmitter
{
    public string Dialog;
}