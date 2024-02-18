using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] private List<HingeJoint2D> hinges;
    [SerializeField] private float yOffset;
    private Vector3 temp;

    [Button]
    public void ArrangeChain()
    {
        temp = Vector3.zero;

        for (int i = 0; i < hinges.Count; i++)
        {
            bool firstHinge = (i == 0);
            HingeJoint2D hinge = hinges[i];
            Rigidbody2D formerHinge = firstHinge ? GetComponent<Rigidbody2D>() : hinges[i - 1].attachedRigidbody;

            if (!firstHinge) temp.y -= yOffset;
            hinge.transform.localPosition = temp;
            hinge.connectedBody = formerHinge;
        }
    }
}
