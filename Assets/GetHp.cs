using System;
using UnityEngine;

public class HpPot : MonoBehaviour
{
    public int hpAmount = 30;
    public string playerLayer =  "Player";
    

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerLayer) && other.gameObject.TryGetComponent(out PlayerController player) )
        {
            Debug.Log("Player picked up HP!");
            player.Heal(hpAmount);
            Destroy(gameObject);
        }
    }
    
    
}