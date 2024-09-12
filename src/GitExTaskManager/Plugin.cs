using GitExtensions.Extensibility.Git;
using GitExtensions.Extensibility.Plugins;
using GitExtensions.Extensibility.Settings;
using GitExtensions.TaskManager.Controls;
using GitExtensions.TaskManager.Properties;
using GitExtensions.TaskManager.Services;
using GitExtensions.TaskManager.Utils;
using GitUI;
using GitUI.CommandsDialogs;
using System.ComponentModel.Composition;

namespace GitExtensions.TaskManager;

[Export(typeof(IGitPlugin))]
public class Plugin : GitPluginBase
{
    internal PluginSettings Configuration { get; private set; }

    public Plugin()
        : base(PluginSettings.HasProperties)
    {
        Id = new Guid("b65844ee-58cd-4243-bf81-13b1c371a0bf");
        Name = "TaskManager";
        Description = "Local repository task manager";
        Icon = Resources.Icon;
    }

    public override void Register(IGitUICommands gitUiCommands)
    {
        base.Register(gitUiCommands);

        Configuration = new PluginSettings(Settings);

        if (gitUiCommands.Module.IsValidGitWorkingDir())
        {
            var mainMenu = FindMainMenu(gitUiCommands);
            if (mainMenu != null && FindMainMenuItem(gitUiCommands, mainMenu) == null)
            {
                var serializer = new YmlSerializer();
                var managerFactory = new TaskManagerFactory(serializer,
                    s => new FileProvider(Path.Combine(gitUiCommands.Module.WorkingDir, s ?? ""), "yaml"));
                _ = mainMenu.Items.Add(new TaskManagerMenuItem(managerFactory, Configuration));
            }
        }
    }

    public override void Unregister(IGitUICommands gitUiCommands)
    {
        base.Unregister(gitUiCommands);

        var mainMenu = FindMainMenu(gitUiCommands);
        if (mainMenu != null)
        {
            var mainMenuItem = FindMainMenuItem(gitUiCommands, mainMenu);
            if (mainMenuItem != null)
            {
                mainMenu.Items.Remove(mainMenuItem);
                mainMenuItem.Dispose();
            }
        }
    }

    public override IEnumerable<ISetting> GetSettings()
        => Configuration;

    public override bool Execute(GitUIEventArgs args)
    {
        _ = args.GitUICommands.StartSettingsDialog(this);
        return false;
    }

    private static MenuStripEx FindMainMenu(IGitUICommands commands)
    {
        var form = (FormBrowse)((GitUICommands)commands).BrowseRepo;
        if (form != null)
        {
            var mainMenu = form.Controls.OfType<MenuStripEx>().FirstOrDefault();
            return mainMenu;
        }

        return null;
    }

    private static TaskManagerMenuItem FindMainMenuItem(IGitUICommands commands, MenuStripEx mainMenu = null)
    {
        mainMenu ??= FindMainMenu(commands);
        return mainMenu?.Items.OfType<TaskManagerMenuItem>().FirstOrDefault();
    }
}
