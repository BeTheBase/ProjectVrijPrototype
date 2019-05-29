using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Did some ugly fixes, but it works for now ( needs cleaning ) 
public class ActiveAbilityHandler : MonoBehaviour
{
    public float Cooldown = 10f;
    public List<Image> CheckStrikeReadyImages;
    public List<ActiveSkillData> AllActiveSkillsData;
    public GameObject BluePrint;

    private int strikeIndex = 0;

    private bool lightningStrikeReady = false;
    private bool fireStrikeReady = false;
    private bool iceStrikeReady = false;
    private bool showBluePrint = false;

    private Vector3 strikeHitPosition;

    private ObjectPooler objectPooler;
    private ActiveSkillData strikeData;

    private void Start()
    {
        strikeData = AllActiveSkillsData[strikeIndex];
        objectPooler = ObjectPooler.Instance;

        for(int index = 0; index < CheckStrikeReadyImages.Count; index++)
        {
            StartCoroutine(WaitForCooldown(Cooldown, CheckStrikeReadyImages[index], index));
            AllActiveSkillsData[index].IsReady = CheckBool(CheckStrikeReadyImages[index]);
        }
    }

    public void SelectStrike(int _index)
    {
        if (BluePrint == null) BluePrint = new GameObject();

        BluePrint.SetActive(true);
        showBluePrint = true;
        strikeIndex = _index;
        strikeData = AllActiveSkillsData[strikeIndex];
        if (_index == 0)
            lightningStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
        else if (_index == 1)
            fireStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
        else if (_index == 2)
            iceStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
    }
    
    public void LightningStrike()
    {
        GameObject strikeInstanceGameObject = objectPooler.SpawnFromPool(strikeData.Effect.name, strikeHitPosition, strikeData.Effect.transform.rotation);
        if (strikeInstanceGameObject == null) return;
        LightningStrike lightningStrikeScript = strikeInstanceGameObject.GetComponent<LightningStrike>();
        if (lightningStrikeScript != null)
        {
            strikeInstanceGameObject.GetComponent<SphereCollider>().radius = strikeData.AreaRange;
            lightningStrikeScript.Damage = strikeData.Damage;
            lightningStrikeScript.AreaRange = strikeData.AreaRange;

        }
        CheckStrikeReadyImages[strikeIndex].fillAmount = 1f;
        StartCoroutine(WaitForCooldownBool(Cooldown, CheckStrikeReadyImages[strikeIndex], lightningStrikeReady));
        lightningStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
    }

    public void IceStrike()
    {
        GameObject strikeInstanceGameObject = objectPooler.SpawnFromPool(strikeData.Effect.name, strikeHitPosition, strikeData.Effect.transform.rotation);
        if (strikeInstanceGameObject == null) return;
        LightningStrike lightningStrikeScript = strikeInstanceGameObject.GetComponent<LightningStrike>();
        if (lightningStrikeScript != null)
        {
            strikeInstanceGameObject.GetComponent<SphereCollider>().radius = strikeData.AreaRange;
            lightningStrikeScript.Damage = strikeData.Damage;
            lightningStrikeScript.AreaRange = strikeData.AreaRange;

        }
        CheckStrikeReadyImages[strikeIndex].fillAmount = 1f;
        StartCoroutine(WaitForCooldownBool(Cooldown, CheckStrikeReadyImages[strikeIndex], iceStrikeReady));
        iceStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
    }

    public void FireStrike()
    {
        GameObject strikeInstanceGameObject = objectPooler.SpawnFromPool(strikeData.Effect.name, strikeHitPosition, strikeData.Effect.transform.rotation);
        if (strikeInstanceGameObject == null) return;
        LightningStrike lightningStrikeScript = strikeInstanceGameObject.GetComponent<LightningStrike>();
        if (lightningStrikeScript != null)
        {
            strikeInstanceGameObject.GetComponent<SphereCollider>().radius = strikeData.AreaRange;
            lightningStrikeScript.Damage = strikeData.Damage;
            lightningStrikeScript.AreaRange = strikeData.AreaRange;

        }
        CheckStrikeReadyImages[strikeIndex].fillAmount = 1f;
        StartCoroutine(WaitForCooldownBool(Cooldown, CheckStrikeReadyImages[strikeIndex], fireStrikeReady));
        fireStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
    }

    private bool CheckBool(Image fillImage)
    {
        if (fillImage.fillAmount <= 0)
            return true;
        else
            return false;
    }
    private IEnumerator WaitForCooldownBool(float cooldown, Image fillImage, bool ready)
    {
        ready = false;
        float fillTime = cooldown;
        while (fillTime > 0)
        {
            fillTime -= Time.deltaTime;
            fillImage.fillAmount = fillTime / cooldown;
            yield return null;
        }
        ready = true;
    }


    private IEnumerator WaitForCooldown(float cooldown, Image fillImage, int index)
    {
        switch(index)
        {
            case 0:
                lightningStrikeReady = false;
                break;
            case 1:
                fireStrikeReady = false;
                break;
            case 2:
                iceStrikeReady = false;
                break;
            default:
                lightningStrikeReady = false;
                break;
        }
        float fillTime = cooldown;
        while(fillTime > 0)
        {
            fillTime -= Time.deltaTime;
            fillImage.fillAmount = fillTime / cooldown;
            yield return null;
        }
        switch (index)
        {
            case 0:
                lightningStrikeReady = true;
                break;
            case 1:
                fireStrikeReady = true;
                break;
            case 2:
                iceStrikeReady = true;
                break;
            default:
                lightningStrikeReady = true;
                break;
        }
    }

    private void Update()
    {
        if(showBluePrint)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
                BluePrint.transform.position = new Vector3(hit.point.x, BluePrint.transform.position.y, hit.point.z);
            else
                return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (BluePrint == null) BluePrint = new GameObject();

            BluePrint.SetActive(false);

            if (lightningStrikeReady || fireStrikeReady || iceStrikeReady)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 1000f))
                {
                    strikeHitPosition = hit.point;
                    if (lightningStrikeReady && strikeIndex == 0)
                        LightningStrike();
                    else if (fireStrikeReady && strikeIndex == 1)
                        FireStrike();
                    else if (iceStrikeReady && strikeIndex == 2)
                        IceStrike();
                }
            }
        }
    }
}
