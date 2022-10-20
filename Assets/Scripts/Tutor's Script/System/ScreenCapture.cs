using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Events;

public class ScreenCapture : MonoBehaviour
{
    // Set screenshot resolution
    public int captureWidth = 1920;
    public int captureHeight = 1080;
    
    // Configure with raw, jpg, png, or ppm (Simple Raw Format)
    public enum Format
    {
        RAW,
        JPG,
        PNG,
        PPM,
    }

    public Format format = Format.JPG;
    
    // Folder to write output (Default to dataPath)
    private string _outputFolder;
    
    // Private variables needed for Screenshot
    private Rect _rect;
    private RenderTexture _renderTexture;
    private Texture2D _screenshot;

    public bool isProcessing;
    private byte[] _currentTexture;
    public Image recentPhoto;

    public UnityEvent OnLoadImage;
    
    // Initialize Directory
    private void Awake()
    {
        _outputFolder = Application.persistentDataPath + "/Screenshots/";
        if (!Directory.Exists(_outputFolder))
        {
            Directory.CreateDirectory(_outputFolder);
            Debug.LogWarning($"Save Path will be : {_outputFolder}");
        }
    }

    private string CreateFileName(int width, int height)
    {
        // Timestamp to append to the screenshot filename
        string timestamp = DateTime.Now.ToString("MMddYYYYHHmmss");
        
        // use width, height, and timestamp for unique file
        var filename = string.Format("{0}/Screenshot_{1}x{2}_{3}.{4}",_outputFolder, width, height, timestamp, format.ToString().ToString());
        
        // Return filename
        return filename;
    }

    private void CaptureScreenshot()
    {
        isProcessing = true;
        
        // Create screenshot object
        if (_renderTexture == null)
        {
            // Creates off-screen render texture to be rendered into
            _rect = new Rect(0, 0, captureWidth, captureHeight);
            _renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            _screenshot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }
        
        // Get main camera and render its output into the off-screen render texture created above
        Camera camera = Camera.main;
        camera.targetTexture = _renderTexture;
        camera.Render();

        // Mark the render texture as active and read the current pixel data into the Texture2D
        RenderTexture.active = _renderTexture;
        _screenshot.ReadPixels(_rect, 0, 0);
        
        // Reset the texture and remove the render texture from the Camera since were done reading the screen data
        camera.targetTexture = null;
        RenderTexture.active = null;
        
        // Get out filename
        string filename = CreateFileName((int)_rect.width, (int)_rect.height);
        
        // Get file header/data bytes for the specified image format
        byte[] fileHeader = null;
        byte[] fileData = null;
        
        // Set the format and encode based on it
        switch (format)
        {
            case Format.RAW:
                fileData = _screenshot.GetRawTextureData();
                break;
            case Format.PNG:
                fileData = _screenshot.EncodeToPNG();
                break;
            case Format.JPG:
                fileData = _screenshot.EncodeToJPG();
                break;
            case Format.PPM:
                // Create a file header - ppm files
                string headerStr = string.Format("P6\n{0} {1}\n255\n", _rect.width, _rect.height);
                fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
                fileData = _screenshot.GetRawTextureData();
                break;
        }

        _currentTexture = fileData;
        
        // Create new thread to offload the saving from the main thread
        new System.Threading.Thread(() =>
        {
            var file = System.IO.File.Create(filename);

            if (fileHeader != null)
            {
                file.Write(fileHeader, 0, fileHeader.Length);
            }
            file.Write(fileData, 0, fileData.Length);
            file.Close();
            Debug.Log(string.Format("Screenshot saved {0}, size {1}", filename, fileData.Length));
            isProcessing = false;
        }).Start();

        StartCoroutine(LoadImage());
        
        // Cleanup
        Destroy(_renderTexture);
        _renderTexture = null;
        _screenshot = null;
    }

    public void TakeScreenshot()
    {
        if (!isProcessing)
        {
            CaptureScreenshot();
        }
        else
        {
            Debug.LogWarning("Currently processing");
        }
    }

    public IEnumerator LoadImage()
    {
        yield return new WaitForEndOfFrame();

        recentPhoto.material.mainTexture = null;
        Texture2D texture = new Texture2D(captureHeight, captureHeight, TextureFormat.RGB24, false);
        texture.LoadImage(_currentTexture);
        recentPhoto.material.mainTexture = texture;
        OnLoadImage?.Invoke();
    }
}
