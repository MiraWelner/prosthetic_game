using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


/**
 *  
        This code goes on the 'rocket' object. It does the vast majority of the heavy lifting - it also instantiates the little 'blocks' that display the required hand motions. If you want to 
        make it so that it takes things other than keycodes, you can edit that  in the keycode definition block although if you want to make the input somthing other than a keycode you will have to 
        change the data type keycode later on in the code
        **/
public class rocket_behavior : MonoBehaviour
{
    /* movements.txt is the file where data is reported*/
    string path = "movements.txt";
    
    KeyCode medium_wrap = KeyCode.Alpha1;
    KeyCode power_sphere = KeyCode.Alpha2;
    KeyCode precision_disk = KeyCode.Alpha3;
    KeyCode prismatic_2_finger = KeyCode.Alpha4;
    KeyCode lateral_tripod = KeyCode.Alpha5;
    KeyCode tripod = KeyCode.Alpha6;
    KeyCode lateral = KeyCode.Alpha7;
    KeyCode light_tool = KeyCode.Alpha8;

    public GameObject MediumWrap;
    public GameObject LateralTripod;
    public GameObject Tripod;
    public GameObject LightTool;
    public GameObject Prismatic2Finger;
    public GameObject PrecisionDisk;
    public GameObject Lateral;
    public GameObject PowerSphere;
    public GameObject projectile;
    public GameObject shield;
    public GameObject greenShip;
    public GameObject antenna;
    public GameObject asteriod1;
    public GameObject asteriod2;
    public GameObject asteriod3;
    public GameObject asteriod4;
    public GameObject asteriod5;
    public GameObject radioSignal;
    public AudioSource bulletShoot;
    public AudioSource shieldUp;
    public AudioSource sendRadio;
    public AudioSource anntenaUp;
    public AudioSource movement;


    public int speed;
    public int minutes;
    public int phaseTime;

    private GameObject clonePos;
    private GameObject shipAntenna;
    private GameObject cloneShield;
    private GameObject radio;
    readonly List<string> positions = new List<string>();
    private static bool created = false;
    private bool positionExits = false;
    private GameObject prevClone;
    private int distance = 100;

