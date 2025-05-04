using UnityEngine;
using System.Collections;
public interface knockback
{
    IEnumerator KnockbackRoutine(Vector3 knockback);
    public void ApplyKnockback(Vector2 sourcePosition, float knockbackForce);
}
