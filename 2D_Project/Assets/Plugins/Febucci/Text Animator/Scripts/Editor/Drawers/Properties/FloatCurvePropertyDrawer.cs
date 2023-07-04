using Febucci.UI.Effects;
using UnityEditor;
using UnityEngine;

namespace Febucci.UI.Core
{
    [CustomPropertyDrawer(typeof(FloatCurveProperty))]
    public class FloatCurvePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty enabled = property.FindPropertyRelative(nameof(FloatCurve.enabled));
            Rect lineByLine = position;
            lineByLine.height = EditorGUIUtility.singleLineHeight;
            
            Rect enabledRect = new Rect(lineByLine.xMax-30, lineByLine.y, 30, lineByLine.height);
            Rect expandedRect = new Rect(lineByLine.x, lineByLine.y, position.width-30, lineByLine.height);
            enabled.boolValue = EditorGUI.Toggle(enabledRect, enabled.boolValue);
            GUI.color = enabled.boolValue ? Color.white : Color.gray;
            
            property.isExpanded = EditorGUI.Foldout(expandedRect, property.isExpanded, label, true);
            GUI.color = Color.white;
            if(!property.isExpanded) return;
            GUI.enabled = enabled.boolValue;

            // -- inner area smaller ---
            position.y = lineByLine.yMax;
            position.height -= lineByLine.height;
            position.x += 15;
            position.width-=15;

            lineByLine.x = position.x;
            lineByLine.width = position.width;
            lineByLine.y += lineByLine.height;
            
            EditorGUI.PropertyField(lineByLine, property.FindPropertyRelative(nameof(FloatCurve.amplitude)));
            lineByLine.y += lineByLine.height;
            EditorGUI.PropertyField(lineByLine, property.FindPropertyRelative(nameof(FloatCurve.waveSize)));
            lineByLine.y += lineByLine.height;
            EditorGUI.PropertyField(lineByLine, property.FindPropertyRelative(nameof(FloatCurve.weightOverTime)));
            GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {           
            return (property.isExpanded)
                    ? EditorGUIUtility.singleLineHeight * 4
                    : EditorGUIUtility.singleLineHeight;
        }
    }
}