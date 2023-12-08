using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    //[SerializeField] private StarterAssetsInputs _input;
    private float timer;
    [SerializeField] private TMP_Text secondText;
    [SerializeField] private TMP_Text minuteText;
    [SerializeField] private Image cleanImage;
    [SerializeField] private SoundEffectManager sound;
    [SerializeField] private StarterAssetsInputs _input;
    private float cleanMeter;
    private float trashMeter;  
    private float maxClean = 0f;
    private float maxTrash = 0f;

    // image for clean meter
    // Start is called before the first frame update

   private void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
       // DontDestroyOnLoad(gameObject);
    sound = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundEffectManager>();
    _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
    }
   void Start()
    {
            // play music
             timer = 150f;
             trashMeter = 0;
             cleanMeter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        //Debug.Log(cleanMeter/maxClean);
        cleanImage.fillAmount = cleanMeter/maxClean;
        //Cheat codes
        if(_input.plusClean){
            UpdateCleanMeter(1);
            _input.plusClean = false;
        }
        if(_input.plusTrash){
            UpdateTrashMeter();
            _input.plusTrash = false;
        }
        if(_input.tenSeconds){
            timer = 10;
            _input.tenSeconds = false;
        }
        if(timer <= 0) {ChooseEnding();}
    }
    private void UpdateTimer(){
        timer -= Time.deltaTime;
       minuteText.text = (Mathf.Floor((timer/60))).ToString("00");
        secondText.text = (timer%60).ToString("00");
    }
    public void UpdateCleanMeter(float updateAmount){
        cleanMeter += updateAmount;


    }
    public void UpdateTrashMeter(){
        trashMeter++;
    }

    public void UpdateMaxTrash(){
        maxTrash++;
    }

    public void UpdateMaxClean(){
        maxClean++;
    }
    public void ChooseEnding(){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        if(trashMeter/maxTrash >= .9f){ //to be fixed
            //secret ending
            SceneManager.LoadScene(6);
        }
        else{
        if(cleanMeter/maxClean >= .9f){
            //good ending
            SceneManager.LoadScene(7);
        }
        else if(cleanMeter/maxClean >= .6f){
            //netural ending
            SceneManager.LoadScene(8);
        }
        else{
            //bad ending
            SceneManager.LoadScene(9);
        }}
    }
}