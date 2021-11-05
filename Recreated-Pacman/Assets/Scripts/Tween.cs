using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public Transform Target { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }

    public Tween (Vector3 StartPos, Vector3 EndPos, float StartTime, float Duration) {
        this.StartPos = StartPos;
        this.EndPos = EndPos;
        this.StartTime = StartTime;
        this.Duration = Duration;
    }
    public Tween (Transform Target, Vector3 StartPos, Vector3 EndPos, float StartTime) {
        this.Target = Target;
        this.StartPos = StartPos;
        this.EndPos = EndPos;
        this.StartTime = StartTime;
        this.Duration = CalculateDuration(StartPos, EndPos);
    }
    public Tween (Transform Target, Vector3 StartPos, Vector3 EndPos, float StartTime, float Speed) {
        this.Target = Target;
        this.StartPos = StartPos;
        this.EndPos = EndPos;
        this.StartTime = StartTime;
        this.Duration = CalculateDuration(StartPos, EndPos, Speed);
    }

    public float CalculateDuration(Vector3 startPos, Vector3 endPos) {
        return Vector3.Distance(startPos, endPos);
    }
    public float CalculateDuration(Vector3 startPos, Vector3 endPos, float speed) {
        float distance = Vector3.Distance(startPos, endPos);
        float duration = distance / speed;
        return duration;
    }
    public float CalculateTimeFraction(float startTime, float duration) {
        float time = (Time.time - startTime) / duration;
        return time;
    }
}