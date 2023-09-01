using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    private Animator _anim;
    public bool _isChanging;

    private string goTo;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Fade_Panel_Fade_In_Animation"))
        {
            _isChanging = true;

            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // Change();
                _isChanging = false;
                _anim.SetBool("isFading", false);
            }
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Fade_Panel_Fade_Out_Animation"))
        {
            _isChanging = true;

            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) _isChanging = false;
        }
    }

    public void Fade()
    {
        _anim.SetBool("isFading", true);
    }

    public void ChangeScreen(string methodName, string parameter)
    {
        var method = this.GetType().GetMethod(methodName);

        object[] parameters = new object[] { parameter };

        method.Invoke(this, parameters);
    }

    // public void Change()
    // {
    //     var aux = GameObject.Find(goTo);

    //     if (aux != null) aux.gameObject.SetActive(true);
    //     // else SceneManager.LoadScene(goTo);
    // }

    public void FadeToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
