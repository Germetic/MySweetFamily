using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   

    public PlayerController[] Players;

    public List<GameObject> Weapon;

    public List<Transform> WeaponSpawnPoints;

    public List<GameObject> SpawnedWeapon;

    public static GameManager Instance = null;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
