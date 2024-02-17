
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "MapBuilder/MapBuilderTool")]
public class MapBuilderTool : ScriptableObject
{
    [InlineEditor(InlineEditorModes.LargePreview)]
    public GameObject[] prefabs;

    [HideInInspector]
    public GameObject currentPrefab;
}