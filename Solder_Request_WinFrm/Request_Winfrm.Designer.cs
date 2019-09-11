namespace Solder_Request_WinFrm
{
    partial class Request_Winfrm
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
            this.timer_service = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timer_service)).BeginInit();
            this.SuspendLayout();
            // 
            // timer_service
            // 
            this.timer_service.Enabled = true;
            this.timer_service.SynchronizingObject = this;
            // 
            // Request_Winfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "Request_Winfrm";
            this.Text = "Request_Winfrm";
            this.Load += new System.EventHandler(this.Request_Winfrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timer_service)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Timers.Timer timer_service;
    }
}