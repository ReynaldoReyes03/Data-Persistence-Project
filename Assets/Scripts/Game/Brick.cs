using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour {
    [HideInInspector] public UnityEvent<int> onDestroyed;
    [HideInInspector] public int PointValue;

    void Start() {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();

        switch (PointValue) {
            case 1 :
                block.SetColor("_BaseColor", Color.white);
                break;
            case 2:
                block.SetColor("_BaseColor", new Color(0.75f, 0.75f, 0.75f));
                break;
            case 5:
                block.SetColor("_BaseColor", new Color(0.33f, 0.31f, 0.32f));
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }

        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other) {
        onDestroyed.Invoke(PointValue);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.2f);
    }
}
