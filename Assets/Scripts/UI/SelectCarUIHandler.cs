using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCarUIHandler : MonoBehaviour
{
    [Header("Car prefab")]
    public GameObject carPrefab;

    [Header("Spawn on")]
    public Transform spawnOnTransform;

    bool isChangingCar = false;

    CarData[] carDatas;

    CarUIHandler carUIHandler = null;

    int selectedCarIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Tải dữ liệu xe
        carDatas = Resources.LoadAll<CarData>("CarData/");

        StartCoroutine(SpawnCarCO(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnPreviousCar();
        }
        else if (Input.GetKey(KeyCode.RightArrow))    
        {
            OnNextCar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectCar();
        }
    }

    public void OnPreviousCar()
    {
        if (isChangingCar)
        return;
        
        selectedCarIndex--;

        if (selectedCarIndex < 0)
            selectedCarIndex = carDatas.Length -1;

        StartCoroutine(SpawnCarCO(true));
    }

    public void OnNextCar()
    {
        if (isChangingCar)
        return;

        selectedCarIndex++;

        if (selectedCarIndex > carDatas.Length - 1)
            selectedCarIndex = 0;

        StartCoroutine(SpawnCarCO(false));
    }

    public void OnSelectCar()
    {
        PlayerPrefs.SetInt("P1SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        PlayerPrefs.SetInt("P2SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        PlayerPrefs.SetInt("P3SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);
        PlayerPrefs.SetInt("P4SelectedCarID", carDatas[selectedCarIndex].CarUniqueID);

        PlayerPrefs.Save();

        SceneManager.LoadScene("Map 1");
    }

    IEnumerator SpawnCarCO(bool isCarAppearingOnRightSide)
    {
        isChangingCar = true;

        if (carUIHandler != null)
            carUIHandler.StartCarExitAnimation(!isCarAppearingOnRightSide);

        GameObject instantiatedCar = Instantiate(carPrefab, spawnOnTransform);
        
        carUIHandler = instantiatedCar.GetComponent<CarUIHandler>();
        carUIHandler.SetupCar(carDatas[selectedCarIndex]);
        carUIHandler.StartCarEntranceAnimation(isCarAppearingOnRightSide);

        yield return new WaitForSeconds(0.4f);

        isChangingCar = false;
    }
}
