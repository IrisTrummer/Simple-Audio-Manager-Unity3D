using UnityEditor;
using UnityEngine;

namespace SimpleAudioManager.Editor
{
    public class SimpleAudioManagerToolWindow : EditorWindow
    {
        private bool enableSingleton = false;
        
        [MenuItem("Tools/Simple Audio Manager/Settings")]
        public static void ShowWindow()
        {
            var window = GetWindow<SimpleAudioManagerToolWindow>("Simple Audio Manager Settings");
            window.minSize = new Vector2(200f, 300f);
        }

        private void OnGUI()
        {
            var headingStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold
            };

            GUILayout.BeginVertical();

            GUILayout.Label("Flags", headingStyle);
            GUILayout.Space(10);

            EditorGUILayout.HelpBox("Sets the NO_SINGLETON_AUDIO_MANAGER flag. Disable this if you want to handle the audio manager instance by yourself.", MessageType.Info);
            enableSingleton = EditorGUILayout.Toggle("Enable Singleton", enableSingleton);

            GUILayout.Space(10);

            EditorGUILayout.HelpBox("Sets the MUTE_AUDIO_MANAGER flag. You can enable this to e.g. mute the audio manager during test execution.", MessageType.Info);
            enableSingleton = EditorGUILayout.Toggle("Enable Singleton", enableSingleton);

            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Save"))
            {
            }
            
            GUILayout.EndVertical();
        }
    }
}