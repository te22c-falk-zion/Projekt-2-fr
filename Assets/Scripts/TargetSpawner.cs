using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject targetPrefab;
    Transform targetsParent;
    [SerializeField]
    int TargetAmount;
    int TargetsSpawned;
    void Start()
    {
        targetsParent = GameObject.Find("Targets").transform;
                for (int i = 0; i < TargetAmount; i++)
        {
            SpawnTargets();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(0,50), 2.5f, Random.Range(0,500));
    }

    void SpawnTargets()
    {
        var target = Instantiate(targetPrefab, RandomPosition(), Quaternion.Euler(new Vector3(90,0,0)));
        target.transform.SetParent(targetsParent);
        TargetsSpawned++;
    }
}
