using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemList : MonoBehaviour
{
    //Variable to check the found object
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;
    public GameObject check4;
    public GameObject check5;
    public GameObject check6;
    public GameObject check7;

    //Timer Variable
    public Text[] timeText;
    float time = 120;
    int min, sec;
    int itemCount = 0;

    //Ending Variable
    public GameObject FailText;
    public GameObject SuccessText;
    public GameObject OverText;
    public GameObject ClearText;
    private bool failCheck = true;
    private bool successCheck = true;

    //Timer Variable2
    public GameObject timerText1;
    public GameObject timerText2;
    public GameObject timerText3;
    public GameObject itemPanel;

    //Fail Ending Variable
    public GameObject door;
    public Transform target;
    public GameObject mom;
    private bool momCheck;

    //sound
    public AudioSource play;
    public AudioSource failSound;
    public AudioSource successSound;

    void Start()
    {
        timeText[0].text = "02";
        timeText[1].text = "00";
        itemCount = 0;
        play.Play();
    }

    void Update()
    {
        //Time reduction
        time -= Time.deltaTime;

        min = (int)time / 60;
        sec = ((int)time - min * 60) % 60;

        //Set it to run only once and run when all objects are found
        if (itemCount >= 7 && successCheck)
        {
            play.Stop();
            successSound.Play();

            timerText1.SetActive(false);
            timerText2.SetActive(false);
            timerText3.SetActive(false);
            itemPanel.SetActive(false);

            StartCoroutine(Success());
            Invoke("SuccessEnding", 4.5f);   //show the ending text after 4.5 seconds
            successCheck = false;
        }

        //Timer
        if(min <= 0 && sec <= 0)
        {
            timeText[0].text = 0.ToString();
            timeText[1].text = 0.ToString();

            //If you don't find it even after the time has passed, run it (failure ending)
            if (itemCount < 7 && failCheck)
            {
                play.Stop();
                failSound.Play();

                timerText1.SetActive(false);
                timerText2.SetActive(false);
                timerText3.SetActive(false);
                itemPanel.SetActive(false);

                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z + 1);
                momCheck = true;
                StartCoroutine(Fail());
                Invoke("FailEnding", 4.5f);   //show the ending text after 4.5 seconds

                failCheck = false;
            }
        }
        else //Change the value displayed on the screen whenever the time keeps changing
        {
            if(sec >= 60)
            {
                min += 1;
                sec -= 60;
            }
            else
            {
                timeText[0].text = min.ToString();
                timeText[1].text = sec.ToString();
            }
        }
        if(momCheck) //Set the mother object to approach the player in the event of a game failure
        {
            if (Vector3.Distance(mom.transform.position, target.transform.position) >= 1.5f)
            {
                mom.transform.LookAt(target);
                Vector3 movement = new Vector3(0, 0, 2 * Time.deltaTime);
                mom.transform.Translate(movement);
            }
                
        }
    }

    //When you box an object that you need to find, the screen shows that the object has been found
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            if(collision.gameObject == GameObject.Find("doll1"))
            {
                check1.SetActive(true); 
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("doll2"))
            {
                check2.SetActive(true);
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("sword"))
            {
                check3.SetActive(true);
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("plant"))
            {
                check4.SetActive(true);
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("doll3"))
            {
                check5.SetActive(true);
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("lighter"))
            {
                check6.SetActive(true);
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }

            if (collision.gameObject == GameObject.Find("smartphone"))
            {
                check7.SetActive(true);
                collision.gameObject.tag = "Untagged";
                itemCount++;
            }
        }
    }

    void SuccessEnding()
    {
        ClearText.SetActive(true);
    }

    void FailEnding()
    {
        OverText.SetActive(true);
    }

    IEnumerator Success()
    {
        SuccessText.SetActive(true); //show the result text
        yield return new WaitForSeconds(4.0f);  //disable the result text after 4 seconds
        SuccessText.SetActive(false);
    }

    IEnumerator Fail()
    {
        FailText.SetActive(true); //show the result text
        yield return new WaitForSeconds(4.0f);  //disable the result text after 4 seconds
        FailText.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("TiTle");
    }
}
