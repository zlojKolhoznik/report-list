using ServerApp;
string connstr = AppSettings.ReadFromJsonFile("appsettings.json").ConnectionString;
using (var context = new ReporlistContext(connstr))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    Group g1 = new Group() { Name = "SE-121B" },
        g2 = new Group() { Name = "SE-122B" };
    Subject s1 = new Subject() { Name = "Software engineering essentials" },
        s2 = new Subject() { Name = "Calculus" },
        s3 = new Subject() { Name = "Programming essentials" };
    User u1 = new User() { Login = "1", Password = "1", IsAdmin = true },
        u2 = new User() { Login = "t1", Password = "t1", IsAdmin = false },
        u3 = new User() { Login = "t2", Password = "t2", IsAdmin = false },
        u4 = new User() { Login = "s1", Password = "s1", IsAdmin = false },
        u5 = new User() { Login = "s2", Password = "s2", IsAdmin = false },
        u6 = new User() { Login = "s3", Password = "s3", IsAdmin = false },
        u7 = new User() { Login = "s4", Password = "s4", IsAdmin = false };
    GroupsManager gm = new GroupsManager(connstr);
    gm.AddGroup(g1);
    gm.AddGroup(g2);
    SubjectsManager sm = new SubjectsManager(connstr);
    sm.AddSubject(s1);
    sm.AddSubject(s2);
    sm.AddSubject(s3);
    AccountManager am = new AccountManager(connstr);
    am.RegisterUser(u1);
    am.RegisterUser(u2);
    am.RegisterUser(u3);
    am.RegisterUser(u4);
    am.RegisterUser(u5);
    am.RegisterUser(u6);
    am.RegisterUser(u7);

    Student st1 = new Student() { Name = "Roman", Surname = "Portianko", DateOfBirth = new DateTime(2005, 5, 24), GroupId = g2.Id, UserId = u4.Id },
        st2 = new Student() { Name = "Dmytrii", Surname = "Shestachenko", DateOfBirth = new DateTime(2005, 3, 6), GroupId = g2.Id, UserId = u5.Id },
        st3 = new Student() { Name = "Kamila", Surname = "Kohut", DateOfBirth = new DateTime(2004, 5, 5), GroupId = g1.Id, UserId = u6.Id },
        st4 = new Student() { Name = "Olexandra", Surname = "Miroshnychenko", DateOfBirth = new DateTime(2005, 9, 28), GroupId = g1.Id, UserId = u7.Id };
    Teacher t1 = new Teacher() { Name = "Olena", Surname = "Hryshko", UserId = u2.Id },
        t2 = new Teacher() { Name = "Iana", Surname = "Beloziorova", UserId = u3.Id };
    StudentsManager stm = new StudentsManager(connstr);
    stm.AddStudent(st1);
    stm.AddStudent(st2);
    stm.AddStudent(st3);
    stm.AddStudent(st4);
    TeachersManager tm = new TeachersManager(connstr);
    tm.AddTeacher(t1, new List<Subject>() { s2 });
    tm.AddTeacher(t2, new List<Subject>() { s1, s3 });

    Lesson l1 = new Lesson() { Topic = "Derivative. Lecture", TeacherId = t1.Id, SubjectId = s2.Id, Groups = new List<Group>() { g1, g2 }, Date = new DateTime(2022, 11, 7, 9, 50, 0) };
    Lesson l2 = new Lesson() { Topic = "Pointers. Address arythmetics", TeacherId = t2.Id, SubjectId = s3.Id, Groups = new List<Group>() { g2 }, Date = new DateTime(2022, 11, 7, 11, 40, 0) };
    byte[] bytes = File.ReadAllBytes(@"C:\Users\Roman\Pictures\test-file.png");
    Homework hw1 = new Homework() { DueDate = new DateTime(2022, 11, 17, 18, 0, 0), FileBytes = bytes, FileExtension = ".png", GroupId = g1.Id, SubjectId = s1.Id, TeacherId = t2.Id };
    Homework hw2 = new Homework() { DueDate = new DateTime(2022, 11, 17, 18, 0, 0), FileBytes = bytes, FileExtension = ".png", GroupId = g2.Id, SubjectId = s2.Id, TeacherId = t1.Id };
    LessonsManager lm = new LessonsManager(connstr);
    lm.AddLesson(l1);
    lm.AddLesson(l2);
    HomeworksManager hwm = new HomeworksManager(connstr);
    hwm.AddHomework(hw1);
    hwm.AddHomework(hw2);

    Mark m1 = new Mark() { HomeworkId = hw1.Id, StudentId = st1.Id, Value = 10 };
    Mark m2 = new Mark() { LessonId = l1.Id, StudentId = st2.Id, Value = 5 };




    MarksManager mm = new MarksManager(connstr);
    mm.AddMark(m1);
    mm.AddMark(m2);
}