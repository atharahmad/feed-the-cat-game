using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ResumeHandler : MonoBehaviour
{

    public static ResumeHandler Instance;
    [SerializeField] private TextMeshProUGUI display;
    [SerializeField] private GameObject holder;
    public Color[] gradiantsColors;
    private void Awake()
    {
        Instance = this;
    }

    public void Resume()
    {
        StartCoroutine(ResumeRoutine());
    }


    private IEnumerator ResumeRoutine()
    {
        int i = Random.Range(0, gradiantsColors.Length / 2);
        VertexGradient gradiant = new VertexGradient(gradiantsColors[i*2], gradiantsColors[i*2 ], gradiantsColors[i * 2 + 1], gradiantsColors[i * 2 + 1]);
        
        
        display.colorGradient = gradiant;
        holder.SetActive(true);
        for (int _i = 0; _i <= 3; _i++)
        {
            if (_i == 3)
                display.text = "Go!";
            else
                display.text = (3 - _i).ToString();
            yield return new WaitForSeconds(1);
        }

        GamePlayManager.TimeScale = 1;
        yield return new WaitForSeconds(0.5f);
        holder.SetActive(false);
    }
}
