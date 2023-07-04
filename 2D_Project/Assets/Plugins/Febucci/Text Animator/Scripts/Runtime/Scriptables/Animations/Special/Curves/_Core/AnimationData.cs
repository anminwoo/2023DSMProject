using UnityEngine;

namespace Febucci.UI.Effects
{
    [System.Serializable]
    public class AnimationData
    {
        //--- Curves ---
        //Movement
        [FloatCurveProperty] public FloatCurve movementX = new FloatCurve(1, 0, 0);
        [FloatCurveProperty] public FloatCurve movementY = new FloatCurve(1, 0, 0);
        [FloatCurveProperty] public FloatCurve movementZ = new FloatCurve(1, 0, 0);

        //Scale
        [FloatCurveProperty] public FloatCurve scaleX = new FloatCurve(2, 0, 1);
        [FloatCurveProperty] public FloatCurve scaleY = new FloatCurve(2, 0, 1);

        //Rotation
        [FloatCurveProperty] public FloatCurve rotX = new FloatCurve(45, 0, 0);
        [FloatCurveProperty] public FloatCurve rotY = new FloatCurve(45, 0, 0);
        [FloatCurveProperty] public FloatCurve rotZ = new FloatCurve(45, 0, 0);

        //Color
        [ColorCurveProperty] public ColorCurve colorCurve = new ColorCurve(0);

        Vector3 movement;
        Vector2 scale;
        Quaternion rot;

        public bool TryCalculatingMatrix(Core.CharacterData character,
            float timePassed,
            float weight,
            out Matrix4x4 matrix,
            out Vector3 offset)
        {
            //Saves a bunch of performance in case user wants only color or movement
            //TODO cache
            matrix = new Matrix4x4();

            if (!(movementX.enabled || movementY.enabled || movementZ.enabled
                  || rotX.enabled || rotY.enabled || rotZ.enabled
                  || scaleX.enabled || scaleY.enabled))
            {
                offset = Vector2.zero;
                return false;
            }

            offset = (character.current.positions[0] + character.current.positions[2]) / 2f;

            //weighted rotation
            rot = Quaternion.Euler(
                    Mathf.LerpUnclamped(0, rotX.Evaluate(timePassed, character.index), weight),
                    Mathf.LerpUnclamped(0, rotY.Evaluate(timePassed, character.index), weight),
                    Mathf.LerpUnclamped(0, rotZ.Evaluate(timePassed, character.index), weight));

            //weighted movement
            movement = new Vector3(
                Mathf.LerpUnclamped(0, movementX.Evaluate(timePassed, character.index), weight),
                Mathf.LerpUnclamped(0, movementY.Evaluate(timePassed, character.index), weight),
                Mathf.LerpUnclamped(0, movementZ.Evaluate(timePassed, character.index), weight));

            //weighted scale
            scale = new Vector2(
                Mathf.LerpUnclamped(1, scaleX.Evaluate(timePassed, character.index), weight),
                Mathf.LerpUnclamped(1, scaleY.Evaluate(timePassed, character.index), weight));

            matrix.SetTRS(
                movement,
                rot,
                scale);

            return true;
        }

        public bool TryCalculatingColor(Core.CharacterData character, float timePassed, float weight, out Color32 color)
        {
            if(!colorCurve.enabled)
            {
                color = Color.white;
                return false;
            }

            color = colorCurve.Evaluate(timePassed, character.index);
            return true;
        }
    }
}