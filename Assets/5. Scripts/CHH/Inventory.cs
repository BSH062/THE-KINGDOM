using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Variable

    public string[] items ;
    public Image[] inventoryItemImg;
    public Sprite basicSprite;
    public Text[] inventoryItemTxt; //아이템 텍스트 명

    #endregion Variable

    #region Unity Method

    private void Awake()
    {

    }

    private void Start()
    {
        
    }


    #endregion Unity Method

    #region Method

    public void BtnInventoryItemClick()
    {
        EventSystem.current.currentSelectedGameObject.transform.Find("Image").gameObject.GetComponent<Image>().sprite = null;
        string selectItemName = EventSystem.current.currentSelectedGameObject.transform.Find("Text (Legacy)").gameObject.GetComponent<Text>().text;

        for(int i=0;i<items.Length;i++)
        {
            if (items[i] == selectItemName)
            {
                items[i] = null;
                inventoryItemTxt[i].text = null;
                inventoryItemImg[i].sprite = basicSprite;
                break;
            }
        }
    }
    #endregion Method
}
