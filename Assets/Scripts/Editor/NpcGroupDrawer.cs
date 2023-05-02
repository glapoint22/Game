using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NpcCollection))]
public class NpcGroupDrawer : PropertyDrawer
{
    private string[] options = UnityEditorInternal.InternalEditorUtility.tags;

    // ------------------------------------------------------------------------------- On GUI -------------------------------------------------------------------------------
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.BeginProperty(position, label, property);

        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), property.isExpanded, label);

        if (property.isExpanded)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("npc"));
            EditorGUI.PropertyField(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 20, position.width, EditorGUIUtility.singleLineHeight), property.FindPropertyRelative("count"));

            SerializedProperty tagProperty = property.FindPropertyRelative("tag");

            string currentValue = tagProperty.stringValue;
            int selectedOptionIndex = -1;

            if (string.IsNullOrEmpty(currentValue))
            {
                selectedOptionIndex = 0;
            }
            else
            {
                // Determine the index of the current value in the options array
                for (int i = 0; i < options.Length; i++)
                {
                    if (options[i] == currentValue)
                    {
                        selectedOptionIndex = i;
                        break;
                    }
                }
            }


            // Show the Popup control
            selectedOptionIndex = EditorGUI.Popup(new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 40, position.width, EditorGUIUtility.singleLineHeight), "Tag:", selectedOptionIndex, options);

            // Update the property value if the selected option has changed
            if (selectedOptionIndex >= 0 && options[selectedOptionIndex] != currentValue)
            {
                tagProperty.stringValue = options[selectedOptionIndex];
            }
        }



        //EditorGUI.EndProperty();
    }




    // ------------------------------------------------------------------------ Get Property Height -------------------------------------------------------------------------
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            return 78;
        }
        else
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}
