namespace DecimalToBinaryConverter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.decimalTextBox = new System.Windows.Forms.TextBox();
            this.binaryTextBox = new System.Windows.Forms.TextBox();
            this.utf8CheckBox = new System.Windows.Forms.CheckBox();
            this.altFunctionCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // decimalTextBox
            // 
            this.decimalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.decimalTextBox, "decimalTextBox");
            this.decimalTextBox.Name = "decimalTextBox";
            this.decimalTextBox.TextChanged += new System.EventHandler(this.decimalTextBox_TextChanged);
            // 
            // binaryTextBox
            // 
            this.binaryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.binaryTextBox, "binaryTextBox");
            this.binaryTextBox.Name = "binaryTextBox";
            this.binaryTextBox.ReadOnly = true;
            // 
            // utf8CheckBox
            // 
            resources.ApplyResources(this.utf8CheckBox, "utf8CheckBox");
            this.utf8CheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.utf8CheckBox.Name = "utf8CheckBox";
            this.utf8CheckBox.UseVisualStyleBackColor = false;
            this.utf8CheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // altFunctionCheckBox
            // 
            resources.ApplyResources(this.altFunctionCheckBox, "altFunctionCheckBox");
            this.altFunctionCheckBox.BackColor = System.Drawing.SystemColors.Window;
            this.altFunctionCheckBox.Name = "altFunctionCheckBox";
            this.altFunctionCheckBox.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.altFunctionCheckBox);
            this.Controls.Add(this.utf8CheckBox);
            this.Controls.Add(this.binaryTextBox);
            this.Controls.Add(this.decimalTextBox);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox decimalTextBox;
        private System.Windows.Forms.TextBox binaryTextBox;
        private System.Windows.Forms.CheckBox utf8CheckBox;
        private System.Windows.Forms.CheckBox altFunctionCheckBox;
    }
}