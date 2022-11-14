// Version: 14112022
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimateChildrenAnimationGroup))]
public class AnimateChildrenAnimationGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        AnimateChildrenAnimationGroup animationGroup = (AnimateChildrenAnimationGroup)target;

        // Children
        EditorGUILayout.PropertyField(serializedObject.FindProperty("animationType"));
        // Looping
        if (animationGroup.animationType == ChildrenAnimationType.Series)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}