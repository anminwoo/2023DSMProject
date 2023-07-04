﻿using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Core.Parsing;
using UnityEngine;
using UnityEngine.Events;

namespace Febucci.UI.Core
{
    /// <summary>
    /// Base class for all Typewriters. <br/>
    /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/typewriters/">Typewriters</see>.<br/>
    /// </summary>
    /// <remarks>
    /// If you want to use the built-in Typewriter, see: <see cref="TypewriterByCharacter"/> or <see cref="TypewriterByWord"/><br/>
    /// <br/>
    /// You can also create custom typewriters by inheriting from this class. <br/>
    /// Manual: <see href="https://www.febucci.com/text-animator-unity/docs/writing-custom-typewriters-c-sharp/">Writing Custom Typewriters (C#)</see>
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Core.TAnimCore))]
    public abstract class TypewriterCore : MonoBehaviour
    {
        [System.Flags]
        public enum StartTypewriterMode
        {
            /// <summary>
            /// Typewriter starts typing ONLY if you invoke "StartShowingText" from any of your script.
            /// </summary>
            FromScriptOnly = 0,

            /// <summary>
            /// Typewriter automatically starts/resumes from the "OnEnable" method
            /// </summary>
            OnEnable = 1,

            /// <summary>
            /// Typewriter automatically starts once you call "ShowText" method [includes Easy Integration]
            /// </summary>
            OnShowText = 2,

            AutomaticallyFromAllEvents = OnEnable | OnShowText //legacy support for unity 2018.x [instead of automatic recognition in 2019+] 
        }

        #region Variables

        #region Management Variables

        TAnimCore _textAnimator;

        /// <summary>
        /// The TextAnimator Component linked to this typewriter
        /// </summary>
        public TAnimCore TextAnimator
        {
            get
            {
                if (_textAnimator != null)
                    return _textAnimator;

#if UNITY_2019_2_OR_NEWER
                if(!TryGetComponent(out _textAnimator))
                {
                    Debug.LogError($"TextAnimator: Text Animator component is null on GameObject {gameObject.name}. Please add a component that inherits from TAnimCore");
                }
#else
                _textAnimator = GetComponent<New.TAnimCore>();
                Assert.IsNotNull(_textAnimator, $"Text Animator Component component is null on GameObject {gameObject.name}. Please add a component that inherits from TAnimCore");
#endif

                return _textAnimator;
            }
        }

        #endregion

        #region Typewriter settings
        /// <summary>
        /// <c>true</c> if the typewriter is enabled
        /// </summary>
        [Tooltip("True if you want to shows the text dynamically")]
        [SerializeField] public bool useTypeWriter = true;

        [SerializeField, Tooltip("Controls from which method(s) the typewriter will automatically start/resume. Default is 'Automatic'")]
        public StartTypewriterMode startTypewriterMode = StartTypewriterMode.AutomaticallyFromAllEvents;

        #region Typewriter Skip
        [SerializeField]
        bool hideAppearancesOnSkip = false;
        [SerializeField, Tooltip("True = plays all remaining events once the typewriter has been skipped")]
        bool triggerEventsOnSkip = false;
        #endregion


        [SerializeField, Tooltip("True = resets the typewriter speed every time a new text is set/shown")] public bool resetTypingSpeedAtStartup = true;

        public enum DisappearanceOrientation
        {
            SameAsTypewriter,
            Inverted
        }

        [SerializeField] public DisappearanceOrientation disappearanceOrientation;

        #endregion

        #endregion

        #region Events
        /// <summary>
        /// Called once the text is completely shown. <br/>
        /// If the typewriter is enabled, this event is called once it has ended showing all letters.
        /// </summary>
        public UnityEvent onTextShowed = new UnityEvent();

        /// <summary>
        /// Called once the typewriter starts showing text.<br/>
        /// </summary>
        /// <remarks>
        /// It is only invoked when the typewriter is enabled.
        /// </remarks>
        public UnityEvent onTypewriterStart = new UnityEvent();

        /// <summary>
        /// Called once the typewriter has completed hiding all the letters.
        /// </summary>
        /// <remarks>
        /// It is only invoked when the typewriter is enabled.
        /// </remarks>
        public UnityEvent onTextDisappeared = new UnityEvent();

        /// <summary>
        /// Called once a character has been shown by the typewriter.<br/>
        /// </summary>
        /// <remarks>
        /// It is only invoked when the typewriter is enabled.
        /// </remarks>
        public CharacterEvent onCharacterVisible = new CharacterEvent();
        
        
        /// <summary>
        /// Called once an event has been shown by the typewriter.<br/>
        /// See the <a href="https://www.febucci.com/text-animator-unity/docs/triggering-events-while-typing/">Events Manual</a> for more info.
        /// </summary>
        /// <remarks>
        /// It is only invoked when the typewriter is enabled.
        /// </remarks>
        public MessageEvent onMessage = new MessageEvent();
        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the given text to the connected TextAnimator component.<br/>
        /// If enabled, it also starts showing letters dynamically. <br/>
        /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/text-animator-players/">Text Animator Players</see>
        /// </summary>
        /// <param name="text"></param>
        /// <remarks>
        /// If the typewriter is enabled but its start mode <see cref="startTypewriterMode"/> doesn't include <see cref="StartTypewriterMode.OnShowText"/>, this method won't start showing letters. You'd have to manually call <see cref="StartShowingText"/> in order to start the typewriter, or include different "start modes" like <see cref="StartTypewriterMode.OnEnable"/> and let the script manage it automatically.
        /// </remarks>
        public void ShowText(string text)
        {
            // --- SETS TEXT ---
            if (string.IsNullOrEmpty(text))
            {
                TextAnimator.SetText(string.Empty, true);
                return;
            }

            TextAnimator.SetText(text, useTypeWriter);
            TextAnimator.firstVisibleCharacter = 0;

            // --- TYPEWRITER ---
            if (!useTypeWriter)
                onTextShowed?.Invoke();
            else if (startTypewriterMode.HasFlag(StartTypewriterMode.OnShowText))
                StartShowingText(true);
        }

        
        /// <summary>
        /// Skips the typewriter animation (if it's currently showing).<br/>
        /// In case the text is revealing, it will show all the letters immediately.<br/>
        /// In case the text is hiding, it will hide all the letters immediately.
        /// </summary>
        /// <remarks>
        /// If both revealing and hiding are occurring, hiding will prevail.
        /// </remarks>
        public void SkipTypewriter()
        {
            if (isShowingText)
            {
                StopAllCoroutines();
                isShowingText = false;
                
                TextAnimator.SetVisibilityEntireText(true, !hideAppearancesOnSkip);
                
                if (triggerEventsOnSkip)
                {
                    TriggerEventsUntil(int.MaxValue);
                }
                
                onTextShowed?.Invoke();
            }
            
            //TODO is hiding

        }

        
        #region Typewriter

        #region Appearing

        /// <summary>
        /// True if the typewriter is currently showing letters
        /// </summary>
        public bool isShowingText { get; private set; }

        /// <summary>
        /// Starts showing letters dynamically
        /// </summary>
        /// <param name="restart"><code>false</code> if you want the typewriter to resume where it has left. <code>true</code> if the typewriter should restart from character 0</param>
        public void StartShowingText(bool restart = false)
        {
            if(TextAnimator.CharactersCount==0) return;
            
            if (!useTypeWriter)
            {
                Debug.LogWarning("TextAnimator: couldn't start coroutine because 'useTypewriter' is disabled");
                return;
            }

            if (isShowingText)
            {
                StopShowingText();
            }

            if (restart)
            {
                TextAnimator.SetVisibilityEntireText(false, false);
                latestActionTriggered = 0;
                latestEventTriggered = 0;
            }

            if (resetTypingSpeedAtStartup) internalSpeed = 1;
            isShowingText = true;
            showRoutine = StartCoroutine(ShowTextRoutine());
        }

        protected abstract float GetWaitAppearanceTimeOf(int charIndex);

        Coroutine showRoutine;
        Coroutine nestedActionRoutine;
        
        float GetDeltaTime(TypingInfo typingInfo) => TextAnimator.time.deltaTime * internalSpeed * typingInfo.speed;
        IEnumerator ShowTextRoutine()
        {
            isShowingText = true;
            
            // --- INITIALIZATION ---
            TypingInfo typingInfo = new TypingInfo();
            
            // --- CALLBACKS ---
            onTypewriterStart?.Invoke();

            TextAnimatorSettings settings = TextAnimatorSettings.Instance;
            bool actionsEnabled = settings && settings.actions.enabled;
            
            // --- SHOWS TEXT LETTERS ---
            for(int i=0;i<TextAnimator.CharactersCount;i++)
            {
                // -- actions --
                if (actionsEnabled)
                {
                    int maxIndex = i + 1;
                    for (int a = latestActionTriggered; a < TextAnimator.Actions.Length && TextAnimator.Actions[a].index<maxIndex; a++)
                    {
                        var actionMarker = TextAnimator.Actions[a];
                        TriggerEventsBeforeAction(maxIndex, actionMarker);
                        yield return nestedActionRoutine = StartCoroutine(TextAnimator.DatabaseActions[actionMarker.name]?.DoAction(actionMarker, this, typingInfo));
                        latestActionTriggered = a+1;
                    }
                }
                
                // -- events --
                TriggerEventsUntil(i+1);
                
                // -- shows letter --
                TextAnimator.SetVisibilityChar(i, true);
                onCharacterVisible?.Invoke(TextAnimator.latestCharacterShown.info.character);
                
                // -- WAITS TIME -- (identical to HideTextRoutine, in order to skip frames correctly)
                float timeToWait = GetWaitAppearanceTimeOf(i);
                
                float deltaTime = GetDeltaTime(typingInfo);
                if (timeToWait < 0) timeToWait = 0;
                if (timeToWait < deltaTime) //waiting less time than a frame, we don't wait yet
                {
                    typingInfo.timePassed += timeToWait;
                    if (typingInfo.timePassed >= deltaTime) //waits only if we "surpassed" a frame duration
                    {
                        yield return null;
                        //saves remaining time to next frame as already waited time
                        typingInfo.timePassed %= deltaTime; 
                    }
                }
                else
                {
                    //waits until enough time has passed
                    while (typingInfo.timePassed < timeToWait)
                    {
                        typingInfo.timePassed += deltaTime;
                        yield return null;
                        deltaTime = GetDeltaTime(typingInfo);
                    }

                    typingInfo.timePassed %= timeToWait; //saves remaining time to next frame
                }
            }

            // --- FINALIZATION ---
            if (actionsEnabled)
            {
                for (int a = latestActionTriggered; a < TextAnimator.Actions.Length && TextAnimator.Actions[a].index<int.MaxValue; a++)
                {
                    var actionMarker = TextAnimator.Actions[a];
                    TriggerEventsBeforeAction(int.MaxValue, actionMarker);
                    yield return nestedActionRoutine = StartCoroutine(TextAnimator.DatabaseActions[actionMarker.name]?.DoAction(actionMarker, this, typingInfo));
                    latestActionTriggered = a+1;
                }
            }
            TriggerEventsUntil(int.MaxValue);

            // --- CALLBACKS ---
            onTextShowed?.Invoke();
            isShowingText = false;
        }

        /// <summary>
        /// Stops showing letters dynamically, leaving the text as it is.
        /// </summary>
        public void StopShowingText()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) //prevents from firing in edit mode from the context menu
                return;
