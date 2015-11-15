using UnityEngine;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{

    public enum Menu
    {
        MainMenu,
        NewGame,
        Continue
    }

    public Menu currentMenu;
    public string PCName;

    void OnGUI()
    {

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        if (currentMenu == Menu.MainMenu)
        {

            GUILayout.Box("Dark Conqueror");
            GUILayout.Space(10);

            if (GUILayout.Button("New Game"))
            {
                currentMenu = Menu.NewGame;
            }

            if (GUILayout.Button("Continue"))
            {
                currentMenu = Menu.Continue;
            }

            if (GUILayout.Button("Quit"))
            {
                Application.Quit();
            }
        }

        else if (currentMenu == Menu.NewGame)
        {

            GUILayout.Box("Name Your Characters");
            GUILayout.Space(10);

            GUILayout.Label("Please enter overlord name");
            PCName = GUILayout.TextField(PCName, 20);

            if (GUILayout.Button("Start"))
            {
                //Move on to game...
                Application.LoadLevel(1);
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Cancel"))
            {
                currentMenu = Menu.MainMenu;
            }

        }

        else if (currentMenu == Menu.Continue)
        {

        }

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }
}
