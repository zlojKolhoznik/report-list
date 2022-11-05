using DatabaseClasses;

namespace ServerApp
{
    /// <summary>
    /// Implements groups getting and adding
    /// </summary>
    internal class GroupsManager : DatabaseAccessManager
    {
        private static object locker = new object();

        public GroupsManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Gets the list of groups that study the specified subject
        /// </summary>
        /// <param name="subject">Subject to search groups for</param>
        /// <returns>List of groups that have lessons of the specified subject</Group></returns>
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

        /// <summary>
        /// Gets the list of groups that are taught by the specified teacher
        /// </summary>
        /// <param name="teacher">Teacher to search groups for</param>
        /// <returns>List of groups that have lessons with the specified teacher</returns>
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

        /// <summary>
        /// Adds a new group to the database
        /// </summary>
        /// <param name="group">Group to add</param>
        /// <exception cref="InvalidOperationException">Thrown when tried to add a groups with already existing name</exception>
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

        /// <summary>
        /// Changes the name of the specified group in the database
        /// </summary>
        /// <param name="group">Group whose name is to be changed</param>
        /// <param name="newName">The new name of the group</param>
        /// <exception cref="InvalidOperationException">Thrown when the new name of the group is equal to its current name</exception>
        public void RenameGroup(Group group, string newName)
        {
            if (group.Name == newName)
            {
                throw new InvalidOperationException("Cannot change the name of the group to the current name");
            }
            lock (locker)
            {
                group.Name = newName;
            }
        }
    }
}
