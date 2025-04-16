using UnityEngine;
using MoreMountains;
using MoreMountains.TopDownEngine;
public class Ui_HealthUpdater : MonoBehaviour
{
    public Transform RedHeartContainer;
    public GameObject RedHearth_Pref;

    public Transform GrayHearthContainer;
    public GameObject GrayHearthHearth_Pref;

    public Transform HollowHeartContainer;
    public GameObject HollowHearth_Pref;

    public Health CharacterHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterHealth.OnHit += OnHit;

        for (int i = 0; i < CharacterHealth.CurrentHealth; i++)
        {
            Instantiate(GrayHearthHearth_Pref, GrayHearthContainer);
            Instantiate(HollowHearth_Pref, HollowHeartContainer);
            Instantiate(RedHearth_Pref, RedHeartContainer);
        }
    }

    public void OnHit()
    {
        Destroy(RedHeartContainer.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
