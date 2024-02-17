using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventEmitter : MonoBehaviour
{
    private Player player;
    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    public void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }
}
