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
    public GameObject tools;
    public GameObject enemyDependent;

    private GameMasterMainWorld gameMaster;
    private Player player;
    private PlayableDirector cutscene;
    [SerializeField] private int campaignSequenceNumber;
    private bool running = false;
    private HostileNPC enemyDependentScript;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("Game Master").GetComponent<GameMasterMainWorld>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cutscene = GetComponent<PlayableDirector>();
        if(enemyDependent != null)
        {
            enemyDependentScript = enemyDependent.GetComponent<HostileNPC>();
        }
    }

    private void Update()
    {
        if(enemyDependentScript != null)
        {
            if(!enemyDependentScript.getAliveStatus())
            {
                if(campaignSequenceNumber == 4)
                {
                    if(!running)
                    {
                        StartCoroutine(cutscene4());
                    }
                }
            }
        }
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
                    gameMaster.setPlayerPosition(new Vector2(-16f,-13.5f));
                    SceneManager.LoadScene("Cave Scene");
                    gameMaster.incrementProgressCounter();
                    savePlayerState();
                    Destroy(gameObject);
                }
                else if(campaignSequenceNumber == 2)
                {
                    if(!running)
                    {
                        StartCoroutine(cutscene2());
                    }
                }
                else if(campaignSequenceNumber == 3)
                {
                    if(!running)
                    {
                        StartCoroutine(cutscene3());
                    }
                }
                else if(campaignSequenceNumber == 5)
                {
                    gameMaster.setPlayerPosition(new Vector2(-6.5f, 21.5f));
                    SceneManager.LoadScene("Main World");
                    gameMaster.incrementProgressCounter();
                    savePlayerState();
                    Destroy(gameObject);
                }
                else if(campaignSequenceNumber == 6)
                {
                    if(!running)
                    {
                        StartCoroutine(cutscene5());
                    }
                }
            }
        }
    }

    private void savePlayerState()
    {
        gameMaster.incrementProgressCounter();
        gameMaster.setPlayerPosition(player.getPosition());
        gameMaster.setPlayerDestinationPosition(player.getDestinationPosition());
        gameMaster.setEquippedItem(player.getEquippedItem());
        gameMaster.setHealth(player.getHealth());
        gameMaster.setHunger(player.getHunger());
        gameMaster.setWood(player.getWood());
        gameMaster.setStone(player.getStone());
        gameMaster.setRawFish(player.getRawFish());
        gameMaster.setRawMeat(player.getRawMeat());
        gameMaster.setArrows(player.getArrows());
        gameMaster.setFish(player.getFish());
        gameMaster.setMeat(player.getMeat());
        gameMaster.updateCraftFire(false);
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
        tutorialBox.text = "-Use WASD or Arrow keys to move.\n-The player faces towards the direction of your mouse cursor.\n-Follow the compass on the top right of the screen to go to the next objective.";
        yield return new WaitForSecondsRealtime(5f);
        player.setDestinationPosition(new Vector2(-6.5f, 21.5f));
        savePlayerState();
        running = false;
        Destroy(gameObject);
    }

    private IEnumerator cutscene2()
    {
        running = true;
        tools.SetActive(true);
        cutscene.Play();
        dialogBox.text = "Should be safe from the wolves here for a while.";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "Huh, those tools look like they are in good condition.";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "I should pick them up, they should help me survive this place, whatever it is.";
        tutorialBox.text = "-To pick up items in the world walk over them.";
        yield return new WaitForSecondsRealtime(3f);
        player.setDestinationPosition(new Vector2(-6.5f, 21.5f));
        savePlayerState();
        running = false;
        Destroy(gameObject);
    }

    private IEnumerator cutscene3()
    {
        running = true;
        tools.SetActive(false);
        player.setArrows(3);
        cutscene.Play();
        dialogBox.text = "How dare you touch my stuff";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "Now you must die";
        tutorialBox.text = "-To equip the bow click the button on the right side of the HUD.\n-You can also use key 1 if shortcut keys are enabled in settings.\n-Aim the bow with your mouse and shoot with left mouseclick.\n-Keep an eye on your arrows displayed bottom left on the HUD.\nStay away from melee enemies to avoid taking damage.\n-Enemy health is displayed on top of them when they take damage.";
        yield return new WaitForSecondsRealtime(3f);
        player.setDestinationPosition(new Vector2(-6.5f, 21.5f));
        savePlayerState();
        running = false;
        Destroy(gameObject);
    }

    private IEnumerator cutscene4()
    {
        running = true;
        cutscene.Play();
        dialogBox.text = "What kind of world is this";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "Skeletons coming to life and all";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "I need to find some people and figure out how to get out of this place";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "But first I'm starving, I need to get some food!\nI should get out of this cave";
        tutorialBox.text = "-The player has basic hunger requirements that you need to satisfy by eating meat and fish\n-Meat can be hunted from animals and fish can be caught from rivers";
        yield return new WaitForSecondsRealtime(3f);
        player.setDestinationPosition(new Vector2(-16f,-13.3f));
        savePlayerState();
        running = false;
        Destroy(gameObject);
    }

    private IEnumerator cutscene5()
    {
        running = true;
        cutscene.Play();
        dialogBox.text = "Huh, what's that paper flying in the air";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "It's coming from the same direction I woke up at";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "Maybe it followed the same path into this world as me\nI should check it out";
        yield return new WaitForSecondsRealtime(2f);
        dialogBox.text = "Hmm what does it say...\n\"O Traveller! You must find the willow tree if you wish to ever see your world again!\"";
        yield return new WaitForSecondsRealtime(4f);
        dialogBox.text = "What!\nIs this for me!";
        yield return new WaitForSecondsRealtime(1f);
        dialogBox.text = "I must find this willow tree!";
        yield return new WaitForSecondsRealtime(1f);
        dialogBox.text = "But first I'm starving\nI should find some food before hunger starts to take a toll on my health";
        yield return new WaitForSecondsRealtime(3f);
        dialogBox.text = "Was that a sheeps sound?\nIt sounded like it came from the east";
        yield return new WaitForSecondsRealtime(3f);
        dialogBox.text = "I should craft some arrows and hunt that sheep";
        tutorialBox.text = "-Consumable objects can be crafted from different resources\n-Wood and stone resources can be chopped and mined from trees and rocks respectively\n-To chop trees use the axe or to mine rocks use the pickaxe\n-The axe and pickaxe can be equipped by clicking on the right side of the HUD or by using the keys 3 and 4\n-Hover over the craft button of consumables to see their crafting recipes and click the button to craft them" ;
        yield return new WaitForSecondsRealtime(3f);
        savePlayerState();
        running = false;
        Destroy(gameObject);
    }
}
