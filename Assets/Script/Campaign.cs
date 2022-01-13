using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Campaign : MonoBehaviour
{
    public Text dialogBox;
    public Text tutorialBox;


    private GameMasterMainWorld gameMaster;
    private Player player;
    private PlayableDirector cutscene;
    [SerializeField] private int campaignSequenceNumber;
    private bool running = false;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GameMasterMainWorld>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cutscene = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        int progressCounter = gameMaster.getProgressCounter();
        if(other.gameObject.CompareTag("Player"))
        {
            if(progressCounter == campaignSequenceNumber)
            {
                if(campaignSequenceNumber == 0)
                {
                    if(!running)
                    {
                        StartCoroutine(cutscene1());
                    }
                }
                else if(campaignSequenceNumber == 1)
                {
                    if(!running)
                    {
                        SceneManager.LoadScene("Cave Scene");
                    }
                }
            }
        }
    }

    private IEnumerator cutscene1()
    {
        running = true;
        yield return new WaitForSecondsRealtime(2f);
        cutscene.Play();
        dialogBox.text = "Where am I?";
        yield return new WaitForSecondsRealtime(0.5f);
        dialogBox.text = "What is this place?";
        yield return new WaitForSecondsRealtime(1f);
        dialogBox.text = "I can't remember anything!";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "Is that a wolf howling!";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "I must find someplace to hide!";
        yield return new WaitForSecondsRealtime(3.5f);
        dialogBox.text = "That cave seems like a good place!";
        yield return new WaitForSecondsRealtime(5f);
        tutorialBox.text = "- Use WASD or Arrow keys to move\n- The player faces towards the direction of your mouse cursor\n- Follow the compass on the top right of the screen to go to the next objective";
        player.setDestinationPosition(new Vector2(-6.5f, 21.5f));
        gameMaster.incrementProgressCounter();
        running = false;
        Destroy(gameObject);
    }

}
