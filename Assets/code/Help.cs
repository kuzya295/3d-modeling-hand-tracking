using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Help : MonoBehaviour
{
    public void Start()
    {
        background.GetComponent<Image>().transform.SetAsFirstSibling();
    }
    public GameObject help, background, exit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x >= 0 && Input.mousePosition.x <= 160 && Input.mousePosition.y >= 658 && Input.mousePosition.y <= 707)
        {
            background.GetComponent<Image>().color = new Color(1, 1, 1, (float)0.8);
            help.GetComponent<Text>().text = "1.Изменение формы.\nЧтобы изменить форму объекта, поднесите к поверхности объекта правую руку, соедините указательный и большой пальцы, а затем, не изменяя жеста, перемещайте руку в пространстве в то положение, в котором должна оказаться выбранная точка для деформации сетки объекта. После разъедините пальцы.\n\n2.Масштабирование.\nЧтобыуменьшить/увеличить объект, поместите обе руки в область видимости контроллера, сожмите кулаки и уменьшайте/увеличивайте расстояние между ними. После разожмите кулаки.\n\n3.Перемещение.\nЧтобы переместить объект, поместите левую руку в центр объекта, сожмите ее в кулак и перемещайте руку в пространстве. После разожмите кулак.\n\n4.Вращение.\nЧтобы вращать объект, поместите правую руку в область видимости контроллера, соедините указательный и большой пальцы и перемещайте их в пространстве. После разъедините пальцы.";
            exit.GetComponent<Image>().enabled = true;
        }
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x >= 1244 && Input.mousePosition.x <= 1295 && Input.mousePosition.y >= 577 && Input.mousePosition.y <= 628 && exit.GetComponent<Image>().enabled == true)
        {
            background.GetComponent<Image>().color = new Color(1, 1, 1, (float)0.0);
            help.GetComponent<Text>().text = "";
            exit.GetComponent<Image>().enabled = false;
        }
    }
}
