using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapon")]
public class Weapons : ScriptableObject
{
    public string weaponName;
    public GameObject projectilePrefab;
    public float damage = 1f;
    public float fireRate = 1f;         // shots per second
    public float projectileSpeed = 5f;
    public float spreadAngle = 0f;      // for multiple directions if needed

    // Fire method (spawns projectiles)
    public void Fire(Vector3 origin, Vector3 direction)
    {
        if (projectilePrefab == null) return;

        GameObject proj = ObjectPooler.Instance.SpawnFromPool(projectilePrefab.name, origin, Quaternion.identity);
        if (proj.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }
    }   
}
