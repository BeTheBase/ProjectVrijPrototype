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
            StartCoroutine(WaitForCooldownBool(AllActiveSkillsData[index].CoolDownTime, CheckStrikeReadyImages[index], AllActiveSkillsData[index].IsReady));
            AllActiveSkillsData[index].IsReady = CheckBool(CheckStrikeReadyImages[index]);
        }
    }

    public void SelectStrike(int _index)
    {
        if (BluePrint == null) BluePrint = new GameObject();

        showBluePrint = true;
        strikeIndex = _index;
        strikeData = AllActiveSkillsData[strikeIndex];
        switch (_index)
        {
            case 0:
                lightningStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
                break;
            case 1:
                fireStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
                break;
            case 2:
                iceStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
                break;
            default:
                break;
        }
        BluePrint = AllActiveSkillsData[_index].StrikeBluePrint;

        BluePrint.SetActive(true);

    }

    public void LightningStrike()
    {
        GameObject strikeInstanceGameObject = objectPooler.SpawnFromPool(strikeData.Effect.name, strikeHitPosition, strikeData.Effect.transform.rotation);
        if (strikeInstanceGameObject == null) return;
        LightningStrike lightningStrikeScript = strikeInstanceGameObject.GetComponent<LightningStrike>();
        if (lightningStrikeScript != null)
        {
            strikeInstanceGameObject.GetComponent<SphereCollider>().radius = strikeData.AreaRange;
            lightningStrikeScript.StrikeDamage = strikeData.Damage;
            lightningStrikeScript.StrikeRange = strikeData.AreaRange;
            if (strikeData.StrikeEffect != null)
                lightningStrikeScript.StrikeEffect = strikeData.StrikeEffect;
        }
        CheckStrikeReadyImages[strikeIndex].fillAmount = 1f;
        StartCoroutine(WaitForCooldownBool(AllActiveSkillsData[strikeIndex].CoolDownTime, CheckStrikeReadyImages[strikeIndex], lightningStrikeReady));
        lightningStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
    }

    public void IceStrike()
    {
        GameObject strikeInstanceGameObject = objectPooler.SpawnFromPool(strikeData.Effect.name, strikeHitPosition, strikeData.Effect.transform.rotation);
        if (strikeInstanceGameObject == null) return;
        IceStrike iceStrike = strikeInstanceGameObject.GetComponent<IceStrike>();
        if (iceStrike != null)
        {
            strikeInstanceGameObject.GetComponent<SphereCollider>().radius = strikeData.AreaRange;
            iceStrike.StrikeDamage = strikeData.Damage;
            iceStrike.StrikeRange = strikeData.AreaRange;
            if(strikeData.StrikeEffect != null)
                iceStrike.StrikeEffect = strikeData.StrikeEffect;
        }
        CheckStrikeReadyImages[strikeIndex].fillAmount = 1f;
        StartCoroutine(WaitForCooldownBool(AllActiveSkillsData[strikeIndex].CoolDownTime, CheckStrikeReadyImages[strikeIndex], iceStrikeReady));
        iceStrikeReady = CheckBool(CheckStrikeReadyImages[strikeIndex]);
    }

    public void FireStrike()
    {
        GameObject strikeInstanceGameObject = objectPooler.SpawnFromPool(strikeData.Effect.name, strikeHitPosition, strikeData.Effect.transform.rotation);
        if (strikeInstanceGameObject == null) return;
        FireStrike fireStrike = strikeInstanceGameObject.GetComponent<FireStrike>();
        if (fireStrike != null)
        {
            strikeInstanceGameObject.GetComponent<SphereCollider>().radius = strikeData.AreaRange;
            fireStrike.StrikeDamage = strikeData.Damage;
            fireStrike.StrikeRange = strikeData.AreaRange;
            if (strikeData.StrikeEffect != null)
                fireStrike.StrikeEffect = strikeData.StrikeEffect;
        }
        CheckStrikeReadyImages[strikeIndex].fillAmount = 1f;
        StartCoroutine(WaitForCooldownBool(AllActiveSkillsData[strikeIndex].CoolDownTime, CheckStrikeReadyImages[strikeIndex], fireStrikeReady));
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

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            showBluePrint = false;
            BluePrint.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(0))
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
