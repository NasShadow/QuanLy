namespace sidebartest
{
    partial class formSubmenu2
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
            this.btn_reload = new Guna.UI2.WinForms.Guna2Button();
            this.txt_Searchtest = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // btn_reload
            // 
            this.btn_reload.BackColor = System.Drawing.Color.Transparent;
            this.btn_reload.DefaultAutoSize = true;
            this.btn_reload.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_reload.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_reload.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_reload.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_reload.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_reload.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_reload.Font = new System.Drawing.Font("SVN-Cintra", 18F);
            this.btn_reload.ForeColor = System.Drawing.Color.White;
            this.btn_reload.Location = new System.Drawing.Point(0, 0);
            this.btn_reload.Name = "btn_reload";
            this.btn_reload.Size = new System.Drawing.Size(1769, 43);
            this.btn_reload.TabIndex = 29;
            this.btn_reload.Text = "RELOAD";
            this.btn_reload.Click += new System.EventHandler(this.btn_reload_Click);
            // 
            // txt_Searchtest
            // 
            this.txt_Searchtest.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Searchtest.DefaultText = "";
            this.txt_Searchtest.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Searchtest.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Searchtest.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Searchtest.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Searchtest.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Searchtest.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Searchtest.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Searchtest.Location = new System.Drawing.Point(100, 129);
            this.txt_Searchtest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_Searchtest.Name = "txt_Searchtest";
            this.txt_Searchtest.PasswordChar = '\0';
            this.txt_Searchtest.PlaceholderText = "Search...";
            this.txt_Searchtest.SelectedText = "";
            this.txt_Searchtest.Size = new System.Drawing.Size(300, 44);
            this.txt_Searchtest.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.txt_Searchtest.TabIndex = 30;
            this.txt_Searchtest.TextChanged += new System.EventHandler(this.guna2TextBox1_TextChanged);
            this.txt_Searchtest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Searchtest_KeyPress);
            // 
            // formSubmenu2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1769, 1035);
            this.Controls.Add(this.txt_Searchtest);
            this.Controls.Add(this.btn_reload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formSubmenu2";
            this.Text = "formSubmenu2";
            this.Load += new System.EventHandler(this.formSubmenu2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_reload;
        private Guna.UI2.WinForms.Guna2TextBox txt_Searchtest;
    }
}