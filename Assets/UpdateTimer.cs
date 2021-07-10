using UnityEngine;
using TMPro;

public class UpdateTimer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Timer;

    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        Timer.text = $"{elapsedTime:F2}";
    }
}
