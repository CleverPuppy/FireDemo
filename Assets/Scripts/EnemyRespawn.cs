using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] enemyPrefab;
    public int maxEnemys = 20;
    public int maxBundle = 10;
    public float minRespawnDistance = 20.0f;
    public float maxRespawnDistance = 100.0f;
    public float respawnY = 0.1f;

    public float respawnBundleColdTime = 30;
    float timelapseSinceLastRespawn = 0;
    
    Vector3 playerLocation;
    int[] currEnemyNum;
    void Start()
    {
        playerLocation = GameObject.Find("player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timelapseSinceLastRespawn += Time.deltaTime;
        if(timelapseSinceLastRespawn > respawnBundleColdTime)
        {
            respawnBundle();
        }
    }

    Vector3 generatePositionR()
    {
        Vector3 pos = Vector3.zero;
        float distance = Random.Range(minRespawnDistance, maxRespawnDistance);
        float angular = Random.Range(0, 2 * Mathf.PI);
        pos.x = playerLocation.x + Mathf.Cos(angular) * distance;
        pos.z = playerLocation.z + Mathf.Sin(angular) * distance;
        pos.y = respawnY;
        return pos;
    }

    void respawnBundle()
    {
        int currEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currEnemy > maxEnemys)
        {
            return;
        }
        int runTimes = Random.Range(1, maxBundle);
        for(int i = 0; i < runTimes; ++i)
        {
            respawnNewEnemy();
        }
        timelapseSinceLastRespawn = 0;
    }
    public void respawnNewEnemy()
    {

        int enemyType = Random.Range(0, enemyPrefab.Length);
        GameObject.Instantiate(enemyPrefab[enemyType],generatePositionR() , Quaternion.LookRotation(Vector3.up));
    }

}
