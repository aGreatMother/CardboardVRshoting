using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class GameManager : MonoBehaviour {
    public GameObject rewardPrefab;
    private GameObject rewardInScene;
    public Transform lunchBox;
    public TextMesh inforBox;
    public int lunchBoxHealthRamain = 3;
    public bool start = false;
    public bool isGameover = false;
    public float timer;
    public AudioSource superSound;
    public AudioSource GameoverSound;
	// Use this for initialization
	void Start () {
    
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {

        if (lunchBoxHealthRamain <= 0)
        {
            GameOver();
            Destroy(lunchBox.gameObject);
            inforBox.text = "Game Over\n"
               + "survive time:" + ((int)(timer / 60f)).ToString() + ":" + ((int)(timer % 60f)).ToString();
            return;
        }
        if (start) {
            timer += Time.deltaTime;
            inforBox.text = "lunch box health remain:" + lunchBoxHealthRamain + "\n"
                + "survive time:" + ((int)(timer / 60f)).ToString() + ":" + ((int)(timer % 60f)).ToString();
        }

        
	}

    public void AppearReward(Vector3 location){
        if(rewardInScene==null&&!Singleton<gun>.Instance.turnSuper)
        rewardInScene = GameObject.Instantiate(rewardPrefab);
        rewardInScene.transform.position = location;
    }
    public SpawnPoint[] spawnPoints;
    public void StartGame() {
        start = true;
        for (int i = 0; i < spawnPoints.Length; i++) {
            spawnPoints[i].gameObject.SetActive(true);
        }
    }

    public void GameOver() {

        if(!isGameover)
            GameoverSound.Play();
        Singleton<gun>.Instance.shooting = false;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].gameObject.SetActive(false);
        }
        isGameover = true;

        
    }
}