#endif
            if(!isShowingText) return;
            isShowingText = false;
            
            if(showRoutine!=null) StopCoroutine(showRoutine);
            if(nestedActionRoutine!=null) StopCoroutine(nestedActionRoutine);
            if(isHidingText) StartDisappearingText();
        }

        #endregion

        #region Disappearing

        /// <summary>
        /// True if the typewriter is currently disappearing the text
        /// </summary>
        public bool isHidingText { get; private set; }
        
        /// <summary>
        /// Starts disappearing the text dynamically
        /// </summary>
        [ContextMenu("Start Disappearing Text")]
        public void StartDisappearingText()
        {
            if (disappearanceOrientation == DisappearanceOrientation.Inverted && isShowingText)
            {
                Debug.LogWarning("TextAnimatorPlayer: Can't start disappearance routine in the opposite direction of the typewriter, because you're still showing the text! (the typewriter might get stuck trying to show and override letters that keep disappearing)");
                return;
            }

            if(isHidingText) return;
            hideRoutine = StartCoroutine(HideTextRoutine());
        }

        Coroutine hideRoutine;
        Coroutine nestedHideRoutine;
        /// <summary>
        /// Stops the typewriter's from disappearing the text dynamically, leaving the text at its current state
        /// </summary>
        [ContextMenu("Stop Disappearing Text")]
        public void StopDisappearingText()
        {
            if(!isHidingText) return;
            isHidingText = false;
            
            if(hideRoutine!=null)StopCoroutine(hideRoutine);
            if(nestedHideRoutine!=null)StopCoroutine(nestedHideRoutine);
            if(isShowingText) StartShowingText(false);
        }

        /// <summary>
        /// Handles characters delay when disappearing text.
        /// </summary>
        /// <param name="charIndex">Current character that should decide how much time to wait. Check <see cref="TAnimCore.Characters"/> to view its info</param>
        /// <returns>time to wait before disappearing the next character</returns>
        protected virtual float GetWaitDisappearanceTimeOf(int charIndex) => GetWaitAppearanceTimeOf(charIndex);
        
        IEnumerator HideTextRoutine()
        {
            isHidingText = true;
            
            // --- INITIALIZATION ---
            TypingInfo typingInfo = new TypingInfo();
            
            // --- CALLBACKS ---
            
            // --- HIDES TEXT ---
            for (int i = 0; i < TextAnimator.CharactersCount; i++)
            {
                TextAnimator.SetVisibilityChar(i, false);
                float timeToWait = GetWaitDisappearanceTimeOf(i);
                
                // -- WAITS TIME -- (identical to ShowTextRoutine, in order to skip frames correctly)
                float deltaTime = GetDeltaTime(typingInfo);
                if (timeToWait < 0) timeToWait = 0;
                if (timeToWait < deltaTime) //waiting less time than a frame, we don't wait yet
                {
                    typingInfo.timePassed += timeToWait;
                    if (typingInfo.timePassed >= deltaTime) //waits only if we "surpassed" a frame duration
                    {
                        yield return null;
                        //saves remaining time to next frame as already waited time
                        typingInfo.timePassed %= deltaTime; 
                    }
                }
                else
                {
                    //waits until enough time has passed
                    while (typingInfo.timePassed < timeToWait)
                    {
                        typingInfo.timePassed += deltaTime;
                        yield return null;
                        deltaTime = GetDeltaTime(typingInfo);
                    }

                    typingInfo.timePassed %= timeToWait; //saves remaining time to next frame
                }
            }

            // --- CALLBACKS ---
            onTextDisappeared?.Invoke();
            isHidingText = false;
        }

        #endregion

        /// <summary>
        /// Makes the typewriter slower/faster, by setting its internal speed multiplier.
        /// </summary>
        /// <param name="value"></param>
        /// <example>
        /// If the typewriter has to wait <c>1</c> second to show the next letter but you set the typewriter speed to <c>2</c>, the typewriter will wait <c>0.5</c> seconds.
        /// </example>
        /// <remarks>
        /// The minimum value is 0.001
        /// </remarks>
        public void SetTypewriterSpeed(float value)
        {
            internalSpeed = Mathf.Clamp(value, .001f, value);
        }

        #endregion

        #endregion

        
        #region Utilties
        
        float internalSpeed = 1;
        
        #region Actions and Events

        int latestActionTriggered = 0;
        int latestEventTriggered = 0;

        void TriggerEventsBeforeAction(int maxIndex, ActionMarker action)
        {
            for (int i = latestEventTriggered; i < TextAnimator.Events.Length && TextAnimator.Events[i].index<maxIndex && TextAnimator.Events[i].internalOrder < action.internalOrder; i++)
            {
                onMessage?.Invoke(TextAnimator.Events[i]);
                latestEventTriggered = i+1;
            }
        }
        void TriggerEventsUntil(int maxIndex)
        {
            for (int i = latestEventTriggered; i < TextAnimator.Events.Length && TextAnimator.Events[i].index<maxIndex; i++)
            {
                onMessage?.Invoke(TextAnimator.Events[i]);
                latestEventTriggered = i+1;
            }
        }

        /// <summary>
        /// Triggers all messages/events that have not yet been triggered, even if they're not shown in the yet.
        /// </summary>
        /// <remarks>
        /// <seealso cref="TriggerVisibleEvents"/>
        /// </remarks>
        public void TriggerRemainingEvents() => TriggerEventsUntil(int.MaxValue);

        /// <summary>
        /// Triggers all messages/events that have not been triggered, but that are in the visible range of the text.
        /// </summary>
        /// <remarks>
        /// <seealso cref="TriggerRemainingEvents"/>
        /// </remarks>
        public void TriggerVisibleEvents() => TriggerEventsUntil(TextAnimator.latestCharacterShown.index);
        #endregion

        #endregion
        
        
        /// <summary>
        /// Unity's default MonoBehavior 'OnEnable' callback.
        /// </summary>
        /// <remarks>
        /// P.S. If you're overriding this method, don't forget to invoke the base one.
        /// </remarks>
        protected virtual void OnEnable()
        {
            if (!useTypeWriter)
                return;

            if (!startTypewriterMode.HasFlag(StartTypewriterMode.OnEnable))
                return;

            StartShowingText();
        }

        /// <summary>
        /// Unity's default MonoBehavior 'OnDisable' callback.
        /// </summary>
        /// <remarks>
        /// P.S. If you're overriding this method, don't forget to invoke the base one.
        /// </remarks>
        protected virtual void OnDisable()
        {
            // for backwards compatibility
        }
        
        #region Obsolete

        [System.Obsolete("Please set the speed through 'SetTypewriterSpeed' method instead")]
        protected float typewriterPlayerSpeed
        {
            get => internalSpeed;
            set => SetTypewriterSpeed(value);
        }

        [System.Obsolete("Please skip the typewriter via the 'SkipTypewriter' method instead")]
        protected bool wantsToSkip
        {
            get => throw new System.NotImplementedException();
            set
            {
                if(value) SkipTypewriter();
            }
        }

        [System.Obsolete("Please use 'isShowingText' instead")]
        protected bool isBaseInsideRoutine => isShowingText;
        
        
        [System.Obsolete("Please use 'TextAnimator' instead")]
        public TAnimCore textAnimator => TextAnimator;

        #endregion

    }

}