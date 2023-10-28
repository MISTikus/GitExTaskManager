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
        statusStrip1 = new StatusStrip();
        toDoList = new ListView();
        SuspendLayout();
        // 
        // statusStrip1
        // 
        statusStrip1.ImageScalingSize = new Size(24, 24);
        statusStrip1.Location = new Point(0, 743);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(1542, 22);
        statusStrip1.TabIndex = 1;
        statusStrip1.Text = "statusStrip1";
        // 
        // toDoList
        // 
        toDoList.Dock = DockStyle.Fill;
        toDoList.Location = new Point(0, 0);
        toDoList.Name = "toDoList";
        toDoList.Size = new Size(1542, 743);
        toDoList.TabIndex = 2;
        toDoList.UseCompatibleStateImageBehavior = false;
        // 
        // ToDoListForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1542, 765);
        Controls.Add(toDoList);
        Controls.Add(statusStrip1);
        Name = "ToDoListForm";
        Text = "ToDoListForm";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private StatusStrip statusStrip1;
    private ListView toDoList;
}