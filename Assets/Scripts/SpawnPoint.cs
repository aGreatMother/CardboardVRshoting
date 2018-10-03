using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public bool Spawning = true;
    public List<GameObject> Prefabs;
    public float SpawnRate = 3f;
	// Use this for initialization
	void OnEnable() {
        StartCoroutine(AppearBullet());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator AppearBullet()
    {
        while (Spawning)
        {   
            GameObject enemy = GameObject.Instantiate(Prefabs[Random.Range(0,Prefabs.Count)]);
            enemy.transform.position = new Vector3(this.transform.position.x, enemy.transform.position.y, this.transform.position.z);
            enemy.transform.forward = Singleton<GameManager>.Instance.lunchBox.position - enemy.transform.position;
            yield return new WaitForSeconds(SpawnRate);
        }
    }
}
