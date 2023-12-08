using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class RoomObject : MonoBehaviour
{
// Start is called before the first frame update
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private Transform player, hand;
    [SerializeField] private float pickUpRange;
    [SerializeField] private float x;
    [SerializeField] private AudioClip pickUpNoise;
    [SerializeField] private AudioClip placeNoise;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundEffectManager sound;
    //[SerializeField] private Transform transform;
    private bool flag;
    private bool equipped;
    private static bool slotFull;
    float startX;
    float startY;
    float startZ;
    private Vector3 distanceToPlayer;
    private string beforeTag;
    //   [SerializeField] private float place distance;
    void Awake(){
        coll = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Transform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sound = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundEffectManager>();
    }
    void Start()
    {
        gameManager.UpdateMaxClean();
        gameManager.UpdateMaxTrash();
        pickUpRange = 2f;
        x = 0.5f;
        rb.isKinematic = false;
        coll.isTrigger = false;

        //spawn at random point in the room
       // startX = Random.Range(0f, 9f);
        //startY = Random.Range(5f, 6f);
        //startZ = Random.Range(3f, 12f);
        //transform.position = new Vector3(startX,startY,startZ);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && _input.pickUp && !slotFull){
            PickUp();
            _input.pickUp = false;
        }
        if(equipped && _input.place){
            Place();
            _input.place = false;
        }
    }
    private void PickUp(){
        equipped = true;
        slotFull = true;
        rb.isKinematic = true;
        coll.isTrigger = true;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        sound.PlaySound(pickUpNoise);
    }
    private void Place(){
        equipped = false;
        slotFull = false;
        rb.isKinematic = false;
        coll.isTrigger = false;
        transform.SetParent(null);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + x);
        sound.PlaySound(placeNoise);
    }
    //collision function
    void OnCollisionEnter(Collision c){
        // need a metric to see if object has been moved
         if(c.gameObject.tag == "Closet"){

            if(gameObject.tag != "Closet"){
                gameManager.UpdateCleanMeter(.5f);
            }
            else{
                gameManager.UpdateCleanMeter(1);
            }
            Destroy(gameObject);
        }
        // if the object is touching and above the trash, destroy the object and increas the trash meter by 1
        if(c.gameObject.tag == "Trash"){
            // destroy the object
            if(tag == "Trash"){ gameManager.UpdateCleanMeter(1);}
            Destroy(gameObject);
            gameManager.UpdateTrashMeter();
        }
        if((c.gameObject.tag != beforeTag) && (c.gameObject.tag != "Player") && !((tag == "Trash")||(tag == "Closet"))) { //check to see if object "moved"

            if(c.gameObject.tag == tag){ // check if object is in right place
            gameManager.UpdateCleanMeter(1);
            } 
            else if (beforeTag == tag){
            gameManager.UpdateCleanMeter(-1);
            }
        }
        beforeTag = c.gameObject.tag;
        }
    }
   
