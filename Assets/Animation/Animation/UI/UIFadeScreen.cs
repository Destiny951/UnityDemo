using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeScreen : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut()
    {
        anim.SetTrigger("fadeOut");
    }
    public void FadeIn()
    {
        anim.SetTrigger("fadeIn");
    }
}
