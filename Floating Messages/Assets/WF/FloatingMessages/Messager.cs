using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WF.FloatingMessages
{
    /// <summary>
    /// Monobehaviour object that is persistent through the life time of the app.
    /// </summary>
    public sealed class Messager : MonoBehaviour
    {
        private static Messager instance;
        private static readonly List<Message> messages = new();

        public static Color DefaultFontColor { get; set; } = Color.yellow;
        public static int DefaultFontSize { get; set; } = 24;
        public static bool DefaultWordWrapSetting { get; set; } = true;
        public static int DefaultOffsetX { get; set; } = 5;
        public static int DefaultOffsetY { get; set; } = 5;

        private float yAxisRef;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Debug.LogError("Multiple instances of " + GetType() + " is not allowed.", gameObject);
                Destroy(this);
            }
        }

        private void Start()
        {
            StartCoroutine(UpdateLifeTimes());
        }

        private IEnumerator UpdateLifeTimes()
        {
            while (true)
            {
                if (messages != null)
                {
                    for (int a = 0; a < messages.Count; a++)
                    {
                        if (!messages[a].UpdateLifeTime(Time.unscaledDeltaTime))
                            messages.RemoveAt(a);
                    }
                }
                yield return null;
            }
        }

        private void OnGUI()
        {
            if (messages == null)
                return;

            yAxisRef = DefaultOffsetY;

            for (int a = 0; a < messages.Count; a++)
            {
                GUIStyle guiStyle = new ();
                guiStyle.fontSize = messages[a].FontSize;
                guiStyle.normal.textColor = messages[a].FontColor;
                guiStyle.wordWrap = DefaultWordWrapSetting;

                GUIContent guiContent = new ();
                guiContent.text = messages[a].MessageContent;

                float nextOffset = guiStyle.CalcHeight(guiContent, Screen.width);
                Rect messageRect = new(DefaultOffsetX, yAxisRef, Screen.width, nextOffset);

                GUI.Label(messageRect, guiContent, guiStyle);
                yAxisRef += nextOffset;
            }
        }

        /// <summary>
        /// Prints a floating message in screen.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        public static void Print(string message, float duration)
        {
            messages.Add(new Message(message, duration, DefaultFontColor, DefaultFontSize));
        }

        /// <summary>
        /// Prints a floating message in screen.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="fontColor"></param>
        public static void Print(string message, float duration, Color fontColor)
        {
            messages.Add(new Message(message, duration, fontColor, DefaultFontSize));
        }

        /// <summary>
        /// Prints a floating message in screen.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="duration"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontSize"></param>
        public static void Print(string message, float duration, Color fontColor, int fontSize)
        {
            messages.Add(new Message(message, duration, fontColor, fontSize));
        }
    }

    /// <summary>
    /// Container class for message definition.
    /// </summary>
    public sealed class Message
    {
        private readonly string messageContent;
        private readonly int fontSize;
        private readonly Color fontColor;

        private float duration;

        public string MessageContent { get { return messageContent; } }
        public int FontSize { get { return fontSize; } }
        public Color FontColor { get { return fontColor; } }

        public Message(string messageContent, float duration, Color fontColor, int fontSize)
        {
            this.messageContent = messageContent;
            this.duration = duration;
            this.fontColor = fontColor;
            this.fontSize = fontSize;
        }

        /// <summary>
        /// Subtracts inserted delta time value from life time.
        /// Returns false if life time no longer has a positive value.
        /// </summary>
        /// <param name="unscaledDeltaTime">Must be equal to unscaled delta time value provided by Unity.</param>
        /// <returns></returns>
        public bool UpdateLifeTime(float unscaledDeltaTime)
        {
            if (duration > 0)
            {
                duration -= unscaledDeltaTime;
                return true;
            }
            else
                return false;
        }
    }
}
