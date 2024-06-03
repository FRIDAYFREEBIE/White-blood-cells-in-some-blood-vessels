using UnityEngine;

// 메인 카메라 클래스 클래스
public abstract class MainCamera : MonoBehaviour, IMainCamera
{
    public abstract void SetMainCamera();
    public abstract void Movement();
    public abstract void ScaleControl();
    public abstract void MovementRestrictions();
}
