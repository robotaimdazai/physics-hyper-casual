using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public Character[] Characters;
    [SerializeField] Button unlockButton;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI nameText;

    int characterIndex = 0;
    Character selectedCharacter;

    
    void Start()
    {
        Character[] loadedCharacterData = DataSaver.LoadCharactersData();
        if (loadedCharacterData!=null)
        {
            Characters = loadedCharacterData;
        }
        selectedCharacter = Characters[characterIndex];
        ChangeCharacter(selectedCharacter);

        
    }

    void Update()
    {

        if (selectedCharacter!=null)
        {
            if (selectedCharacter.Unlocked)
            {
                unlockButton.gameObject.SetActive(false);
                return;
            }

            unlockButton.gameObject.SetActive(true);

            if (CrownManager.Instance.CrownCount>=selectedCharacter.Price)
            {
                unlockButton.interactable = true;
            }
            else
            {
                unlockButton.interactable = false;
            }
        }
    }

    public void Next()
    {
        characterIndex++;

        if (characterIndex > Characters.Length-1)
        {
            characterIndex = 0;
        }

        selectedCharacter = Characters[characterIndex];
        ChangeCharacter(selectedCharacter);
        priceText.text = selectedCharacter.Price.ToString();
    }

    public void Previous()
    {
         characterIndex--;

        if (characterIndex < 0 )
        {
            characterIndex = Characters.Length-1;
        }

        selectedCharacter = Characters[characterIndex];
        ChangeCharacter(selectedCharacter);
        
    }

    private void ChangeCharacter(Character character)
    {
        if (character == null)
        {
            return;
        }

        if (characterImage)
        {
            characterImage.sprite = Utility.LoadSprite(character.Name);
        }
        if (priceText)
        {
            priceText.text = character.Price.ToString();
        }
        if (nameText)
        {
            nameText.text = character.Name;
        }

        //changing player skin
        if (character.Unlocked)
        {
            Sprite sprite = Utility.LoadSprite(character.Name);
            Player.Instance.ChangeSkin(sprite);
        }
        
    }

    public void Unlock()
    {
        if (selectedCharacter == null)
        {
            return;
        }
        if (selectedCharacter.Price>CrownManager.Instance.CrownCount)
        {
            return;
        }

        Debug.Log("Purchased");
        selectedCharacter.Unlocked = true;
        CrownManager.Instance.CrownCount-=selectedCharacter.Price;

        //changing player skin
        Sprite sprite = Utility.LoadSprite(selectedCharacter.Name);
        Player.Instance.ChangeSkin(sprite);
    }

    
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            DataSaver.SaveCharactersData(Characters);
        }
    }

    private void OnApplicationQuit() 
    {
        DataSaver.SaveCharactersData(Characters);
    }
}
