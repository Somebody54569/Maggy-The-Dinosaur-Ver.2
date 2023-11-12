using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    public GameObject showImgPanel;
    public GameObject capturePanel;
    public GameObject saveImgPanel;
    public GameObject shareImgPanel;

    public RawImage showImg;

    public string gameName = "Maggy The Dinosaur Ver.2";

    private byte[] currentTexture;
    private string currentFilePath;

    public GameObject BannerAd;
    [SerializeField] BannerAdExample bannerAdExample;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ScreenShotName()
    {
        return string.Format("{0}_{1}.png", gameName, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void Capture()
    {
        
        StartCoroutine(TakeScreenShot());
        capturePanel.SetActive(false);
        StartCoroutine(wait());
    }
    
    private IEnumerator TakeScreenShot()
    {
        capturePanel.SetActive(false);
        yield return new WaitForEndOfFrame();
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        currentFilePath = Path.Combine(Application.temporaryCachePath, "temp_img.png");
        currentTexture = screenshot.EncodeToPNG();
        
        File.WriteAllBytes(currentFilePath, currentTexture);
        ShowScreenshot();
        //
        //
        // To avoid memory leak
        Destroy(screenshot);
    }

    public void ShowScreenshot()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.LoadImage(currentTexture);
        showImg.material.mainTexture = tex;
        showImgPanel.SetActive(true);
        bannerAdExample.ShowBannerAd();
    }

    public void SaveToGallery()
    {
        NativeGallery.Permission permission =
            NativeGallery.SaveImageToGallery(currentFilePath, gameName, ScreenShotName(),
                (success, path) =>
                {
                    Debug.Log("Media save result: "+ success + " "+path);
                    if (success)
                    {
                        saveImgPanel.SetActive(true);
#if UNITY_EDITOR
                        string editerFilePath = Path.Combine(Application.persistentDataPath, ScreenShotName());
                        File.WriteAllBytes(editerFilePath, currentTexture);
#endif
                    }
                });
        Debug.Log("Permission result: " + permission);
    }

    public void ShareImage()
    {
        Debug.Log("Share button clicked");
        new NativeShare().AddFile(currentFilePath)
            .SetSubject("Share Screenshot Maggy The Dinosaur Ver.2")
            .SetCallback((result, shareTarget) =>
            {
                Debug.Log("Share result: " + result + ", selected app: " + shareTarget);
                if (result == NativeShare.ShareResult.Shared)
                {
                    shareImgPanel.SetActive(true);
                }
            }).Share();
                
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }
}
