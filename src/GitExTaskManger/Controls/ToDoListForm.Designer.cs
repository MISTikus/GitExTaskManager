namespace GitExTaskManger.Controls;

partial class ToDoListForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        var listViewItem1 = new ListViewItem(new string[] { "Title", "Created" }, -1);
        statusStrip1 = new StatusStrip();
        StatusLabel = new ToolStripStatusLabel();
        toolStripStatusLabel2 = new ToolStripStatusLabel();
        toolStripStatusLabel1 = new ToolStripStatusLabel();
        ChangeModeButton = new ToolStripSplitButton();
        showResolvedToolStripMenuItem = new ToolStripMenuItem();
        panel1 = new Panel();
        ResolveButton = new Button();
        RemoveButton = new Button();
        EditButton = new Button();
        AddButton = new Button();
        ToDoList = new ListView();
        columnHeader4 = new ColumnHeader();
        columnHeader5 = new ColumnHeader();
        statusStrip1.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // statusStrip1
        // 
        statusStrip1.ImageScalingSize = new Size(24, 24);
        statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel, toolStripStatusLabel2, toolStripStatusLabel1, ChangeModeButton });
        statusStrip1.Location = new Point(0, 733);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1542, 32);
        statusStrip1.TabIndex = 1;
        statusStrip1.Text = "statusStrip1";
        // 
        // StatusLabel
        // 
        StatusLabel.Name = "StatusLabel";
        StatusLabel.Size = new Size(179, 25);
        StatusLabel.Text = "toolStripStatusLabel1";
        // 
        // toolStripStatusLabel2
        // 
        toolStripStatusLabel2.Name = "toolStripStatusLabel2";
        toolStripStatusLabel2.Size = new Size(1226, 25);
        toolStripStatusLabel2.Spring = true;
        // 
        // toolStripStatusLabel1
        // 
        toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        toolStripStatusLabel1.Size = new Size(0, 25);
        // 
        // ChangeModeButton
        // 
        ChangeModeButton.AutoToolTip = false;
        ChangeModeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        ChangeModeButton.DropDownItems.AddRange(new ToolStripItem[] { showResolvedToolStripMenuItem });
        ChangeModeButton.ImageTransparentColor = Color.Magenta;
        ChangeModeButton.Name = "ChangeModeButton";
        ChangeModeButton.Size = new Size(122, 29);
        ChangeModeButton.Text = "View mode";
        ChangeModeButton.TextImageRelation = TextImageRelation.TextAboveImage;
        // 
        // showResolvedToolStripMenuItem
        // 
        showResolvedToolStripMenuItem.Name = "showResolvedToolStripMenuItem";
        showResolvedToolStripMenuItem.Size = new Size(229, 34);
        showResolvedToolStripMenuItem.Text = "ShowResolved";
        showResolvedToolStripMenuItem.Click += ShowResolved_Click;
        // 
        // panel1
        // 
        panel1.Controls.Add(ResolveButton);
        panel1.Controls.Add(RemoveButton);
        panel1.Controls.Add(EditButton);
        panel1.Controls.Add(AddButton);
        panel1.Dock = DockStyle.Right;
        panel1.Location = new Point(1441, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(101, 733);
        panel1.TabIndex = 3;
        // 
        // ResolveButton
        // 
        ResolveButton.Dock = DockStyle.Top;
        ResolveButton.Enabled = false;
        ResolveButton.Location = new Point(0, 190);
        ResolveButton.Name = "ResolveButton";
        ResolveButton.Size = new Size(101, 95);
        ResolveButton.TabIndex = 3;
        ResolveButton.Text = "Resolve";
        ResolveButton.TextAlign = ContentAlignment.BottomCenter;
        ResolveButton.UseVisualStyleBackColor = true;
        ResolveButton.Click += ResolveButton_Click;
        // 
        // RemoveButton
        // 
        RemoveButton.Dock = DockStyle.Bottom;
        RemoveButton.Enabled = false;
        RemoveButton.Location = new Point(0, 638);
        RemoveButton.Name = "RemoveButton";
        RemoveButton.Size = new Size(101, 95);
        RemoveButton.TabIndex = 2;
        RemoveButton.Text = "Remove";
        RemoveButton.TextAlign = ContentAlignment.BottomCenter;
        RemoveButton.UseVisualStyleBackColor = true;
        RemoveButton.Click += RemoveButton_Click;
        // 
        // EditButton
        // 
        EditButton.Dock = DockStyle.Top;
        EditButton.Enabled = false;
        EditButton.Location = new Point(0, 95);
        EditButton.Name = "EditButton";
        EditButton.Size = new Size(101, 95);
        EditButton.TabIndex = 1;
        EditButton.Text = "Edit";
        EditButton.TextAlign = ContentAlignment.BottomCenter;
        EditButton.UseVisualStyleBackColor = true;
        EditButton.Click += EditButton_Click;
        // 
        // AddButton
        // 
        AddButton.Dock = DockStyle.Top;
        AddButton.Location = new Point(0, 0);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(101, 95);
        AddButton.TabIndex = 0;
        AddButton.Text = "Add";
        AddButton.TextAlign = ContentAlignment.BottomCenter;
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // ToDoList
        // 
        ToDoList.BackColor = SystemColors.Menu;
        ToDoList.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5 });
        ToDoList.Dock = DockStyle.Fill;
        ToDoList.FullRowSelect = true;
        ToDoList.GridLines = true;
        ToDoList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        ToDoList.Items.AddRange(new ListViewItem[] { listViewItem1 });
        ToDoList.Location = new Point(0, 0);
        ToDoList.MultiSelect = false;
        ToDoList.Name = "ToDoList";
        ToDoList.Size = new Size(1441, 733);
        ToDoList.TabIndex = 4;
        ToDoList.UseCompatibleStateImageBehavior = false;
        ToDoList.View = View.Details;
        ToDoList.SelectedIndexChanged += ToDoList_SelectedIndexChanged;
        // 
        // columnHeader4
        // 
        columnHeader4.Text = "Title";
        // 
        // columnHeader5
        // 
        columnHeader5.Text = "Created";
        // 
        // ToDoListForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1542, 765);
        Controls.Add(ToDoList);
        Controls.Add(panel1);
        Controls.Add(statusStrip1);
        Name = "ToDoListForm";
        Text = "ToDoListForm";
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        panel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private StatusStrip statusStrip1;
    private Panel panel1;
    private ListView ToDoList;
    private ToolStripStatusLabel StatusLabel;
    private Button EditButton;
    private Button AddButton;
    private Button ResolveButton;
    private Button RemoveButton;
    private ToolStripSplitButton ChangeModeButton;
    private ToolStripMenuItem showResolvedToolStripMenuItem;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
}