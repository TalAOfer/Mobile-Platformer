
#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
public class MapBuilderWindow : EditorWindow
{

    private MapBuilderTool mapBuilderTool;
    private GameObject tempInstance;
    private Vector2 scrollPos;
    private bool isPlacingPrefab = false;

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
                StartPlacingPrefab(prefab);
            }
        }
        EditorGUILayout.EndScrollView();
    }

    private void StartPlacingPrefab(GameObject prefab)
    {
        if (isPlacingPrefab && tempInstance != null) DestroyImmediate(tempInstance);

        tempInstance = Instantiate(prefab);
        tempInstance.hideFlags = HideFlags.HideAndDontSave;
        isPlacingPrefab = true;

        // Attach to the scene GUI event
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!isPlacingPrefab) return;

        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive)); // Block other mouse interactions
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            tempInstance.transform.position = hit.point;
        }

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            GUIUtility.hotControl = 0;
            isPlacingPrefab = false;
            tempInstance.hideFlags = HideFlags.None; // Make the instance visible in hierarchy
            Undo.RegisterCreatedObjectUndo(tempInstance, "Place " + tempInstance.name); // Allow undo
            tempInstance = null; // Clear the temp instance
            SceneView.duringSceneGui -= OnSceneGUI; // Detach from the scene GUI event
            Event.current.Use(); // Consume the event
        }

        sceneView.Repaint();
    }

}

#endif