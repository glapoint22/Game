using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Mission))]
public class MissionDrawer : PropertyDrawer
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

            // Mission Name
            rect = SetProperty(property.FindPropertyRelative("missionName"), rect);
            rect = Separator(rect);


            // Mission Objectives
            rect = SetProperty(property.FindPropertyRelative("missionObjectives"), rect);
            rect = Separator(rect);




            // Chain
            SerializedProperty chainProperty = property.FindPropertyRelative("chain");
            rect = SetProperty(chainProperty, rect);

            if (chainProperty.boolValue)
            {
                rect = SetProperty(property.FindPropertyRelative("missionIndex"), rect);
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
            rect = SetProperty(property.FindPropertyRelative("shortDescription"), rect);
            rect = Space(rect);


            // Long Description
            rect = SetProperty(property.FindPropertyRelative("longDescription"), rect);
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


        

        EditorGUI.EndProperty();
    }
    
    
    
    
    
    
    
    
    
    
    // ------------------------------------------------------------------------ Get Property Height -------------------------------------------------------------------------
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            float height = 20f;

            // Mission Name
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionName"));
            height += 22;

            // Mission Objectives
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionObjectives"));
            height += 22;


            // Chain
            SerializedProperty chainProperty = property.FindPropertyRelative("chain");
            height += EditorGUI.GetPropertyHeight(chainProperty);

            if (chainProperty.boolValue)
            {
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("missionIndex"));
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
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("shortDescription"));
            height += 2;


            // Long Description
            height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("longDescription"));
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