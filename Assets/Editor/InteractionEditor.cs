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
                interaction.SetEventKey(EditorGUILayout.TextField(interaction.EventRecord.eventName));
                
                //EditorGUILayout.LabelField("Event Status", GUILayout.MaxWidth(100));
                interaction.SetEventValue(EditorGUILayout.Toggle(interaction.EventRecord.status));
                
                EditorGUILayout.EndHorizontal();
                break;
            case Interaction.Type.Dialogue:
                EditorGUILayout.PropertyField(dialogue);
                break;
        }

        EditorGUILayout.PropertyField(forceInteract);
        EditorGUILayout.PropertyField(affectTimeline);
        
        if (interaction.AffectTimeline)
        {
            EditorGUILayout.PropertyField(timeline);
            EditorGUILayout.PropertyField(worldLine);
            EditorGUILayout.PropertyField(worldOrder);
        }
        
        EditorGUILayout.Space(10);
        
        EditorGUILayout.PropertyField(OnInteraction);
        
        serializedObject.ApplyModifiedProperties();
    }
}
