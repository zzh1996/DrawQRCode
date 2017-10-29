namespace DrawQRCode
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.QRCodeDisplay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.QRCodeDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // QRCodeDisplay
            // 
            this.QRCodeDisplay.Location = new System.Drawing.Point(27, 65);
            this.QRCodeDisplay.Name = "QRCodeDisplay";
            this.QRCodeDisplay.Size = new System.Drawing.Size(437, 388);
            this.QRCodeDisplay.TabIndex = 0;
            this.QRCodeDisplay.TabStop = false;
            this.QRCodeDisplay.Click += new System.EventHandler(this.QRCodeDisplay_Click);
            this.QRCodeDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.QRCodeDisplay_MouseDown);
            this.QRCodeDisplay.MouseMove += new System.Windows.Forms.MouseEventHandler(this.QRCodeDisplay_MouseMove);
            this.QRCodeDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.QRCodeDisplay_MouseUp);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 608);
            this.Controls.Add(this.QRCodeDisplay);
            this.Name = "FormMain";
            this.Text = "DrawQRCode";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.QRCodeDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox QRCodeDisplay;
    }
}

