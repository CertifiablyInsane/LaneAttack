using UnityEngine;

public class AnimatorListener : MonoBehaviour
{
    public delegate void AnimationEvent();
    public event AnimationEvent OnEvent01;
    public event AnimationEvent OnEvent02;
    public event AnimationEvent OnEvent03;
    public event AnimationEvent OnEvent04;

    public void TriggerEvent01()
    {
        OnEvent01?.Invoke();
    }
    public void TriggerEvent02()
    {
        OnEvent02?.Invoke();
    }
    public void TriggerEvent03()
    {
        OnEvent03?.Invoke();
    }
    public void TriggerEvent04()
    {
        OnEvent04?.Invoke();
    }
}
