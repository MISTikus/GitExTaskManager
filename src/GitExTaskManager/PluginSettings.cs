using GitExtensions.Extensibility.Settings;
using System.Collections;

namespace GitExtensions.TaskManager
{
    internal class PluginSettings : IEnumerable<ISetting>
    {
        private readonly SettingsSource source;

        /// <summary>
        /// Gets a property holding <see cref="UseEpics"/> property.
        /// </summary>
        public static BoolSetting UseEpicsProperty { get; }
            = new BoolSetting("Use Epics", "Use Epics to group tasks and issues", false);

        /// <summary>
        /// Gets current value of <see cref="UseEpicsProperty"/>.
        /// </summary>
        public bool UseEpics => source.GetBool(UseEpicsProperty.Name, UseEpicsProperty.DefaultValue);

        public PluginSettings(SettingsSource source) => this.source = source;

        #region IEnumerable<ISetting>

        private static readonly List<ISetting> properties = new(1)
        {
            UseEpicsProperty,
        };

        public static bool HasProperties => properties.Count > 0;

        public IEnumerator<ISetting> GetEnumerator() => properties.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
