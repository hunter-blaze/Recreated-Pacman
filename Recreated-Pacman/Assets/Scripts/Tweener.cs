using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    Tween activeTween;
    // Start is called before the first frame update
    void Start()
    {
        // activeTween = new Tween();
    }

    // Update is called once per frame
    void Update()
    {
        // activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, Mathf.Pow(((Time.time - activeTween.StartTime) / activeTween.Duration), 3.0f)); 
    }
}
