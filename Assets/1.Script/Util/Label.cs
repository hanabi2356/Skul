using UnityEditor;
using UnityEngine;

public class Label : PropertyAttribute
{
    public string label;
    public Label(string label) => this.label = label;
    
}
