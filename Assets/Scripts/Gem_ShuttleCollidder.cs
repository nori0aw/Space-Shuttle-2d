using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gem_ShuttleCollidder : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI gemPointsText;
    private float waitForParticleSystemTime;
    private ParticleSystem burstParticleSystem;
    bool destroy =false;
    private void Start()
    {
        burstParticleSystem = GetComponent<ParticleSystem>();
        waitForParticleSystemTime = 1f;

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<SpaceShuttle>())
        {
            /*  transform.GetComponent<Rigidbody2D>().isKinematic = true;
              transform.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
              transform.GetComponent<SpriteRenderer>().enabled = false;*/
            transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetComponent<PolygonCollider2D>().enabled = false;

            SoundManager.Instance.PlayGemSound();
            burstParticleSystem.Play();
            SpaceShuttleGameManager.Instance.TopupFuel();
            gemPointsText.text = "+" + SpaceShuttleGameManager.Instance.GetFuleTopupAmount().ToString();
            gemPointsText.enabled = true;
            animator.enabled = true;
            destroy = true;
            //gameObject.GetComponent<ParticleSystem>().Play();
        }
    }


    private void Update()
    {
        if (destroy)
        {
            waitForParticleSystemTime -= Time.deltaTime;
            if (waitForParticleSystemTime < 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }


}
