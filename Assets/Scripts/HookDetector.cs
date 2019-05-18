
using UnityEngine;

public class HookDetector
{
    public Collider2D[] GetHooksInRadius(Vector3 grabber, float distance, LayerMask hookLayer)
    {
        Collider2D[] hooksInRadius = Physics2D.OverlapCircleAll(grabber, distance, hookLayer);
        return hooksInRadius;
    }

}
