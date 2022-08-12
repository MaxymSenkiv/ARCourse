using System.Collections.Generic;
using UnityEngine;

namespace ARCourse
{
    public class ModelGenerator : MonoBehaviour
    {
        [SerializeField] private int _blocksAmountX;
        [SerializeField] private int _blocksAmountZ;
        [SerializeField] private Transform _cube;
        [SerializeField] private Transform _parent;

        private List<List<Color>> _colors = new List<List<Color>>();

        public void DividePhoto(Texture2D texture2D, int screenWidth, int screenHeight)
        {
            int blockWidth = screenWidth / _blocksAmountX;
            int blockHeight = screenHeight / _blocksAmountZ;

            for (int i = 0; i < screenWidth; i += blockWidth)
            {
                for (int j = 0; j < screenHeight; j += blockHeight)
                {
                    // texture2D.GetPixels(i, j, blockWidth, blockHeight);
                    // Debug.Log(i + " " + j);

                    // Debug.Log(GetBlockColor(texture2D.GetPixels(i, j, blockWidth, blockHeight),
                    //     blockHeight, blockHeight));

                    Color color = GetBlockColor(texture2D.GetPixels(i, j, blockWidth, blockHeight),
                        blockHeight, blockHeight);
                    if (color != Color.clear)
                    {
                        Transform cube = Instantiate(_cube, _parent);
                        cube.GetComponent<Renderer>().material.color = color;
                        cube.position = new Vector3(i / blockWidth, CalculateCubeY(color), j / blockHeight);
                        cube.localScale = new Vector3(1, CalculateCubeY(color) * 2 + 1, 1);
                    }
                }
            }
        }

        private float CalculateCubeY(Color color)
        {
            return ((1 - ((color.r + color.g + color.b) / 3)) * 20) / 2;
        }

        private Color GetBlockColor(Color[] colors, int blockWidth, int blockHeight)
        {
            float colorR = 0;
            float colorG = 0;
            float colorB = 0;
            float colorA = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                if ((colors[i].r < 0.2 && colors[i].g < 0.1 && colors[i].b < 0.3) ||
                    (colors[i].r > 0.9 && colors[i].g > 0.9 && colors[i].b > 0.9))
                    break;
                colorR += colors[i].r;
                colorG += colors[i].g;
                colorB += colors[i].b;
                colorA += colors[i].a;
            }

            colorR = colorR / colors.Length;
            colorG = colorG / colors.Length;
            colorB = colorB / colors.Length;
            colorA = colorA / colors.Length;

            if ((colorR < 0.15 && colorG < 0.15 && colorB < 0.15) /*||
                (colorR > 0.9 && colorG > 0.9 && colorB > 0.9)*/)
                return Color.clear;

            return new Color(colorR, colorG, colorB, colorA);
        }
    }
}