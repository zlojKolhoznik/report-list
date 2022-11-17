namespace AdminPanel.Forms
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.deleteUserButton = new System.Windows.Forms.Button();
            this.alterUserButton = new System.Windows.Forms.Button();
            this.registerUserButton = new System.Windows.Forms.Button();
            this.usersDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.deleteStudentButton = new System.Windows.Forms.Button();
            this.alterStudentButton = new System.Windows.Forms.Button();
            this.addStudentButton = new System.Windows.Forms.Button();
            this.studentsDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.deleteTeacherSubjectButton = new System.Windows.Forms.Button();
            this.addTeacherSubjectButton = new System.Windows.Forms.Button();
            this.deleteTeacherButton = new System.Windows.Forms.Button();
            this.alterTeacherButton = new System.Windows.Forms.Button();
            this.addTeacherButton = new System.Windows.Forms.Button();
            this.teachersDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.deleteGroupsLesson = new System.Windows.Forms.Button();
            this.addGroupsLesson = new System.Windows.Forms.Button();
            this.deleteLessonButton = new System.Windows.Forms.Button();
            this.alterLessonButton = new System.Windows.Forms.Button();
            this.addLessonButton = new System.Windows.Forms.Button();
            this.lessonsDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.deleteGroupButton = new System.Windows.Forms.Button();
            this.alterGroupButton = new System.Windows.Forms.Button();
            this.addGroupButton = new System.Windows.Forms.Button();
            this.groupsDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.deleteHomeworkButton = new System.Windows.Forms.Button();
            this.alterHomeworkButton = new System.Windows.Forms.Button();
            this.addHomeworkButton = new System.Windows.Forms.Button();
            this.homeworksDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.deleteMarkButton = new System.Windows.Forms.Button();
            this.alterMarkButton = new System.Windows.Forms.Button();
            this.addMarkButton = new System.Windows.Forms.Button();
            this.marksDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.deleteSubjectButton = new System.Windows.Forms.Button();
            this.alterSubjectButton = new System.Windows.Forms.Button();
            this.addSubjectButton = new System.Windows.Forms.Button();
            this.subjectsDataGrid = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.studentsDataGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teachersDataGrid)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lessonsDataGrid)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupsDataGrid)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.homeworksDataGrid)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marksDataGrid)).BeginInit();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subjectsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(987, 579);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.deleteUserButton);
            this.tabPage1.Controls.Add(this.alterUserButton);
            this.tabPage1.Controls.Add(this.registerUserButton);
            this.tabPage1.Controls.Add(this.usersDataGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(979, 551);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Користувачі";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // deleteUserButton
            // 
            this.deleteUserButton.Location = new System.Drawing.Point(597, 522);
            this.deleteUserButton.Name = "deleteUserButton";
            this.deleteUserButton.Size = new System.Drawing.Size(142, 23);
            this.deleteUserButton.TabIndex = 3;
            this.deleteUserButton.Text = "Видалити користувача";
            this.deleteUserButton.UseVisualStyleBackColor = true;
            this.deleteUserButton.Click += new System.EventHandler(this.OnDeleteUserButtonClick);
            // 
            // alterUserButton
            // 
            this.alterUserButton.Location = new System.Drawing.Point(413, 522);
            this.alterUserButton.Name = "alterUserButton";
            this.alterUserButton.Size = new System.Drawing.Size(178, 23);
            this.alterUserButton.TabIndex = 2;
            this.alterUserButton.Text = "Змінити дані користувача";
            this.alterUserButton.UseVisualStyleBackColor = true;
            this.alterUserButton.Click += new System.EventHandler(this.OnAlterUserButtonClick);
            // 
            // registerUserButton
            // 
            this.registerUserButton.Location = new System.Drawing.Point(225, 522);
            this.registerUserButton.Name = "registerUserButton";
            this.registerUserButton.Size = new System.Drawing.Size(182, 23);
            this.registerUserButton.TabIndex = 1;
            this.registerUserButton.Text = "Зарєструвати користувача";
            this.registerUserButton.UseVisualStyleBackColor = true;
            this.registerUserButton.Click += new System.EventHandler(this.OnRegisterUserButtonClick);
            // 
            // usersDataGrid
            // 
            this.usersDataGrid.AllowUserToAddRows = false;
            this.usersDataGrid.AllowUserToDeleteRows = false;
            this.usersDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersDataGrid.Location = new System.Drawing.Point(6, 6);
            this.usersDataGrid.Name = "usersDataGrid";
            this.usersDataGrid.ReadOnly = true;
            this.usersDataGrid.RowTemplate.Height = 25;
            this.usersDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersDataGrid.Size = new System.Drawing.Size(967, 510);
            this.usersDataGrid.TabIndex = 0;
            this.usersDataGrid.SelectionChanged += new System.EventHandler(this.OnUsersDataGridSelectionChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.deleteStudentButton);
            this.tabPage2.Controls.Add(this.alterStudentButton);
            this.tabPage2.Controls.Add(this.addStudentButton);
            this.tabPage2.Controls.Add(this.studentsDataGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(979, 551);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Студенти";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // deleteStudentButton
            // 
            this.deleteStudentButton.Location = new System.Drawing.Point(562, 522);
            this.deleteStudentButton.Name = "deleteStudentButton";
            this.deleteStudentButton.Size = new System.Drawing.Size(142, 23);
            this.deleteStudentButton.TabIndex = 7;
            this.deleteStudentButton.Text = "Видалити студента";
            this.deleteStudentButton.UseVisualStyleBackColor = true;
            this.deleteStudentButton.Click += new System.EventHandler(this.OnDeleteStudentButtonClick);
            // 
            // alterStudentButton
            // 
            this.alterStudentButton.Location = new System.Drawing.Point(413, 522);
            this.alterStudentButton.Name = "alterStudentButton";
            this.alterStudentButton.Size = new System.Drawing.Size(143, 23);
            this.alterStudentButton.TabIndex = 6;
            this.alterStudentButton.Text = "Змінити дані студента";
            this.alterStudentButton.UseVisualStyleBackColor = true;
            this.alterStudentButton.Click += new System.EventHandler(this.OnAlterStudentButtonClick);
            // 
            // addStudentButton
            // 
            this.addStudentButton.Location = new System.Drawing.Point(281, 522);
            this.addStudentButton.Name = "addStudentButton";
            this.addStudentButton.Size = new System.Drawing.Size(126, 23);
            this.addStudentButton.TabIndex = 5;
            this.addStudentButton.Text = "Додати студента";
            this.addStudentButton.UseVisualStyleBackColor = true;
            this.addStudentButton.Click += new System.EventHandler(this.OnAddStudentButtonClick);
            // 
            // studentsDataGrid
            // 
            this.studentsDataGrid.AllowUserToAddRows = false;
            this.studentsDataGrid.AllowUserToDeleteRows = false;
            this.studentsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.studentsDataGrid.Location = new System.Drawing.Point(6, 6);
            this.studentsDataGrid.Name = "studentsDataGrid";
            this.studentsDataGrid.ReadOnly = true;
            this.studentsDataGrid.RowTemplate.Height = 25;
            this.studentsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.studentsDataGrid.Size = new System.Drawing.Size(967, 510);
            this.studentsDataGrid.TabIndex = 4;
            this.studentsDataGrid.SelectionChanged += new System.EventHandler(this.OnStudentsDataGridSelectionChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.deleteTeacherSubjectButton);
            this.tabPage3.Controls.Add(this.addTeacherSubjectButton);
            this.tabPage3.Controls.Add(this.deleteTeacherButton);
            this.tabPage3.Controls.Add(this.alterTeacherButton);
            this.tabPage3.Controls.Add(this.addTeacherButton);
            this.tabPage3.Controls.Add(this.teachersDataGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(979, 551);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Викладачі";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // deleteTeacherSubjectButton
            // 
            this.deleteTeacherSubjectButton.Location = new System.Drawing.Point(570, 522);
            this.deleteTeacherSubjectButton.Name = "deleteTeacherSubjectButton";
            this.deleteTeacherSubjectButton.Size = new System.Drawing.Size(118, 23);
            this.deleteTeacherSubjectButton.TabIndex = 17;
            this.deleteTeacherSubjectButton.Text = "Вилучити предмет";
            this.deleteTeacherSubjectButton.UseVisualStyleBackColor = true;
            this.deleteTeacherSubjectButton.Click += new System.EventHandler(this.DeleteTeacherSubjectButtonClick);
            // 
            // addTeacherSubjectButton
            // 
            this.addTeacherSubjectButton.Location = new System.Drawing.Point(303, 522);
            this.addTeacherSubjectButton.Name = "addTeacherSubjectButton";
            this.addTeacherSubjectButton.Size = new System.Drawing.Size(104, 23);
            this.addTeacherSubjectButton.TabIndex = 16;
            this.addTeacherSubjectButton.Text = "Додати предмет";
            this.addTeacherSubjectButton.UseVisualStyleBackColor = true;
            this.addTeacherSubjectButton.Click += new System.EventHandler(this.OnAddTeacherSubjectButtonClick);
            // 
            // deleteTeacherButton
            // 
            this.deleteTeacherButton.Location = new System.Drawing.Point(694, 522);
            this.deleteTeacherButton.Name = "deleteTeacherButton";
            this.deleteTeacherButton.Size = new System.Drawing.Size(130, 23);
            this.deleteTeacherButton.TabIndex = 15;
            this.deleteTeacherButton.Text = "Видалити викладача";
            this.deleteTeacherButton.UseVisualStyleBackColor = true;
            this.deleteTeacherButton.Click += new System.EventHandler(this.OnDeleteTeacherButtonClick);
            // 
            // alterTeacherButton
            // 
            this.alterTeacherButton.Location = new System.Drawing.Point(413, 522);
            this.alterTeacherButton.Name = "alterTeacherButton";
            this.alterTeacherButton.Size = new System.Drawing.Size(151, 23);
            this.alterTeacherButton.TabIndex = 14;
            this.alterTeacherButton.Text = "Змінити дані викладача";
            this.alterTeacherButton.UseVisualStyleBackColor = true;
            this.alterTeacherButton.Click += new System.EventHandler(this.OnAlterTeacherButtonClick);
            // 
            // addTeacherButton
            // 
            this.addTeacherButton.Location = new System.Drawing.Point(171, 522);
            this.addTeacherButton.Name = "addTeacherButton";
            this.addTeacherButton.Size = new System.Drawing.Size(126, 23);
            this.addTeacherButton.TabIndex = 13;
            this.addTeacherButton.Text = "Додати викладача";
            this.addTeacherButton.UseVisualStyleBackColor = true;
            this.addTeacherButton.Click += new System.EventHandler(this.OnAddTeacherButtonClick);
            // 
            // teachersDataGrid
            // 
            this.teachersDataGrid.AllowUserToAddRows = false;
            this.teachersDataGrid.AllowUserToDeleteRows = false;
            this.teachersDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.teachersDataGrid.Location = new System.Drawing.Point(6, 6);
            this.teachersDataGrid.Name = "teachersDataGrid";
            this.teachersDataGrid.ReadOnly = true;
            this.teachersDataGrid.RowTemplate.Height = 25;
            this.teachersDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.teachersDataGrid.Size = new System.Drawing.Size(967, 510);
            this.teachersDataGrid.TabIndex = 12;
            this.teachersDataGrid.SelectionChanged += new System.EventHandler(this.OnTeachersDataGridSelectionChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.deleteGroupsLesson);
            this.tabPage4.Controls.Add(this.addGroupsLesson);
            this.tabPage4.Controls.Add(this.deleteLessonButton);
            this.tabPage4.Controls.Add(this.alterLessonButton);
            this.tabPage4.Controls.Add(this.addLessonButton);
            this.tabPage4.Controls.Add(this.lessonsDataGrid);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(979, 551);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Заняття";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // deleteGroupsLesson
            // 
            this.deleteGroupsLesson.Location = new System.Drawing.Point(571, 522);
            this.deleteGroupsLesson.Name = "deleteGroupsLesson";
            this.deleteGroupsLesson.Size = new System.Drawing.Size(123, 23);
            this.deleteGroupsLesson.TabIndex = 21;
            this.deleteGroupsLesson.Text = "Вилучити групу";
            this.deleteGroupsLesson.UseVisualStyleBackColor = true;
            this.deleteGroupsLesson.Click += new System.EventHandler(this.OnDeleteGroupsLessonClick);
            // 
            // addGroupsLesson
            // 
            this.addGroupsLesson.Location = new System.Drawing.Point(304, 522);
            this.addGroupsLesson.Name = "addGroupsLesson";
            this.addGroupsLesson.Size = new System.Drawing.Size(103, 23);
            this.addGroupsLesson.TabIndex = 20;
            this.addGroupsLesson.Text = "Додати групу";
            this.addGroupsLesson.UseVisualStyleBackColor = true;
            this.addGroupsLesson.Click += new System.EventHandler(this.OnAddGroupsLessonClick);
            // 
            // deleteLessonButton
            // 
            this.deleteLessonButton.Location = new System.Drawing.Point(700, 522);
            this.deleteLessonButton.Name = "deleteLessonButton";
            this.deleteLessonButton.Size = new System.Drawing.Size(124, 23);
            this.deleteLessonButton.TabIndex = 19;
            this.deleteLessonButton.Text = "Видалити заняття";
            this.deleteLessonButton.UseVisualStyleBackColor = true;
            this.deleteLessonButton.Click += new System.EventHandler(this.OnDeleteLessonButtonClick);
            // 
            // alterLessonButton
            // 
            this.alterLessonButton.Location = new System.Drawing.Point(413, 522);
            this.alterLessonButton.Name = "alterLessonButton";
            this.alterLessonButton.Size = new System.Drawing.Size(152, 23);
            this.alterLessonButton.TabIndex = 18;
            this.alterLessonButton.Text = "Змінити дані заняття";
            this.alterLessonButton.UseVisualStyleBackColor = true;
            this.alterLessonButton.Click += new System.EventHandler(this.OnAlterLessonButtonClick);
            // 
            // addLessonButton
            // 
            this.addLessonButton.Location = new System.Drawing.Point(172, 522);
            this.addLessonButton.Name = "addLessonButton";
            this.addLessonButton.Size = new System.Drawing.Size(126, 23);
            this.addLessonButton.TabIndex = 17;
            this.addLessonButton.Text = "Додати заняття";
            this.addLessonButton.UseVisualStyleBackColor = true;
            this.addLessonButton.Click += new System.EventHandler(this.AddLessonButtonClick);
            // 
            // lessonsDataGrid
            // 
            this.lessonsDataGrid.AllowUserToAddRows = false;
            this.lessonsDataGrid.AllowUserToDeleteRows = false;
            this.lessonsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lessonsDataGrid.Location = new System.Drawing.Point(6, 6);
            this.lessonsDataGrid.Name = "lessonsDataGrid";
            this.lessonsDataGrid.ReadOnly = true;
            this.lessonsDataGrid.RowTemplate.Height = 25;
            this.lessonsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lessonsDataGrid.Size = new System.Drawing.Size(967, 510);
            this.lessonsDataGrid.TabIndex = 16;
            this.lessonsDataGrid.SelectionChanged += new System.EventHandler(this.OnLessonsDataGridSelectionChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.deleteGroupButton);
            this.tabPage5.Controls.Add(this.alterGroupButton);
            this.tabPage5.Controls.Add(this.addGroupButton);
            this.tabPage5.Controls.Add(this.groupsDataGrid);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(979, 551);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Групи";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // deleteGroupButton
            // 
            this.deleteGroupButton.Location = new System.Drawing.Point(562, 522);
            this.deleteGroupButton.Name = "deleteGroupButton";
            this.deleteGroupButton.Size = new System.Drawing.Size(124, 23);
            this.deleteGroupButton.TabIndex = 11;
            this.deleteGroupButton.Text = "Видалити групу";
            this.deleteGroupButton.UseVisualStyleBackColor = true;
            this.deleteGroupButton.Click += new System.EventHandler(this.OnDeleteGroupButtonClick);
            // 
            // alterGroupButton
            // 
            this.alterGroupButton.Location = new System.Drawing.Point(413, 522);
            this.alterGroupButton.Name = "alterGroupButton";
            this.alterGroupButton.Size = new System.Drawing.Size(143, 23);
            this.alterGroupButton.TabIndex = 10;
            this.alterGroupButton.Text = "Змінити назву групи";
            this.alterGroupButton.UseVisualStyleBackColor = true;
            this.alterGroupButton.Click += new System.EventHandler(this.OnAlterGroupButtonClick);
            // 
            // addGroupButton
            // 
            this.addGroupButton.Location = new System.Drawing.Point(281, 522);
            this.addGroupButton.Name = "addGroupButton";
            this.addGroupButton.Size = new System.Drawing.Size(126, 23);
            this.addGroupButton.TabIndex = 9;
            this.addGroupButton.Text = "Додати групу";
            this.addGroupButton.UseVisualStyleBackColor = true;
            this.addGroupButton.Click += new System.EventHandler(this.OnAddGroupButtonClick);
            // 
            // groupsDataGrid
            // 
            this.groupsDataGrid.AllowUserToAddRows = false;
            this.groupsDataGrid.AllowUserToDeleteRows = false;
            this.groupsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupsDataGrid.Location = new System.Drawing.Point(6, 6);
            this.groupsDataGrid.Name = "groupsDataGrid";
            this.groupsDataGrid.ReadOnly = true;
            this.groupsDataGrid.RowTemplate.Height = 25;
            this.groupsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.groupsDataGrid.Size = new System.Drawing.Size(967, 510);
            this.groupsDataGrid.TabIndex = 8;
            this.groupsDataGrid.SelectionChanged += new System.EventHandler(this.OnGroupsDataGridSelectionChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.deleteHomeworkButton);
            this.tabPage6.Controls.Add(this.alterHomeworkButton);
            this.tabPage6.Controls.Add(this.addHomeworkButton);
            this.tabPage6.Controls.Add(this.homeworksDataGrid);
            this.tabPage6.Location = new System.Drawing.Point(4, 24);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(979, 551);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Домашні завдання";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // deleteHomeworkButton
            // 
            this.deleteHomeworkButton.Location = new System.Drawing.Point(589, 522);
            this.deleteHomeworkButton.Name = "deleteHomeworkButton";
            this.deleteHomeworkButton.Size = new System.Drawing.Size(176, 23);
            this.deleteHomeworkButton.TabIndex = 15;
            this.deleteHomeworkButton.Text = "Видалити домашнє завдання";
            this.deleteHomeworkButton.UseVisualStyleBackColor = true;
            this.deleteHomeworkButton.Click += new System.EventHandler(this.OnDeleteHomeworkButtonClick);
            // 
            // alterHomeworkButton
            // 
            this.alterHomeworkButton.Location = new System.Drawing.Point(413, 522);
            this.alterHomeworkButton.Name = "alterHomeworkButton";
            this.alterHomeworkButton.Size = new System.Drawing.Size(170, 23);
            this.alterHomeworkButton.TabIndex = 14;
            this.alterHomeworkButton.Text = "Змінити домашнє завдання";
            this.alterHomeworkButton.UseVisualStyleBackColor = true;
            this.alterHomeworkButton.Click += new System.EventHandler(this.OnAlterHomeworkButtonClick);
            // 
            // addHomeworkButton
            // 
            this.addHomeworkButton.Location = new System.Drawing.Point(240, 522);
            this.addHomeworkButton.Name = "addHomeworkButton";
            this.addHomeworkButton.Size = new System.Drawing.Size(167, 23);
            this.addHomeworkButton.TabIndex = 13;
            this.addHomeworkButton.Text = "Додати домашнє завдання";
            this.addHomeworkButton.UseVisualStyleBackColor = true;
            this.addHomeworkButton.Click += new System.EventHandler(this.OnAddHomeworkButtonClick);
            // 
            // homeworksDataGrid
            // 
            this.homeworksDataGrid.AllowUserToAddRows = false;
            this.homeworksDataGrid.AllowUserToDeleteRows = false;
            this.homeworksDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.homeworksDataGrid.Location = new System.Drawing.Point(6, 6);
            this.homeworksDataGrid.Name = "homeworksDataGrid";
            this.homeworksDataGrid.ReadOnly = true;
            this.homeworksDataGrid.RowTemplate.Height = 25;
            this.homeworksDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.homeworksDataGrid.Size = new System.Drawing.Size(967, 510);
            this.homeworksDataGrid.TabIndex = 12;
            this.homeworksDataGrid.SelectionChanged += new System.EventHandler(this.OnHomeworksDataGridSelectionChanged);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.deleteMarkButton);
            this.tabPage7.Controls.Add(this.alterMarkButton);
            this.tabPage7.Controls.Add(this.addMarkButton);
            this.tabPage7.Controls.Add(this.marksDataGrid);
            this.tabPage7.Location = new System.Drawing.Point(4, 24);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(979, 551);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Оцінки";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // deleteMarkButton
            // 
            this.deleteMarkButton.Location = new System.Drawing.Point(571, 522);
            this.deleteMarkButton.Name = "deleteMarkButton";
            this.deleteMarkButton.Size = new System.Drawing.Size(124, 23);
            this.deleteMarkButton.TabIndex = 19;
            this.deleteMarkButton.Text = "Видалити оцінку";
            this.deleteMarkButton.UseVisualStyleBackColor = true;
            this.deleteMarkButton.Click += new System.EventHandler(this.OnDeleteMarkButtonClick);
            // 
            // alterMarkButton
            // 
            this.alterMarkButton.Location = new System.Drawing.Point(413, 522);
            this.alterMarkButton.Name = "alterMarkButton";
            this.alterMarkButton.Size = new System.Drawing.Size(152, 23);
            this.alterMarkButton.TabIndex = 18;
            this.alterMarkButton.Text = "Змінити дані оцінки";
            this.alterMarkButton.UseVisualStyleBackColor = true;
            this.alterMarkButton.Click += new System.EventHandler(this.OnAlterMarkButtonClick);
            // 
            // addMarkButton
            // 
            this.addMarkButton.Location = new System.Drawing.Point(281, 522);
            this.addMarkButton.Name = "addMarkButton";
            this.addMarkButton.Size = new System.Drawing.Size(126, 23);
            this.addMarkButton.TabIndex = 17;
            this.addMarkButton.Text = "Додати оцінку";
            this.addMarkButton.UseVisualStyleBackColor = true;
            this.addMarkButton.Click += new System.EventHandler(this.OnAddMarkButtonClick);
            // 
            // marksDataGrid
            // 
            this.marksDataGrid.AllowUserToAddRows = false;
            this.marksDataGrid.AllowUserToDeleteRows = false;
            this.marksDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marksDataGrid.Location = new System.Drawing.Point(6, 6);
            this.marksDataGrid.Name = "marksDataGrid";
            this.marksDataGrid.ReadOnly = true;
            this.marksDataGrid.RowTemplate.Height = 25;
            this.marksDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.marksDataGrid.Size = new System.Drawing.Size(967, 510);
            this.marksDataGrid.TabIndex = 16;
            this.marksDataGrid.SelectionChanged += new System.EventHandler(this.OnMarksDataGridSelectionChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.deleteSubjectButton);
            this.tabPage8.Controls.Add(this.alterSubjectButton);
            this.tabPage8.Controls.Add(this.addSubjectButton);
            this.tabPage8.Controls.Add(this.subjectsDataGrid);
            this.tabPage8.Location = new System.Drawing.Point(4, 24);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(979, 551);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Предмети";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // deleteSubjectButton
            // 
            this.deleteSubjectButton.Location = new System.Drawing.Point(571, 522);
            this.deleteSubjectButton.Name = "deleteSubjectButton";
            this.deleteSubjectButton.Size = new System.Drawing.Size(124, 23);
            this.deleteSubjectButton.TabIndex = 15;
            this.deleteSubjectButton.Text = "Видалити предмет";
            this.deleteSubjectButton.UseVisualStyleBackColor = true;
            this.deleteSubjectButton.Click += new System.EventHandler(this.OnDeleteSubjectButtonClick);
            // 
            // alterSubjectButton
            // 
            this.alterSubjectButton.Location = new System.Drawing.Point(413, 522);
            this.alterSubjectButton.Name = "alterSubjectButton";
            this.alterSubjectButton.Size = new System.Drawing.Size(152, 23);
            this.alterSubjectButton.TabIndex = 14;
            this.alterSubjectButton.Text = "Змінити назву предмета";
            this.alterSubjectButton.UseVisualStyleBackColor = true;
            this.alterSubjectButton.Click += new System.EventHandler(this.OnAlterSubjectButtonClick);
            // 
            // addSubjectButton
            // 
            this.addSubjectButton.Location = new System.Drawing.Point(281, 522);
            this.addSubjectButton.Name = "addSubjectButton";
            this.addSubjectButton.Size = new System.Drawing.Size(126, 23);
            this.addSubjectButton.TabIndex = 13;
            this.addSubjectButton.Text = "Додати предмет";
            this.addSubjectButton.UseVisualStyleBackColor = true;
            this.addSubjectButton.Click += new System.EventHandler(this.OnAddSubjectButtonClick);
            // 
            // subjectsDataGrid
            // 
            this.subjectsDataGrid.AllowUserToAddRows = false;
            this.subjectsDataGrid.AllowUserToDeleteRows = false;
            this.subjectsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subjectsDataGrid.Location = new System.Drawing.Point(6, 6);
            this.subjectsDataGrid.Name = "subjectsDataGrid";
            this.subjectsDataGrid.ReadOnly = true;
            this.subjectsDataGrid.RowTemplate.Height = 25;
            this.subjectsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.subjectsDataGrid.Size = new System.Drawing.Size(967, 510);
            this.subjectsDataGrid.TabIndex = 12;
            this.subjectsDataGrid.SelectionChanged += new System.EventHandler(this.OnSubjectsDataGridSelectionChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 603);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Панель адміністратора електронного щоденника";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.studentsDataGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.teachersDataGrid)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lessonsDataGrid)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupsDataGrid)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.homeworksDataGrid)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marksDataGrid)).EndInit();
            this.tabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.subjectsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private Button deleteUserButton;
        private Button alterUserButton;
        private Button registerUserButton;
        private DataGridView usersDataGrid;
        private Button deleteStudentButton;
        private Button alterStudentButton;
        private Button addStudentButton;
        private DataGridView studentsDataGrid;
        private Button deleteGroupButton;
        private Button alterGroupButton;
        private Button addGroupButton;
        private DataGridView groupsDataGrid;
        private Button deleteTeacherSubjectButton;
        private Button addTeacherSubjectButton;
        private Button deleteTeacherButton;
        private Button alterTeacherButton;
        private Button addTeacherButton;
        private DataGridView teachersDataGrid;
        private Button deleteSubjectButton;
        private Button alterSubjectButton;
        private Button addSubjectButton;
        private DataGridView subjectsDataGrid;
        private Button deleteHomeworkButton;
        private Button alterHomeworkButton;
        private Button addHomeworkButton;
        private DataGridView homeworksDataGrid;
        private Button deleteLessonButton;
        private Button alterLessonButton;
        private Button addLessonButton;
        private DataGridView lessonsDataGrid;
        private Button deleteMarkButton;
        private Button alterMarkButton;
        private Button addMarkButton;
        private DataGridView marksDataGrid;
        private Button deleteGroupsLesson;
        private Button addGroupsLesson;
    }
}