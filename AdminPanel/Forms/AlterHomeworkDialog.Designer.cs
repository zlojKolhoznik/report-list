namespace AdminPanel.Forms
{
    partial class AlterHomeworkDialog
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
            this.label5 = new System.Windows.Forms.Label();
            this.fileSelectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.dueDateInput = new System.Windows.Forms.DateTimePicker();
            this.groupInput = new System.Windows.Forms.ComboBox();
            this.teacherInput = new System.Windows.Forms.ComboBox();
            this.subjectInput = new System.Windows.Forms.ComboBox();
            this.filePathOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Викладач";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Предмет";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Шлях до файлу";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Група";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Дата здачі";
            // 
            // fileSelectButton
            // 
            this.fileSelectButton.Location = new System.Drawing.Point(427, 346);
            this.fileSelectButton.Name = "fileSelectButton";
            this.fileSelectButton.Size = new System.Drawing.Size(75, 23);
            this.fileSelectButton.TabIndex = 5;
            this.fileSelectButton.Text = "Обрати";
            this.fileSelectButton.UseVisualStyleBackColor = true;
            this.fileSelectButton.Click += new System.EventHandler(this.OnFileSelectButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(297, 415);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Відміна";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(216, 415);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 7;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // dueDateInput
            // 
            this.dueDateInput.Location = new System.Drawing.Point(12, 267);
            this.dueDateInput.Name = "dueDateInput";
            this.dueDateInput.Size = new System.Drawing.Size(490, 23);
            this.dueDateInput.TabIndex = 8;
            // 
            // groupInput
            // 
            this.groupInput.FormattingEnabled = true;
            this.groupInput.Location = new System.Drawing.Point(12, 187);
            this.groupInput.Name = "groupInput";
            this.groupInput.Size = new System.Drawing.Size(490, 23);
            this.groupInput.TabIndex = 9;
            // 
            // teacherInput
            // 
            this.teacherInput.FormattingEnabled = true;
            this.teacherInput.Location = new System.Drawing.Point(12, 27);
            this.teacherInput.Name = "teacherInput";
            this.teacherInput.Size = new System.Drawing.Size(490, 23);
            this.teacherInput.TabIndex = 10;
            // 
            // subjectInput
            // 
            this.subjectInput.FormattingEnabled = true;
            this.subjectInput.Location = new System.Drawing.Point(12, 101);
            this.subjectInput.Name = "subjectInput";
            this.subjectInput.Size = new System.Drawing.Size(490, 23);
            this.subjectInput.TabIndex = 11;
            // 
            // filePathOutput
            // 
            this.filePathOutput.Enabled = false;
            this.filePathOutput.Location = new System.Drawing.Point(12, 347);
            this.filePathOutput.Name = "filePathOutput";
            this.filePathOutput.Size = new System.Drawing.Size(409, 23);
            this.filePathOutput.TabIndex = 12;
            // 
            // AlterHomeworkDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(514, 450);
            this.Controls.Add(this.filePathOutput);
            this.Controls.Add(this.subjectInput);
            this.Controls.Add(this.teacherInput);
            this.Controls.Add(this.groupInput);
            this.Controls.Add(this.dueDateInput);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.fileSelectButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AlterHomeworkDialog";
            this.Text = "AlterHomeworkDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button fileSelectButton;
        private Button cancelButton;
        private Button okButton;
        private DateTimePicker dueDateInput;
        private ComboBox groupInput;
        private ComboBox teacherInput;
        private ComboBox subjectInput;
        private TextBox filePathOutput;
    }
}