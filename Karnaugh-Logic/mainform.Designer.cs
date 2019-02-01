namespace Karnaugh_Logic
{
    partial class mainWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.menu = new System.Windows.Forms.ToolStripDropDownButton();
            this.Exit_item = new System.Windows.Forms.ToolStripMenuItem();
            this.バージョンToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.PythonPathBox = new System.Windows.Forms.TextBox();
            this.PythonPathButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.afterExpBox = new System.Windows.Forms.TextBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.LogicTexBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PythonFileBrowser = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Status";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // menu
            // 
            this.menu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Exit_item,
            this.バージョンToolStripMenuItem});
            this.menu.Image = ((System.Drawing.Image)(resources.GetObject("menu.Image")));
            this.menu.ImageTransparentColor = System.Drawing.Color.LightSteelBlue;
            this.menu.Name = "menu";
            this.menu.RightToLeftAutoMirrorImage = true;
            this.menu.Size = new System.Drawing.Size(53, 22);
            this.menu.Text = "メニュー";
            this.menu.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Exit_item
            // 
            this.Exit_item.Name = "Exit_item";
            this.Exit_item.Size = new System.Drawing.Size(180, 22);
            this.Exit_item.Text = "終了";
            this.Exit_item.Click += new System.EventHandler(this.Exit_item_Click);
            // 
            // バージョンToolStripMenuItem
            // 
            this.バージョンToolStripMenuItem.Name = "バージョンToolStripMenuItem";
            this.バージョンToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.バージョンToolStripMenuItem.Text = "バージョン";
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.IsSplitterFixed = true;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.label3);
            this.mainSplitContainer.Panel1.Controls.Add(this.PythonPathBox);
            this.mainSplitContainer.Panel1.Controls.Add(this.PythonPathButton);
            this.mainSplitContainer.Panel1.Controls.Add(this.label2);
            this.mainSplitContainer.Panel1.Controls.Add(this.afterExpBox);
            this.mainSplitContainer.Panel1.Controls.Add(this.RunButton);
            this.mainSplitContainer.Panel1.Controls.Add(this.LogicTexBox);
            this.mainSplitContainer.Panel1.Controls.Add(this.label1);
            this.mainSplitContainer.Size = new System.Drawing.Size(800, 403);
            this.mainSplitContainer.SplitterDistance = 266;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Python実行環境";
            // 
            // PythonPathBox
            // 
            this.PythonPathBox.Location = new System.Drawing.Point(12, 237);
            this.PythonPathBox.Name = "PythonPathBox";
            this.PythonPathBox.Size = new System.Drawing.Size(251, 19);
            this.PythonPathBox.TabIndex = 6;
            // 
            // PythonPathButton
            // 
            this.PythonPathButton.Location = new System.Drawing.Point(188, 262);
            this.PythonPathButton.Name = "PythonPathButton";
            this.PythonPathButton.Size = new System.Drawing.Size(75, 23);
            this.PythonPathButton.TabIndex = 5;
            this.PythonPathButton.Text = "参照";
            this.PythonPathButton.UseVisualStyleBackColor = true;
            this.PythonPathButton.Click += new System.EventHandler(this.PythonPathButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "簡略化後式";
            // 
            // afterExpBox
            // 
            this.afterExpBox.Location = new System.Drawing.Point(12, 154);
            this.afterExpBox.Multiline = true;
            this.afterExpBox.Name = "afterExpBox";
            this.afterExpBox.Size = new System.Drawing.Size(251, 43);
            this.afterExpBox.TabIndex = 3;
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(12, 73);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(251, 39);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "簡略化";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // LogicTexBox
            // 
            this.LogicTexBox.Location = new System.Drawing.Point(12, 15);
            this.LogicTexBox.Multiline = true;
            this.LogicTexBox.Name = "LogicTexBox";
            this.LogicTexBox.Size = new System.Drawing.Size(251, 52);
            this.LogicTexBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "論理式";
            // 
            // PythonFileBrowser
            // 
            this.PythonFileBrowser.FileName = "openFileDialog1";
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainWindow";
            this.Text = "カルノー図簡略化ツール";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.mainWindow_Paint);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox afterExpBox;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.TextBox LogicTexBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PythonPathBox;
        private System.Windows.Forms.Button PythonPathButton;
        private System.Windows.Forms.OpenFileDialog PythonFileBrowser;
        private System.Windows.Forms.ToolStripDropDownButton menu;
        private System.Windows.Forms.ToolStripMenuItem Exit_item;
        private System.Windows.Forms.ToolStripMenuItem バージョンToolStripMenuItem;
    }
}

