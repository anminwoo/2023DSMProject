﻿using Febucci.UI.Core;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Effects
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "Diagonal Expand Appearance", menuName = "Text Animator/Animations/Appearances/Diagonal Expand")]
    [EffectInfo("diagexp", EffectCategory.Appearances)]
    public sealed class DiagonalExpandAppearance : AppearanceScriptableBase
    {
        public bool diagonalFromBttmLeft;

        int targetA;
        int targetB;
        
        //--Temp variables--
        Vector3 middlePos;
        float pct;

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            diagonalFromBttmLeft = true;
            UpdateOrientation();
        }

        void UpdateOrientation()
        {
            if (diagonalFromBttmLeft) //expands bottom left and top right
            {
                targetA = 0;
                targetB = 2;
            }
            else //expands bottom right and top left
            {
                targetA = 1;
                targetB = 3;
            }
        }

        public override void ApplyEffectTo(ref Core.CharacterData character, TAnimCore animator)
        {
            middlePos = character.current.positions.GetMiddlePos();
            pct = Tween.EaseInOut(character.passedTime / duration);

            character.current.positions[targetA] = Vector3.LerpUnclamped(middlePos, character.current.positions[targetA], pct);
            //top right copies from bottom right
            character.current.positions[targetB] = Vector3.LerpUnclamped(middlePos, character.current.positions[targetB], pct);
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "bot":
                    diagonalFromBttmLeft = (int)modifier.value == 1;
                    UpdateOrientation();
                    break;
                default: base.SetModifier(modifier); break;
            }
        }
    }

}