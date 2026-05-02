#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class SkulStatDataImporter : EditorWindow
{
    [MenuItem("Tools/Skul Stat Import")]
    public static void ShowWindow()
    {
        GetWindow<SkulStatDataImporter>("CSV To SO");
    }

}
#endif
