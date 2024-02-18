#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class MapBuilderWindow : EditorWindow
{
    private MapBuilderTool mapBuilderTool;
    private Vector2 scrollPos;

    [MenuItem("Tools/Map Builder")]
    private static void ShowWindow()
    {
        var window = GetWindow<MapBuilderWindow>();
        window.titleContent = new GUIContent("Map Builder");
        window.Show();
    }

    private void OnGUI()
    {
        mapBuilderTool = EditorGUILayout.ObjectField("Map Builder Tool", mapBuilderTool, typeof(MapBuilderTool), false) as MapBuilderTool;

        if (mapBuilderTool == null)
        {
            EditorGUILayout.HelpBox("MapBuilderTool Scriptable Object is not assigned.", MessageType.Warning);
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (var prefab in mapBuilderTool.prefabs)
        {
            if (GUILayout.Button(new GUIContent(prefab.name, AssetPreview.GetAssetPreview(prefab)), GUILayout.Height(50)))
            {
                InstantiatePrefabAtLocation(prefab);
            }
        }
        EditorGUILayout.EndScrollView();
    }

    private void InstantiatePrefabAtLocation(GameObject prefab)
    {
        // Define the location where you want the prefab to be instantiated
        Vector3 instantiateLocation = new Vector3(-11, 3, 0); // Example location

        // Instantiate the prefab at the specified location
        GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        instantiatedPrefab.transform.position = instantiateLocation;
    }
}

#endif