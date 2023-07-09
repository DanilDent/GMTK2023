using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityImageManager : MonoBehaviour
{
    [SerializeField] private CityImageConfig config;

    [SerializeField] private Image backGround;
    [SerializeField] private Image npc;

    private Sprite neutralStatusBG;
    private Sprite wellBeingStatusBG;
    private Sprite declineStatusBG;
    private Sprite neutralStatusNPC;
    private Sprite wellBeingStatusNPC;

    // Start is called before the first frame update
    void Start()
    {
        neutralStatusNPC = config.NeutralStatusNPC;
        wellBeingStatusNPC = config.WellBeingStatusNPC;
        neutralStatusBG = config.NeutralStatusBG;
        wellBeingStatusBG = config.WellBeingStatusBG;
        declineStatusBG = config.DeclineStatusBG;

        EventService.Instance.CityStatusChange += OnCityStatus;
    }
    private void OnDestroy()
    {
        EventService.Instance.CityStatusChange -= OnCityStatus;
    }

    private void OnCityStatus(int newStatus)
    {
        switch (newStatus)
        {
            case -1:
                {
                    backGround.sprite = declineStatusBG;
                    //npc.sprite = neutralStatusNPC;
                    break;
                }
            case 0:
                {
                    backGround.sprite = neutralStatusBG;
                    //npc.sprite = neutralStatusNPC;
                    break;
                }
            case 1:
                {
                    backGround.sprite = wellBeingStatusBG;
                    //npc.sprite = wellBeingStatusNPC;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}

[CreateAssetMenu(fileName = "City Image Config", menuName = "Configs/CityImageConfig")]
public class CityImageConfig : ScriptableObject
{
    [SerializeField] private Sprite neutralStatusBG;
    [SerializeField] private Sprite wellBeingStatusBG;
    [SerializeField] private Sprite declineStatusBG;
    [SerializeField] private Sprite neutralStatusNPC;
    [SerializeField] private Sprite wellBeingStatusNPC;

    public Sprite NeutralStatusBG => neutralStatusBG;
    public Sprite WellBeingStatusBG => wellBeingStatusBG;
    public Sprite DeclineStatusBG => declineStatusBG;
    public Sprite NeutralStatusNPC => neutralStatusNPC;
    public Sprite WellBeingStatusNPC => wellBeingStatusNPC;
}
