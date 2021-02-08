using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    //Health and Level
    [SerializeField] private Text health, level;
    [SerializeField] private Image healthFill, levelFill;
    [SerializeField] private Player player;

    //Stats
    private List<Text> playerStatTexts = new List<Text>();
    [SerializeField] private Text playerStatPrefab;
    [SerializeField] private Transform playerStatPanel;

    //Equipped Weapon
    [SerializeField] private Sprite defaultWeaponSprite;
    [SerializeField] private Text weaponStatPrefab;
    [SerializeField] private Transform weaponStatPanel;
    [SerializeField] private Text weaponNameText;
    [SerializeField] private Image weaponIcon;
    private PlayerWeaponController playerWeaponController;
    private List<Text> weaponStatTexts = new List<Text>();

    void Start()
    {
        playerWeaponController = player.GetComponent<PlayerWeaponController>();
        UIEventHandler.OnPlayerHealthChanged += UpdateHealth;
        UIEventHandler.OnStatsChanged += UpdateStats;
        UIEventHandler.OnItemEquipped += UpdateEquippedWeapon;
        UIEventHandler.OnPlayerLevelChange += UpdateLevel;

        Initialize();
    }

    private void Initialize()
    {
        UpdateHealth(player.currentHealth, player.maxHealth);
        UpdateLevel();
        InitializeStats();
    }

    void UpdateHealth(int currentHealth, int maxHealth)
    {
        health.text = currentHealth.ToString();
        healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    void UpdateLevel()
    {
        level.text = player.playerLevel.Level.ToString();
        levelFill.fillAmount = (float)player.playerLevel.CurrentExperience / (float)player.playerLevel.RequiredExperience;
    }

    void InitializeStats()
    {
        for (int i = 0; i < player.CharacterStats.stats.Count; i++)
        {
            playerStatTexts.Add(Instantiate(playerStatPrefab));
            playerStatTexts[i].transform.SetParent(playerStatPanel);
        }
        UpdateStats();
    }

    void UpdateStats()
    {
        for (int i = 0; i < player.CharacterStats.stats.Count; i++)
        {
            playerStatTexts[i].text = player.CharacterStats.stats[i].StatName + ": " + player.CharacterStats.stats[i].GetCalculatedStatValue().ToString();
        }
    }

    void UpdateEquippedWeapon(Item item)
    {
        weaponNameText.text = item.ItemName;
        weaponIcon.sprite = Resources.Load<Sprite>("UI/Inventory/Icons/Items/" + item.ObjectSlug);

        for (int i = 0; i < weaponStatTexts.Count; i++)
        {
            Destroy(weaponStatTexts[i].gameObject);
        }
        
        weaponStatTexts.Clear();

        for (int i = 0; i < item.Stats.Count; i++)
        {
            weaponStatTexts.Add(Instantiate(weaponStatPrefab));
            weaponStatTexts[i].transform.SetParent(weaponStatPanel);
            weaponStatTexts[i].text = item.Stats[i].StatName + ": " + item.Stats[i].GetCalculatedStatValue().ToString();
        }
        Debug.Log(weaponStatTexts.Count);
    }

    public void UnequipWeapon()
    {
        if (weaponNameText.text != "Unarmed")
        {
            weaponNameText.text = "Unarmed";
            weaponIcon.sprite = defaultWeaponSprite;
            for (int i = 0; i < weaponStatTexts.Count; i++)
            {
                weaponStatTexts[i].text = "";
            }
            playerWeaponController.UnequipWeapon();
        }
        else
        {
            Debug.Log("You unarmed.");
        }
    }
}
