namespace Rejive
{
    partial class TestForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Item 1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("01- blah blah blah");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("02 - Blah blue green");
            this.checkLabel1 = new Rejive.CheckLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // checkLabel1
            // 
            this.checkLabel1.Alpha = 160;
            this.checkLabel1.AutoSize = true;
            this.checkLabel1.Checked = false;
            this.checkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkLabel1.Location = new System.Drawing.Point(45, 92);
            this.checkLabel1.Name = "checkLabel1";
            this.checkLabel1.Size = new System.Drawing.Size(19, 13);
            this.checkLabel1.TabIndex = 0;
            this.checkLabel1.Text = ">>";
            this.checkLabel1.Click += new System.EventHandler(this.checkLabel1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.AutoArrange = false;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(0, 119);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(284, 211);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 330);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckLabel checkLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
    }
}