using UnityEngine;

public class PowerPellet : Pellet
{
    public AudioSource powerPelletEatSoundEffect;

    public float duration = 8f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.PlayPowerPelletSound(); // Play the PowerPellet sound
            gameManager.PowerPelletEaten(this); }
    }
}
