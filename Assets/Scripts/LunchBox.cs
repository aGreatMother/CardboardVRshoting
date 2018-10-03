using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchBox : MonoBehaviour {

    private Color origincalColor;
    private Renderer shader;
    private bool turnRed = false;
    // Use this for initialization
    void Start () {
        shader = this.GetComponent<Renderer>();
        origincalColor = shader.material.color;
	}

    private float colorChangeTime = 0.3f;
    private float colorChangeTimeCount = 0f;
    // Update is called once per frame
    void Update () {


        if (turnRed)
        {
            if (colorChangeTimeCount < colorChangeTime)
                colorChangeTimeCount += Time.deltaTime;
            else
            { shader.material.color = origincalColor;
                turnRed = false;
            }
        }

    }

    public void ChangeColor() {
        colorChangeTimeCount = 0f;
        turnRed = true;
        shader.material.color = Color.red;
    }
}
