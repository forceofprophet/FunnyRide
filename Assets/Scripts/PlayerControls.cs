using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    Animator unicycleAnimator;
    Animator riderAnimator;
    [SerializeField] GameObject rootBone;
    [SerializeField] GameObject rider;
    public float tiltingAngle;
    [SerializeField] Rigidbody unicycleRigidbody;
    float Horz;
    int horzFloat = Animator.StringToHash("Horz");
    bool isTilt;
    int tilt = 1;
    public int driveMode = 1;
    float yVelocity = 0.5f;
    [SerializeField] RagDollScript ragDollScript;
    float speed = 4.0f;
    public int score = 0;
    public float totalScore;
    public float timeScore;
    public Text scoreText;
    public Text timeText;
    public Text totalText;
    [SerializeField] UIMenu uiMenu;
    [SerializeField] Stopwatch stopwatch;
    private AudioSource _audio;
    public AudioClip woodHit;
    public AudioClip coins;
    void Awake()
    {
        unicycleAnimator = GetComponent<Animator>();
        riderAnimator = rider.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        StopAllCoroutines();
        unicycleRigidbody.interpolation = RigidbodyInterpolation.None;
        unicycleRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;  // иначе уницикл вращается во время езды 
    }
    void FixedUpdate()
    {
        unicycleRigidbody.velocity = new Vector3(Horz * 1.5f, unicycleRigidbody.velocity.y, speed); // скоростьсть и направление движения игрока 
    }

    void Update()
    {
        timeText.text = timeScore.ToString();
        timeScore = stopwatch.currentTime;
        totalScore = score + stopwatch.mathfTime;
        scoreText.text = score.ToString();
        unicycleAnimator.SetFloat(horzFloat, Horz, 0.1f, Time.deltaTime);
        riderAnimator.SetFloat(horzFloat, Horz, 0.1f, Time.deltaTime);
        if (driveMode == 1)
        {
            Horz = Input.GetAxis("Horizontal");
            Driving();
        }
        if (driveMode == 0)
        {
            Horz = 0;
            speed = 0;
        }
        if(driveMode == -1)
        {
            Horz = 0;
            speed = Mathf.SmoothDamp(speed, 0.0f, ref yVelocity, 1.0f);  // если упал не от столкновения - замедляем скорость движения
        }
        
        if (tilt == 0)
        {
            tiltingAngle = (rootBone.transform.localEulerAngles.x - 90);
        }
        if (tilt == 2)
        {
            tiltingAngle = (rootBone.transform.localEulerAngles.x - 90) * -1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unicycleRigidbody.constraints = RigidbodyConstraints.None;
            driveMode = 0;
        }
    }
    void Driving()
    {
        RandomNumber();
        StartCoroutine("TiltRandom"); // запускаем корутину
        if (tilt == 0)     // если число равно 0 - останавливаем корутину, запускаем анимациб падения при которой отслеживается перемещение игрока.
        {                  // если отклонится в сторону наклона - падение, если ничего не предпринимается и будет достигнут особый угол - падение, если отклонится в противоположную сторону - выравнивание
            
            StopCoroutine("TiltRandom");
            isTilt = false;
            unicycleAnimator.SetInteger("TiltInt", 0);
            riderAnimator.SetInteger("TiltInt", 0);
            if (tiltingAngle <= -23.0f)
            {
                TiltFalling();
            }
            if (Horz >= 0.3f)  
            {
                tilt = 1;
                unicycleAnimator.SetInteger("TiltInt", 1);
                unicycleAnimator.SetFloat("AnimMultiplier", 1.0f);
                riderAnimator.SetInteger("TiltInt", 1);
                riderAnimator.SetFloat("AnimMultiplier", 1.0f);
                StartCoroutine("TiltRandom");
            }
            if (Horz <= -0.8f)
            {
                unicycleAnimator.SetFloat("AnimMultiplier", 1.8f); // ускоряем анимацию падения
                riderAnimator.SetFloat("AnimMultiplier", 1.8f);
            }
            if (Horz <= -0.3f)
            {
                unicycleAnimator.SetFloat("AnimMultiplier", 1.3f);
                riderAnimator.SetFloat("AnimMultiplier", 1.3f);
            }
        }
        if (tilt == 2) // аналогично описанному выше
        {
            
            StopCoroutine("TiltRandom");
            isTilt = false;
            unicycleAnimator.SetInteger("TiltInt", 2);
            riderAnimator.SetInteger("TiltInt", 2);
            if (tiltingAngle >= 23.0f)
            {
                TiltFalling();
            }
            if (Horz <= -0.3f)
            {
                tilt = 1;
                unicycleAnimator.SetInteger("TiltInt", 1);
                unicycleAnimator.SetFloat("AnimMultiplier", 1.0f);
                riderAnimator.SetInteger("TiltInt", 1);
                riderAnimator.SetFloat("AnimMultiplier", 1.0f);
                StartCoroutine("TiltRandom");
            }
            if (Horz >= 0.8f)
            {
                unicycleAnimator.SetFloat("AnimMultiplier", 1.8f);
                riderAnimator.SetFloat("AnimMultiplier", 1.8f);
            }
            if (Horz >= 0.3f)
            {
                unicycleAnimator.SetFloat("AnimMultiplier", 1.3f);
                riderAnimator.SetFloat("AnimMultiplier", 1.3f);
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Box")
        {
            _audio.pitch = (Random.Range(0.8f, 1.2f));
            _audio.PlayOneShot(woodHit);
            unicycleRigidbody.constraints = RigidbodyConstraints.None;
            ragDollScript.RagdollModeOn();
            driveMode = 0;
            FindObjectOfType<AudioManager>().Play("failSound");
            uiMenu.Loss();
        }
        if (other.gameObject.tag == "coin")
        {
            score++;
            _audio.pitch = (Random.Range(0.8f, 1.2f));
            _audio.PlayOneShot(coins);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "leftCurb")
        {
            unicycleAnimator.SetInteger("TiltInt", 2);
            unicycleAnimator.SetFloat("AnimMultiplier", 2.0f);
            riderAnimator.SetInteger("TiltInt", 2);
            riderAnimator.SetFloat("AnimMultiplier", 2.0f);
            driveMode = -1;
            FindObjectOfType<AudioManager>().Play("failSound");
            uiMenu.Loss();
            StartCoroutine("RagDollActivator");
        }
        if (other.gameObject.tag == "rightCurb")
        {
            unicycleAnimator.SetInteger("TiltInt", 0);
            unicycleAnimator.SetFloat("AnimMultiplier", 2.0f);
            riderAnimator.SetInteger("TiltInt", 2);
            riderAnimator.SetFloat("AnimMultiplier", 2.0f);
            driveMode = -1;
            FindObjectOfType<AudioManager>().Play("failSound");
            uiMenu.Loss();
            StartCoroutine("RagDollActivator");
        }
    }
    void RandomNumber()   //генератор случайных чисел
    {
        if (isTilt == true)
        {
            tilt = Random.Range(0, 3);
        }
    }
    void TiltFalling()
    {
        Horz = 0;
        unicycleRigidbody.constraints = RigidbodyConstraints.None;
        ragDollScript.RagdollModeOn();
        driveMode = -1;
        FindObjectOfType<AudioManager>().Play("failSound");
        uiMenu.Loss();
    }
    IEnumerator TiltRandom()
    {
        yield return new WaitForSeconds(Random.Range(3.3f, 5.4f) * Random.Range(0.8f, 1.5f)); 
        isTilt = true;
        yield break;
    }
    IEnumerator RagDollActivator()
    {
        yield return new WaitForSeconds(1.5f);
        ragDollScript.RagdollModeOn();
    }
    
}
