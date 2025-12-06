using System;
using UnityEngine;

public class XpOrb : MonoBehaviour
{
    public int xpAmount = 50;
    public string playerLayer =  "Player";
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerLayer) && other.gameObject.TryGetComponent(out PlayerController player) )
        {
            Debug.Log("Player picked up XP!");
            player.AddXp(xpAmount);
            Destroy(gameObject);
        }
    }
    
    
}