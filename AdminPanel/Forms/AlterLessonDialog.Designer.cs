namespace AdminPanel.Forms
{
    partial class AlterLessonDialog
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
            this.teacherInput = new System.Windows.Forms.ComboBox();
            this.subjectInput = new System.Windows.Forms.ComboBox();
            this.dateInput = new System.Windows.Forms.DateTimePicker();
            this.topicInput = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тема";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Викладач";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Предмет";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дата і час";
            // 
            // teacherInput
            // 
            this.teacherInput.FormattingEnabled = true;
            this.teacherInput.Location = new System.Drawing.Point(16, 86);
            this.teacherInput.Name = "teacherInput";
            this.teacherInput.Size = new System.Drawing.Size(467, 23);
            this.teacherInput.TabIndex = 4;
            // 
            // subjectInput
            // 
            this.subjectInput.FormattingEnabled = true;
            this.subjectInput.Location = new System.Drawing.Point(16, 178);
            this.subjectInput.Name = "subjectInput";
            this.subjectInput.Size = new System.Drawing.Size(467, 23);
            this.subjectInput.TabIndex = 5;
            // 
            // dateInput
            // 
            this.dateInput.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateInput.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateInput.Location = new System.Drawing.Point(16, 262);
            this.dateInput.Name = "dateInput";
            this.dateInput.Size = new System.Drawing.Size(467, 23);
            this.dateInput.TabIndex = 6;
            // 
            // topicInput
            // 
            this.topicInput.Location = new System.Drawing.Point(16, 27);
            this.topicInput.Name = "topicInput";
            this.topicInput.Size = new System.Drawing.Size(467, 23);
            this.topicInput.TabIndex = 7;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(173, 332);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(272, 332);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Відміна";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // AlterLessonDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(495, 367);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.topicInput);
            this.Controls.Add(this.dateInput);
            this.Controls.Add(this.subjectInput);
            this.Controls.Add(this.teacherInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AlterLessonDialog";
            this.Text = "AlterLessonDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox teacherInput;
        private ComboBox subjectInput;
        private DateTimePicker dateInput;
        private TextBox topicInput;
        private Button okButton;
        private Button cancelButton;
    }
}