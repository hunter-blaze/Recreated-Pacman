using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMotionController : MonoBehaviour
{
    [SerializeField] Transform pacman;
    [SerializeField] Animation pacman_Anim;

    // int lastTime;
    int lastMoveTime;
    float timer;

    const float distance = 0.32f;
    const float x = 2.5f * distance;
    const float y = 2.0f * distance;

    const float moveHorizontal = 5.0f;
    const float moveVertical = 4.0f;

    bool moveCheck; // check if either pacman moves horizontal or vertical

    void Start()
    {
        ResetTime();
        pacman = this.GetComponent<Transform>();
        pacman_Anim = this.GetComponent<Animation>();
        pacman.localPosition = new Vector3(-x, y, 0); // pacman is place inside a parent
    }

    void Update()
    {
        timer += Time.deltaTime;

        // if ((int)timer > lastTime) {
        //     lastTime = (int)timer;
		// 	Debug.Log(lastTime);
        // }

        if (moveCheck) {    // Horizontal moves first
            if ((int)timer > lastMoveTime - 1 + (int)moveHorizontal) {
                lastMoveTime = (int)timer;
                MovePacman();
                moveCheck = false;
                // Debug.Log("Horizontal");
            }
        } else {    // Vertical is next
            if ((int)timer > lastMoveTime - 1 + (int)moveVertical) {
                lastMoveTime = (int)timer;
                MovePacman();
                moveCheck = true;
                // Debug.Log("Vertical");
            }
        }
    }

    void ResetTime() {
        lastMoveTime = 0;
        // lastTime = -1; 
        timer = 0.0f;
        moveCheck = true;
    }

    void MovePacman() {
        if (pacman.localPosition.x == -x && pacman.localPosition.y == y) { // top left (-2.5f, 2.0f)
            pacman.localPosition = new Vector3(x, y, 0);
        } else if (pacman.localPosition.x == x && pacman.localPosition.y == y) {  // top right (2.5f, 2.0f)
            pacman.localPosition = new Vector3(x, -y, 0);
        } else if (pacman.localPosition.x == x && pacman.localPosition.y == -y) {  // bottom right (2.5f, -2.0f)
            pacman.localPosition = new Vector3(-x, -y, 0);
        } else {    // bottom left (-2.5f, -2.0f)
            pacman.localPosition = new Vector3(-x, y, 0);
        }
    }
}
