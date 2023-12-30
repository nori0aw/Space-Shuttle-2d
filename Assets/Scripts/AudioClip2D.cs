using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClip2D : MonoBehaviour
{
    // Start is called before the first frame update
    private float lifetime;
    void Start()
    {
        lifetime = transform.GetComponent<AudioSource>().clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime; 
        if ( lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
