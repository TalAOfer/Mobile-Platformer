
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName ="events")]
[Serializable]
public class AllEvents : SerializedScriptableObject
{
    [SerializeField]
    private Dictionary<string, GameEvent> events = new();

    public Dictionary<string, GameEvent> Events => events;
}

