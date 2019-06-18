using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGame : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();

    public AnimationCurve curve;

    IEnumerator animateTapStart()
    {
        for (float t = 0; t < curve.keys[curve.length - 1].time; t += Time.deltaTime)
        {
            children.ForEach(x =>
            {
                var pos = x.GetComponent<RectTransform>().position;
                x.GetComponent<RectTransform>().position = new Vector2(pos.x - curve.Evaluate(t), pos.y);
                
            });

            yield return null;
        }
    }

    public PlayerController target;
    // Start is called before the first frame update
    void Start()
    {
        var objs = UnityEngine.Object.FindObjectsOfType<Text>().Where(x => x.gameObject.name == "toto");
        
        foreach(Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!target.run)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                StartCoroutine("animateTapStart");
                target.run = true;
            }
        }
    }
}
