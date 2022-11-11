using Microsoft.EntityFrameworkCore;
using ServerApp.Model;

namespace ServerApp.IO
{
    /// <summary>
    /// Implements groups getting and adding
    /// </summary>
    internal class GroupsManager
    {
        /// <summary>
        /// Gets the list of groups that study the specified subject
        /// </summary>
        /// <param name="subject">The subject to select groups for</param>
        /// <returns>List of groups that have lessons from the specified subject</returns>
        public List<Group> GetGroups(Subject subject)
        {
            List<Group> result;
            using (var context = new ReportlistContext())
            {
                result = context.Groups.Where(g => g.GroupsLessons.Select(gl => gl.Lessons).Select(l => l.SubjectId).Contains(subject.Id)).ToList();
            }
            return result;
        }

        /// <summary>
        /// Gets the list of groups that are taught by the specified teacher
        /// </summary>
        /// <param name="teacher">Teacher to search groups for</param>
        /// <param name="subject">This parameter will select only these groups that study specified subject, ignored if null</param>
        /// <returns>List of groups that have lessons with the specified teacher</returns>
        public List<Group> GetGroups(Teacher teacher, Subject? subject = null)
        {
            List<Group> result;
            using (var context = new ReportlistContext())
            {
                result = context.Groups.
                    Include(g => g.GroupsLessons).
                    ThenInclude(gl => gl.Lessons).
                    Where(g => g.GroupsLessons.Any(gl => gl.Lessons.TeacherId == teacher.Id)).
                    ToList();
            }
            if (subject != null)
            {
                result = result.Where(g => g.GroupsLessons.Select(gl => gl.Lessons).Select(l => l.SubjectId).Contains(subject.Id)).ToList();
            }
            return result;
        }

        /// <summary>
        /// Adds a new group to the database
        /// </summary>
        /// <param name="group">Group to add</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddGroup(Group group)
        {
            using (var context = new ReportlistContext())
            {
                if (context.Groups.Any(g => g.Name == group.Name))
                {
                    throw new InvalidOperationException("Group with this name already exists in database");
                }
                context.Groups.Add(group);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Changes the name of the specified group in the database
        /// </summary>
        /// <param name="group">Group whose name is to be changed</param>
        /// <param name="newName">The new name of the group</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameGroup(Group group, string newName)
        {
            using (var context = new ReportlistContext())
            {
                var toChange = context.Groups.FirstOrDefault(g => g.Id == group.Id);
                if (toChange == null)
                {
                    throw new ArgumentException("Cannot rename the group that is not in the database", nameof(group));
                }
                if (toChange.Name == newName)
                {
                    throw new InvalidOperationException("Cannot change the name of the group to the current name");
                }
                toChange.Name = newName;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the group from the database
        /// </summary>
        /// <remarks>This will also remove all the marks, lessons, homeworks and students from this group. Use wisely</remarks>
        /// <param name="group">Group to delete</param>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveGroup(Group group)
        {
            using (var context = new ReportlistContext())
            {
                var toRemove = context.Groups.FirstOrDefault(g => g.Id == group.Id);
                if (toRemove == null)
                {
                    throw new ArgumentException("Cannot delete the group that is not in the database");
                }
                context.Groups.Remove(toRemove);
                context.SaveChanges();
            }
        }
    }
}
