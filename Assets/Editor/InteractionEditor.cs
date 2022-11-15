using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interaction))]
public class InteractionEditor : Editor
{
    SerializedProperty type;
    SerializedProperty eventKey;
    SerializedProperty dialogue;
    SerializedProperty forceInteract;
    
    SerializedProperty affectTimeline;
    SerializedProperty timeline;
    SerializedProperty worldLine;
    SerializedProperty worldOrder;

    SerializedProperty OnInteraction;

    SerializedProperty interfaceElement;
    SerializedProperty icon;

    private SerializedProperty isRequireEvent;
    private SerializedProperty requireEvent;
    
    private void OnEnable()
    {
        type = serializedObject.FindProperty("type");
        eventKey = serializedObject.FindProperty("eventKey");
        dialogue = serializedObject.FindProperty("dialogue");
        forceInteract = serializedObject.FindProperty("forceInteract");
        
        affectTimeline = serializedObject.FindProperty("affectTimeline");
        timeline = serializedObject.FindProperty("timeline");
        worldLine = serializedObject.FindProperty("worldLine");
        worldOrder = serializedObject.FindProperty("worldOrder");
        
        OnInteraction = serializedObject.FindProperty("OnInteraction");
        interfaceElement = serializedObject.FindProperty("interfaceElement");
        icon = serializedObject.FindProperty("icon");
        
        isRequireEvent = serializedObject.FindProperty("isRequireEvent");
        requireEvent = serializedObject.FindProperty("requireEvent");
    }

    public override void OnInspectorGUI()
    {
        Interaction interaction = (Interaction)target;
        
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(type);

        switch (interaction.SelectedType)
        {
            case Interaction.Type.Event:
                //EditorGUILayout.PropertyField(eventKey);
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.LabelField("Event Settings",GUILayout.MaxWidth(100));
                EditorGUILayout.TextField(interaction.EventRecord.eventName);
                
                //EditorGUILayout.LabelField("Event Status", GUILayout.MaxWidth(100));
                EditorGUILayout.Toggle(interaction.EventRecord.status);
                
                EditorGUILayout.EndHorizontal();
                
                goto default;

            case Interaction.Type.Dialogue:
                EditorGUILayout.PropertyField(dialogue);
                
                goto default;
            
            case Interaction.Type.Maintenance:
                EditorGUILayout.PropertyField(interfaceElement);
                EditorGUILayout.PropertyField(icon);
                break;
            
            default:
                EditorGUILayout.PropertyField(forceInteract);

                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.PropertyField(isRequireEvent);
                if(isRequireEvent.boolValue)
                    EditorGUILayout.PropertyField(requireEvent, GUIContent.none);
                
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.PropertyField(affectTimeline);
                if (affectTimeline.boolValue)
                {
                    EditorGUILayout.PropertyField(timeline);
                    EditorGUILayout.PropertyField(worldLine);
                    EditorGUILayout.PropertyField(worldOrder);
                }
                break;
        }
        
        EditorGUILayout.Space(10);
        
        EditorGUILayout.PropertyField(OnInteraction);
        
        serializedObject.ApplyModifiedProperties();
    }
}
