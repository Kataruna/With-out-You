using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DialogueProperties))]
public class DialogueBlueprintDrawer : PropertyDrawer
{
    private SerializedProperty _mode;
    private SerializedProperty _character;
    private SerializedProperty _name;
    private SerializedProperty _mood;
    private SerializedProperty _sentence;
    private SerializedProperty _choices;
    private SerializedProperty _eventKey;
    private SerializedProperty _eventStatus;
    private SerializedProperty _event;
    private SerializedProperty _timeline;
    private SerializedProperty _world;
    private SerializedProperty _doChangeOnThisState;
    private SerializedProperty _animatronic;
    private SerializedProperty _actionScript;

    private float lineHeight = EditorGUIUtility.singleLineHeight;

    //How to draw to the Inspector Window
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.PropertyField(position, property, label, true);

        #region CustomInspector

        EditorGUI.BeginProperty(position, label, property);

        //Fill our properties
        _mode = property.FindPropertyRelative("mode");
        _character = property.FindPropertyRelative("character");
        _name = property.FindPropertyRelative("name");
        _mood = property.FindPropertyRelative("mood");
        _sentence = property.FindPropertyRelative("message");
        _choices = property.FindPropertyRelative("choices");
        _eventKey = property.FindPropertyRelative("eventKey");
        _eventStatus = property.FindPropertyRelative("eventStatus");
        _event = property.FindPropertyRelative("events");
        _timeline = property.FindPropertyRelative("timeline");
        _world = property.FindPropertyRelative("world");
        _doChangeOnThisState = property.FindPropertyRelative("doChangeOnThisState");
        _animatronic = property.FindPropertyRelative("animatronic");
        _actionScript = property.FindPropertyRelative("actionScript");

        //Draw foldOutBox
        Rect foldOutBox = new Rect(position.xMin, position.yMin, position.size.x, lineHeight);

