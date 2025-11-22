using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Event triggered whenever a weapon fires
    public static event Action<Weapons, Vector3, Vector3> OnAttackFired;

    [Header("Weapons")]
    public List<ActiveWeapon> activeWeapons = new List<ActiveWeapon>();

    private PlayerController playerController;

    [System.Serializable]
    public class ActiveWeapon
    {
        public Weapons weapon;
        [HideInInspector] public float cooldownTimer;
    }

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Transform target = GetClosestEnemy();
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        foreach (ActiveWeapon aw in activeWeapons)
        {
            aw.cooldownTimer -= Time.deltaTime;
            if (aw.cooldownTimer <= 0f && aw.weapon != null)
            {
                aw.weapon.Fire(transform.position, direction);
                aw.cooldownTimer = 1f / aw.weapon.fireRate;

                OnAttackFired?.Invoke(aw.weapon, transform.position, direction);
            }
        }
    }

    private Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
