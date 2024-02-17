using UnityEngine;
using UnityEditor;

public class DeleteHiddenObjects : EditorWindow
{
    private string objectNameToDelete = "";

    [MenuItem("Tools/Delete Hidden Objects")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DeleteHiddenObjects));
    }

    void OnGUI()
    {
        GUILayout.Label("Delete Hidden Object", EditorStyles.boldLabel);
        objectNameToDelete = EditorGUILayout.TextField("Object Name", objectNameToDelete);

        if (GUILayout.Button("Delete"))
        {
            DeleteObjectByName(objectNameToDelete);
        }
    }

    private static void DeleteObjectByName(string name)
    {
        var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            if (obj.hideFlags == HideFlags.HideAndDontSave && obj.name == name)
            {
                DestroyImmediate(obj);
                Debug.Log($"Deleted hidden object: {name}");
            }
        }
    }
}