        switch ((DialogueProperties.Mode) _mode.intValue)
        {
            case DialogueProperties.Mode.MainCharacter:
                if (_sentence.stringValue == String.Empty)
                {
                    property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                        $"{(DialogueProperties.Character) _character.intValue}");
                }
                else if (_sentence.stringValue.Length < 40)
                {
                    property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                        $"{(DialogueProperties.Character) _character.intValue} - {_sentence.stringValue}...");
                }
                else
                {
                    property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                        $"{(DialogueProperties.Character) _character.intValue} - {_sentence.stringValue.Substring(0, 40)}...");
                }

                break;
            case DialogueProperties.Mode.SideCharacter:
                if (_sentence.stringValue == String.Empty)
                {
                    property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                        $"{_name.stringValue}");
                }
                else if (_sentence.stringValue.Length < 40)
                {
                    property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                        $"{_name.stringValue} - {_sentence.stringValue}...");
                }
                else
                {
                    property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                        $"{_name.stringValue} - {_sentence.stringValue.Substring(0, 40)}...");
                }

                break;
            case DialogueProperties.Mode.Choice:
                property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                    $"Choice");
                break;
            case DialogueProperties.Mode.SwitchMood:
                property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                    $"{(DialogueProperties.Character) _character.intValue}");
                break;
            case DialogueProperties.Mode.UpdateEvent:
                property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                    "Update Event Horizon");
                break;
            case DialogueProperties.Mode.Event:
                property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                    "Events");
                break;
            case DialogueProperties.Mode.TimelineChange:
                property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                    "Timeline Change");
                break;
            case DialogueProperties.Mode.Action:
                property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded,
                    "Action!");
                break;
        }


        //Sentence {Convert.ToInt32(label.ToString().Substring(label.ToString().Length-1,1))+1}

        if (property.isExpanded)
        {
            /*
            _mode.intValue =
                EditorGUI.Popup(
                    new Rect(position.min.x, position.min.y + (lineHeight * 1.25f), position.size.x, lineHeight),
                    "Mode", _mode.intValue, _mode.enumNames);
                    */

            DrawModeProperty(position, 1);

            switch ((DialogueProperties.Mode) _mode.intValue)
            {
                case DialogueProperties.Mode.MainCharacter:
                    DrawCharacterProperty(position, 2);
                    DrawMoodProperty(position, 2);
                    DrawMessageProperty(position, 3);
                    break;
                case DialogueProperties.Mode.SideCharacter:
                    DrawNameProperty(position, 2);
                    DrawMessageProperty(position, 3);
                    break;
                case DialogueProperties.Mode.Choice:
                    DrawChoiceProperty(position, 2);
                    break;
                case DialogueProperties.Mode.SwitchMood:
                    DrawCharacterProperty(position, 2);
                    DrawMoodProperty(position, 2);
                    break;
                case DialogueProperties.Mode.UpdateEvent:
                    DrawEventProperty(position, 2);
                    break;
                case DialogueProperties.Mode.Event:
                    DrawEventsProperty(position, 2);
                    break;
                case DialogueProperties.Mode.TimelineChange:
                    DrawTimelineProperty(position, 2);
                    DrawWorldProperty(position, 3);
                    DrawDoChangeTimelineProperty(position, 4);
                    break;
                case DialogueProperties.Mode.Action:
                    DrawAnimatronicProperty(position, 2);
                    DrawActionScriptProperty(position, 3);
                    break;
            }
        }

        EditorGUI.EndProperty();

        #endregion
    }

    //Request more vertical Spacing, return it
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += 16;
        }

        return (lineHeight * totalLines);
    }

    private void DrawModeProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;

        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);
        EditorGUI.PropertyField(drawArea, _mode, new GUIContent("Mode"));
    }

    private void DrawCharacterProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;
        
        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.half, line, 1);
        EditorGUI.PropertyField(drawArea, _character, new GUIContent("Name"));
    }
    
    private void DrawAnimatronicProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;
        
        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.half, line, 1);
        EditorGUI.PropertyField(drawArea, _animatronic, new GUIContent("Actor"));
    }

    private void DrawNameProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;


        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);
        EditorGUI.PropertyField(drawArea, _name, new GUIContent("Name"));
    }

    private void DrawMoodProperty(Rect position, int line)
    {

        Rect drawArea = DrawProperty(position, PositionInLine.back, HorizontalSize.half, line, 1);
        EditorGUI.PropertyField(drawArea, _mood, new GUIContent("Mood"));
    }

    private void DrawMessageProperty(Rect position, int line)
    {
        EditorStyles.textField.wordWrap = true;
        
        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 5);
        EditorGUI.PropertyField(drawArea, _sentence, new GUIContent("Message"));
    }

    private void DrawChoiceProperty(Rect position, int line)
    {
        EditorStyles.textField.wordWrap = true;
        
        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);
        EditorGUI.PropertyField(drawArea, _choices, new GUIContent("Message"));
    }
    
    private void DrawEventProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;
        
        Rect drawAreaKey = DrawProperty(position, PositionInLine.front, HorizontalSize.half, line, 1);
        Rect drawAreaStatus = DrawProperty(position, PositionInLine.back, HorizontalSize.half, line, 1);
        
        EditorGUI.PropertyField(drawAreaKey, _eventKey, new GUIContent("Key"));
        EditorGUI.PropertyField(drawAreaStatus, _eventStatus, new GUIContent("Status"));
    }
    
    private void DrawEventsProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;

        Rect drawAreaKey = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);

        EditorGUI.PropertyField(drawAreaKey, _event, new GUIContent("Event"));
    }
    
    private void DrawTimelineProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;

        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);

        EditorGUI.PropertyField(drawArea, _timeline, new GUIContent("Timeline"));
    }
    
    private void DrawWorldProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;

        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);

        EditorGUI.PropertyField(drawArea, _world, new GUIContent("Worldline"));
    }
    
    private void DrawDoChangeTimelineProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;

        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);

        EditorGUI.PropertyField(drawArea, _doChangeOnThisState, new GUIContent("Switch"));
    }
    
    private void DrawActionScriptProperty(Rect position, int line)
    {
        EditorGUIUtility.labelWidth = 60;

        Rect drawArea = DrawProperty(position, PositionInLine.front, HorizontalSize.full, line, 1);

        EditorGUI.PropertyField(drawArea, _actionScript, new GUIContent("Action Script"));
    }

    private Rect DrawProperty(Rect position, PositionInLine pos, HorizontalSize size, int line, float heightScale)
    {
        float xPos = position.min.x;

        switch (pos)
        {
            case PositionInLine.front:
                xPos = position.min.x;
                break;
            case PositionInLine.back:
                xPos = position.min.x + (position.width * .51f);
                break;
        }

        float yPos = position.min.y + (lineHeight * line);
        if (line > 1) yPos += 5 * (line - 1);

        float width = position.size.x;
        switch (size)
        {
            case HorizontalSize.full:
                width = position.size.x;
                break;
            case HorizontalSize.half:
                width = position.size.x * 0.5f;
                break;
        }

        float height = lineHeight * heightScale;

        return new Rect(xPos, yPos, width, height);
    }

    private enum HorizontalSize
    {
        half,
        full
    }

    private enum PositionInLine
    {
        front,
        back
    }
}