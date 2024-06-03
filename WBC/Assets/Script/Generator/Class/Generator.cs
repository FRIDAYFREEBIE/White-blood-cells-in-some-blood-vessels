using UnityEngine;

// 생성기 클래스
public abstract class Generator : MonoBehaviour, IGenerator
{
    public abstract void Generate();
}