using System;
using UnityEngine;

namespace WF.FloatingMessages
{
    /// <summary>
    /// Instantiates a Messager object that will persistent through the app.
    /// </summary>
    public class Initalizer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void PostSceneLoad()
        {
            new GameObject(typeof(Messager).ToString(), new Type[] { typeof(Messager) });
        }
    }
}
