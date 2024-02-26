using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour
{
    [SerializeField]
    protected LayerMask groundLayer;
    [SerializeField]
    protected LayerMask platformLayer;

    public LayerMask GetGround()
    {
        return groundLayer;
    }
    public LayerMask GetPlatform()
    {
        return platformLayer;
    }
}
