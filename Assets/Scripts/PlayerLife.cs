using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int maxLife;
    
    [SerializeField] private Slider healthBar;

    [HideInInspector] public int currentLife;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentLife;
    }
}
