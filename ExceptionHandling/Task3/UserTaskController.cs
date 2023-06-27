using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskController
    {
        private readonly UserTaskService _taskService;

        public UserTaskController(UserTaskService taskService)
        {
            _taskService = taskService;
        }

        public bool AddTaskForUser(int userId, string description, IResponseModel model)
        {
            string message = GetMessageForModel(userId, description);
            if (message != null)
            {
                model.AddAttribute("action_result", message);
                return false;
            }

            return true;
        }

        private string GetMessageForModel(int userId, string description)
        {
            var task = new UserTask(description);
            try
            {
                int result = _taskService.AddTaskForUser(userId, task);

                return null;
            }
            catch (Exception e) when (e is ArgumentException || e is EntityNotFoundException || e is EntityAlreadyExistsException)
            {
                return e.Message;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}