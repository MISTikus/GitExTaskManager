namespace GitExtensions.TaskManager.Controls;

partial class ToDoListControl
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

    #region Component Designer generated code

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
        this.changeModeButton = new ToolStripSplitButton();
        this.showResolvedMenuItem = new ToolStripMenuItem();
        this.panel1 = new Panel();
        this.resolveButton = new Button();
        this.removeButton = new Button();
        this.editButton = new Button();
        this.addButton = new Button();
        this.ToDoList = new ListView();
        this.columnHeader4 = new ColumnHeader();
        this.columnHeader1 = new ColumnHeader();
        this.columnHeader5 = new ColumnHeader();
        this.typeColumnHeader = new ColumnHeader();
        this.statusStrip1.SuspendLayout();
        this.panel1.SuspendLayout();
        SuspendLayout();
        // 
        // statusStrip1
        // 
        this.statusStrip1.ImageScalingSize = new Size(24, 24);
        this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.StatusLabel, this.toolStripStatusLabel2, this.toolStripStatusLabel1, this.changeModeButton });
        this.statusStrip1.Location = new Point(0, 596);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new Size(1160, 32);
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
        this.toolStripStatusLabel2.Size = new Size(844, 25);
        this.toolStripStatusLabel2.Spring = true;
        // 
        // toolStripStatusLabel1
        // 
        this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        this.toolStripStatusLabel1.Size = new Size(0, 25);
        // 
        // changeModeButton
        // 
        this.changeModeButton.AutoToolTip = false;
        this.changeModeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        this.changeModeButton.DropDownItems.AddRange(new ToolStripItem[] { this.showResolvedMenuItem });
        this.changeModeButton.ImageTransparentColor = Color.Magenta;
        this.changeModeButton.Name = "changeModeButton";
        this.changeModeButton.Size = new Size(122, 29);
        this.changeModeButton.Text = "View mode";
        this.changeModeButton.TextImageRelation = TextImageRelation.TextAboveImage;
        this.changeModeButton.ButtonClick += ChangeModeButton_ButtonClick;
        // 
        // showResolvedMenuItem
        // 
        this.showResolvedMenuItem.Name = "showResolvedMenuItem";
        this.showResolvedMenuItem.Size = new Size(278, 34);
        this.showResolvedMenuItem.Text = "Show resolved items";
        this.showResolvedMenuItem.DropDownClosed += ShowResolvedMenuItem_DropDownClosed;
        this.showResolvedMenuItem.DropDownOpened += ShowResolvedMenuItem_DropDownOpened;
        this.showResolvedMenuItem.Click += ShowResolved_Click;
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.resolveButton);
        this.panel1.Controls.Add(this.removeButton);
        this.panel1.Controls.Add(this.editButton);
        this.panel1.Controls.Add(this.addButton);
        this.panel1.Dock = DockStyle.Right;
        this.panel1.Location = new Point(1059, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(101, 596);
        this.panel1.TabIndex = 3;
        // 
        // resolveButton
        // 
        this.resolveButton.Dock = DockStyle.Top;
        this.resolveButton.Enabled = false;
        this.resolveButton.Location = new Point(0, 190);
        this.resolveButton.Name = "resolveButton";
        this.resolveButton.Size = new Size(101, 95);
        this.resolveButton.TabIndex = 3;
        this.resolveButton.Text = "Resolve";
        this.resolveButton.TextAlign = ContentAlignment.BottomCenter;
        this.resolveButton.UseVisualStyleBackColor = true;
        this.resolveButton.Click += ResolveButton_Click;
        // 
        // removeButton
        // 
        this.removeButton.Dock = DockStyle.Bottom;
        this.removeButton.Enabled = false;
        this.removeButton.Location = new Point(0, 501);
        this.removeButton.Name = "removeButton";
        this.removeButton.Size = new Size(101, 95);
        this.removeButton.TabIndex = 2;
        this.removeButton.Text = "Remove";
        this.removeButton.TextAlign = ContentAlignment.BottomCenter;
        this.removeButton.UseVisualStyleBackColor = true;
        this.removeButton.Click += RemoveButton_Click;
        // 
        // editButton
        // 
        this.editButton.Dock = DockStyle.Top;
        this.editButton.Enabled = false;
        this.editButton.Location = new Point(0, 95);
        this.editButton.Name = "editButton";
        this.editButton.Size = new Size(101, 95);
        this.editButton.TabIndex = 1;
        this.editButton.Text = "Edit";
        this.editButton.TextAlign = ContentAlignment.BottomCenter;
        this.editButton.UseVisualStyleBackColor = true;
        this.editButton.Click += EditButton_Click;
        // 
        // addButton
        // 
        this.addButton.Dock = DockStyle.Top;
        this.addButton.Location = new Point(0, 0);
        this.addButton.Name = "addButton";
        this.addButton.Size = new Size(101, 95);
        this.addButton.TabIndex = 0;
        this.addButton.Text = "Add";
        this.addButton.TextAlign = ContentAlignment.BottomCenter;
        this.addButton.UseVisualStyleBackColor = true;
        this.addButton.Click += AddButton_Click;
        // 
        // ToDoList
        // 
        this.ToDoList.BackColor = SystemColors.Menu;
        this.ToDoList.Columns.AddRange(new ColumnHeader[] { this.typeColumnHeader, this.columnHeader4, this.columnHeader1, this.columnHeader5 });
        this.ToDoList.Dock = DockStyle.Fill;
        this.ToDoList.FullRowSelect = true;
        this.ToDoList.GridLines = true;
        this.ToDoList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        this.ToDoList.Items.AddRange(new ListViewItem[] { listViewItem1 });
        this.ToDoList.Location = new Point(0, 0);
        this.ToDoList.MultiSelect = false;
        this.ToDoList.Name = "ToDoList";
        this.ToDoList.ShowItemToolTips = true;
        this.ToDoList.Size = new Size(1059, 596);
        this.ToDoList.TabIndex = 4;
        this.ToDoList.UseCompatibleStateImageBehavior = false;
        this.ToDoList.View = View.Details;
        this.ToDoList.SelectedIndexChanged += ToDoList_SelectedIndexChanged;
        this.ToDoList.DoubleClick += ToDoList_DoubleClick;
        // 
        // columnHeader4
        // 
        this.columnHeader4.DisplayIndex = 0;
        this.columnHeader4.Text = "Title";
        // 
        // columnHeader1
        // 
        this.columnHeader1.Text = "State";
        // 
        // columnHeader5
        // 
        this.columnHeader5.DisplayIndex = 1;
        this.columnHeader5.Text = "Created";
        // 
        // typeColumnHeader
        // 
        this.typeColumnHeader.DisplayIndex = 3;
        this.typeColumnHeader.Text = "Type";
        // 
        // ToDoListControl
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(this.ToDoList);
        Controls.Add(this.panel1);
        Controls.Add(this.statusStrip1);
        Name = "ToDoListControl";
        Size = new Size(1160, 628);
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
    private Button editButton;
    private Button addButton;
    private Button resolveButton;
    private Button removeButton;
    private ToolStripSplitButton changeModeButton;
    private ToolStripMenuItem showResolvedMenuItem;
    private ToolStripStatusLabel toolStripStatusLabel2;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader1;
    private ColumnHeader typeColumnHeader;
}
