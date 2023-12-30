using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class SpaceShuttle : MonoBehaviour
{

    [SerializeField] private ParticleSystem mainThruster;
    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem rightThruster;

    [SerializeField] float thrustersForce;
    [SerializeField] private float tiltAngelSpeed;

    [SerializeField] private GameInput gameInput;
    // Start is called before the first frame update

    private Rigidbody2D shuttleRigidbody;
    private float fuel;
    private float fuelConsumptionMultiplier;
    private float fuelTopupAmout;
    private bool ignoreUserInput = false;
    
    
    void Start()
    {
        fuel = 100f;
        ChangeThrustersEmmision(false);
        shuttleRigidbody = GetComponent<Rigidbody2D>();

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            shuttleRigidbody.mass = 0.4f;
        }
        else
        {
            shuttleRigidbody.mass = 0.2f;
        }


        switch (SceneLoader.DefficultyLevel)
        {
            case 1:
                fuelConsumptionMultiplier = 5f;
                fuelTopupAmout = 50;
                break;
            case 2:
                fuelConsumptionMultiplier = 5f;
                fuelTopupAmout = 25;
                break;
            case 3:
                fuelConsumptionMultiplier = 5f;
                fuelTopupAmout = 15;
                break;
            default:
                Debug.LogError("DefficultyLevel(" + SceneLoader.DefficultyLevel + ")is wrong");
                break;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 20)
        {
            SoundManager.Instance.PlayBumpSound();
        }
            
        
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (!ignoreUserInput)
        {
            HandelMovment();
        }
        
      
    }

    private void HandelMovment()
    {


        Vector2 inputVector = GameInput.Instance.GetMovmentVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);
       
        if (moveDir != Vector2.zero)
        {

            UseFuel(moveDir.y > 0, moveDir.x != 0);

            ChangeThrustersEmmision(moveDir.y > 0, moveDir.x < 0 || (moveDir.y < 0 && moveDir.x == 0), moveDir.x > 0 || (moveDir.y < 0 && moveDir.x == 0)) ;
            float sine = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
            float cos = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);

            if(fuel > 0)
            {
                if (moveDir.y > 0f)
                {


                    shuttleRigidbody.AddForce(new Vector3(thrustersForce * (-sine), thrustersForce * cos, 0));

                }



                if (moveDir.x > 0f)
                {

                    shuttleRigidbody.AddTorque(-tiltAngelSpeed, ForceMode2D.Impulse);


                    //transform.Rotate(Vector3.forward, -tiltAngelSpeed * Time.deltaTime);
                }
                if (moveDir.x < 0f)
                {
                    shuttleRigidbody.AddTorque(tiltAngelSpeed, ForceMode2D.Impulse);
                    //transform.Rotate(Vector3.forward,  tiltAngelSpeed * Time.deltaTime);
                }
                if (moveDir.x != 0)
                {
                    shuttleRigidbody.AddForce(new Vector3((thrustersForce / 3F) * (-sine), (thrustersForce / 3F) * cos, 0));
                }
            }
            else
            {
                ChangeThrustersEmmision(false, false, false);
                SoundManager.Instance.PlayBigThrusterSound(false);
                SoundManager.Instance.PlaySmallThrusterSound(false);
            }


            
        }
        else
        {
            ChangeThrustersEmmision(false, false, false);
            SoundManager.Instance.PlayBigThrusterSound(false);
            SoundManager.Instance.PlaySmallThrusterSound(false);

        }
    }

    private void ChangeThrustersEmmision(bool emmitMain, bool emmitLeft =false, bool emmitRigt = false)
    {
        if (emmitMain && !mainThruster.isEmitting)
        {
            mainThruster.Play();
           
        }
        if(!emmitMain && mainThruster.isEmitting)
        {
            mainThruster.Stop();

        }


        if (emmitLeft && !leftThruster.isEmitting)
        {
            leftThruster.Play();
           
        }
        if(!emmitLeft && leftThruster.isEmitting)
        {
            leftThruster.Stop();

        }

        if (emmitRigt && !rightThruster.isEmitting)
        {
            rightThruster.Play();

        }
        if (!emmitRigt && rightThruster.isEmitting)
        {
            rightThruster.Stop();

        }

        SoundManager.Instance.PlayBigThrusterSound(emmitMain, 0.8f);
        SoundManager.Instance.PlaySmallThrusterSound(emmitLeft || emmitRigt, 0.4f);
    }

    public void IgnoreUserInput(bool ignoreUserInput)
    {
        this.ignoreUserInput = ignoreUserInput;
    }

    public float GetFuel()
    {
        return fuel;
    }
    public void TopUpFuel()
    {

        fuel = fuel + fuelTopupAmout;
        if (fuel > 100f)
        {
            fuel = 100f;
        }
    }

    private void UseFuel(bool mainThruster, bool smallThruster)
    {
        if(fuel > 0)
        {
            if (mainThruster)
            {
                fuel = fuel - (fuelConsumptionMultiplier * Time.deltaTime);
            }
            if (smallThruster)
            {
                fuel = fuel - ((fuelConsumptionMultiplier / 4) * Time.deltaTime);
            }
        }
        else
        {
            fuel = 0f;
        }

        
        
    }

    public int GetFuleTopupAmount()
    {
        return (int)fuelTopupAmout;
    }
}
