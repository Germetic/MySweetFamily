using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [Range(0, 10)]
    public float MinTimeBetweenSpawn;
    [Range(0, 20)]
    public float MaxTimeBetweenSpawn;

    private void Start()
    {
        StartCoroutine(WeaponSpawnCoroutine());
    }

    private IEnumerator WeaponSpawnCoroutine()
    {
        while(true)
        {
            if(GlobalManager.Instance.IsCountDownEnded && !GlobalManager.Instance.IsScoreShowed)
            {
                GameObject _weaponPrefab = GetRandomWeapon();
                Transform _point = GetRandomPoint();
                if (_weaponPrefab != null)
                {
                    GameObject _weapon = Instantiate(_weaponPrefab);
                    _weapon.transform.position = _point.position;
                    GameManager.Instance.SpawnedWeapon.Add(_weapon);
                }
            }
            yield return new WaitForSecondsRealtime(Random.Range(MinTimeBetweenSpawn,MaxTimeBetweenSpawn));
        }
    }

    private Transform GetRandomPoint()
    {
        return GameManager.Instance.WeaponSpawnPoints[Random.Range(0, GameManager.Instance.WeaponSpawnPoints.Count - 1)];
    }

    private GameObject GetRandomWeapon()
    {
        GameObject _weapon = null;
        bool isDone = false;

        while(!isDone)
        {
            _weapon = GameManager.Instance.Weapon[Random.Range(0, GameManager.Instance.Weapon.Count - 1)];
            if (_weapon.GetComponent<SwordWeapon>().Location == ScenesNumbers.Menu || _weapon.GetComponent<SwordWeapon>().Location == GlobalManager.Instance.CurrentLocation)
                isDone = true;
        }

        return _weapon;
    }
}
