namespace GitExtensions.TaskManager.Controls;

partial class ItemForm
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
        this.panel1 = new Panel();
        this.okButton = new Button();
        this.cancelAddButton = new Button();
        this.panel2 = new Panel();
        this.mainBox = new GroupBox();
        this.descriptionBox = new TextBox();
        this.descriptionLabel = new Label();
        this.titleBox = new TextBox();
        this.label1 = new Label();
        this.commentsGroupBox = new GroupBox();
        this.panel4 = new Panel();
        this.commentsPanel = new Panel();
        this.panel3 = new Panel();
        this.commentBox = new TextBox();
        this.addCommentButton = new Button();
        this.panel1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.mainBox.SuspendLayout();
        this.commentsGroupBox.SuspendLayout();
        this.panel4.SuspendLayout();
        this.panel3.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.okButton);
        this.panel1.Controls.Add(this.cancelAddButton);
        this.panel1.Dock = DockStyle.Bottom;
        this.panel1.Location = new Point(0, 490);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(978, 57);
        this.panel1.TabIndex = 0;
        // 
        // okButton
        // 
        this.okButton.BackColor = Color.YellowGreen;
        this.okButton.Dock = DockStyle.Right;
        this.okButton.Enabled = false;
        this.okButton.Location = new Point(678, 0);
        this.okButton.Name = "okButton";
        this.okButton.Size = new Size(150, 57);
        this.okButton.TabIndex = 1;
        this.okButton.Text = "Ok";
        this.okButton.UseVisualStyleBackColor = false;
        this.okButton.Click += OkButton_Click;
        // 
        // cancelAddButton
        // 
        this.cancelAddButton.BackColor = Color.Salmon;
        this.cancelAddButton.Dock = DockStyle.Right;
        this.cancelAddButton.Location = new Point(828, 0);
        this.cancelAddButton.Name = "cancelAddButton";
        this.cancelAddButton.Size = new Size(150, 57);
        this.cancelAddButton.TabIndex = 0;
        this.cancelAddButton.Text = "Cancel";
        this.cancelAddButton.UseVisualStyleBackColor = false;
        this.cancelAddButton.Click += CancelButton_Click;
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.mainBox);
        this.panel2.Controls.Add(this.commentsGroupBox);
        this.panel2.Dock = DockStyle.Fill;
        this.panel2.Location = new Point(0, 0);
        this.panel2.Name = "panel2";
        this.panel2.Size = new Size(978, 490);
        this.panel2.TabIndex = 1;
        // 
        // mainBox
        // 
        this.mainBox.Controls.Add(this.descriptionBox);
        this.mainBox.Controls.Add(this.descriptionLabel);
        this.mainBox.Controls.Add(this.titleBox);
        this.mainBox.Controls.Add(this.label1);
        this.mainBox.Dock = DockStyle.Fill;
        this.mainBox.Location = new Point(0, 0);
        this.mainBox.Name = "mainBox";
        this.mainBox.Padding = new Padding(8, 16, 8, 8);
        this.mainBox.Size = new Size(527, 490);
        this.mainBox.TabIndex = 1;
        this.mainBox.TabStop = false;
        this.mainBox.Text = "Main";
        // 
        // descriptionBox
        // 
        this.descriptionBox.Dock = DockStyle.Fill;
        this.descriptionBox.Location = new Point(8, 121);
        this.descriptionBox.Multiline = true;
        this.descriptionBox.Name = "descriptionBox";
        this.descriptionBox.Size = new Size(511, 361);
        this.descriptionBox.TabIndex = 3;
        this.descriptionBox.TextChanged += DescriptionBox_TextChanged;
        // 
        // descriptionLabel
        // 
        this.descriptionLabel.AutoSize = true;
        this.descriptionLabel.Dock = DockStyle.Top;
        this.descriptionLabel.Location = new Point(8, 96);
        this.descriptionLabel.Name = "descriptionLabel";
        this.descriptionLabel.Size = new Size(102, 25);
        this.descriptionLabel.TabIndex = 2;
        this.descriptionLabel.Text = "Description";
        // 
        // titleBox
        // 
        this.titleBox.Dock = DockStyle.Top;
        this.titleBox.Location = new Point(8, 65);
        this.titleBox.Name = "titleBox";
        this.titleBox.Size = new Size(511, 31);
        this.titleBox.TabIndex = 1;
        this.titleBox.TextChanged += TitleBox_TextChanged;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Dock = DockStyle.Top;
        this.label1.Location = new Point(8, 40);
        this.label1.Name = "label1";
        this.label1.Size = new Size(44, 25);
        this.label1.TabIndex = 0;
        this.label1.Text = "Title";
        // 
        // commentsGroupBox
        // 
        this.commentsGroupBox.Controls.Add(this.panel4);
        this.commentsGroupBox.Dock = DockStyle.Right;
        this.commentsGroupBox.Location = new Point(527, 0);
        this.commentsGroupBox.Name = "commentsGroupBox";
        this.commentsGroupBox.Padding = new Padding(8, 16, 8, 8);
        this.commentsGroupBox.Size = new Size(451, 490);
        this.commentsGroupBox.TabIndex = 0;
        this.commentsGroupBox.TabStop = false;
        this.commentsGroupBox.Text = "Comments";
        // 
        // panel4
        // 
        this.panel4.AutoScroll = true;
        this.panel4.Controls.Add(this.commentsPanel);
        this.panel4.Controls.Add(this.panel3);
        this.panel4.Dock = DockStyle.Fill;
        this.panel4.Location = new Point(8, 40);
        this.panel4.Name = "panel4";
        this.panel4.Size = new Size(435, 442);
        this.panel4.TabIndex = 0;
        // 
        // commentsPanel
        // 
        this.commentsPanel.AutoScroll = true;
        this.commentsPanel.Dock = DockStyle.Fill;
        this.commentsPanel.Location = new Point(0, 0);
        this.commentsPanel.Name = "commentsPanel";
        this.commentsPanel.Size = new Size(435, 290);
        this.commentsPanel.TabIndex = 1;
        // 
        // panel3
        // 
        this.panel3.Controls.Add(this.commentBox);
        this.panel3.Controls.Add(this.addCommentButton);
        this.panel3.Dock = DockStyle.Bottom;
        this.panel3.Location = new Point(0, 290);
        this.panel3.Name = "panel3";
        this.panel3.Size = new Size(435, 152);
        this.panel3.TabIndex = 0;
        // 
        // commentBox
        // 
        this.commentBox.Dock = DockStyle.Fill;
        this.commentBox.Location = new Point(0, 0);
        this.commentBox.Multiline = true;
        this.commentBox.Name = "commentBox";
        this.commentBox.ScrollBars = ScrollBars.Vertical;
        this.commentBox.Size = new Size(435, 118);
        this.commentBox.TabIndex = 1;
        // 
        // addCommentButton
        // 
        this.addCommentButton.Dock = DockStyle.Bottom;
        this.addCommentButton.Location = new Point(0, 118);
        this.addCommentButton.Name = "addCommentButton";
        this.addCommentButton.Size = new Size(435, 34);
        this.addCommentButton.TabIndex = 0;
        this.addCommentButton.Text = "Add comment";
        this.addCommentButton.UseVisualStyleBackColor = true;
        this.addCommentButton.Click += AddComment_Click;
        // 
        // ItemForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(978, 547);
        Controls.Add(this.panel2);
        Controls.Add(this.panel1);
        Name = "ItemForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ItemForm";
        this.panel1.ResumeLayout(false);
        this.panel2.ResumeLayout(false);
        this.mainBox.ResumeLayout(false);
        this.mainBox.PerformLayout();
        this.commentsGroupBox.ResumeLayout(false);
        this.panel4.ResumeLayout(false);
        this.panel3.ResumeLayout(false);
        this.panel3.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private Panel panel2;
    private GroupBox mainBox;
    private TextBox descriptionBox;
    private Label descriptionLabel;
    private TextBox titleBox;
    private Label label1;
    private GroupBox commentsGroupBox;
    private Button okButton;
    private Button cancelAddButton;
    private Panel panel4;
    private Panel panel3;
    private TextBox commentBox;
    private Button addCommentButton;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel commentsPanel;
}