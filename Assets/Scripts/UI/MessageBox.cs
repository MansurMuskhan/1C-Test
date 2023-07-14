using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private static MessageBox i;

    [SerializeField] GameObject _body;
    [SerializeField] Text _messageText;
    [SerializeField] Button _button;
    [SerializeField] Text _buttonText;

    private void Awake()
    {
        if (!i) i = this; else Destroy(this);
    }

    public static void Show(string message, Action btnAct, string btnText = "Ok")
    {
        i._messageText.text = message;
        i._buttonText.text = btnText;
        i._button.onClick.AddListener(() => { btnAct.Invoke(); i.Quit(); });
        i._body.SetActive(true);
    }

    void Quit()
    {
        _messageText.text = "";
        _buttonText.text = "";
        _button.onClick.RemoveAllListeners();
        _body.SetActive(false);
    }
}
