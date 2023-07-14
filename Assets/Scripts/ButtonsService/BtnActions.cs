using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BtnListenerAction
{
    ShowSettings,
}
public class BtnActions : MonoBehaviour
{
    #region
    public delegate void Operation();
    public delegate void Operation<T>(T val);
    public static Dictionary<BtnListenerAction, Operation> NoneActs = new Dictionary<BtnListenerAction, Operation>();
    public static Dictionary<BtnListenerAction, Operation<int>> IntActs = new Dictionary<BtnListenerAction, Operation<int>>();
    public static Dictionary<BtnListenerAction, Operation<float>> FloatActs = new Dictionary<BtnListenerAction, Operation<float>>();
    public static Dictionary<BtnListenerAction, Operation<string>> StringActs = new Dictionary<BtnListenerAction, Operation<string>>();
    public static Dictionary<BtnListenerAction, Operation<bool>> BoolActs = new Dictionary<BtnListenerAction, Operation<bool>>();
    public static Dictionary<BtnListenerAction, Operation<Transform>> TransformActs = new Dictionary<BtnListenerAction, Operation<Transform>>();
    public static Dictionary<BtnListenerAction, Operation<Component>> ComponentActs = new Dictionary<BtnListenerAction, Operation<Component>>();
    public static Dictionary<BtnListenerAction, Operation<Button>> ButtonActs = new Dictionary<BtnListenerAction, Operation<Button>>();
    #endregion
    private void Awake()
    {
        BoolActs.Add(BtnListenerAction.ShowSettings, (e) => { if (!e) { ActionsService.ValuesUpdate.Invoke(); } UILinks.SettingsPanel.SetActive(e); });
    }
}
