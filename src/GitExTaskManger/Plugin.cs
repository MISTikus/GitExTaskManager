using GitExtensions.TaskManger.Controls;
using GitExtensions.TaskManger.Properties;
using GitExtensions.TaskManger.Services;
using GitExtensions.TaskManger.Utils;
using GitUI;
using GitUI.CommandsDialogs;
using GitUIPluginInterfaces;
using ResourceManager;
using System.ComponentModel.Composition;

namespace GitExtensions.TaskManger
{
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

            if (gitUiCommands.GitModule.IsValidGitWorkingDir())
            {
                var mainMenu = FindMainMenu(gitUiCommands);
                if (mainMenu != null && FindMainMenuItem(gitUiCommands, mainMenu) == null)
                {
                    var provider = new FileProvider(gitUiCommands.GitModule.WorkingDir);
                    var serializer = new YmlSerializer();

                    var manager = new Domain.TaskManger(provider, serializer, "yaml");
                    mainMenu.Items.Add(new TaskManagerMenuItem(manager));
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
            args.GitUICommands.StartSettingsDialog(this);
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
}
