using UnityEngine;

public class StationarySpell : MonoBehaviour
{
    public int attackDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            damageable.Hit(attackDamage, Vector2.zero);
        }
    }
}

