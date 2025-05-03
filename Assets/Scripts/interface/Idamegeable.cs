using UnityEngine;

public interface Idamegeable
{  // Start is called once before the first execution of Update after the MonoBehaviour is created
  Teams Team { get; }
void ChangeHealthOfTheCharacter(int amount);
}
