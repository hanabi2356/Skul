#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Label))]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Label attr= (Label) attribute;
        label.text = attr.label;
        EditorGUI.PropertyField(position, property, label);
    }
}
#endif
