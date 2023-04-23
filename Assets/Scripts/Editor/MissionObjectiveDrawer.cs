using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MissionObjective))]
public class MissionObjectiveDrawer : PropertyDrawer
{
    // ------------------------------------------------------------------------------- On GUI -------------------------------------------------------------------------------
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.isExpanded, label);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel = 1;
            Rect rect = new(position.x, position.y + 20, position.width, position.height);



            // Mission Objective Type
            SerializedProperty missionTypeProperty = property.FindPropertyRelative("missionObjectiveType");
            rect = SetProperty(missionTypeProperty, rect);
            rect = Space(rect);

            switch ((MissionObjectiveType)missionTypeProperty.enumValueIndex)
            {
                case MissionObjectiveType.Kill:
                    rect = SetProperty(property.FindPropertyRelative("npcs"), rect);
                    break;

                case MissionObjectiveType.Collect:
                    rect = SetProperty(property.FindPropertyRelative("collectionItems"), rect);
                    break;

                case MissionObjectiveType.Deliver:
                    rect = SetProperty(property.FindPropertyRelative("deliveryItems"), rect);
                    break;

                case MissionObjectiveType.Explore:
                    rect = SetProperty(property.FindPropertyRelative("explorationAreas"), rect);
                    break;


                case MissionObjectiveType.UseItem:
                    rect = SetProperty(property.FindPropertyRelative("itemsToUse"), rect);
                    break;

                case MissionObjectiveType.InteractWithItem:
                    rect = SetProperty(property.FindPropertyRelative("itemsToInteract"), rect);
                    break;

                case MissionObjectiveType.Escort:
                    rect = SetProperty(property.FindPropertyRelative("npcsToEscort"), rect);
                    break;
            }
        }


        EditorGUI.EndProperty();
    }




    // ------------------------------------------------------------------------ Get Property Height -------------------------------------------------------------------------
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            float height = 20f;


            // Mission Objective Type
            SerializedProperty missionTypeProperty = property.FindPropertyRelative("missionObjectiveType");
            height += EditorGUI.GetPropertyHeight(missionTypeProperty) + 2;

            switch ((MissionObjectiveType)missionTypeProperty.enumValueIndex)
            {
                case MissionObjectiveType.Kill:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("npcs"));
                    break;

                case MissionObjectiveType.Collect:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("collectionItems"));
                    break;

                case MissionObjectiveType.Deliver:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("deliveryItems"));
                    break;

                case MissionObjectiveType.Explore:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("explorationAreas"));
                    break;

                case MissionObjectiveType.UseItem:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("itemsToUse"));
                    break;

                case MissionObjectiveType.InteractWithItem:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("itemsToInteract"));
                    break;

                case MissionObjectiveType.Escort:
                    height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("npcsToEscort"));
                    break;
            }

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