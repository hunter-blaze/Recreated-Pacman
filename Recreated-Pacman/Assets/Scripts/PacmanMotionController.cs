using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMotionController : MonoBehaviour
{
    [SerializeField] Transform pacman;
    [SerializeField] Animator pacman_Anim;

    Tween activeTween;

    const float distance = 0.32f;
    const float x = 2.5f * distance;
    const float y = 2.0f * distance;

    const float moveHorizontal = 5.0f;
    const float moveVertical = 4.0f;


    void Start()
    {
        pacman = this.GetComponent<Transform>();
        pacman_Anim = this.GetComponent<Animator>();
        pacman.localPosition = new Vector3(-x, y, 0); // pacman is place inside a parent
    }

    void Update()
    {
        AddTween();
        if (activeTween != null) {
            MovementDirection();
            pacman.localPosition = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, (Time.time - activeTween.StartTime) / activeTween.Duration);

            if (Vector3.Distance(pacman.localPosition, activeTween.EndPos) == 0)      
                activeTween = null;
        }

    }

    void AddTween() {
        if (activeTween == null) {
            if (pacman.localPosition.x == -x && pacman.localPosition.y == y) // top left (-2.5f, 2.0f)
                activeTween = new Tween(pacman.localPosition, new Vector3(x, y, 0), Time.time, moveHorizontal); // move right
            if (pacman.localPosition.x == x && pacman.localPosition.y == y)  // top right (2.5f, 2.0f)
                activeTween = new Tween(pacman.localPosition, new Vector3(x, -y, 0), Time.time, moveVertical); // move down
            if (pacman.localPosition.x == x && pacman.localPosition.y == -y)  // bottom right (2.5f, -2.0f)
                activeTween = new Tween(pacman.localPosition, new Vector3(-x, -y, 0), Time.time, moveHorizontal); // move left
            if (pacman.localPosition.x == -x && pacman.localPosition.y == -y)   // bottom left (-2.5f, -2.0f)
                activeTween = new Tween(pacman.localPosition, new Vector3(-x, y, 0), Time.time, moveVertical); // move up
        }
    }

    void MovementDirection() {
        if (pacman.localPosition.x < activeTween.EndPos.x)
            pacman_Anim.SetInteger("Movement", 0);
        if (pacman.localPosition.y > activeTween.EndPos.y)
            pacman_Anim.SetInteger("Movement", 1);
        if (pacman.localPosition.x > activeTween.EndPos.x)
            pacman_Anim.SetInteger("Movement", 2);
        if (pacman.localPosition.y < activeTween.EndPos.y)
            pacman_Anim.SetInteger("Movement", 3);
    }
}
