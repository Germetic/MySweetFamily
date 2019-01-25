using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplayController : MonoBehaviour
{
    public GameObject DisplayDmaagePrefab;
    public GameObject DamagerPositionMark;
    public static DamageDisplayController Instance = null;

    public void Display(Vector3 position, int damage)
    {
        GameObject spawnedObject = Instantiate(DisplayDmaagePrefab, position, Quaternion.identity);
        DamagaDunamicDisplayElement damagaDunamicDisplayElement = spawnedObject.GetComponent<DamagaDunamicDisplayElement>();

        damagaDunamicDisplayElement.Initialize(damage);
    }
    public void DisplayDamager(Transform damagerPosition)
    {
        DamagerPositionMark.transform.position = damagerPosition.position;
    }
    void Start()
    {
        if (Instance == null)
        { 
            Instance = this; 
        }
        else if (Instance == this)
        { 
            Destroy(gameObject); 
        }
    }
}
