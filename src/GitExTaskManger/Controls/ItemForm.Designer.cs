namespace GitExTaskManger.Controls;

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
        panel1 = new Panel();
        OkButton = new Button();
        CancelAddButton = new Button();
        panel2 = new Panel();
        MainBox = new GroupBox();
        DescriptionBox = new TextBox();
        label2 = new Label();
        TitleBox = new TextBox();
        label1 = new Label();
        groupBox2 = new GroupBox();
        panel4 = new Panel();
        panel3 = new Panel();
        CommentBox = new TextBox();
        AddCommentButton = new Button();
        CommentsPanel = new Panel();
        panel1.SuspendLayout();
        panel2.SuspendLayout();
        MainBox.SuspendLayout();
        groupBox2.SuspendLayout();
        panel4.SuspendLayout();
        panel3.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(OkButton);
        panel1.Controls.Add(CancelAddButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 490);
        panel1.Name = "panel1";
        panel1.Size = new Size(978, 57);
        panel1.TabIndex = 0;
        // 
        // OkButton
        // 
        OkButton.BackColor = Color.YellowGreen;
        OkButton.Dock = DockStyle.Right;
        OkButton.Enabled = false;
        OkButton.Location = new Point(678, 0);
        OkButton.Name = "OkButton";
        OkButton.Size = new Size(150, 57);
        OkButton.TabIndex = 1;
        OkButton.Text = "Ok";
        OkButton.UseVisualStyleBackColor = false;
        OkButton.Click += OkButton_Click;
        // 
        // CancelAddButton
        // 
        CancelAddButton.BackColor = Color.Salmon;
        CancelAddButton.Dock = DockStyle.Right;
        CancelAddButton.Location = new Point(828, 0);
        CancelAddButton.Name = "CancelAddButton";
        CancelAddButton.Size = new Size(150, 57);
        CancelAddButton.TabIndex = 0;
        CancelAddButton.Text = "Cancel";
        CancelAddButton.UseVisualStyleBackColor = false;
        CancelAddButton.Click += CancelButton_Click;
        // 
        // panel2
        // 
        panel2.Controls.Add(MainBox);
        panel2.Controls.Add(groupBox2);
        panel2.Dock = DockStyle.Fill;
        panel2.Location = new Point(0, 0);
        panel2.Name = "panel2";
        panel2.Size = new Size(978, 490);
        panel2.TabIndex = 1;
        // 
        // MainBox
        // 
        MainBox.Controls.Add(DescriptionBox);
        MainBox.Controls.Add(label2);
        MainBox.Controls.Add(TitleBox);
        MainBox.Controls.Add(label1);
        MainBox.Dock = DockStyle.Fill;
        MainBox.Location = new Point(0, 0);
        MainBox.Name = "MainBox";
        MainBox.Padding = new Padding(8, 16, 8, 8);
        MainBox.Size = new Size(527, 490);
        MainBox.TabIndex = 1;
        MainBox.TabStop = false;
        MainBox.Text = "Main";
        // 
        // DescriptionBox
        // 
        DescriptionBox.Dock = DockStyle.Fill;
        DescriptionBox.Location = new Point(8, 121);
        DescriptionBox.Multiline = true;
        DescriptionBox.Name = "DescriptionBox";
        DescriptionBox.Size = new Size(511, 361);
        DescriptionBox.TabIndex = 3;
        DescriptionBox.TextChanged += DescriptionBox_TextChanged;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Dock = DockStyle.Top;
        label2.Location = new Point(8, 96);
        label2.Name = "label2";
        label2.Size = new Size(102, 25);
        label2.TabIndex = 2;
        label2.Text = "Description";
        // 
        // TitleBox
        // 
        TitleBox.Dock = DockStyle.Top;
        TitleBox.Location = new Point(8, 65);
        TitleBox.Name = "TitleBox";
        TitleBox.Size = new Size(511, 31);
        TitleBox.TabIndex = 1;
        TitleBox.TextChanged += TitleBox_TextChanged;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Dock = DockStyle.Top;
        label1.Location = new Point(8, 40);
        label1.Name = "label1";
        label1.Size = new Size(44, 25);
        label1.TabIndex = 0;
        label1.Text = "Title";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(panel4);
        groupBox2.Dock = DockStyle.Right;
        groupBox2.Location = new Point(527, 0);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(8, 16, 8, 8);
        groupBox2.Size = new Size(451, 490);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "Comments";
        // 
        // panel4
        // 
        panel4.AutoScroll = true;
        panel4.Controls.Add(CommentsPanel);
        panel4.Controls.Add(panel3);
        panel4.Dock = DockStyle.Fill;
        panel4.Location = new Point(8, 40);
        panel4.Name = "panel4";
        panel4.Size = new Size(435, 442);
        panel4.TabIndex = 0;
        // 
        // panel3
        // 
        panel3.Controls.Add(CommentBox);
        panel3.Controls.Add(AddCommentButton);
        panel3.Dock = DockStyle.Bottom;
        panel3.Location = new Point(0, 290);
        panel3.Name = "panel3";
        panel3.Size = new Size(435, 152);
        panel3.TabIndex = 0;
        // 
        // CommentBox
        // 
        CommentBox.Dock = DockStyle.Fill;
        CommentBox.Location = new Point(0, 0);
        CommentBox.Multiline = true;
        CommentBox.Name = "CommentBox";
        CommentBox.ScrollBars = ScrollBars.Vertical;
        CommentBox.Size = new Size(435, 118);
        CommentBox.TabIndex = 1;
        // 
        // AddCommentButton
        // 
        AddCommentButton.Dock = DockStyle.Bottom;
        AddCommentButton.Location = new Point(0, 118);
        AddCommentButton.Name = "AddCommentButton";
        AddCommentButton.Size = new Size(435, 34);
        AddCommentButton.TabIndex = 0;
        AddCommentButton.Text = "Add comment";
        AddCommentButton.UseVisualStyleBackColor = true;
        AddCommentButton.Click += AddComment_Click;
        // 
        // CommentsPanel
        // 
        CommentsPanel.AutoScroll = true;
        CommentsPanel.Dock = DockStyle.Fill;
        CommentsPanel.Location = new Point(0, 0);
        CommentsPanel.Name = "CommentsPanel";
        CommentsPanel.Size = new Size(435, 290);
        CommentsPanel.TabIndex = 1;
        // 
        // ItemForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(978, 547);
        Controls.Add(panel2);
        Controls.Add(panel1);
        Name = "ItemForm";
        Text = "ItemForm";
        panel1.ResumeLayout(false);
        panel2.ResumeLayout(false);
        MainBox.ResumeLayout(false);
        MainBox.PerformLayout();
        groupBox2.ResumeLayout(false);
        panel4.ResumeLayout(false);
        panel3.ResumeLayout(false);
        panel3.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private Panel panel2;
    private GroupBox MainBox;
    private TextBox DescriptionBox;
    private Label label2;
    private TextBox TitleBox;
    private Label label1;
    private GroupBox groupBox2;
    private Button OkButton;
    private Button CancelAddButton;
    private Panel panel4;
    private Panel panel3;
    private TextBox CommentBox;
    private Button AddCommentButton;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel CommentsPanel;
}