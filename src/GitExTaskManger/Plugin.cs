using GitExTaskManger.Controls;
using GitExTaskManger.Properties;
using GitExTaskManger.Services;
using GitUI;
using GitUI.CommandsDialogs;
using GitUIPluginInterfaces;
using ResourceManager;
using System.ComponentModel.Composition;

namespace GitExTaskManger
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

        public override void Register(IGitUICommands commands)
        {
            base.Register(commands);

            Configuration = new PluginSettings(Settings);

            if (commands.GitModule.IsValidGitWorkingDir())
            {
                var mainMenu = FindMainMenu(commands);
                if (mainMenu != null && FindMainMenuItem(commands, mainMenu) == null)
                {
                    var provider = new GitSolutionFileProvider(commands.GitModule.WorkingDir, commands.GitModule.GitExecutable);

                    mainMenu.Items.Add(new SolutionListMenuItem(provider, Configuration));
                }
            }
        }

        public override void Unregister(IGitUICommands commands)
        {
            base.Unregister(commands);

            var mainMenu = FindMainMenu(commands);
            if (mainMenu != null)
            {
                var mainMenuItem = FindMainMenuItem(commands, mainMenu);
                if (mainMenuItem != null)
                {
                    mainMenu.Items.Remove(mainMenuItem);
                    mainMenuItem.Dispose();
                }
            }
        }

        public override IEnumerable<ISetting> GetSettings()
            => Configuration;

        public override bool Execute(GitUIEventArgs e)
        {
            e.GitUICommands.StartSettingsDialog(this);
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

        private static SolutionListMenuItem FindMainMenuItem(IGitUICommands commands, MenuStripEx mainMenu = null)
        {
            mainMenu ??= FindMainMenu(commands);
            return mainMenu?.Items.OfType<SolutionListMenuItem>().FirstOrDefault();
        }
    }
}
