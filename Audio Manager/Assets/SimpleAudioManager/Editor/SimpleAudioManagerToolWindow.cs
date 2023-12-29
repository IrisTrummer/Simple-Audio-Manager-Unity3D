using UnityEditor;
using UnityEngine;

namespace SimpleAudioManager.Editor
{
    public class SimpleAudioManagerToolWindow : EditorWindow
    {
        private bool enableSingleton = true;
        private bool mute;
        private bool changed;

        [MenuItem("Tools/Simple Audio Manager/Settings")]
        public static void ShowWindow()
        {
            var window = GetWindow<SimpleAudioManagerToolWindow>("Simple Audio Manager Settings");
            window.minSize = new Vector2(200f, 300f);
            window.LoadFlags();
        }

        private void LoadFlags()
        {
            enableSingleton = !IsFlagSet("NO_SINGLETON_AUDIO_MANAGER");
            mute = IsFlagSet("MUTE_AUDIO_MANAGER");

            Repaint();
        }

        private void OnGUI()
        {
            var headingStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold
            };

            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical();

            GUILayout.Label("Flags", headingStyle);
            GUILayout.Space(10);

            EditorGUILayout.HelpBox("Toggles the NO_SINGLETON_AUDIO_MANAGER flag. Disable this if you want to handle the audio manager instance by yourself.", MessageType.Info);
            enableSingleton = EditorGUILayout.Toggle("Enable Singleton", enableSingleton);

            GUILayout.Space(10);

            EditorGUILayout.HelpBox("Toggles the MUTE_AUDIO_MANAGER flag. You can enable this to e.g. mute the audio manager during test execution.", MessageType.Info);
            mute = EditorGUILayout.Toggle("Mute", mute);

            changed |= EditorGUI.EndChangeCheck();

            GUILayout.FlexibleSpace();

            if (changed)
                EditorGUILayout.HelpBox("There are unsaved changes. Click the 'Save' button below to apply them.", MessageType.Warning);

            if (GUILayout.Button("Save"))
            {
                SetFlag("NO_SINGLETON_AUDIO_MANAGER", !enableSingleton);
                SetFlag("MUTE_AUDIO_MANAGER", mute);
                changed = false;
            }

            GUILayout.EndVertical();
        }

        private bool IsFlagSet(string flag)
        {
            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            return symbols.Contains(flag);
        }

        private void SetFlag(string flag, bool enabled)
        {
            BuildTargetGroup targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

            if (enabled && !symbols.Contains(flag))
            {
                symbols += $";{flag}";
            }
            else if (!enabled && symbols.Contains(flag))
            {
                symbols = symbols.Replace(flag, "").Replace(";;", ";").TrimEnd(';');
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, symbols);
        }
    }
}