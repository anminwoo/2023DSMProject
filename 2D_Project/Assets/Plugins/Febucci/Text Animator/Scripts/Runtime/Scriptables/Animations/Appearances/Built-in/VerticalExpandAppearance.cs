﻿using Febucci.UI.Core;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Effects
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Vertical Expand Appearance", menuName = "Text Animator/Animations/Appearances/Vertical Expand")]
    [EffectInfo("vertexp", EffectCategory.Appearances)]
    public sealed class VerticalExpandAppearance : AppearanceScriptableBase
    {
        public bool startsFromBottom = true;
        int startA, targetA;
        int startB, targetB;

        //--Temp variables--
        float pct;

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            SetOrientation(startsFromBottom);
        }

        void SetOrientation(bool fromBottom)
        {
            if (fromBottom) //From bottom to top 
            {

                //top left copies bottom left
                startA = 0;
                targetA = 1;

                //top right copies bottom right
                startB = 3;
                targetB = 2;
            }
            else //from top to bottom
            {

                //bottom left copies top left
                startA = 1;
                targetA = 0;

                //bottom right copies top right
                startB = 2;
                targetB = 3;
            }
        }

        public override void ApplyEffectTo(ref Core.CharacterData character, TAnimCore animator)
        {
            pct = Tween.EaseInOut(character.passedTime / duration);

           character.current.positions[targetA] = Vector3.LerpUnclamped(character.current.positions[startA], character.current.positions[targetA], pct);
           character.current.positions[targetB] = Vector3.LerpUnclamped(character.current.positions[startB], character.current.positions[targetB], pct);
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "bot": SetOrientation((int)modifier.value == 1); break;
                default: base.SetModifier(modifier); break; 
            }
        }
    }

}