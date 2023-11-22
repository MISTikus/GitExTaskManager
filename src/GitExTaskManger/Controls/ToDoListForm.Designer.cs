namespace GitExtensions.TaskManger.Controls;

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
        this.statusStrip1 = new StatusStrip();
        this.StatusLabel = new ToolStripStatusLabel();
        this.toolStripStatusLabel2 = new ToolStripStatusLabel();
        this.toolStripStatusLabel1 = new ToolStripStatusLabel();
        this.ChangeModeButton = new ToolStripSplitButton();
        this.ShowResolvedMenuItem = new ToolStripMenuItem();
        this.panel1 = new Panel();
        this.ResolveButton = new Button();
        this.RemoveButton = new Button();
        this.EditButton = new Button();
        this.AddButton = new Button();
        this.ToDoList = new ListView();
        this.columnHeader4 = new ColumnHeader();
        this.columnHeader1 = new ColumnHeader();
        this.columnHeader5 = new ColumnHeader();
        this.statusStrip1.SuspendLayout();
        this.panel1.SuspendLayout();
        SuspendLayout();
        // 
        // statusStrip1
        // 
        this.statusStrip1.ImageScalingSize = new Size(24, 24);
        this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.StatusLabel, this.toolStripStatusLabel2, this.toolStripStatusLabel1, this.ChangeModeButton });
        this.statusStrip1.Location = new Point(0, 733);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new Size(1542, 32);
        this.statusStrip1.TabIndex = 1;
        this.statusStrip1.Text = "statusStrip1";
        // 
        // StatusLabel
        // 
        this.StatusLabel.Name = "StatusLabel";
        this.StatusLabel.Size = new Size(179, 25);
        this.StatusLabel.Text = "toolStripStatusLabel1";
        // 
        // toolStripStatusLabel2
        // 
        this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
        this.toolStripStatusLabel2.Size = new Size(1226, 25);
        this.toolStripStatusLabel2.Spring = true;
        // 
        // toolStripStatusLabel1
        // 
        this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        this.toolStripStatusLabel1.Size = new Size(0, 25);
        // 
        // ChangeModeButton
        // 
        this.ChangeModeButton.AutoToolTip = false;
        this.ChangeModeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this.ChangeModeButton.DropDownItems.AddRange(new ToolStripItem[] { this.ShowResolvedMenuItem });
        this.ChangeModeButton.ImageTransparentColor = Color.Magenta;
        this.ChangeModeButton.Name = "ChangeModeButton";
        this.ChangeModeButton.Size = new Size(122, 29);
        this.ChangeModeButton.Text = "View mode";
        this.ChangeModeButton.TextImageRelation = TextImageRelation.TextAboveImage;
        this.ChangeModeButton.ButtonClick += ChangeModeButton_ButtonClick;
        // 
        // ShowResolvedMenuItem
        // 
        this.ShowResolvedMenuItem.Name = "ShowResolvedMenuItem";
        this.ShowResolvedMenuItem.Size = new Size(278, 34);
        this.ShowResolvedMenuItem.Text = "Show resolved items";
        this.ShowResolvedMenuItem.DropDownClosed += ShowResolvedMenuItem_DropDownClosed;
        this.ShowResolvedMenuItem.DropDownOpened += ShowResolvedMenuItem_DropDownOpened;
        this.ShowResolvedMenuItem.Click += ShowResolved_Click;
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.ResolveButton);
        this.panel1.Controls.Add(this.RemoveButton);
        this.panel1.Controls.Add(this.EditButton);
        this.panel1.Controls.Add(this.AddButton);
        this.panel1.Dock = DockStyle.Right;
        this.panel1.Location = new Point(1441, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(101, 733);
        this.panel1.TabIndex = 3;
        // 
        // ResolveButton
        // 
        this.ResolveButton.Dock = DockStyle.Top;
        this.ResolveButton.Enabled = false;
        this.ResolveButton.Location = new Point(0, 190);
        this.ResolveButton.Name = "ResolveButton";
        this.ResolveButton.Size = new Size(101, 95);
        this.ResolveButton.TabIndex = 3;
        this.ResolveButton.Text = "Resolve";
        this.ResolveButton.TextAlign = ContentAlignment.BottomCenter;
        this.ResolveButton.UseVisualStyleBackColor = true;
        this.ResolveButton.Click += ResolveButton_Click;
        // 
        // RemoveButton
        // 
        this.RemoveButton.Dock = DockStyle.Bottom;
        this.RemoveButton.Enabled = false;
        this.RemoveButton.Location = new Point(0, 638);
        this.RemoveButton.Name = "RemoveButton";
        this.RemoveButton.Size = new Size(101, 95);
        this.RemoveButton.TabIndex = 2;
        this.RemoveButton.Text = "Remove";
        this.RemoveButton.TextAlign = ContentAlignment.BottomCenter;
        this.RemoveButton.UseVisualStyleBackColor = true;
        this.RemoveButton.Click += RemoveButton_Click;
        // 
        // EditButton
        // 
        this.EditButton.Dock = DockStyle.Top;
        this.EditButton.Enabled = false;
        this.EditButton.Location = new Point(0, 95);
        this.EditButton.Name = "EditButton";
        this.EditButton.Size = new Size(101, 95);
        this.EditButton.TabIndex = 1;
        this.EditButton.Text = "Edit";
        this.EditButton.TextAlign = ContentAlignment.BottomCenter;
        this.EditButton.UseVisualStyleBackColor = true;
        this.EditButton.Click += EditButton_Click;
        // 
        // AddButton
        // 
        this.AddButton.Dock = DockStyle.Top;
        this.AddButton.Location = new Point(0, 0);
        this.AddButton.Name = "AddButton";
        this.AddButton.Size = new Size(101, 95);
        this.AddButton.TabIndex = 0;
        this.AddButton.Text = "Add";
        this.AddButton.TextAlign = ContentAlignment.BottomCenter;
        this.AddButton.UseVisualStyleBackColor = true;
        this.AddButton.Click += AddButton_Click;
        // 
        // ToDoList
        // 
        this.ToDoList.BackColor = SystemColors.Menu;
        this.ToDoList.Columns.AddRange(new ColumnHeader[] { this.columnHeader4, this.columnHeader1, this.columnHeader5 });
        this.ToDoList.Dock = DockStyle.Fill;
        this.ToDoList.FullRowSelect = true;
        this.ToDoList.GridLines = true;
        this.ToDoList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        this.ToDoList.Items.AddRange(new ListViewItem[] { listViewItem1 });
        this.ToDoList.Location = new Point(0, 0);
        this.ToDoList.MultiSelect = false;
        this.ToDoList.Name = "ToDoList";
        this.ToDoList.ShowItemToolTips = true;
        this.ToDoList.Size = new Size(1441, 733);
        this.ToDoList.TabIndex = 4;
        this.ToDoList.UseCompatibleStateImageBehavior = false;
        this.ToDoList.View = View.Details;
        this.ToDoList.SelectedIndexChanged += ToDoList_SelectedIndexChanged;
        this.ToDoList.DoubleClick += ToDoList_DoubleClick;
        // 
        // columnHeader4
        // 
        this.columnHeader4.Text = "Title";
        // 
        // columnHeader1
        // 
        this.columnHeader1.DisplayIndex = 2;
        this.columnHeader1.Text = "State";
        // 
        // columnHeader5
        // 
        this.columnHeader5.DisplayIndex = 1;
        this.columnHeader5.Text = "Created";
        // 
        // ToDoListForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1542, 765);
        Controls.Add(this.ToDoList);
        Controls.Add(this.panel1);
        Controls.Add(this.statusStrip1);
        Name = "ToDoListForm";
        Text = "ToDoListForm";
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.panel1.ResumeLayout(false);
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
    private ToolStripMenuItem ShowResolvedMenuItem;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader1;
}