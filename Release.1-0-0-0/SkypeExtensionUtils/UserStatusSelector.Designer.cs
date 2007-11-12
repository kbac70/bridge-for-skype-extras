namespace Skype.Extension.Utils
{
    partial class UserStatusSelector
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
            this.combo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // combo
            // 
            this.combo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.combo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combo.DropDownHeight = 96;
            this.combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo.FormattingEnabled = true;
            this.combo.IntegralHeight = false;
            this.combo.ItemHeight = 16;
            this.combo.Location = new System.Drawing.Point(0, 0);
            this.combo.MaxDropDownItems = 6;
            this.combo.Name = "combo";
            this.combo.Size = new System.Drawing.Size(131, 22);
            this.combo.TabIndex = 0;
            this.combo.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.combo_DrawItem);
            this.combo.SelectedIndexChanged += new System.EventHandler(this.combo_SelectedIndexChanged);
            // 
            // UserStatusSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combo);
            this.Name = "UserStatusSelector";
            this.Size = new System.Drawing.Size(131, 22);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox combo;
    }
}
