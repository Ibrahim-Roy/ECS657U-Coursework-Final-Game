using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Campaign : MonoBehaviour
{
    public Text dialogBox;


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
            }
        }
    }

    private IEnumerator cutscene1()
    {
        running = true;
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
        running = false;
        player.setDestinationPosition(new Vector2(-1.4f, -4.5f));
        Destroy(gameObject);
    }

}
