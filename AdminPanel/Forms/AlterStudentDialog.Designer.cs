namespace AdminPanel.Forms
{
    partial class AlterStudentDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.dateOfBirthInput = new System.Windows.Forms.DateTimePicker();
            this.groupInput = new System.Windows.Forms.ComboBox();
            this.surnameInput = new System.Windows.Forms.TextBox();
            this.firstnameInput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.accountInput = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ім\'я";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Прізвище";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Група";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дата народження";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(92, 353);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(173, 353);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Відміна";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // dateOfBirthInput
            // 
            this.dateOfBirthInput.Location = new System.Drawing.Point(12, 259);
            this.dateOfBirthInput.Name = "dateOfBirthInput";
            this.dateOfBirthInput.Size = new System.Drawing.Size(365, 23);
            this.dateOfBirthInput.TabIndex = 6;
            // 
            // groupInput
            // 
            this.groupInput.FormattingEnabled = true;
            this.groupInput.Location = new System.Drawing.Point(12, 178);
            this.groupInput.Name = "groupInput";
            this.groupInput.Size = new System.Drawing.Size(365, 23);
            this.groupInput.TabIndex = 7;
            // 
            // surnameInput
            // 
            this.surnameInput.Location = new System.Drawing.Point(12, 71);
            this.surnameInput.Name = "surnameInput";
            this.surnameInput.Size = new System.Drawing.Size(365, 23);
            this.surnameInput.TabIndex = 8;
            // 
            // firstnameInput
            // 
            this.firstnameInput.Location = new System.Drawing.Point(12, 27);
            this.firstnameInput.Name = "firstnameInput";
            this.firstnameInput.Size = new System.Drawing.Size(365, 23);
            this.firstnameInput.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Обліковий запис";
            // 
            // accountInput
            // 
            this.accountInput.FormattingEnabled = true;
            this.accountInput.Location = new System.Drawing.Point(12, 115);
            this.accountInput.Name = "accountInput";
            this.accountInput.Size = new System.Drawing.Size(365, 23);
            this.accountInput.TabIndex = 11;
            // 
            // AlterStudentDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(389, 388);
            this.Controls.Add(this.accountInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.firstnameInput);
            this.Controls.Add(this.surnameInput);
            this.Controls.Add(this.groupInput);
            this.Controls.Add(this.dateOfBirthInput);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AlterStudentDialog";
            this.Text = "AlterStudentDIalog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button okButton;
        private Button cancelButton;
        private DateTimePicker dateOfBirthInput;
        private ComboBox groupInput;
        private TextBox surnameInput;
        private TextBox firstnameInput;
        private Label label5;
        private ComboBox accountInput;
    }
}