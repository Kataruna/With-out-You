using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DialogueProperties))]
public class DialogueBlueprintDrawer : PropertyDrawer
{
    private SerializedProperty _name;
    private SerializedProperty _mood;
    private SerializedProperty _sentence;

    private float lineHeight = EditorGUIUtility.singleLineHeight;

    //How to draw to the Inspector Window
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        //Fill our properties
        _name = property.FindPropertyRelative("character");
        _mood = property.FindPropertyRelative("mood");
        _sentence = property.FindPropertyRelative("message");
        
        //Draw foldOutBox
        Rect foldOutBox = new Rect(position.xMin, position.yMin , position.size.x, lineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, 
            $"Sentence {Convert.ToInt32(label.ToString().Substring(label.ToString().Length-1,1))+1}");

        if (property.isExpanded)
        {
            DrawNameProperty(position);
            DrawMoodProperty(position);
            DrawMessageProperty(position);
        }
        
        EditorGUI.EndProperty();
    }

    //Request more vertical Spacing, return it
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += 7;
        }
        
        return (lineHeight * totalLines);
    }
    
    private void DrawNameProperty(Rect position)
    {
        EditorGUIUtility.labelWidth = 60;

        float xPos = position.min.x;
        float yPos = position.min.y + (lineHeight * 1.25f);
        float width = position.size.x * .4f;
        float height = lineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _name, new GUIContent("Name"));
    }
    
    private void DrawMoodProperty(Rect position)
    {
        float xPos = position.min.x + (position.width * .5f);;
        float yPos = position.min.y + (lineHeight * 1.25f);
        float width = position.size.x * .5f;
        float height = lineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _mood, new GUIContent("Mood"));
    }

    private void DrawMessageProperty(Rect position)
    {
        EditorStyles.textField.wordWrap = true;
        float xPos = position.min.x;
        float yPos = position.min.y + (lineHeight*2.5f);
        float width = position.size.x;
        float height = lineHeight*5;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _sentence, new GUIContent("Message"));
    }
}
