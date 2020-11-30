using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float rotationSpeed;
    public float thrusterSpeed;
    public float brakingSpeed;
    public float rollSpeed;

    public GameObject _ForVect;
    public GameObject _RightVect;
    public GameObject _LeftVect;
    public GameObject _TopVect;
    public GameObject _BotVect;

    public GameObject bottomThruster;

    private float roll;

    public TextMeshProUGUI _XPos;
    public TextMeshProUGUI _YPos;
    public TextMeshProUGUI _ZPos;
    public TextMeshProUGUI _Velocity;
    public TextMeshProUGUI _Fuel;
    public TextMeshProUGUI _LifeSupport;
    public TextMeshProUGUI _Electricity;

    public TextMeshProUGUI TlifeSupport;
    public TextMeshProUGUI TfuelTank;
    public TextMeshProUGUI TelectricityLevel;

    public float _lifeSupport;
    public float _fuelTank;
    public float _electricityLevel;

    private float fuelUsage;
    private float electricityUsage;
    private float lifeSupportUsage;

    public bool isRandomSpawn;

    private bool allowTransfer;

    //Menus
    public GameObject Menu;
    private bool isPaused;
    public GameObject MenuBackground;

    //Sound
    public GameObject ThrusterSound;

    public GameObject FlashingLight;
    private float lightIntensity;
    





    // Start is called before the first frame update
    void Start()
    {
        ThrusterSound.GetComponent<AudioSource>().volume = 0f;
        Time.timeScale = 1;
        Menu.SetActive(false);
        MenuBackground.SetActive(false);
        rb = GetComponent<Rigidbody>();
        allowTransfer = true;

        _lifeSupport = 100;
        lifeSupportUsage = 0.0050f;
        lightIntensity = 0f;

        _fuelTank = 100;
        fuelUsage = 0.04f;

        _electricityLevel = 100;
        electricityUsage = 0.0025f;

        TlifeSupport.SetText((lifeSupportUsage * 50).ToString("#.0000")+"/S");
        TfuelTank.SetText(fuelUsage.ToString("#.0000")+"/T");
        TelectricityLevel.SetText((electricityUsage*50).ToString("#.0000") + "/S");

        if (isRandomSpawn)
        {
            Vector3 randSpawn = new Vector3(Random.Range(-1000, 1000), Random.Range(-100, 100), Random.Range(-100, 100));
            Vector3 randRotation = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
            transform.position += randSpawn;
            transform.localEulerAngles = randRotation;
        }
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if(Input.GetKey("z"))
        {
            Menu.SetActive(true);
            Time.timeScale = 0.1f;
            MenuBackground.SetActive(true);
        }

        if (Input.GetKey("x"))
        {
            Menu.SetActive(false);
            Time.timeScale = 1;
            MenuBackground.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene level = SceneManager.GetActiveScene();
            SceneManager.LoadScene(level.name);
        }

        if (_electricityLevel > 0)
        {
            //_XPos.text = "" + transform.position.x.ToString("#.0000");
            _XPos.SetText(transform.position.x.ToString("#.0000"));
            _YPos.SetText(transform.position.y.ToString("#.0000"));
            _ZPos.SetText(transform.position.z.ToString("#.0000"));
            _Velocity.SetText(rb.velocity.ToString());
            _Electricity.SetText(_electricityLevel.ToString("#") + "J");
        }
        else
        {
            _XPos.SetText("NaN");
            _YPos.SetText("NaN");
            _ZPos.SetText("NaN");
            _Velocity.SetText("NaN");
            _Electricity.SetText("NaN" + "<J>");
        }

        if (_fuelTank > 0)
        {
            //_Fuel.text = "" + _fuelTank.ToString("#") + "G";
            _Fuel.SetText(_fuelTank.ToString("#") + "G");
        }
        else
        {
            _Fuel.SetText("NaN" + "<G>");

        }

        if (_lifeSupport > 0)
        {
            _LifeSupport.SetText(_lifeSupport.ToString("#") + "H");
        }
        else
        {
            _LifeSupport.SetText("NaN" + "<H>");
        }

        Vector3 ForwardVector = _ForVect.transform.position - transform.position;
        Vector3 RightVector = _RightVect.transform.position - transform.position;
        Vector3 TopVector = _TopVect.transform.position - transform.position;

        if (_fuelTank > 0)
        {
            float verticalMov = Input.GetAxisRaw("Vertical");
            float horizontalMov = -1 * Input.GetAxisRaw("Horizontal");

            rb.AddTorque(RightVector * verticalMov * rotationSpeed * Time.deltaTime);
            rb.AddTorque(TopVector * horizontalMov * rotationSpeed * Time.deltaTime);

            if (Input.GetKey("p"))
            {
                rb.AddForce(ForwardVector * thrusterSpeed * Time.deltaTime);
                depleteFuel();
            }

            if (Input.GetKey("o"))
            {
                rb.AddForce(-ForwardVector * brakingSpeed * Time.deltaTime);
                depleteFuel();
            }

            if (Input.GetKey("b"))
            {
                rb.AddForce(TopVector * thrusterSpeed * Time.deltaTime);
                depleteFuel();
            }

            if (Input.GetKey("space"))
            {
                rb.AddForce(-1 * TopVector * thrusterSpeed * Time.deltaTime);
                depleteFuel();
            }

            if (Input.GetKey("q"))
            {
                roll = -1f;
                rb.AddTorque(ForwardVector * rollSpeed * roll * Time.deltaTime);
                depleteFuel();
            }

            if (Input.GetKey("e"))
            {
                roll = 1f;
                rb.AddTorque(ForwardVector * rollSpeed * roll * Time.deltaTime);
                depleteFuel();
            }

            if (Input.GetKeyDown("g") && (allowTransfer))
            {
                //StartCoroutine("TransferEnergy");
                TransferFuel();
            }

            if (Input.GetKeyDown("h") && (allowTransfer))
            {
                //StartCoroutine("TransferEnergy");
                TransferLife();
            }

            if (Input.GetKeyDown("j") && (allowTransfer))
            {
                //StartCoroutine("TransferEnergy");
                TransferElec();
            }

        }

        if (Input.GetKeyDown("p") || Input.GetKeyDown("o") || Input.GetKeyDown("q") || Input.GetKeyDown("w") || Input.GetKeyDown("e") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d") || Input.GetKeyDown("b") || Input.GetKeyDown("space"))
        {
            ThrusterSound.GetComponent<AudioSource>().volume = 1f;
        }

        if (Input.GetKeyUp("p") || Input.GetKeyUp("o") || Input.GetKeyUp("q") || Input.GetKeyUp("w") || Input.GetKeyUp("e") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d") || Input.GetKeyUp("b") || Input.GetKeyUp("space"))
        {
            ThrusterSound.GetComponent<AudioSource>().volume = 0f;
        }



        depleteElectricity();
        depleteLifeSupport();

    }

    public void depleteFuel()
    {
        _fuelTank -= fuelUsage;
    }

    public void depleteElectricity()
    {
        _electricityLevel -= electricityUsage;
    }

    public void depleteLifeSupport()
    {
        _lifeSupport -= lifeSupportUsage;
        if (_lifeSupport < 20f)
        {
            lightIntensity += (lifeSupportUsage/5);
            FlashingLight.GetComponent<Light>().intensity = lightIntensity;
        }
        else
        {
            lightIntensity = 0f;
        }
        

    }

    private void TransferFuel()
    {
        if (_electricityLevel > 0 && _lifeSupport>0) {
            allowTransfer = false;
            //Debug.Log(powerTo);
            _fuelTank += 20;
            _electricityLevel -= 10;
            _lifeSupport -= 10;
            allowTransfer = true;
        }
    }

    private void TransferLife()
    {
        if (_electricityLevel > 0 && _fuelTank > 0)
        {
            allowTransfer = false;
            //Debug.Log(powerTo);
            _fuelTank -= 10;
            _electricityLevel -= 10;
            _lifeSupport += 20;
            allowTransfer = true;
        }
       

    }

    private void TransferElec()
    {
        if (_fuelTank > 0 && _lifeSupport > 0)
        {
            allowTransfer = false;
            //Debug.Log(powerTo);
            _fuelTank -= 10;
            _electricityLevel += 20;
            _lifeSupport -= 10;
            allowTransfer = true;
        }
    }

}
