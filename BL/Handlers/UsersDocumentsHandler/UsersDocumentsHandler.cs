using System.Collections.Generic;
using System.Linq;
using BL.Handlers.CurrentStepsHandlers;
using BL.Handlers.DocumentHandlers;
using BL.Models;

namespace BL.Handlers.UsersDocumentsHandler
{
    public class UsersDocumentsHandler
    {
        protected CurrentStepsRepositoryHandler _currentStepsHandler =
            new CurrentStepsRepositoryHandler();

        protected DocumentsRepositoryHandler _documentsHandler =
            new DocumentsRepositoryHandler();

        public List<Document> Outbox(int userId)
        {
            var documets = new List<Document>(_documentsHandler.GetUserDocuments(userId));
            return documets;
        }

        public List<Document> Inbox(int userId)
        {
            var steps = new List<CurrentStep>(_currentStepsHandler.GetStepsForUser(userId));

            var documentsIds = steps.Select(x => x.DocumentId);
            var documents = documentsIds.Select(x => _documentsHandler.FindById(x)).ToList();

            return documents;
        }
    }
}