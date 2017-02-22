 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class menuShop : MonoBehaviour {


    public Vector4 info_menu_back;
    public Vector4 info_menu_elements;
    public Vector4 info_menu_drawModel;
    public Vector4 info_menu_nameCurItem;
    public Vector4 info_menu_descCurItem;
    public Vector4 info_menu_buyElements;
    public GUIStyle style_box;
    public GUIStyle style_elements;
    public GUIStyle style_select_elements;
    public GUIStyle style_boxItemButton;
    public GUIStyle style_boxItemName;
    public GUIStyle style_boxItemDesc;
    public List<itemShop> items = new List<itemShop>();
    public List<category> categories = new List<category>();
    public Transform modelPre;
    public Transform nullObj;

    private bool isOpen;

    void Initial ()
    {
        
        modelPre = GetComponent<viewItem>().CreateAreaModel(Engine.nullObj, 0);
        modelPre.GetComponent<viewManager>().model = modelPre.FindChild("model");
        modelPre.GetComponent<viewManager>().model.Rotate(270, 180, 0);
    }

    void removeInit()
    {
        GetComponent<viewItem>().RemoveAreaModel(modelPre.GetComponent<viewManager>().unique_id);
        modelPre = null;
    }

    void setModel (Transform model)
    {

        Quaternion prevAng = new Quaternion(0, 0, 0, 0);

        if (modelPre.GetComponent<viewManager>().model != null)
        {
            prevAng = modelPre.GetComponent<viewManager>().getAngleModel();
            modelPre.GetComponent<viewManager>().model.parent = null;
            Destroy(modelPre.GetComponent<viewManager>().model.gameObject);
        }

        modelPre.GetComponent<viewManager>().SetModel(model, new Vector3(0, 0, 0), Quaternion.identity);
        modelPre.GetComponent<viewManager>().rotate = true;
        modelPre.GetComponent<viewManager>().model = modelPre.FindChild("model");
        modelPre.GetComponent<viewManager>().setAngleModel(prevAng);
        if (curItem != null)
        {
            modelPre.GetComponent<viewManager>().setFOV((float)curItem.fov);

        }
    }

    void _turn()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            Initial();
        }
        else
        {
            removeInit();
            curCategory = "";
            curItem = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            _turn();
    }

    void Awake()
    {
        Engine.nullObj = nullObj;
        InitialCategories();
        InitialElements();

    }

    void InitialCategories()
    {
        addNewCategories("cpu", "Процессоры");
        addNewCategories("motherboard", "Материнские платы");
    }

    void InitialElements()
    {
        addNewItem("Intel Core i7", "some description",  "prefabs/corei7", "cpu", 0,3);
        addNewItem("MotherBoard", "some description вшщаоывшщоашщываощшыв вышоашщвыоащшывоащшыоващш вышщаоывшщаоывшщаощывш оывавщшоывшщаоывщшаоывщш ыошщаывощашоывщшаоы аощыаощывощывоащоывщ аывоващоывщоащывоащ ываоывощаоывщаоывща оыващоывщаоывщаоывщоащ ыоащывоащоыщваоывщвоа аощвыоащывоащывоащвыо аоывщаоывщаощывоащывоа аовщыаощывоащвыоащывоащовыщаоывщаоывщаощывоащывоащ ываощвыоащывоащы",  "prefabs/mainboard", "motherboard",0, 25);

    }

    void addNewCategories(string tName, string fName)
    {
        category cat = new category();
        cat.name = tName;
        cat.fine_name = fName;

        categories.Add(cat);
    }

    void addNewItem(string name, string desc, string prefabPath , string category, int cost , int ff)
    {
        itemShop item = new itemShop();
        item.name = name;
        item.desc = desc;
        item.prefabPath = prefabPath;
        item._cost = cost;
        item.ID = items.Count;
        item.category = category;
        item.fov = ff;

        items.Add(item);
    }

    public string curCategory = string.Empty;
    itemShop curItem = null;
    void OnGUI()
    {
        if (isOpen == false)
            return;

        GUI.Box(new Rect(Screen.width / info_menu_back.x, Screen.height / info_menu_back.y, Screen.width / info_menu_back.z, Screen.height / info_menu_back.w), "", style_box);

        if (curCategory == "")
        {

            for (int i = 0; i < categories.Count; i++)
            {
               bool bsel = GUI.Button(new Rect(Screen.width / info_menu_elements.x, (Screen.height / info_menu_elements.y) + i * (Screen.height / info_menu_elements.w) + (i*2), Screen.width / info_menu_elements.z, Screen.height / info_menu_elements.w), categories[i].fine_name, style_elements);
            
               if (bsel)
               {
                   curCategory = categories[i].name;
               }

            }

        } 
        else
        {

            int i = 0;
            foreach (itemShop item in items)
            {
                if (item.category != curCategory)
                    continue;

                bool bsel = GUI.Button(new Rect(Screen.width / info_menu_elements.x, (Screen.height / info_menu_elements.y) + i * (Screen.height / info_menu_elements.w) + (i * 2), Screen.width / info_menu_elements.z, Screen.height / info_menu_elements.w), item.name, style_elements);

                if (bsel)
                {
                    curItem = item;
                    setModel((Transform)Resources.Load(curItem.prefabPath,typeof (Transform)));
                }

                i++;
            }

            bool back = GUI.Button(new Rect(Screen.width / info_menu_elements.x, (Screen.height / info_menu_elements.y) + i * (Screen.height / info_menu_elements.w) + (i * 2), Screen.width / info_menu_elements.z, Screen.height / info_menu_elements.w), "Назад", style_elements);

            if (back)
            {
                curCategory = "";
                curItem = null;
                setModel(Engine.nullObj);
            }

        }



        if (curItem != null)
        {
            GUI.DrawTexture(new Rect(getR(info_menu_drawModel.x, true), getR(info_menu_drawModel.y, false), getR(info_menu_drawModel.z, true), getR(info_menu_drawModel.w,false)), modelPre.FindChild("Camera").GetComponent<Camera>().targetTexture);

            GUI.Box(new Rect(getR(info_menu_nameCurItem.x, true), getR(info_menu_nameCurItem.y, false), getR(info_menu_nameCurItem.z, true), getR(info_menu_nameCurItem.w, false)), curItem.name, style_boxItemName);
            GUI.Box(new Rect(getR(info_menu_descCurItem.x, true), getR(info_menu_descCurItem.y, false), getR(info_menu_descCurItem.z, true), getR(info_menu_descCurItem.w, false)), curItem.desc, style_boxItemDesc);

            bool buy = GUI.Button(new Rect(getR(info_menu_buyElements.x, true), getR(info_menu_buyElements.y, false), getR(info_menu_buyElements.z, true), getR(info_menu_buyElements.w, false)), "Купить", style_boxItemButton);



        }

    }


    float getR ( float w , bool isW)
    {
        if (isW)
            return Screen.width / w;
        else
            return Screen.height / w;
    }



}

[Serializable]
public class itemShop
{
    public int ID;
    public int _cost;
    public string name;
    public string desc;
    public string prefabPath;
    public string category;
    public int fov = 60;
}

[Serializable]
public class category
{
    public string name;
    public string fine_name;
}
