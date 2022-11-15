using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Traveler))]
public class TravelerEditor : Editor
{
    private SerializedProperty mode;
    private SerializedProperty nextScene;
    private SerializedProperty sceneOrder;
    
    private SerializedProperty forceInteract;
    
    private SerializedProperty isRequireEvent;
    private SerializedProperty requireEvent;

    private SerializedProperty doChangeTime;
    private SerializedProperty destinationTimeline;
    private SerializedProperty destinationWorld;

    private SerializedProperty interfaceElement;
    private SerializedProperty icon;

    private void OnEnable()
    {
        mode = serializedObject.FindProperty("mode");
        nextScene = serializedObject.FindProperty("nextScene");
        sceneOrder = serializedObject.FindProperty("sceneOrder");
        
        forceInteract = serializedObject.FindProperty("forceInteract");
        
        isRequireEvent = serializedObject.FindProperty("isRequireEvent");
        requireEvent = serializedObject.FindProperty("requireEvent");
        
        doChangeTime = serializedObject.FindProperty("doChangeTime");
        destinationTimeline = serializedObject.FindProperty("destinationTimeline");
        destinationWorld = serializedObject.FindProperty("destinationWorld");
        
        icon = serializedObject.FindProperty("icon");
        interfaceElement = serializedObject.FindProperty("interfaceElement");
    }
    
    public override void OnInspectorGUI()
    {
        Traveler traveler = (Traveler)target;
        
        serializedObject.Update();

        EditorGUILayout.PropertyField(mode);
        switch (traveler.SelectedMode)
        {
            case Traveler.Mode.Edit:
                EditorGUILayout.PropertyField(nextScene);
                EditorGUILayout.PropertyField(sceneOrder);
                EditorGUILayout.PropertyField(doChangeTime);
                if (doChangeTime.boolValue)
                {
                    EditorGUILayout.PropertyField(destinationTimeline);
                    EditorGUILayout.PropertyField(destinationWorld);
                    EditorGUILayout.Space(10);
                }
                
                EditorGUILayout.PropertyField(forceInteract);
                
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.PropertyField(isRequireEvent);
                if (isRequireEvent.boolValue)
                    EditorGUILayout.PropertyField(requireEvent, GUIContent.none);
                
                EditorGUILayout.EndHorizontal();
                
                break;
            case Traveler.Mode.Maintenance:
                EditorGUILayout.PropertyField(interfaceElement);
                EditorGUILayout.PropertyField(icon);
                break;
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
