using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject endText;
    [TextArea(5, 20)]
    public string[] dialogue;
    public GameObject bubble;
    public Text text;
    public float aliveTime = 5f;

    private float timer;
    private bool end = false;

    void Start()
    {
        Say(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timer = 0;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            bubble.SetActive(false);
            if (end)
            {
                endText.SetActive(true);
            }  
        }
    }

    void Say(int num)
    {
        bubble.SetActive(true);

        text.text = dialogue[num];

        timer = aliveTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "double jump unlock")
        {
            Say(1);
        }
        if (other.gameObject.tag == "hook_unlock")
        {
            Say(2);
        }
        if (other.gameObject.tag == "dash_unlock")
        {
            Say(3);
        }
        if(other.gameObject.tag == "finish_unlock")
        {
            Say(4);
            Destroy(other.gameObject);
            end = true;
        }
    }
}
