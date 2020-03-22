using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class unlock : MonoBehaviour
{

    private void Start() {
        gameObject.GetComponent<DistanceJoint2D>().enabled = false;
        gameObject.GetComponent<Hook>().enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "hook_unlock")
        {
            gameObject.GetComponent<Hook>().enabled = true;
            gameObject.GetComponent<Hook>().distance = 4f;
            gameObject.GetComponent<DistanceJoint2D>().enabled = false;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "dash_unlock")
        {
            gameObject.AddComponent<Dash>();
            gameObject.GetComponent<Dash>().dashParticle = (GameObject)Resources.Load("DashEffect");
            gameObject.GetComponent<Dash>().startDashTime = 0.1f;
           // gameObject.GetComponent<Dash>().dashParticle = (GameObject)GameObject.Instantiate(), Vector3.zero, Quaternion.identity);
            //gameObject.GetComponent<Dash>().dashParticle =   (AssetDatabase.FindAssets("DashEffect"))[0];
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "finish_unlock")
        {
            gameObject.GetComponent<movement>().enabled = false;
            gameObject.GetComponent<Hook>().enabled = false;
            if(gameObject.GetComponent<Dash>() != null)
            {
                gameObject.GetComponent<Dash>().enabled = false;
            }
            gameObject.GetComponent<Animator>().SetFloat("jump", 0);
            gameObject.GetComponent<Animator>().SetFloat("speed", 0);

        }
    }
}
