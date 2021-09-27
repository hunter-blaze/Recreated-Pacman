using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	[SerializeField] Sprite[] sprites;
    [SerializeField] RuntimeAnimatorController powerPellet;

    GameObject spriteObject;
    GameObject genenrateMiddle;

    const float dist = 0.32f;

    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };


    void Start()
    {
        Destroy(GameObject.Find("LevelMap-Level 01"));
        GameObject generateLvl = new GameObject("GeneratedLevel");
        GameObject genenratedQuad = new GameObject("genenratedQuad");
        GameObject genenratedSide = new GameObject("genenratedSide");
        GameObject genenrateMiddle = new GameObject("genenrateMiddle");

        for (int y = 0; y < levelMap.GetLength(0); y++) {
            for (int x = 0; x < levelMap.GetLength(1); x++) {
                
                int value = levelMap[y, x] - 1;
                
                if (value > - 1) {
                    if (y + 1 < levelMap.GetLength(0)) {
                        spriteObject = new GameObject("LevelObject", typeof(SpriteRenderer));
                        spriteObject.transform.Translate(new Vector3(x * dist, -y * dist, 0));
                        spriteObject.transform.parent = genenratedQuad.transform;
                        spriteObject.GetComponent<SpriteRenderer>().sprite = sprites[value];
                        spriteObject.transform.Rotate(0, 0, SpriteRotation(y, x, value + 1));

                        genenratedQuad.transform.parent = genenratedSide.transform;
                        genenratedSide.transform.parent = generateLvl.transform;
                    } else {
                        spriteObject = new GameObject("LevelObject", typeof(SpriteRenderer));
                        spriteObject.transform.Translate(new Vector3(x * dist, -y * dist, 0));
                        spriteObject.GetComponent<SpriteRenderer>().sprite = sprites[value];
                        spriteObject.transform.Rotate(0, 0, SpriteRotation(y, x, value + 1));

                        spriteObject.transform.parent = genenrateMiddle.transform;
                        genenrateMiddle.transform.parent = generateLvl.transform;
                    }

                    // Power Peller Animation
                    if (value == 5) {
                        spriteObject.AddComponent<Animator>().runtimeAnimatorController = powerPellet;
                    }  
                }
            }
        }

        Instantiate(genenratedQuad, new Vector3((levelMap.GetLength(1) * dist * 2) - dist , 0, 0), Quaternion.Euler(0, 180, 0), genenratedSide.transform);
        Instantiate(genenratedSide, new Vector3(0, -((levelMap.GetLength(0) - 1) * 2) * dist), Quaternion.Euler(180, 0, 0), generateLvl.transform);
        Instantiate(genenrateMiddle, new Vector3((levelMap.GetLength(1) * dist * 2) - dist , 0, 0), Quaternion.Euler(0, 180, 0), generateLvl.transform);
        generateLvl.transform.position = new Vector3(-(levelMap.GetLength(1) - 2) * dist, (levelMap.GetLength(0) - 2) * dist, 0);

    }

    float SpriteRotation(int r, int c, int value) {
        float angle = 0.0f;
        int left = 0, right = 0, top = 0, bottom = 0;
        if (c > 0)
            left = levelMap[r, c - 1];
        if (c < levelMap.GetLength(1) - 1)
            right = levelMap[r, c + 1];
        if (r > 0)
            top = levelMap[r - 1, c];
        if (r < levelMap.GetLength(0) - 1)
            bottom = levelMap[r + 1, c];

        switch (value) {
            case 1:
                if ((top == 1 || top == 2) && (right == 1 || right == 2))
                    angle = 90.0f;
                else if ((left == 1 || left == 2) && (top == 1 || top == 2))
                    angle = 180.0f;
                else if ((left == 1 || left == 2) && (bottom == 1 || bottom == 2))
                    angle = 270.0f;
                break;
            case 2:
                if ((top == 1 || top == 2) && (bottom == 1 || bottom == 2))
                    angle = 90.0f;
                break;
            case 3:
                int[] values = {top, right, bottom, left};
                int count = 0;
                for (int i = 0; i < values.Length; i++) {
                    if (values[i] == 3 || values[i] == 4 || values[i] == 7)
                        count++;
                }
                if (count == 1 && (r == 0 || r == levelMap.GetLength(0) - 1 || c == 0 || c == levelMap.GetLength(1) - 1)) {
                    if (top == 3 || top == 4 || top == 7)
                        angle = 90.0f;
                    else if (left == 3 || left == 4 || left == 7)
                        angle = 180.0f;
                    else if (bottom == 3 || bottom == 4 || bottom == 7)
                        angle = 270.0f;
                }

                if (count == 4) {
                    int topright = levelMap[r - 1, c + 1];
                    int bottomright = levelMap[r + 1, c + 1];
                    int bottomleft = levelMap[r + 1, c - 1];
                    int topleft = levelMap[r - 1, c - 1];

                    int[] diagValues = {topright, bottomright, bottomleft, bottomright};
                    int diagFreeCount = 0;
                    for (int i = 0; i < diagValues.Length; i++) {
                        if (diagValues[i] == 0 || diagValues[i] == 5 || diagValues[i] == 6)
                            diagFreeCount++;
                    }
                    if (diagFreeCount == 1) {
                        if (topright == 0 || topright == 5 || topright == 6)
                            angle = 90.0f;
                        else if (bottomleft == 0 || bottomleft == 5 || bottomleft == 6)
                            angle = 180.0f;
                        else if (topleft == 0 || topleft == 5 || topleft == 6)
                            angle = 270.0f;
                    }
                } else {
                    if (((top == 3 || top == 4) && (right == 3 || right == 4)) || ((bottom == 5 || bottom == 6) && (left == 5 || left == 6)))
                        angle = 90.0f;
                    else if (((left == 3 || left == 4) && (top == 3 || top == 4)) || ((right == 5 || right == 6) && (bottom == 5 || bottom == 6)))
                        angle = 180.0f;
                    else if (((left == 3 || left == 4) && (bottom == 3 || bottom == 4)) || ((right == 5 || right == 6) && (top == 5 || top == 6)))
                        angle = 270.0f;
                }
                if (count == 3 && (r == 0 || r == levelMap.GetLength(0) - 1 || c == 0 || c == levelMap.GetLength(1) - 1)) {
                    if (c == levelMap.GetLength(1) - 1) {
                        int topleft = levelMap[r - 1, c - 1];
                        int bottomleft = levelMap[r + 1, c - 1];
                        if (topleft == 0 || topleft == 5 || topleft == 6)
                            angle = 180.0f;
                        else if (bottomleft == 0 || bottomleft == 5 || bottomleft == 6) {
                            angle = 270.0f;
                        }
                    }
                    if (r == levelMap.GetLength(0) - 1) {
                        int topright = levelMap[r - 1, c + 1];
                        int topleft = levelMap[r - 1, c - 1];
                        if (topright == 0 || topright == 5 || topright == 6)
                            angle = 90.0f;
                        else if (topleft == 0 || topleft == 5 || topleft == 6) {
                            angle = 180.0f;
                        }
                    }
                    if (c == 0) {
                        int bottomright = levelMap[r + 1, c + 1];
                        if (bottomright == 0 || bottomright == 5 || bottomright == 6) {
                            angle = 270.0f;
                        }
                    }
                    if (r == 0) {
                        int bottomleft = levelMap[r + 1, c - 1];
                        if (bottomleft == 0 || bottomleft == 5 || bottomleft == 6)
                             angle = 270.0f;
                    }
                }
                break;
            case 4:
                if (((top == 3 || top == 4) && (bottom == 3 || bottom == 4)) || (left == 5 || right == 5) || (left == 6 || right == 6) || (top == 7 || bottom == 7) || ((left == 0 || right == 0) && ((top == 3 || bottom == 3) || (top == 4 || bottom == 4)) && !((top == 5 || bottom == 5 || top == 6 || bottom == 6))))
                    angle = 90.0f;
                break;
            case 7: // outer & inner
                if (((top == 1 || top == 2) && (right == 3 || right == 4)) || ((bottom == 1 || bottom == 2) && (right == 3 || right == 4)) && c > 0)
                    angle = 90.0f;
                else if (((left == 1 || left == 2) && (bottom == 3 || bottom == 4)) || ((right == 1 || right == 2) && (bottom == 3 || bottom == 4)) ||  ((left == 1 || left == 2) && (right == 1 || right == 2) && r > 0) )
                    angle = 180.0f;
                else if (((top == 1 || top == 2) && (left == 3 || left == 4)) || ((bottom == 1 || bottom == 2) && (left == 3 || left == 4)))
                    angle = 270.0f;
                break;
        }

        return angle;
    }
}
