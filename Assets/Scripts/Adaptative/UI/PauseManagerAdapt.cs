using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagerAdapt : MonoBehaviour
{
    private bool isPaused;
    public GameObject pauseMenu;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        UpdateInventory();
        SceneManager.LoadScene("MenuAdapt");
        Time.timeScale = 1f;
    }

    public void UpdateInventory()
    {
        int coin = 0, health = 0, energy = 0, key = 0, gem = 0;

        foreach (InventoryItem item in inventory.inventoryItems)
        {
            switch (item.itemName)
            {
                case "Coin":
                    coin = item.quantity;
                    break;
                case "Health Potion":
                    health = item.quantity;
                    break;
                case "Energy Potion":
                    energy = item.quantity;
                    break;
                case "Gem":
                    gem = item.quantity;
                    break;
                case "Key":
                    key = item.quantity;
                    break;
            }
        }

        InventoryData temp = new InventoryData(coin, health, energy, key, gem);
        DataBaseHandler.PostInventory(temp, AuthHandler.userId, () => { }, AuthHandler.idToken);
    }
}
