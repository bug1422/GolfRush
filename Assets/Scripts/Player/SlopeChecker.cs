using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class SlopeChecker : SlopeInfo
{
    [SerializeField]
    private float maxAngle;
    [SerializeField]
    private float slopeCheckDistance;
    [SerializeField]
    private float collideDistance;
    [SerializeField]
    private float offset;
    [SerializeField]
    private PhysicsMaterial2D fullFriction;
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    private LayerMask groundLayer;
    protected override void Initializtion()
    {
        base.Initializtion();
        maxSlopeAngle = maxAngle;
        groundLayer = GetComponent<Layers>().GetGround();
    }
    private void Update()
    {
        CheckInsidePlatform();
    }
    private void FixedUpdate()
    {
        SlopeCheck();
    }
    private void CheckInsidePlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, collideDistance, groundLayer);
        isCollidingWithPlatform = false;
        if(hit)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                Debug.DrawRay(transform.position, Vector2.down * collideDistance, Color.black);
                isCollidingWithPlatform = true;
            }
        }
    }
    private void SlopeCheck()
    {
        Vector2 checkPos = new Vector2(transform.position.x, transform.position.y + offset);
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundLayer);
        if (!isCollidingWithPlatform)
        {
            if (slopeHitFront)
            {
                isOnSlope = true;
                slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            }
            else if (slopeHitBack)
            {
                isOnSlope = true;
                slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            }
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            var flag1 = slopeNormalPerp != Vector2.up && slopeNormalPerp != Vector2.down;
            var flag2 = slopeNormalPerp != Vector2.left && slopeNormalPerp != Vector2.right;
            if (!isCollidingWithPlatform && (flag1 && flag2) && slopeSideAngle == lastSlopeAngle)
            {
                isOnSlope = true;
                Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }
            else
            {
                isOnSlope = false;
            }
            lastSlopeAngle = slopeDownAngle;
        }
        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && xInput == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }
        else
        {
            rb.sharedMaterial = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + offset), new Vector2(transform.position.x+slopeCheckDistance, transform.position.y + offset));
    }
}
