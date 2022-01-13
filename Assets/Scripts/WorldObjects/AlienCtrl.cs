using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienCtrl : MonoBehaviour, IRocketTarget
{
    public Color colorDamage = Color.red;
    public HealthComponent hp;
    public Player player;
    public GameObject prefabExplosion;


    Animator animator;


    float delta;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hp.OnDeath += HandleOutOfHealth;
    }

    private void Start()
    {
        delta = transform.position.x - player.transform.position.x;
        animator.SetTrigger("Lift");
        hp.Init();
    }

    private void LateUpdate()
    {
        if (!player) return;

        // fly away from player
        var pos = transform.position;
        pos.x = player.transform.position.x + delta;
        transform.position = pos;
    }

    public void Damage(int value)
    {
        int removed = hp.Remove(value);
        Game.current
            .ShowFloater($"-{value} hp", transform.position + Vector3.up, transform)
            .SetColor(colorDamage);
    }

    void HandleOutOfHealth()
    {
        animator.SetTrigger("Destroy");
        player.enabled = false;
        player.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void Explode()
    {
        Instantiate(prefabExplosion, transform.position, Quaternion.identity);
        Game.current.GameOver(true);
    }

}

public interface IRocketTarget
{
    Transform transform { get; }
    void Damage(int value);
}
