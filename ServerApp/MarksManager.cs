using DatabaseClasses;

namespace ServerApp
{
    
    internal class MarksManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public MarksManager(string connStr) : base(connStr)
        {

        }

        
        public List<Mark> GetMarks(Student student, Subject? subject = null)
        {
            List<Mark> result;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    result = context.Marks.Where(s => s.Student.Id == student.Id).ToList();
                }
            }
            if (subject != null)
            {
                result = SelectMarks(result, subject);
            }
            return result;
        }

        public List<Mark> GetMarks(Group group, Subject? subject = null, Teacher? teacher = null)
        {
            List<Mark> result;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    result = context.Marks.Where(s => s.Student.Group.Id == group.Id).ToList();
                }
            }
            result = SelectMarks(result, subject, teacher);
            return result;
        }

        public void AddMark(Mark mark)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    context.Marks.Add(mark);
                    context.SaveChanges();
                }
            }
        }

        public void ChangeMark(Mark mark, int newValue)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    Mark? markFromDb = context.Marks.FirstOrDefault(m => m.Id == mark.Id);
                    if (markFromDb == null)
                    {
                        throw new ArgumentException("This mark is not in database", nameof(mark));
                    }
                    markFromDb.Value = newValue;
                    context.SaveChanges();
                }
            }
        }

        private static List<Mark> SelectMarks(List<Mark> source, Subject? subject = null, Teacher? teacher = null)
        {
            if (subject != null)
            {
                source = source.Where(m => m.Subject.Id == subject!.Id).ToList();
            }
            if (teacher != null)
            {
                source = source.Where(m => m.Teacher.Id == teacher!.Id).ToList();
            }
            return source;
        }
    }
}
