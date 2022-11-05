using DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    internal class GroupsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public GroupsManager(string connStr) : base(connStr)
        {

        }

        public List<Group> GetGroups(Subject subject)
        {
            var result = new List<Group>();
            foreach (var lesson in subject.Lessons)
            {
                if(!result.Any(g => g.Id == lesson.Group.Id))
                {
                    result.Add(lesson.Group);
                }
            }
            return result;
        }

        public List<Group> GetGroups(Teacher teacher)
        {
            var result = new List<Group>();
            foreach (var lesson in teacher.Lessons)
            {
                if(!result.Any(g => g.Id == lesson.Group.Id))
                {
                    result.Add(lesson.Group);
                }
            }
            return result;
        }

        public void AddGroup(Group group)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    if (context.Groups.Any(g => g.Name == group.Name))
                    {
                        throw new InvalidOperationException("Group with this name already exists in database");
                    }
                    context.Groups.Add(group);
                    context.SaveChanges();
                }
            }
        }

        public void RenameGroup(Group group, string newName)
        {
            if (group.Name == newName)
            {
                throw new InvalidOperationException("Cannot change the name of the group to the current name");
            }
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    Group? groupFromDb = context.Groups.FirstOrDefault(g => g.Name == group.Name);
                    if (groupFromDb == null)
                    {
                        throw new InvalidOperationException("Group you are trying to access is not in the database");
                    }
                    groupFromDb.Name = newName;
                    context.SaveChanges();
                }
            }
        }
    }
}
