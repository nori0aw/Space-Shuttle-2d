using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Fireworks : MonoBehaviour
{
    // Start is called before the first frame update

    float lifeDuration;
    void Start()
    {
        lifeDuration = GetComponent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        lifeDuration -= Time.deltaTime;
        if (lifeDuration < 0)
        {
            Destroy(gameObject);
        }
    }
}