    int randvar;
    float timePressed;


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "game")
        {
            InvokeRepeating(nameof(GenerateBlock), 0, phaseTime);
            StartCoroutine(LoadLevelAfterDelay(minutes * 60));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        /*If the rocket is still in the intro scene, it waits to havethe game scene triggered if it is clicked. It also determines positions are possible to be required during the game by seeing
         which ones the user clicks */
        if (currentScene.name == "intro")
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "Rocket")
                    {
                        SceneManager.LoadScene("game");
                    }
                    else
                    {
                        if (positions.FindIndex(x => x == hit.transform.name) == -1)
                        {
                            positions.Add(hit.transform.name);
                        }
                        else
                        {
                            positions.RemoveAt(positions.FindIndex(x => x.StartsWith(hit.transform.name)));

                        }
                    }
                }
            }
        }

        /*If the rocket is in the game scene, it consistantly moves forward. At random intervals, enemies appear that the user must deal with*/
        if(currentScene.name == "game")
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (Input.GetKeyDown(medium_wrap)||Input.GetKeyDown(power_sphere)||Input.GetKeyDown(precision_disk)||Input.GetKeyDown(prismatic_2_finger))
            {
                Instantiate(movement);
            }


            //Finds when precision disk starts
            if (Input.GetKeyDown(precision_disk))
            {
                timePressed = Time.time;
            }
            //Moves up when precision_disk happens
            if (Input.GetKey(precision_disk))
            {
                Vector3 position = this.transform.position;
                position.x += System.Convert.ToSingle(0.05);
                this.transform.position = position;
            }
            //Prints how long precision disk held
            if(Input.GetKeyUp(precision_disk))
            {
                timePressed = Time.time - timePressed;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Precision Disk held for " + timePressed + " seconds");
                writer.Close();

            }

            //Finds when prismatic 2 finger starts
            if (Input.GetKeyDown(prismatic_2_finger))
            {
                timePressed = Time.time;
            }
            //Moves down when prismatic 2 finger happens
            if (Input.GetKey(prismatic_2_finger))
            {
                Vector3 position = this.transform.position;
                position.x -= System.Convert.ToSingle(0.05);
                this.transform.position = position;
            }
            //Prints how long prismatic 2 finger was held
            if (Input.GetKeyUp(prismatic_2_finger))
            {
                timePressed = Time.time - timePressed;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Prismatic 2 finger held for " + timePressed + " seconds");
                writer.Close();

            }

            //Finds when power sphere starts
            if (Input.GetKeyDown(power_sphere))
            {
                timePressed = Time.time;
            }
            //Moves left when power sphere is held
            if (Input.GetKey(power_sphere))
            {
                Vector3 position = this.transform.position;
                position.y -= System.Convert.ToSingle(0.05);
                this.transform.position = position;
            }
            //Prints how long power sphere was held
            if (Input.GetKeyUp(power_sphere))
            {
                timePressed = Time.time - timePressed;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Power sphere held for " + timePressed + " seconds");
                writer.Close();

            }

            //Finds when medium wrap starts
            if (Input.GetKeyDown(medium_wrap))
            {
                timePressed = Time.time;
            }
            //Moves right when medium wrap happens
            if (Input.GetKey(medium_wrap))
            {
                Vector3 position = this.transform.position;
                position.y += System.Convert.ToSingle(0.05);
                this.transform.position = position;
            }
            //Prints how long medium wrap held
            if (Input.GetKeyUp(medium_wrap))
            {
                timePressed = Time.time - timePressed;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Medium wrap held for " + timePressed + " seconds");
                writer.Close();

            }

            //Shoots bullet and finds when lateral tripod starts
            if (Input.GetKeyDown(lateral_tripod))
            {
                timePressed = Time.time;
                Instantiate(bulletShoot);
                GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            }
            //Prints how lateral tripod wrap held
            if (Input.GetKeyUp(lateral_tripod))
            {
                timePressed = Time.time - timePressed;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Lateral Tripod held for " + timePressed + " seconds");
                writer.Close();

            }

            //Makes sheild and finds when tripod starts
            if (Input.GetKeyDown(tripod))
            {
                timePressed = Time.time;
                Instantiate(shieldUp);
                cloneShield = (GameObject)Instantiate(shield, transform.position, Quaternion.identity);
                clonePos.transform.parent = gameObject.transform;
            }
            //Destroys sheild, prints how long tripod done
            if (Input.GetKeyUp(tripod))
            {
                Destroy(cloneShield);
                timePressed = Time.time - timePressed;
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Tripod held for " + timePressed + " seconds");
                writer.Close();
            }

            //Starts sending radio, initiates lateral
            if (Input.GetKeyDown(lateral))
            {
                timePressed = Time.time;
                Instantiate(sendRadio);
                radio = (GameObject)Instantiate(radioSignal, transform.position, Quaternion.identity);
                radio.transform.parent = gameObject.transform;
                radio.transform.Rotate(0, 45, 90, Space.Self);
            }
            //prints how long lateral was held, destroy radio
            if (Input.GetKeyUp(lateral))
            {
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Later held for " + timePressed + " seconds");
                writer.Close();
                Destroy(radio);
            }

            //Puts anntena up, starts tracking time
            if (Input.GetKeyDown(light_tool))
            {
                timePressed = Time.time;
                Instantiate(anntenaUp);
                var antennaPos = transform.position + (transform.up/3) + (transform.forward/2) - (transform.right/2);
                shipAntenna = (GameObject)Instantiate(antenna, antennaPos, Quaternion.identity);
                shipAntenna.transform.Rotate(-20,10,0, Space.Self);
                shipAntenna.transform.parent = gameObject.transform;
            }
            //prints how long antenna up, destroy antenna
            if (Input.GetKeyUp(light_tool))
            {
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine("Light Tool held for " + timePressed + " seconds");
                writer.Close();
                Destroy(shipAntenna);
            }
        }
    }

    /*This generates the 'block' object which displays the required hand mostion*/
    void GenerateBlock()
    {
        transform.Translate(0,0, transform.position.z);
        System.Random random = new System.Random();
        randvar = random.Next(positions.Count);
        if (positionExits)
        {
            prevClone = clonePos;
            Destroy(prevClone);
        }
        else
        {
            positionExits = true;
        }
        var clonePoseLocation = transform.position + (transform.right * 3);
        clonePos = (GameObject)Instantiate(Resources.Load(positions[randvar]), clonePoseLocation, Quaternion.identity);
        clonePos.transform.parent = gameObject.transform;
        Debug.Log(positions[randvar]);
        if(positions[randvar] == "MediumWrap")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("MEDIUM WRAP");
            writer.Close();
            MeteorUp();
        } else if(positions[randvar] == "PowerSphere")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("POWER SPHERE");
            writer.Close();
            MeteorDown();
        } else if(positions[randvar] == "PrecicsionDisk")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("PRECISION DISK");
            writer.Close();
            MeteorLeft();
        } else if(positions[randvar] == "Prismatic2Finger")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("PRISMATIC 2 FINGER");
            writer.Close();
            MeteorRight();
        }
        else if (positions[randvar] == "LateralTripod")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("LATERAL TRIPOD");
            writer.Close();
            BulletShot();
        }
        else if (positions[randvar] == "Tripod")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("TRIPOD");
            writer.Close();
            Shields();
        }
        else if (positions[randvar] == "Lateral")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("LATERAL");
            writer.Close();
            RadioSend();
        }
        else if (positions[randvar] == "Light Tool")
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("LIGHT TOOL");
            writer.Close();
            RadioReceive();
        }
    }

    /*The following functions illustrate the responses to the hand motions being performed*/
    void MeteorUp()
    {
        var asteroidPosition = transform.position + (transform.forward * distance) + (transform.up * -50);
        Instantiate(asteriod1, asteroidPosition, Quaternion.identity);
    }

    void MeteorDown()
    {
        var asteroidPosition = transform.position + (transform.forward * distance) + (transform.up * 50);
        Instantiate(asteriod2, asteroidPosition, Quaternion.identity);
    }

    void MeteorLeft()
    {
        var asteroidPosition = transform.position + (transform.forward * distance) + (transform.right * -50);
        Instantiate(asteriod3, asteroidPosition, Quaternion.identity);
    }

    void MeteorRight()
    {
        var asteroidPosition = transform.position + (transform.forward * distance) + (transform.right * 50);
        Instantiate(asteriod3, asteroidPosition, Quaternion.identity);
    }

    void BulletShot()
    {
        var asteroidPosition = transform.position + (transform.forward * distance);
        Instantiate(asteriod4, asteroidPosition, Quaternion.identity);
    }

    void Shields()
    {
        for (int i = -5; i < 5; i++)
        {
            for (int j = -5; j < 0; j++)
            {
                var asteroidPosition = transform.position + (transform.forward * distance/2) + (transform.right * i)+(transform.up * j/2);
                Instantiate(asteriod5, asteroidPosition, Quaternion.identity);
            }
            for (int j = 1; j < 5; j++)
            {
                var asteroidPosition = transform.position + (transform.forward * distance/2) + (transform.right * i) + (transform.up * j/2);
                Instantiate(asteriod5, asteroidPosition, Quaternion.identity);
            }
        }
    }

    void RadioSend()
    {
        var shipPos = transform.position + (transform.forward * distance) + (transform.right * 10);
        GameObject newGreenShip = Instantiate(greenShip, shipPos, Quaternion.identity);
        newGreenShip.transform.Rotate(0, -135, 0, Space.Self);

    }

    void RadioReceive()
    {
        var shipPos = transform.position + (transform.forward * distance) + (transform.right * -15);
        GameObject ship = Instantiate(greenShip, shipPos, Quaternion.identity);
        ship.transform.Rotate(0, 135, 0, Space.Self);
        radio = (GameObject)Instantiate(radioSignal, shipPos, Quaternion.identity);
        radio.transform.Rotate(0, 135, 90, Space.Self);
    }



    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("landing");
        Destroy(this.gameObject);
    }
}
