namespace BankSimulation
{
    partial class Form1
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
            this.lblTicket = new System.Windows.Forms.Label();
            this.lblServing = new System.Windows.Forms.Label();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTicket
            // 
            this.lblTicket.AutoEllipsis = true;
            this.lblTicket.AutoSize = true;
            this.lblTicket.Location = new System.Drawing.Point(263, 321);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(62, 18);
            this.lblTicket.TabIndex = 0;
            this.lblTicket.Text = "label1";
            // 
            // lblServing
            // 
            this.lblServing.AutoSize = true;
            this.lblServing.Location = new System.Drawing.Point(463, 321);
            this.lblServing.Name = "lblServing";
            this.lblServing.Size = new System.Drawing.Size(62, 18);
            this.lblServing.TabIndex = 1;
            this.lblServing.Text = "label2";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(257, 110);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(284, 98);
            this.btnCustomer.TabIndex = 2;
            this.btnCustomer.Text = "button1";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 481);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.lblServing);
            this.Controls.Add(this.lblTicket);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.Label lblServing;
        private System.Windows.Forms.Button btnCustomer;
    }
}

