using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureHuntManager : MonoBehaviour
{
    // These are UX variables used in slowing down the start of the game and giving a text tutorial
    bool countdownStarted = false;
    float startTimer = 0.0f;
    float startTime = 15.0f;
    float countDownMod = 0.0f;

    [SerializeField] GameObject tutorialPanel;

    public float hunterSpeed = 5.0f;

    public Transform rightMostCameraBound;
    public Transform leftMostCameraBound;

    public GameObject playerView;
    public GameObject kyodaiClaw;
    public Animator kyodaiClawAnimator;

    bool ended = false;
    float endedTime = 5.0f;
    float endedTimer = 0.0f;

    float gametimer = 0.0f;
    float gameTime = 15.0f;

    bool attacking = false;
    Vector3 kyodaiClawBackPosition = new Vector3(-.0183f, -.1846f, .1202f);
    Vector3 kyodaiClawAttackackPosition = new Vector3(-.1183f, .024f, .42147f);

    [SerializeField] GameObject timeLeftBar;
    [SerializeField] Text scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayDastardlySchemeMusic();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ended)
        {
            endedTimer += Time.deltaTime;
            if(endedTimer > endedTime)
            {
                SceneChanger.instance.LoadGameScene();
            }
        }
        else
        {
            if (countdownStarted)
            {
                RunStartSequence();
                gametimer += Time.deltaTime * 1.5f * countDownMod;
                if (gametimer > gameTime)
                {
                    //SceneChanger.instance.LoadGameScene();
                    ended = true;
                    AudioManager.instance.PlayHeavyDrumBeatsSFX();
                }
                timeLeftBar.transform.localScale = new Vector3(1 - (gametimer / gameTime), 1, 1);
            }
            else
            {
                RunStartSequence();
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerView.transform.position = Vector3.MoveTowards(playerView.transform.position, leftMostCameraBound.position, hunterSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerView.transform.position = Vector3.MoveTowards(playerView.transform.position, rightMostCameraBound.position, hunterSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (!attacking)
            {
                kyodaiClawAnimator.SetTrigger("Attack");
                attacking = true;

                int reeowRng = Random.Range(0, 15);
                if(reeowRng == 5) AudioManager.instance.PlayReeowSFX();
            }
        }
        if (attacking)
        {
            kyodaiClawAnimator.transform.localPosition = Vector3.MoveTowards(kyodaiClawAnimator.transform.localPosition, kyodaiClawAttackackPosition, Time.deltaTime*1.25f);
            if (kyodaiClawAnimator.transform.localPosition == kyodaiClawAttackackPosition) attacking = false;
        } else
        {
            kyodaiClawAnimator.transform.localPosition = Vector3.MoveTowards(kyodaiClawAnimator.transform.localPosition, kyodaiClawBackPosition, Time.deltaTime);
        }
    }

    public void HitFeet()
    {
        gametimer -= 2.0f;
        if (gametimer < 0) gametimer = 0;
        AudioManager.instance.PlayOwSFX();
        score++;
        scoreText.text = score.ToString();
    }

    void RunStartSequence()
    {
        if (Input.anyKey)
        {
            countdownStarted = true;
        }
        if (countdownStarted && (startTime != startTimer))
        {
            startTimer += Time.deltaTime;
            countDownMod = (startTimer / startTime);
            // steadily hide the UI Elements
            if(startTimer >= startTime)
            {
                startTimer = startTime;
                tutorialPanel.SetActive(false);
                countDownMod = 1;
            }
        }
    }
}
