using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddySimpleAI : MonoBehaviour
{
    private bool move = true;
    public int direction = 1;
    public float speed = 10f;

    public float time = 3f;
    private float timeStore;
    
    void Start()
    {
        timeStore = time;
    }

    void Update()
    {
        if(!move)
        {
            // Tant qu'il reste du temps
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                // X secondes se sont écoulés
                move = true;
                GetComponent<Rigidbody>().isKinematic = false;
                time = timeStore;
            }
        }
    }
    void FixedUpdate()
    {
        if(move)
        {
            GetComponent<Rigidbody>().velocity = Vector3.right * direction * speed;
            GetComponent<Animator>().SetBool("Roll_Anim", true);
            GetComponent<Animator>().SetBool("Open_Anim", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Open_Anim", true);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "limiter")
        {
            direction = -direction;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "unitychan")
        {
            direction = -direction;
            move = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
