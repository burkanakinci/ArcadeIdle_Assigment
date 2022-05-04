using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "Cube Data")]
public class CubeData : ScriptableObject
{
    public float cubeJumpPower = 2f;
    public float cubeJumpDuration = 1f;
    public float cubeSpawnRate = 3f;
    public float cubePunchScaleDuration = 0.65f;
    public float cubePunchScaleMultiplier = 1.2f;
}