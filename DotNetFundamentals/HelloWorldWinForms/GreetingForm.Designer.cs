namespace HelloWorldWinForms
{
    partial class GreetingForm
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
            this.submit = new System.Windows.Forms.Button();
            this.names = new System.Windows.Forms.TextBox();
            this.namesLabel = new System.Windows.Forms.Label();
            this.greetings = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(403, 100);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(145, 48);
            this.submit.TabIndex = 0;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // names
            // 
            this.names.Location = new System.Drawing.Point(181, 68);
            this.names.Name = "names";
            this.names.Size = new System.Drawing.Size(367, 26);
            this.names.TabIndex = 1;
            // 
            // namesLabel
            // 
            this.namesLabel.AutoSize = true;
            this.namesLabel.Location = new System.Drawing.Point(177, 45);
            this.namesLabel.Name = "namesLabel";
            this.namesLabel.Size = new System.Drawing.Size(242, 20);
            this.namesLabel.TabIndex = 2;
            this.namesLabel.Text = "Enter names (comma separated)";
            // 
            // greetings
            // 
            this.greetings.AutoSize = true;
            this.greetings.Location = new System.Drawing.Point(177, 216);
            this.greetings.Name = "greetings";
            this.greetings.Size = new System.Drawing.Size(0, 20);
            this.greetings.TabIndex = 3;
            // 
            // GreetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.greetings);
            this.Controls.Add(this.namesLabel);
            this.Controls.Add(this.names);
            this.Controls.Add(this.submit);
            this.Name = "GreetingForm";
            this.Text = "Greeting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.TextBox names;
        private System.Windows.Forms.Label namesLabel;
        private System.Windows.Forms.Label greetings;
    }
}

