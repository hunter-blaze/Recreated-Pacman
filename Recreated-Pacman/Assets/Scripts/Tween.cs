using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
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
}