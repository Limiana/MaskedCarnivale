namespace MaskedCarnivale.Windows;
using Dalamud.Bindings.ImGui;
using System;
using System.IO;
using System.Text;

public static class WindowNameConfig
{
    private static string configDir =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                     "XIVLauncher", "pluginConfigs", "MaskedCarnivalle");

    private static string configFile = Path.Combine(configDir, "windowname.txt");

    private static string windowName = "MaskedCarnivale";
    private static bool initialized = false;

    public static void Draw()
    {
        if(!initialized)
        {
            Load();
            initialized = true;
        }

        ImGui.SetNextItemWidth(150f);
        ImGui.InputText("Window Name", ref windowName);
        ImGui.SameLine();
        if(ImGui.Button("Apply"))
        {
            Save();
        }
    }

    private static void Load()
    {
        try
        {
            if(File.Exists(configFile))
            {
                string content = File.ReadAllText(configFile, Encoding.UTF8).Trim();
                if(!string.IsNullOrEmpty(content))
                {
                    windowName = content;
                }
            }
        }
        catch
        {
            windowName = "MaskedCarnivale";
        }
    }

    private static void Save()
    {
        try
        {
            Directory.CreateDirectory(configDir);
            File.WriteAllText(configFile, windowName, Encoding.UTF8);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Failed to save window name: " + ex.Message);
        }
    }
}
