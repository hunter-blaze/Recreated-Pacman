using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBorderMainMenu : MonoBehaviour
{
    public RectTransform pelletBorder;
    public RectTransform border;
    

    private Vector2[] positionList;

    private int nextPosition = 0;
    private Tween activeTween;
    private float speed = 100.0f;

    private void Start() {
        positionList = new Vector2[4] {new Vector2(0.5f, 1.0f), new Vector2(0.0f, 0.5f), new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.5f)};
        pelletBorder.anchorMax = positionList[nextPosition];
        pelletBorder.anchorMin = positionList[nextPosition];
        activeTween = new Tween(transform, pelletBorder.anchoredPosition, FindNextPosition(nextPosition), Time.time, speed);
    }

    private void Update() {
        if(Vector2.Distance(pelletBorder.anchoredPosition, activeTween.EndPos) > 0.1f) {
            pelletBorder.anchoredPosition = Vector2.Lerp(activeTween.StartPos, activeTween.EndPos, activeTween.CalculateTimeFraction(activeTween.StartTime, activeTween.Duration));
        } else {
            pelletBorder.anchoredPosition = activeTween.EndPos;
            pelletBorder.Rotate(Vector3.forward, 90.0f);
            
            if (pelletBorder.eulerAngles.z != 90.0f && pelletBorder.localScale.y < 0) {
                Vector3 scale = pelletBorder.localScale;
                scale.y *= -1;
                pelletBorder.localScale = scale;
            } else {
                Vector3 scale = pelletBorder.localScale;
                scale.y *= -1;
                pelletBorder.localScale = scale;
            }

            nextPosition = (nextPosition == 3) ? 0 : nextPosition + 1;

            Vector3 currentPos = pelletBorder.position;
            pelletBorder.anchorMax = positionList[nextPosition];
            pelletBorder.anchorMin = positionList[nextPosition];
            pelletBorder.position = currentPos;

            activeTween = new Tween(transform, pelletBorder.anchoredPosition, FindNextPosition(nextPosition), Time.time, speed);
        }
    }

    private Vector2 FindNextPosition(int nextPosition) {
        if (nextPosition == 0)
            return new Vector2(border.rect.xMin + (pelletBorder.rect.width / 2.0f), pelletBorder.anchoredPosition.y);
        else if (nextPosition == 1)
            return new Vector2(pelletBorder.anchoredPosition.x, border.rect.yMin + (pelletBorder.rect.height / 2.0f));
        else if (nextPosition == 2)
            return new Vector2(border.rect.xMax - (pelletBorder.rect.width / 2.0f), pelletBorder.anchoredPosition.y);
        else if (nextPosition == 3)
            return new Vector2(pelletBorder.anchoredPosition.x, border.rect.yMax - (pelletBorder.rect.height / 2.0f));
        
        return Vector2.zero;
    }
}
