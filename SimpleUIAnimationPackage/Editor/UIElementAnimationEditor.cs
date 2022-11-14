// Version: 14112022
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIElementAnimation))]
public class UIElementAnimationEditor : Editor
{
    protected static bool showGeneralSettings = true;
    protected static bool showMoveSettings = true;
    protected static bool showScaleSettings = true;
    protected static bool showRotateSettings = true;
    protected static bool showAlphaFadeSettings = true;
    protected static bool showChangeColorSettings = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        UIElementAnimation animation = (UIElementAnimation)target;

        #region General
        showGeneralSettings = EditorGUILayout.Foldout(showGeneralSettings, "General");
        if (showGeneralSettings)
        {
            GUILayout.BeginVertical("box");
            // Animation Time
            EditorGUILayout.PropertyField(serializedObject.FindProperty("animationTime"));
            // Looping
            EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
            GUILayout.EndVertical();
        }
        #endregion

        #region Move        
        showMoveSettings = EditorGUILayout.Foldout(showMoveSettings, "Move");
        if (showMoveSettings)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("move"), new GUIContent("Apply"));
            if (animation.move)
            {
                // Start Position
                EditorGUILayout.PropertyField(serializedObject.FindProperty("startPosition"));
                // Start Position Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set Start"))
                {
                    animation.SetStartPosition();
                }
                if (GUILayout.Button("Show Start"))
                {
                    animation.ShowStartPosition();
                }
                GUILayout.EndHorizontal();
                // End Position
                EditorGUILayout.PropertyField(serializedObject.FindProperty("endPosition"));
                // End Position Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set End"))
                {
                    animation.SetEndPosition();
                }
                if (GUILayout.Button("Show End"))
                {
                    animation.ShowEndPosition();
                }
                GUILayout.EndHorizontal();
                // X Animation Curve        
                EditorGUILayout.PropertyField(serializedObject.FindProperty("xMoveCurve"));
                // Y Animation Curve        
                EditorGUILayout.PropertyField(serializedObject.FindProperty("yMoveCurve"));

            }
            GUILayout.EndVertical();
        }
        #endregion

        #region Scale
        showScaleSettings = EditorGUILayout.Foldout(showScaleSettings, "Scale");
        if (showScaleSettings)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("scale"), new GUIContent("Apply"));
            if (animation.scale)
            {
                // Start Scale
                EditorGUILayout.PropertyField(serializedObject.FindProperty("startScale"));
                // Start Scale Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set Start"))
                {
                    animation.SetStartScale();
                }
                if (GUILayout.Button("Show Start"))
                {
                    animation.ShowStartScale();
                }
                GUILayout.EndHorizontal();
                // End Scale
                EditorGUILayout.PropertyField(serializedObject.FindProperty("endScale"));
                // End Scale Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set End"))
                {
                    animation.SetEndScale();
                }
                if (GUILayout.Button("Show End"))
                {
                    animation.ShowEndScale();
                }
                GUILayout.EndHorizontal();
                // X Animation Curve
                EditorGUILayout.PropertyField(serializedObject.FindProperty("xScaleCurve"));
                // Y Animation Curve
                EditorGUILayout.PropertyField(serializedObject.FindProperty("yScaleCurve"));

            }
            GUILayout.EndVertical();
        }
        #endregion

        #region Rotate
        showRotateSettings = EditorGUILayout.Foldout(showRotateSettings, "Rotate");
        if (showRotateSettings)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotate"), new GUIContent("Apply"));
            if (animation.rotate)
            {
                // Start Rotation
                EditorGUILayout.PropertyField(serializedObject.FindProperty("startRotation"));
                // Start Rotation Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set Start"))
                {
                    animation.SetStartRotation();
                }
                if (GUILayout.Button("Show Start"))
                {
                    animation.ShowStartRotation();
                }
                GUILayout.EndHorizontal();
                // End Rotation
                EditorGUILayout.PropertyField(serializedObject.FindProperty("endRotation"));
                // End Rotation Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set End"))
                {
                    animation.SetEndRotation();
                }
                if (GUILayout.Button("Show End"))
                {
                    animation.ShowEndRotation();
                }
                GUILayout.EndHorizontal();
                // Z Animation Curve
                EditorGUILayout.PropertyField(serializedObject.FindProperty("zRotationCurve"));

            }
            GUILayout.EndVertical();
        }
        #endregion

        #region AlphaFade
        showAlphaFadeSettings = EditorGUILayout.Foldout(showAlphaFadeSettings, "Alpha Fade");
        if (showAlphaFadeSettings)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("alphaFade"), new GUIContent("Apply"));
            if (animation.alphaFade)
            {
                // Start Alpha
                EditorGUILayout.PropertyField(serializedObject.FindProperty("startAlpha"));
                // Start Alpha Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set Start"))
                {
                    animation.SetStartAlpha();
                }
                if (GUILayout.Button("Show Start"))
                {
                    animation.ShowStartAlpha();
                }
                GUILayout.EndHorizontal();
                // End Alpha
                EditorGUILayout.PropertyField(serializedObject.FindProperty("endAlpha"));
                // End Alpha Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set End"))
                {
                    animation.SetEndAlpha();
                }
                if (GUILayout.Button("Show End"))
                {
                    animation.ShowEndAlpha();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
        #endregion

        #region ColorChange
        showChangeColorSettings = EditorGUILayout.Foldout(showChangeColorSettings, "Color Change");
        if (showChangeColorSettings)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("colorChange"), new GUIContent("Apply"));
            if (animation.colorChange)
            {
                // Start Color
                EditorGUILayout.PropertyField(serializedObject.FindProperty("startColor"));
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set Start"))
                {
                    animation.SetStartColor();
                }
                if (GUILayout.Button("Show Start"))
                {
                    animation.ShowStartColor();
                }
                GUILayout.EndHorizontal();
                // End Color
                EditorGUILayout.PropertyField(serializedObject.FindProperty("endColor"));
                // End Rotation Buttons
                GUILayout.BeginHorizontal("box");
                if (GUILayout.Button("Set End"))
                {
                    animation.SetEndColor();
                }
                if (GUILayout.Button("Show End"))
                {
                    animation.ShowEndColor();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }
        #endregion

        serializedObject.ApplyModifiedProperties();
    }
}
