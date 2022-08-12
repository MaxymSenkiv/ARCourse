using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ARCourse
{
    public class PhotoCamera : MonoBehaviour
    {
        [SerializeField] private ModelGenerator _modelGenerator;
        [SerializeField] private Transform _cameraScreen;
        private int _screenWidth = Screen.width;
        private int _screenHeight = Screen.height;

        // private void Start()
        // {
        //     WebCamTexture webCamTexture = new WebCamTexture();
        //     _cameraScreen.GetComponent<Renderer>().material.mainTexture = webCamTexture;
        //     webCamTexture.Play();
        // }

        public void TakePhoto()
        {
            StartCoroutine(Photo());
        }
        
        private IEnumerator Photo()
        {
            yield return new WaitForEndOfFrame();

            var texture2D = new Texture2D(_screenWidth, _screenHeight, TextureFormat.RGB24, false );

            texture2D.ReadPixels( new Rect(0, 0, _screenWidth, _screenHeight), 0, 0 );
            texture2D.Apply();

            byte[] bytes = texture2D.EncodeToPNG();
            File.WriteAllBytes("Assets/Photos/photo.png", bytes);

            _modelGenerator.DividePhoto(texture2D, _screenWidth, _screenHeight);
            
            Destroy( texture2D );
        }
    }
}