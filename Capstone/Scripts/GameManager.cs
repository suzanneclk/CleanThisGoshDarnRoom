using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameManager instance;
    private float timer;
    [SerializeField] private TMP_Text secondText;
    [SerializeField] private TMP_Text minuteText;
    [SerializeField] private Image cleanMeterImg;
    private float cleanMeter;
    private float trashMeter;  
    [SerializeField] private float maxClean;
    private float maxTrash;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource intenseMusic;
    // image for clean meter
    // Start is called before the first frame update
   private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
   void Start()
    {
             timer = 600f;
             cleanMeter = 0;
             trashMeter = 0;
             maxClean = 100;
             maxTrash = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //music.Play();
        UpdateTimer();
        //if the timer is less than 3, make the music intense
        /*if(timer <=3){
            music.Stop();
            intenseMusic.Play();
        } */

        if(timer <= 0) ChooseEnding();
    }
    private void UpdateTimer(){
        timer -= Time.deltaTime;
       minuteText.text = ((timer/60)-1).ToString("00");
        secondText.text = (timer%60).ToString("00");
    }
    public void UpdateCleanMeter(float updateAmount){
        cleanMeter += updateAmount;
        //Debug.Log(cleanMeter);
        //update the meter to be cleanMeter/maxClean

    }
    public void UpdateTrashMeter(){
        trashMeter++;
    }
    private void ChooseEnding(){
        if(trashMeter/maxTrash >= .5){
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
