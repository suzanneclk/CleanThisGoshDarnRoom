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
    [SerializeField] private AudioSource pickUpNoise;
    [SerializeField] private AudioSource placeNoise;
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private Transform transform;
    private bool flag;
    private bool equipped;
    private static bool slotFull;
    float startX;
    float startY;
    float startZ;
    private Vector3 distanceToPlayer;
    //   [SerializeField] private float place distance;
    void Start()
    {
        pickUpRange = 3f;
        x = 1f;
        rb.isKinematic = false;
        coll.isTrigger = false;
        //spawn at random point in the room
        startX = Random.Range(-7f, 7f);
        startY = Random.Range(0f, 6f);
        startZ = Random.Range(-2f, 11f);
        transform.position = new Vector3(startX,startY,startZ);
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && _input.pickUp && !slotFull){
            PickUp();
            _input.pickUp = false;
        }
        /*
        if(equipped && _input.place){
            Place();
            _input.place = false;
        }*/
    }
    private void PickUp(){
       pickUpNoise.Play();
        equipped = true;
        slotFull = true;
        rb.isKinematic = true;
        coll.isTrigger = true;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    private void Place(){
        placeNoise.Play();
        equipped = false;
        slotFull = false;
        rb.isKinematic = false;
        coll.isTrigger = false;
        transform.SetParent(null);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + x);
    }
    //collision function
    void OnCollisionEnter(Collision c){
        //if tag matches and object is above furniture, clean meter goes up
        if((c.gameObject.tag == this.tag) && (transform.position.y >c.gameObject.transform.position.y) && flag){
            gameManager.instance.UpdateCleanMeter(1);
            flag = false;
        }
        else{
            gameManager.UpdateCleanMeter(-1);
            flag = true;
        }
        // if the object is touching and above the trash, destroy the object and increas the trash meter by 1
        if((c.gameObject.tag == "Trash") && (transform.position.y >c.gameObject.transform.position.y)){
            // destroy the object
            gameManager.UpdateTrashMeter();
        }
    }}
   
