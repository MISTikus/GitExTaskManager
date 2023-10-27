﻿using GitExTaskManger.Services;

namespace GitExTaskManger.Controls;
internal class SolutionListMenuItem : ToolStripMenuItem
{
    private readonly ISolutionFileProvider provider;
    private readonly PluginSettings settings;

    internal SolutionListMenuItem(ISolutionFileProvider provider, PluginSettings settings)
    {
        this.provider = provider;
        this.settings = settings;

        Text = "Solution R&unner";
        DropDownOpening += OnDropDownOpening;
        DropDown.ShowItemToolTips = true;

        DropDown.Items.Add(new LoadingMenuItem());
    }

    private async void OnDropDownOpening(object sender, EventArgs e)
    {
        DropDown.Items.Clear();

        var loading = new LoadingMenuItem();
        DropDown.Items.Add(loading);

        var solutions = new HashSet<string>();
        DropDown.Items.AddRange(await CreateBundleItemsAsync(settings.IsTopLevelSearchedOnly, settings.EnableVSCodeWorkspaces, solutions));
        DropDown.Items.Remove(loading);

        if (DropDown.Items.Count > 0)
            DropDown.Items[0].Select();

        if (settings.IsTopLevelSearchedOnly)
        {
            var separator = new ToolStripSeparator();
            DropDown.Items.Add(separator);
            DropDown.Items.Add(loading);

            var items = await CreateBundleItemsAsync(false, settings.EnableVSCodeWorkspaces, solutions);
            if (items.Length > 0)
                DropDown.Items.AddRange(items);
            else
                DropDown.Items.Remove(separator);

            DropDown.Items.Remove(loading);
        }
    }

    private async Task<ToolStripItem[]> CreateBundleItemsAsync(bool isTopLevelSearchedOnly, bool includeWorkspaces, HashSet<string> solutions)
    {
        return await Task.Run(async () =>
        {
            var newItems = new List<ToolStripItem>();

            IEnumerable<string> solutionFiles = await provider.GetListAsync(isTopLevelSearchedOnly, includeWorkspaces);
            foreach (var filePath in solutionFiles)
            {
                if (solutions.Add(filePath))
                    newItems.Add(new SolutionItemMenuItem(filePath, settings));
            }

            newItems.Sort((x, y) => x.Text.CompareTo(y.Text));

            return newItems.ToArray();
        });
    }
}