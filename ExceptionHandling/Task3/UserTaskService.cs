using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public int AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
                throw new ArgumentException("Invalid userId");

            var user = _userDao.GetUser(userId);
            if (user == null)
                throw new EntityNotFoundException("User not found");

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    throw new EntityAlreadyExistsException("The task already exists");
            }

            tasks.Add(task);

            return 0;
        }
    }
}