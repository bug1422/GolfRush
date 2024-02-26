using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeInfo : Player
{
    protected static float maxSlopeAngle;
    protected static Vector2 slopeNormalPerp;
    protected static float slopeDownAngle;
    protected static float lastSlopeAngle;
    protected static float slopeSideAngle;
    protected static float xInput;
    protected static bool isCollidingWithPlatform;
    protected static bool canWalkOnSlope = false;
}
