﻿using DatabaseClasses;
using System.ComponentModel.DataAnnotations;

namespace ServerApp
{
    /// <summary>
    /// Class that does account-related work
    /// </summary>
    internal class AccountManager : DatabaseAccessManager
    {
        private static object locker = new object();
        
        public AccountManager(string connStr) : base(connStr)
        {

        }

        /// <summary>
        /// Method that gets the user with specified login
        /// </summary>
        /// <param name="login">Login of the user</param>
        /// <returns>The User object if the user if found in database or null if the user is not found</returns>
        /// <exception cref="ArgumentException">Thrown when two or more users with login specified found in database</exception>
        public User? GetUser(string login)
        {
            User? result = null;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    try
                    {
                        result = context.Users.Where(u => u.Login == login).SingleOrDefault();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new ArgumentException("Login has two or more users in database. Could not detect specified user.", nameof(login), ex);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Method that removes the user from the database
        /// </summary>
        /// <param name="login">Login of the user</param>
        /// <exception cref="ArgumentException">Thrown when two or more users with login specified found in database</exception>
        public void RemoveUser(string login)
        {
            lock(locker)
            {
                using (var context = new ReporlistContext(connStr))
                {

                    try
                    {
                        User? user = context.Users.Where(u => u.Login == login).SingleOrDefault();
                        if (user == null)
                        {
                            return;
                        }
                        context.Users.Remove(user);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new ArgumentException("Login has two or more users in database. Could not detect specified user.", nameof(login), ex);
                    }
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method that adds the new user to the database
        /// </summary>
        /// <param name="user">The User object to be added</param>
        /// <exception cref="InvalidOperationException">Thrown when user with such login already exists in database</exception>
        public void RegisterUser(User user)
        {
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    if (context.Users.Where(u => u.Login == user.Login).Any())
                    {
                        throw new InvalidOperationException("Tried to register already registered user");
                    }
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method that changed the password of the user
        /// </summary>
        /// <param name="user">The user to change password</param>
        /// <param name="newPassword">The new password of the user. Cannot be the same as the current password</param>
        /// <exception cref="InvalidOperationException">Thrown when the new password is equal to the current password</exception>
        /// <exception cref="ArgumentException">Thrown when the user is not registered</exception>
        public void ChangePassword(User user, string newPassword)
        {
            if (user.Password == newPassword)
            {
                throw new InvalidOperationException("Cannot change the password to the current password");
            }
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    User? userFromDb = context.Users.Where(u => u.Id == user.Id).SingleOrDefault();
                    if (userFromDb == null)
                    {
                        throw new ArgumentException("The user is not registered", nameof(user));
                    }
                    userFromDb.Password = newPassword;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Method that looks for a student who is the specified registered user
        /// </summary>
        /// <param name="user">User to search a student for</param>
        /// <returns>The Student object that is binded to current user or null if no such Student object found</returns>
        /// <exception cref="ArgumentException">Thrown when more than one students are binded to that user</exception>
        public Student? GetStudent(User user)
        {
            Student? result;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    try
                    {
                        result = context.Students.Where(s => s.User.Id == user.Id).SingleOrDefault();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new ArgumentException("Two or more students are binded to that user", nameof(user), ex);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Method that looks for a teacher who is the specified registered user
        /// </summary>
        /// <param name="user">User to search a teacher for</param>
        /// <returns>The Teacher object that is binded to current user or null if no such Teacher object found</returns>
        /// <exception cref="ArgumentException">Thrown when more than one teachers are binded to that user</exception>
        public Teacher? GetTeacher(User user)
        {
            Teacher? result;
            lock (locker)
            {
                using (var context = new ReporlistContext(connStr))
                {
                    try
                    {
                        result = context.Teachers.Where(t => t.User.Id == user.Id).SingleOrDefault();
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new ArgumentException("Two or more students are binded to that user", nameof(user), ex);
                    }
                } 
            }
            return result;
        }
    }
}