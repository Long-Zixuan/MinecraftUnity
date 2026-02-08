using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMC
{


    public class BaseBlock : MonoBehaviour
    {
        public float destroyTime;

        public GameObject brokenEffect;
        public GameObject breakingEffect;
        protected GameObject breakingEffectInstance_;
        public AudioSource brokenSound;
        public AudioSource breakingSound;

        public ItemOnWorld drop_;

        //public BlockType blockType;
        //public BlockType blockType;


        protected float breakingTimer_;

        protected void Awake()
        {
            //StartCoroutine(activeLogic());
        }

        /*protected void OnDisable()
        {
            print("Disable");
            StartCoroutine(activeLogic());
        }*/

        // Start is called before the first frame update
        void Start()
        {

        }


        /*IEnumerator activeLogic()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) <= 10)
                {
                    if (!gameObject.activeSelf)
                    {
                        gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (gameObject.activeSelf)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }*/



        // Update is called once per frame
        void Update()
        {

        }

        public virtual void OnBlockSelected()
        {
            print("selected:" + gameObject.name);
        }

        public virtual void OnBlockDisSelected()
        {
            breakingTimer_ = 0;
            print("Dis selected:" + gameObject.name);
        }

        public virtual void tryBreak()
        {
            if (breakingEffectInstance_ == null)
            {
                breakingEffectInstance_ = Instantiate(breakingEffect, transform.position, Quaternion.identity);
            }

            breakingTimer_ += Time.deltaTime;
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

            if (drop_ != null)
            {
                Instantiate(drop_, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        public virtual bool OnToggle()
        {
            return false;
        }



    }
}
