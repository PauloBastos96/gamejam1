using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public String typeOfSpawn;
    public NightColor sky;
    public NightColor floor;

    enum StateOfDay
    {
        Day,
        Night
    }

    StateOfDay stateOfDay;

    void Start()
    {
        // Initialize the state to Day
        stateOfDay = StateOfDay.Day;

        // Start the cycle
        StartCoroutine(DayNightCycle());
    }

    IEnumerator DayNightCycle()
    {
        while (true)
        {
            // Toggle between Day and Night
            stateOfDay = (stateOfDay == StateOfDay.Day) ? StateOfDay.Night : StateOfDay.Day;
            // Update game objects based on the state
            UpdateGameObjects();

            // Wait for 5 seconds
            yield return new WaitForSeconds(5f);
        }
    }

    void UpdateGameObjects()
    {
        // Set active/inactive state of game objects based on the current time of day
        switch (stateOfDay)
        {
            case StateOfDay.Day:
                typeOfSpawn = "Day";
                break;
            case StateOfDay.Night:
                typeOfSpawn = "Night";
                break;
        }
        sky.ChangeColorRecursively(sky.transform);
        floor.ChangeColorRecursively(floor.transform);
    }

    public bool IsNight()
    {
        return stateOfDay == StateOfDay.Night;
    }
}
