using System;
using System.IO;

public class SettingsWriter
{
    string filepath;
    DateTime lastWriteTime;

    public SettingsWriter(string path)
    {
        filepath = path;
    }

    public void Save(SettingsBlock[] settings)
    {
        StreamWriter outFile = new StreamWriter(filepath);
        outFile.Write(_header);

        foreach (SettingsBlock block in settings)
        {
            outFile.Write("\n\n-------------------------------------------------------------");
            outFile.Write($"\n\n>>>> {block.name} <<<<");
            foreach (SettingBase setting in block)
            {
                outFile.Write($"\n---{setting.name}---");
                outFile.Write($"\n{setting.description}\n---");

                SettingRange range = setting.GetRange();
                outFile.Write($"\nDefault Value: {range.def}");
                outFile.Write($"\nSuggested Range: {range.min} - {range.max}");
                outFile.Write($"\n---\nValue: {setting.GetValueString()}\n");
            }
            outFile.Write("\n>>>>      <<<<");
        }
        outFile.Close();

        lastWriteTime = File.GetLastWriteTime(filepath);
    }

    public bool HasWriteTimeChanged()
    {
        DateTime newWriteTime = File.GetLastWriteTime(filepath);
        if (lastWriteTime != newWriteTime)
        {
            lastWriteTime = newWriteTime;
            return true;
        }
        return false;
    }


    const string _header = ">>>> INFO <<<<" +
    "\nThis document follows a strict format in order to be both readable and functional,"
    + "\nthe only things you need to change are after \"Value:\"."
    + "\nParamaters are listed in a uniform format like so:" +
    "\n---Parameter name---" +
    "\ndescription" +
    "\n---" +
    "\nDefault Value: ##" +
    "\nSuggested Range: ## - ##" +
    "\n---" +
    "\nValue: [actual value here]\n" +
    "\n>>>>      <<<<\n\n";
}
