using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    public float destroyTime;

    public GameObject brokenEffect;
    public GameObject breakingEffect;
    protected GameObject breakingEffectInstance_;
    public AudioSource brokenSound;
    public AudioSource breakingSound;
    
    public ItemOnWorld drop_;
    
    
    protected float breakingTimer_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnBlockClicked()
    {
        print("Clicked:"+gameObject.name);
    }
    
    public virtual void OnBlockHover()
    {
        breakingTimer_ = 0;
        print("Hover:"+gameObject.name);
    }

    public virtual void tryBreak()
    {
        if (breakingEffectInstance_ == null)
        {
            breakingEffectInstance_ = Instantiate(breakingEffect, transform.position, Quaternion.identity);
        }
        breakingTimer_+= Time.deltaTime;
        if (breakingSound != null)
        {
            breakingSound.Play();
        }
            
        if (breakingTimer_ >= destroyTime)
        {
            broken();
        }
    }

    public void resetBreakingTimer()
    {
        breakingTimer_ = 0;
    }
    
    protected virtual void broken()
    {
        Instantiate(brokenEffect, transform.position, Quaternion.identity);
        if (brokenSound != null)
        {
            brokenSound.Play();
        }
        if(drop_ != null)
        {
            Instantiate(drop_, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    
    
}
