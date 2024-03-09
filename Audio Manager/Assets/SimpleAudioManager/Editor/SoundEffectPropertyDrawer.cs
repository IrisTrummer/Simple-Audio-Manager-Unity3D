using UnityEditor;
using UnityEngine;

namespace SimpleAudioManager.Editor
{
    [CustomPropertyDrawer(typeof(SoundEffect))]
    public class SoundEffectPropertyDrawer : PropertyDrawer
    {
        private enum LabelType
        {
            None,
            Abbreviated,
            Full
        }

        private const float LabelHideWidth = 130;
        private const float LabelAbbreviationWidth = 200;
        private const float FullLabelWidth = 47f;
        private const float AbbreviatedLabelWidth = 25f;

        private const float VolumeFieldWidth = 50f;
        private const float Spacing = 10;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Tooltip
            Rect tooltipLabel = position;
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            tooltipLabel.width -= position.width;
            EditorGUI.LabelField(tooltipLabel, new GUIContent("", property.tooltip));

            // Space Partitioning
            // volume should get half of the available space at max, since input field doesn't need a lot of space
            LabelType labelType = GetFittingLabelType(position);
            float labelWidth = GetLabelWidthForType(labelType);
            float neededVolumePartWidth = Mathf.Min(labelWidth + VolumeFieldWidth, (position.width - Spacing) / 2f);

            // Audio Clip
            position.width -= neededVolumePartWidth + Spacing;
            SerializedProperty clip = property.FindPropertyRelative("audioClip");
            EditorGUI.PropertyField(position, clip, GUIContent.none);

            // Volume
            position.x += position.width + Spacing;
            position.width = neededVolumePartWidth;
            EditorGUIUtility.labelWidth = labelWidth;

            SerializedProperty volume = property.FindPropertyRelative("volume");
            EditorGUI.PropertyField(position, volume, labelType == LabelType.None ? GUIContent.none : new GUIContent(labelType == LabelType.Full ? "Volume" : "Vol."));

            EditorGUI.EndProperty();
        }

        LabelType GetFittingLabelType(Rect position)
        {
            return position.width switch
            {
                < LabelHideWidth => LabelType.None,
                < LabelAbbreviationWidth => LabelType.Abbreviated,
                _ => LabelType.Full
            };
        }

        float GetLabelWidthForType(LabelType labelType)
        {
            if (labelType == LabelType.None)
                return 0;

            return labelType == LabelType.Abbreviated ? AbbreviatedLabelWidth : FullLabelWidth;
        }
    }
}