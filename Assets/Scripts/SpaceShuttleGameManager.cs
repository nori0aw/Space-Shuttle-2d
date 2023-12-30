using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Transactions;

using Unity.VisualScripting;
using UnityEngine;

using static UnityEngine.TouchScreenKeyboard;

public class SpaceShuttleGameManager : MonoBehaviour
{
    [SerializeField] AstroidsSO astroidsSO;
    [SerializeField] SpaceShuttle spaceShuttle;
    [SerializeField] GameObject astroids;
    [SerializeField] GameObject walls;
    [SerializeField] ArrowCanvas arrowCanvas;
    [SerializeField] GameObject gem;

    [SerializeField] GameObject[] Fireworks;


    

    private int currentMaxY;
    private int astroidGenerateStep = 400;
    private GameObject leftWall;
    private GameObject rightWall;

    private float gamePlayingTimer = 0f;
    private int gemCounter = 0; 
    public static SpaceShuttleGameManager Instance { private set; get; }

    private int GemGenerateStep = 300;
    private GameObject currentGem;

    private bool isPaused = false;
    private bool isGameEnded = false;

    private float gameEndCelabrationTime = 7f;
    private float GameoverTimer = 4f;
    private float fireworkstime =0;
    private string endGameMessage = "Great Job!";
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }
    void Start()
    {
        
        leftWall = walls.transform.GetChild(0).gameObject;
        rightWall = walls.transform.GetChild(1).gameObject;
        GenerateAstroids(-180, 180, 50, (float) astroidGenerateStep, 25);
        currentMaxY = astroidGenerateStep;
        GenerateGem(-180, 180, 100, 200);

        GameInput.Instance.OnPause += GameInput_OnPause;


    }

    private void GameInput_OnPause(object sender, System.EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        if (!isPaused) 
        {
            GameInput.Instance.DisableUserInput();
            Time.timeScale = 0;
            isPaused = true;
            PauseGameUI.Instance.Show();
            JoystickCanvas.Instance.Hide();
        }
        else
        {
            GameInput.Instance.EnableUserInput();
            Time.timeScale = 1;
            isPaused = false;
            PauseGameUI.Instance.Hide();
            SoundOptionsUI.Instance.Hide();
            JoystickCanvas.Instance.Show();
        }

    }

    public bool GetPauseStatus()
    {
        return isPaused;
    }

    private void GenerateGem(float minX, float maxX, float minY, float maxY){

        Collider2D [] intersecting;
        float x, y;
        
        do
        {

            x = Random.Range(minX, maxX);
            y = Random.Range(minY, maxY);
            intersecting = Physics2D.OverlapCircleAll(new Vector2(x, y), 2f);
            if (intersecting.Length == 0)
            {
                //code to run if nothing is intersecting as the length is 0
            }
            else
            {
                Debug.Log("an object already exists");
            }
        } while (intersecting.Length > 0);

        


        currentGem = Instantiate(gem);
        currentGem.transform.position = new Vector3(x, y, 0f);
        currentGem.GetComponent<Rigidbody2D>().AddTorque(100f);
        

        arrowCanvas.SetGem(currentGem.gameObject);
        
    }

    

    

    // Update is called once per frame
    void Update()
    {
        if (spaceShuttle.transform.position.y > currentMaxY - (astroidGenerateStep / 2)) {
           
            GenerateAstroids(-180, 180, currentMaxY, currentMaxY + astroidGenerateStep, 25);
            currentMaxY += astroidGenerateStep;
            GenerateWalls();
        }


        if(!isGameEnded)
        {
            if (currentGem == null)
            {
                gemCounter++;
                if (gemCounter >= GetGemsTotalCount())
                {
                    EndGame();
                    return;
                }
                GenerateGem(-180, 180, spaceShuttle.transform.position.y + 50, spaceShuttle.transform.position.y + GemGenerateStep);

            }

            gamePlayingTimer += Time.deltaTime;


            // this is to check if there is a need to end the game in the case of no fuel 
            if (spaceShuttle.GetFuel() <= 0 ) 
            {
                GameoverTimer -= Time.deltaTime;
                if (GameoverTimer < 0)
                {
                    EndGameUI.Instance.Show("You Ran Out Of Fuel!");
                }
            }
            //////////////////////////////////////////////////////////////////////////


            // this section is to check if the space shuttle is stuck on the floor.
            float zAngel = spaceShuttle.transform.rotation.eulerAngles.z;
            if (zAngel > 180) { zAngel -= 360f; }

            if ((spaceShuttle.transform.position.y >0.6f && spaceShuttle.transform.position.y < 0.7f) &&
                 (Mathf.Abs(zAngel)  > 89.8f) && Mathf.Abs(zAngel) < 90.8f) 
            {
                GameoverTimer -= Time.deltaTime;
                if (GameoverTimer < 0)
                {
                    EndGameUI.Instance.Show("You Got Stuck!");
                }
            }
            /////////////////////////////////////////////////////////////////////
            ///


            // Check if the mobile back button is pressed to pause the game

            if (GameInput.Instance.IsMobilePlatform())
            {
                if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) 
                {
                    TogglePauseGame();  
                }
            }
            ////////////////////////////////////////////////////////////////////
            
        }
        else
        {
            gameEndCelabrationTime -= Time.deltaTime;

            if (gameEndCelabrationTime < 0)
            {
 
                EndGameUI.Instance.Show(endGameMessage);

            }

            if(fireworkstime <= 0)
            {
                GenerateFirework();
                fireworkstime = Random.Range(0f, 0.3f);

            }
            else
            {
                fireworkstime-= Time.deltaTime;
            }

        }


    



    }

    private void GenerateFirework()
    {
        int i = Random.Range(0, Fireworks.Length );
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float x = Random.Range(min.x,max.x);
        float y = Random.Range(min.y, max.y);
        GameObject firework = Fireworks[i];
        firework.transform.position = new Vector3(x, y, 0f);
        if(gameEndCelabrationTime < 0)
        {
            firework.GetComponent<AudioSource>().volume = 0f;
        }
        else 
        {
            firework.GetComponent<AudioSource>().volume = 0.5f;
        }

        Instantiate(firework);
    }


    private void GenerateAstroids(float minX, float maxX, float minY, float maxY , int numberOfAstroids)
    {
        for(int i = 0; i < numberOfAstroids; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            float rotationForce = Random.Range(500f, 1000f);
            
            int astroidNumber = Random.Range(1, astroidsSO.Astroids.Length);
            GameObject astroid = astroidsSO.Astroids[astroidNumber];
            astroid.transform.position = new Vector3(x, y, 0f);



            Rigidbody2D astroidRigidbody = Instantiate(astroid, astroids.transform).GetComponent<Rigidbody2D>();
            if(i % 2 == 0)
            {
                astroidRigidbody.AddTorque(rotationForce);
            }
            else
            {
                astroidRigidbody.AddTorque(- rotationForce);
            }
            


        }

    }

    private void EndGame()
    {
        isGameEnded = true;
        spaceShuttle.IgnoreUserInput(true);
        spaceShuttle.GetComponent<Rigidbody2D>().isKinematic = true;
        spaceShuttle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SoundManager.Instance.LowerMusicVol();

        float lastBestTime = PlayerPrefs.GetFloat(SceneLoader.DefficultyLevel.ToString(), -1f);
        if (lastBestTime < 0 || lastBestTime > gamePlayingTimer)
        {
            PlayerPrefs.SetFloat(SceneLoader.DefficultyLevel.ToString(), gamePlayingTimer);
            endGameMessage = "You Got New Record!";
        }
    }
    private void GenerateWalls()
    {
        GameObject newLeftWall = Instantiate(leftWall, walls.transform);
        GameObject newrightWall = Instantiate(rightWall, walls.transform);

        newLeftWall.transform.position = new Vector3(leftWall.transform.position.x, leftWall.transform.position.y + astroidGenerateStep);
        newrightWall.transform.position = new Vector3(rightWall.transform.position.x, rightWall.transform.position.y + astroidGenerateStep);

        leftWall = newLeftWall;
        rightWall = newrightWall;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public float GetGamePlayingTime()
    {
        return gamePlayingTimer;
    }

    public int GetGemCount()
    {
        return gemCounter;
    }

    public int GetGemsTotalCount()
    {
        switch (SceneLoader.DefficultyLevel)
        {
            case 1:
                return 30;
            case 2:
                return 40;
            case 3:
                return 50;
            default: 
                Debug.LogError("DefficultyLevel("+ SceneLoader.DefficultyLevel + ")is wrong");
                break;
        }
        throw new System.Exception("DefficultyLevel(" + SceneLoader.DefficultyLevel + ")is wrong");

    }

    public float GetFuelLevel()
    {
        return spaceShuttle.GetFuel();
    }

    public void TopupFuel()
    {
        spaceShuttle.TopUpFuel();
    }
    

    private void OnApplicationPause(bool pause)
    {
        if (pause && !isPaused)
        {
            TogglePauseGame();
        }
        
    }

    

    public int GetFuleTopupAmount()
    {
        return spaceShuttle.GetFuleTopupAmount();
    }
}
