namespace AdminPanel.Forms
{
    partial class AlterMarkDialog
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.studentInput = new System.Windows.Forms.ComboBox();
            this.lessonInput = new System.Windows.Forms.ComboBox();
            this.homeworkInput = new System.Windows.Forms.ComboBox();
            this.valueInput = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.valueInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Оцінка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Студент";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(16, 99);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(67, 19);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Заняття";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(16, 162);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(131, 19);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Домашнє завдання";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.OnRadioButton2CheckedChanged);
            this.radioButton2.Click += new System.EventHandler(this.OnRadioButton2CheckedChanged);
            // 
            // studentInput
            // 
            this.studentInput.FormattingEnabled = true;
            this.studentInput.Location = new System.Drawing.Point(12, 57);
            this.studentInput.Name = "studentInput";
            this.studentInput.Size = new System.Drawing.Size(395, 23);
            this.studentInput.TabIndex = 5;
            // 
            // lessonInput
            // 
            this.lessonInput.FormattingEnabled = true;
            this.lessonInput.Location = new System.Drawing.Point(12, 124);
            this.lessonInput.Name = "lessonInput";
            this.lessonInput.Size = new System.Drawing.Size(395, 23);
            this.lessonInput.TabIndex = 7;
            // 
            // homeworkInput
            // 
            this.homeworkInput.FormattingEnabled = true;
            this.homeworkInput.Location = new System.Drawing.Point(12, 187);
            this.homeworkInput.Name = "homeworkInput";
            this.homeworkInput.Size = new System.Drawing.Size(395, 23);
            this.homeworkInput.TabIndex = 8;
            // 
            // valueInput
            // 
            this.valueInput.Location = new System.Drawing.Point(12, 258);
            this.valueInput.Name = "valueInput";
            this.valueInput.Size = new System.Drawing.Size(395, 23);
            this.valueInput.TabIndex = 9;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(109, 344);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "ОК";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(190, 344);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Відміна";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.OnCancelButtonClick);
            // 
            // AlterMarkDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(419, 379);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.valueInput);
            this.Controls.Add(this.homeworkInput);
            this.Controls.Add(this.lessonInput);
            this.Controls.Add(this.studentInput);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AlterMarkDialog";
            this.Text = "AlterMarkDialog";
            ((System.ComponentModel.ISupportInitialize)(this.valueInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private ComboBox studentInput;
        private ComboBox lessonInput;
        private ComboBox homeworkInput;
        private NumericUpDown valueInput;
        private Button okButton;
        private Button cancelButton;
    }
}