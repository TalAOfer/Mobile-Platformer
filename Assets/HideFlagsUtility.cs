using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HideFlagsUtility : MonoBehaviour
{
    // Use this function to destroy the objects with the specified names
    [Button("Destroy Specific Objects")]
    void DestroySpecificObjects()
    {
        string[] objectNames = { "E_Button", "E_Toggle", "E_Portal" };

#if UNITY_EDITOR
        // Use the Undo system to register the destruction so it's possible to undo it
        Undo.SetCurrentGroupName("Destroy Specific Objects");

        // Find all game objects in the scene, including inactive ones
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var obj in allObjects)
        {
            // Check if the object is not part of the scene (e.g., a prefab asset or an editor-only object)
            if (!obj.scene.isLoaded || obj.hideFlags != HideFlags.None)
                continue;

            // Check if the object's name matches any of the specified names
            foreach (string name in objectNames)
            {
                if (obj.name == name)
                {
                    // Destroy the object and register the destruction for undo
                    Undo.DestroyObjectImmediate(obj);
                    Debug.Log($"Destroyed object: {obj.name}");
                    break; // Break out of the inner foreach loop
                }
            }
        }

        Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
#endif
    }
}