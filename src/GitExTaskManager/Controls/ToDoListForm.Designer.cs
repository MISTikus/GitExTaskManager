namespace GitExtensions.TaskManager.Controls;

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
        this.epicPanel = new Panel();
        this.epicGroup = new GroupBox();
        this.panel3 = new Panel();
        this.epicCancelButton = new Button();
        this.epicSaveButton = new Button();
        this.panel1 = new Panel();
        this.epicTitleBox = new TextBox();
        this.label1 = new Label();
        this.panel2 = new Panel();
        this.splitContainer = new SplitContainer();
        this.issuesListControl = new ToDoListControl();
        this.backlogListControl = new ToDoListControl();
        this.epicPanel.SuspendLayout();
        this.epicGroup.SuspendLayout();
        this.panel3.SuspendLayout();
        this.panel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.splitContainer).BeginInit();
        this.splitContainer.Panel1.SuspendLayout();
        this.splitContainer.Panel2.SuspendLayout();
        this.splitContainer.SuspendLayout();
        SuspendLayout();
        // 
        // epicPanel
        // 
        this.epicPanel.Controls.Add(this.epicGroup);
        this.epicPanel.Dock = DockStyle.Left;
        this.epicPanel.Location = new Point(0, 0);
        this.epicPanel.Name = "epicPanel";
        this.epicPanel.Size = new Size(299, 765);
        this.epicPanel.TabIndex = 0;
        // 
        // epicGroup
        // 
        this.epicGroup.Controls.Add(this.panel3);
        this.epicGroup.Controls.Add(this.panel1);
        this.epicGroup.Controls.Add(this.epicTitleBox);
        this.epicGroup.Controls.Add(this.label1);
        this.epicGroup.Dock = DockStyle.Fill;
        this.epicGroup.Location = new Point(0, 0);
        this.epicGroup.Name = "epicGroup";
        this.epicGroup.Padding = new Padding(16);
        this.epicGroup.Size = new Size(299, 765);
        this.epicGroup.TabIndex = 0;
        this.epicGroup.TabStop = false;
        this.epicGroup.Text = "Epic";
        // 
        // panel3
        // 
        this.panel3.Controls.Add(this.epicCancelButton);
        this.panel3.Controls.Add(this.epicSaveButton);
        this.panel3.Dock = DockStyle.Top;
        this.panel3.Location = new Point(16, 104);
        this.panel3.Name = "panel3";
        this.panel3.Size = new Size(267, 46);
        this.panel3.TabIndex = 3;
        // 
        // epicCancelButton
        // 
        this.epicCancelButton.BackColor = Color.Salmon;
        this.epicCancelButton.Dock = DockStyle.Fill;
        this.epicCancelButton.Location = new Point(133, 0);
        this.epicCancelButton.Name = "epicCancelButton";
        this.epicCancelButton.Size = new Size(134, 46);
        this.epicCancelButton.TabIndex = 1;
        this.epicCancelButton.Text = "Cancel";
        this.epicCancelButton.UseVisualStyleBackColor = false;
        this.epicCancelButton.Click += EpicCancelButton_Click;
        // 
        // epicSaveButton
        // 
        this.epicSaveButton.BackColor = Color.YellowGreen;
        this.epicSaveButton.Dock = DockStyle.Left;
        this.epicSaveButton.Location = new Point(0, 0);
        this.epicSaveButton.Name = "epicSaveButton";
        this.epicSaveButton.Size = new Size(133, 46);
        this.epicSaveButton.TabIndex = 0;
        this.epicSaveButton.Text = "Save";
        this.epicSaveButton.UseVisualStyleBackColor = false;
        this.epicSaveButton.Click += EpicSaveButton_Click;
        // 
        // panel1
        // 
        this.panel1.Dock = DockStyle.Top;
        this.panel1.Location = new Point(16, 96);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(267, 8);
        this.panel1.TabIndex = 2;
        // 
        // epicTitleBox
        // 
        this.epicTitleBox.Dock = DockStyle.Top;
        this.epicTitleBox.Location = new Point(16, 65);
        this.epicTitleBox.Name = "epicTitleBox";
        this.epicTitleBox.Size = new Size(267, 31);
        this.epicTitleBox.TabIndex = 1;
        this.epicTitleBox.TextChanged += EpicTitleBox_TextChanged;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Dock = DockStyle.Top;
        this.label1.Location = new Point(16, 40);
        this.label1.Name = "label1";
        this.label1.Size = new Size(44, 25);
        this.label1.TabIndex = 0;
        this.label1.Text = "Title";
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.splitContainer);
        this.panel2.Dock = DockStyle.Fill;
        this.panel2.Location = new Point(299, 0);
        this.panel2.Name = "panel2";
        this.panel2.Size = new Size(1243, 765);
        this.panel2.TabIndex = 1;
        // 
        // splitContainer
        // 
        this.splitContainer.Dock = DockStyle.Fill;
        this.splitContainer.Location = new Point(0, 0);
        this.splitContainer.Name = "splitContainer";
        this.splitContainer.Orientation = Orientation.Horizontal;
        // 
        // splitContainer.Panel1
        // 
        this.splitContainer.Panel1.Controls.Add(this.issuesListControl);
        // 
        // splitContainer.Panel2
        // 
        this.splitContainer.Panel2.Controls.Add(this.backlogListControl);
        this.splitContainer.Size = new Size(1243, 765);
        this.splitContainer.SplitterDistance = 358;
        this.splitContainer.TabIndex = 0;
        // 
        // issuesListControl
        // 
        this.issuesListControl.Dock = DockStyle.Fill;
        this.issuesListControl.Location = new Point(0, 0);
        this.issuesListControl.Name = "issuesListControl";
        this.issuesListControl.Size = new Size(1243, 358);
        this.issuesListControl.TabIndex = 0;
        // 
        // backlogListControl
        // 
        this.backlogListControl.Dock = DockStyle.Fill;
        this.backlogListControl.Location = new Point(0, 0);
        this.backlogListControl.Name = "backlogListControl";
        this.backlogListControl.Size = new Size(1243, 403);
        this.backlogListControl.TabIndex = 0;
        // 
        // ToDoListForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1542, 765);
        Controls.Add(this.panel2);
        Controls.Add(this.epicPanel);
        Name = "ToDoListForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ToDoListForm";
        this.epicPanel.ResumeLayout(false);
        this.epicGroup.ResumeLayout(false);
        this.epicGroup.PerformLayout();
        this.panel3.ResumeLayout(false);
        this.panel2.ResumeLayout(false);
        this.splitContainer.Panel1.ResumeLayout(false);
        this.splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.splitContainer).EndInit();
        this.splitContainer.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel epicPanel;
    private Panel panel2;
    private GroupBox epicGroup;
    private Panel panel3;
    private Button epicCancelButton;
    private Button epicSaveButton;
    private Panel panel1;
    private TextBox epicTitleBox;
    private Label label1;
    private SplitContainer splitContainer;
    private ToDoListControl issuesListControl;
    private ToDoListControl backlogListControl;
}