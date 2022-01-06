using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject alertTextContainer;
    public GameObject alertTextBoxPrefab;
    public GameObject craftingRecipeDisplay;
    public Slider healthBar;
    public Slider hungerBar;
    public Text woodText;
    public Text stoneText;
    public Text rawFishText;
    public Text rawMeatText;
    public Text arrowsText;

    public void setMaxHealthBarValue(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        setHealth(maxHealth);
    }

    public void setMaxHungerBarValue(int maxHunger)
    {
        hungerBar.maxValue = maxHunger;
        setHunger(maxHunger);
    }

    public void updateHUD(string HUDComponent, int value)
    {
        if(HUDComponent == "Health")
        {
            setHealth(value);
        }
        else if(HUDComponent == "Hunger")
        {
            setHunger(value);
        }
        else if(HUDComponent == "Wood")
        {
            setText(woodText, value);
        }
        else if(HUDComponent == "Stone")
        {
            setText(stoneText, value);
        }
        else if(HUDComponent == "Raw Fish")
        {
            setText(rawFishText, value);
        }
        else if(HUDComponent == "Raw Meat")
        {
            setText(rawMeatText, value);
        }
        else if(HUDComponent == "Arrows")
        {
            setText(arrowsText, value);
        }
    }

    public void alert(string alertText)
    {
        var newTextBox = Instantiate(alertTextBoxPrefab, new Vector3(0,0,0), Quaternion.identity);
        newTextBox.transform.SetParent(alertTextContainer.transform, false);
        newTextBox.GetComponentInChildren<Text>().text = alertText;
        Destroy(newTextBox, 1);
    }

    public void showCraftingRecipe(string recipeName)
    {
        craftingRecipeDisplay.SetActive(true);
        string recipeText = "Recipe:\n";
        if(recipeName == "Arrow")
        {
            recipeText += "1x Wood\n1x Stone";
        }
        craftingRecipeDisplay.GetComponentInChildren<Text>().text = recipeText;
    }

    public void hideCraftingRecipe(){
        craftingRecipeDisplay.SetActive(false);
    }

    private void setText(Text textbox, string text)
    {
        textbox.text = text;
    }

    private void setText(Text textbox, int text)
    {
        textbox.text = text.ToString();
    }

    private void setHealth(int health)
    {
        healthBar.value = health;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.red;
    }

    private void setHunger(int hunger)
    {
        hungerBar.value = hunger;
        hungerBar.fillRect.GetComponentInChildren<Image>().color = new Color(254,184,0,255);
    }
}
