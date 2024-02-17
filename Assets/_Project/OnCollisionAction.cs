using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionAction : MonoBehaviour
{
    [SerializeField] private bool _onEnter;
    [ShowIf("_onEnter")]
    [FoldoutGroup("Enter Response")]
    [SerializeField] private CustomGameEvent _enterResponse;

    [SerializeField] private bool _onExit;
    [ShowIf("_onExit")]
    [FoldoutGroup("Exit Response")]
    [SerializeField] private CustomGameEvent _exitResponse;

    [SerializeField] private string _collidedTag;
    [SerializeField] private bool _debug;

    private void Awake()
    {
        if (!TryGetComponent<Collider2D>(out _))
            Debug.Log("No collider on " + gameObject.name);
    }

    #region if !Trigger

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_collidedTag))
        {
            if (_debug) Debug.Log("Enter Happened");
            _enterResponse?.Invoke(this, null);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_collidedTag))
        {
            if (_debug) Debug.Log("Exit Happened");
            _exitResponse?.Invoke(this, null);
        }
    }

    #endregion

    #region if Trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_collidedTag))
        {
            if (_debug) Debug.Log("Enter Happened");
            _enterResponse?.Invoke(this, null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_collidedTag))
        {
            if (_debug) Debug.Log("Exit Happened");
            _exitResponse?.Invoke(this, null);
        }
    }

    #endregion


}
