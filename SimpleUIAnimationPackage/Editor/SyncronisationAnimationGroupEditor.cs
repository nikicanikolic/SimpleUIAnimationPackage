using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SyncronisationAnimationGroup))]
public class SyncronisationAnimationGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SyncronisationAnimationGroup animationGroup = (SyncronisationAnimationGroup)target;

        // Entry Animations
        EditorGUILayout.PropertyField(serializedObject.FindProperty("entryAnimations"));
        // Inver Button
        if (GUILayout.Button("Invert To Exit"))
        {
            animationGroup.InvertEntryToExit();
        }
        // Exit Animations
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exitAnimations"));

        serializedObject.ApplyModifiedProperties();
    }
}