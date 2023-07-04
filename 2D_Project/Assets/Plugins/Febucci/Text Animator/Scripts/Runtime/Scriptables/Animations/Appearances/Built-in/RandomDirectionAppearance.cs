﻿using Febucci.UI.Core;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Effects
{
    [UnityEngine.Scripting.Preserve]
    [CreateAssetMenu(fileName = "RandomDir Appearance", menuName = "Text Animator/Animations/Appearances/Random Direction")]
    [EffectInfo("rdir", EffectCategory.Appearances)]
    public sealed class RandomDirectionAppearance : AppearanceScriptableBase
    {
        public float baseAmount = 10;
        float amount;
        Vector3[] directions;

        public override void ResetContext(TAnimCore animator)
        {
            base.ResetContext(animator);
            amount = baseAmount;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            directions = new Vector3[20];

            //Calculates a random direction for each character (which won't change)
            for(int i = 0; i < directions.Length; i++)
            {
                directions[i] = TextUtilities.fakeRandoms[Random.Range(0, TextUtilities.fakeRandomsCount - 1)] * Mathf.Sign(Mathf.Sin(i));
            }
        }

        
        public override void ApplyEffectTo(ref Core.CharacterData character, TAnimCore animator)
        {
            int index = character.index % directions.Length;
            
            //Moves all towards a direction
            character.current.positions.MoveChar(
                directions[index] 
                * amount 
                * character.uniformIntensity 
                * Tween.EaseIn(1 - character.passedTime / duration));
        }

        public override void SetModifier(ModifierInfo modifier)
        {
            switch (modifier.name)
            {
                case "a": amount = baseAmount * modifier.value; break;
                default: base.SetModifier(modifier); break;
            }
        }
    }

}