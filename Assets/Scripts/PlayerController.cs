using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    //I HAVE ADDED A BLACK SCREEN IT DOESN'T FEEL LIKE WINNING WITHOUT IT!!!
    public GameObject winBackdrop;
    //AND LOSING BECAUSE HOW CAN YOU WIN IF YOU CAN'T LOSE??
    public GameObject loseTextObject;
    public GameObject pointTextObject;
    public GameObject deathBarrier;
    public GameObject restartPrompt;
    public GameObject dialogue1;
    public GameObject dialogue2;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
 
        SetCountText();
        loseTextObject.SetActive(false);
        winTextObject.SetActive(false);
        winBackdrop.SetActive(false);
        pointTextObject.SetActive(false);
        restartPrompt.SetActive(false);
        dialogue1.SetActive(false);
        dialogue2.SetActive(false);
    }

    //*The Dialogue is very... special, because I'm not any good at coding :D
    void DialogueContinue()
    {
        dialogue1.SetActive(false);
        dialogue2.SetActive(true);
        Invoke("DialogueEnd", 4F);
    }

    void DialogueEnd()
    {
        dialogue2.SetActive(false);
    }

    void DisableText()
    {
        pointTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 17)
        {
            winTextObject.SetActive(true);
            winBackdrop.SetActive(true);
            deathBarrier.SetActive(false);
            restartPrompt.SetActive(true);
        }
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
       
    }
    void OnTriggerEnter(Collider other)
    {
        //THE GAME OBJECTS ARE THE CUBES at the moment at least :)
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            //This increases the count !!
            count = count + 1;

            SetCountText();

            pointTextObject.SetActive(true);
            //MAKE THE TEXT DISAPPEAR???
            Invoke("DisableText", 0.4F);
        }

        if (other.gameObject.CompareTag("Die"))
        {
            winBackdrop.SetActive(true);
            loseTextObject.SetActive(true);
            restartPrompt.SetActive(true);
        }

        if (other.gameObject.CompareTag("Hatred"))
        {
            dialogue1.SetActive(true);
            Invoke("DialogueContinue", 4F);
        }
    }
}
