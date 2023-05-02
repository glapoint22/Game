using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Mission))]
public class MissionDrawer : PropertyDrawer
{
    // ------------------------------------------------------------------------------- On GUI -------------------------------------------------------------------------------
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.isExpanded, label);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel = 1;
            Rect rect = new(position.x, position.y + 20, position.width, position.height);

            // Title
            rect = SetProperty(property.FindPropertyRelative("title"), rect);
            rect = Separator(rect);


            // Mission Objectives
            rect = SetProperty(property.FindPropertyRelative("objectives"), rect);
            rect = Separator(rect);




            // Prerequisite
            SerializedProperty prerequisiteProperty = property.FindPropertyRelative("prerequisite");
            rect = SetProperty(prerequisiteProperty, rect);

            // If a prerequisite is chosen
            if (prerequisiteProperty.boolValue)
            {
                // Access the MissionsManager object from the serialized object
                MissionsManager missionsManager = (MissionsManager)property.serializedObject.targetObject;

                // Create an array of mission titles, excluding the current mission
                string[] missions = missionsManager.Missions
                .Where(x => x.Title != property.FindPropertyRelative("title").stringValue)
                .Select(x => x.Title)
                .ToArray();

                // Get the ID of the prerequisite mission
                string prerequisiteMissionId = property.FindPropertyRelative("prerequisiteMissionId").stringValue;

                // Find the index of the prerequisite mission in the missions array, or default to 0 if it's not found
                int selectedIndex = string.IsNullOrEmpty(prerequisiteMissionId) ? 0 : Array.FindIndex(missionsManager.Missions, x => x.id == prerequisiteMissionId);

                // Display a popup menu for the missions array, with the selected index
                selectedIndex = EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, 18), "Mission", selectedIndex, missions);

                // Update the value of the prerequisiteMissionId property with the ID of the selected mission
                SerializedProperty prerequisiteMissionIdProperty = property.FindPropertyRelative("prerequisiteMissionId");
                prerequisiteMissionIdProperty.stringValue = missionsManager.Missions[selectedIndex].id;

                // Update the new rect
                rect = new Rect(rect.x, rect.y + 18, rect.width, rect.height);
            }

            rect = Separator(rect);



            // Timed
            SerializedProperty timedProperty = property.FindPropertyRelative("timed");
            rect = SetProperty(timedProperty, rect);

            if (timedProperty.boolValue)
            {
                rect = SetProperty(property.FindPropertyRelative("timeInMinutes"), rect);
            }

            rect = Separator(rect);



            // Short Description
            rect = SetProperty(property.FindPropertyRelative("objectivesText"), rect);
            rect = Space(rect);


            // Long Description
            rect = SetProperty(property.FindPropertyRelative("description"), rect);
            rect = Space(rect);


            // Mission Completed Text
            rect = SetProperty(property.FindPropertyRelative("missionCompletedText"), rect);
            rect = Separator(rect);


            // Mission Giver
            rect = SetProperty(property.FindPropertyRelative("missionGiver"), rect);
            rect = Space(rect);


            // Mission Receiver
            rect = SetProperty(property.FindPropertyRelative("missionReceiver"), rect);
            rect = Separator(rect);


            // Rewards
            rect = SetProperty(property.FindPropertyRelative("rewards"), rect);
            rect = Separator(rect);


            // Reputations
            SetProperty(property.FindPropertyRelative("reputations"), rect);
        }
    }










    // ------------------------------------------------------------------------ Get Property Height -------------------------------------------------------------------------
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            float height = 20f;

            // Title
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("title"));
            height += 22;

            // Mission Objectives
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("objectives"));
            height += 22;


            // Prerequisite
            SerializedProperty prerequisiteProperty = property.FindPropertyRelative("prerequisite");
            height += EditorGUI.GetPropertyHeight(prerequisiteProperty);

            if (prerequisiteProperty.boolValue)
            {
                //height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionIndex"));
                height += EditorGUIUtility.singleLineHeight;
            }

            height += 22;



            // Timed
            SerializedProperty timedProperty = property.FindPropertyRelative("timed");
            height += EditorGUI.GetPropertyHeight(timedProperty);

            if (timedProperty.boolValue)
            {
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("timeInMinutes"));
            }

            height += 22;




            // Short Description
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("objectivesText"));
            height += 2;


            // Long Description
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("description"));
            height += 2;


            // Mission Completed Text
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionCompletedText"));
            height += 22;


            // Mission Giver
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionGiver"));
            height += 2;


            // Mission Receiver
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionReceiver"));
            height += 22;


            // Rewards
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("rewards"));
            height += 22;


            // Reputations
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("reputations"));
            height += 5;

            return height;
        }
        else
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }










    // ---------------------------------------------------------------------------- Set Property ----------------------------------------------------------------------------
    private Rect SetProperty(SerializedProperty property, Rect rect)
    {
        float height = EditorGUI.GetPropertyHeight(property);

        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, height), property);

        return new Rect(rect.x, rect.y + height, rect.width, rect.height);
    }










    // ------------------------------------------------------------------------------ Separator -----------------------------------------------------------------------------
    private Rect Separator(Rect rect)
    {
        float height = 2;
        float space = 10;

        EditorGUI.DrawRect(new Rect(rect.x, rect.y + space, rect.width, height * 0.5f), new Color(0.16f, 0.16f, 0.16f));
        EditorGUI.DrawRect(new Rect(rect.x, rect.y + space + height * 0.5f, rect.width, height * 0.5f), new Color(0.4f, 0.4f, 0.4f));
        return new Rect(rect.x, rect.y + height + (space * 2), rect.width, rect.height);
    }










    // -------------------------------------------------------------------------------- Space -------------------------------------------------------------------------------
    private Rect Space(Rect rect)
    {
        return new Rect(rect.x, rect.y + 2, rect.width, rect.height);
    }
}