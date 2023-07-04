﻿using UnityEngine;

namespace Febucci.UI
{
    /// <summary>
    /// Built-in typewriter, which shows letters dynamically word after word.<br/>
    /// To enable it, add this component near a <see cref="Febucci.UI.Core.TAnimCore"/> one<br/>
    /// - Base class: <see cref="Febucci.UI.Core.TypewriterCore"/><br/>
    /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/typewriters/">TextAnimatorPlayers</see>
    /// </summary>
    [HelpURL("https://www.febucci.com/text-animator-unity/docs/typewriters/")]
    public class TypewriterByWord: Core.TypewriterCore
    {
        [SerializeField, Attributes.CharsDisplayTime] float waitForNormalWord = 0.3f;
        [SerializeField, Attributes.CharsDisplayTime] float waitForWordWithPuntuaction = 0.5f;
        [SerializeField, Attributes.CharsDisplayTime] float disappearanceDelay = 0.5f;
        
        bool IsCharInsideAnyWord(int charIndex)
        {
            return TextAnimator.Characters[charIndex].wordIndex >= 0;
        }

        protected override float GetWaitAppearanceTimeOf(int charIndex)
        {
            if (!IsCharInsideAnyWord(charIndex) && TextAnimator.latestCharacterShown.index>0)
            {
                int latestWordShownIndex = TextAnimator.Characters[TextAnimator.latestCharacterShown.index-1].wordIndex;
                if (latestWordShownIndex >= 0 && latestWordShownIndex < TextAnimator.WordsCount)
                {
                    var word = TextAnimator.Words[latestWordShownIndex];
                    return char.IsPunctuation(TextAnimator.Characters[word.lastCharacterIndex].info.character)
                        ? waitForWordWithPuntuaction
                        : waitForNormalWord;
                }

                return waitForNormalWord;
            }

            return 0;
        }

        protected override float GetWaitDisappearanceTimeOf(int charIndex)
        {
            return !IsCharInsideAnyWord(charIndex) ? disappearanceDelay : 0;
        }
    }
}