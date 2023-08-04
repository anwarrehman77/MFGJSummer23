using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DetectRamps : MonoBehaviour
{
    public float launchForce = 50f;
    public int scoreMultiplier = 1;

    [SerializeField]
    GameObject gameManagerObject;
    [SerializeField]
    CinemachineVirtualCamera cam;

    private Rigidbody2D rb2d;
    private PlayerMovement playerMovement;
    private Trick playerTrick;
    private BoxCollider2D wheelsCollider;
    private PlayerHealth playerHealth;

    private float targetYPos;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerTrick = GetComponent<Trick>();
        wheelsCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ramps")
        {
            StartCoroutine(TakeOff());
        }

        if (col.gameObject.tag == "Correct")
        {
            StartCoroutine(ScoreBoost(2, 5f));
        }
    }

    void CheckRot()
    {
        if ((transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 60) || (transform.eulerAngles.z >= 300 && transform.eulerAngles.z <= 359.9999f)) transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else playerHealth.Die();
    }

    IEnumerator TakeOff()
    {
        float takeOffTime = Time.time;

        rb2d.velocity = Vector2.zero; // Can delete this line to conserve current momentum
        targetYPos = transform.position.y;
        
        playerMovement.enabled = false;
        wheelsCollider.enabled = false;
        playerTrick.enabled = true;
        
        rb2d.gravityScale = 1f;
        
        rb2d.AddForce(new Vector2(1f, 1f) * launchForce, ForceMode2D.Impulse);

        StartCoroutine(ChangeFOV(cam, 30f, 0.3f));
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;

        yield return new WaitUntil(() => (transform.position.y <= targetYPos) && (Time.time > takeOffTime + 0.02f));

        StartCoroutine(ChangeFOV(cam, 60f, 0.3f));
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;

        StartCoroutine(gameManagerObject.GetComponent<GameManager>().SetScoreText(playerTrick.rotations * 100 * scoreMultiplier, 0.5f));
        
        rb2d.gravityScale = 0f;
        playerTrick.rotations = 0;

        playerMovement.enabled = true;
        playerTrick.enabled = false;
        wheelsCollider.enabled = true;

        CheckRot();
    }

    IEnumerator ChangeFOV(CinemachineVirtualCamera cam, float endFOV, float duration)
    {
        float startFOV = cam.m_Lens.FieldOfView;
        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, endFOV, timeElapsed / duration);
            yield return null;
            timeElapsed += Time.deltaTime;
        }
    }

    IEnumerator ScoreBoost(int multiplyFactor, float duration)
    {
        scoreMultiplier *= multiplyFactor;
        yield return new WaitForSeconds(duration);
        scoreMultiplier /= multiplyFactor;
    }
}
