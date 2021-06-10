using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private float distToGround = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI pickUpLeftText;
    public TextMeshProUGUI ballSpeedText;
    public TextMeshProUGUI nextTargetText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject restartButton;
    public bool isGameActive;

    private Rigidbody rb;
    private int count;
    private GameObject[] pickupArray;
    private int pickupTotal;

    private float movementX;
    private float movementY;

    private float timer = 61;
    private float nextTarget = 6;
    private bool isTimer = false;


    [SerializeField] AudioClip[] playerSounds;
    public float soundFxVolume;
    AudioSource myAudioSource;
    AudioSource[] audios;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
        count = 0;

        pickupArray = GameObject.FindGameObjectsWithTag("PickUp");
        pickupTotal = pickupArray.Length;
        Time.timeScale = 0f;

        SetCountText();
        winTextObject.SetActive(false);
        restartButton.SetActive(false);
        loseTextObject.SetActive(false);

        myAudioSource = GetComponent<AudioSource>();
        audios = Camera.main.gameObject.GetComponents<AudioSource>();

        isTimer = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Cibles acquises: " + count.ToString();

        //if(count >= 12)
        if (count >= pickupTotal)
        {
            winTextObject.SetActive(true);
            restartButton.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        pickUpLeftText.text = "Cibles restantes: " + (pickupTotal - count).ToString();
        ballSpeedText.text = "Vitesse balle: " + speed.ToString();


        if (kb.spaceKey.wasPressedThisFrame) 
        {
            Debug.Log("Space Pressed " + rb.velocity);
            if (IsGrounded())
            {
                rb.velocity = Vector2.up * speed / 2;
            }
        }
        if (isTimer)
        {
            if (timer > 0 && nextTarget > 0)
            {
                timer -= Time.deltaTime;
                nextTarget -= Time.deltaTime;
                timerText.text = "Temps restant: " + Mathf.FloorToInt(timer % 60).ToString();
                nextTargetText.text = "Prochaine cible: " + Mathf.FloorToInt(nextTarget % 60).ToString();

                if (timer <= 10 || nextTarget <= 2)
                {
                    NearEndGameSound();
                }
            }
            else
            {
                isTimer = false;
                timer = 0;
                loseTextObject.SetActive(true);
                restartButton.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            //nextTarget = 6;
            StartGameSound();
            SetCountText();
            //GetComponent<AudioSource>().Play();
            AudioClip clip = playerSounds[0];
            myAudioSource.PlayOneShot(clip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle"))
        {
            AudioClip clip = playerSounds[1];
            myAudioSource.PlayOneShot(clip);
        }
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void NearEndGameSound()
    {
        //audios[0].pitch = soundFxVolume;
        audios[0].volume = soundFxVolume;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, (float)(distToGround + 0.1));
    }


public void StartGameSound()
    {
        //audios[0].pitch = soundFxVolume;
        audios[0].volume = soundFxVolume;
    }
}