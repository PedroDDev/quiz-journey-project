using UnityEngine;

public class FadeController : MonoBehaviour
{
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetFade()
    {
        _anim.SetTrigger("FadeIn");
    }
}
