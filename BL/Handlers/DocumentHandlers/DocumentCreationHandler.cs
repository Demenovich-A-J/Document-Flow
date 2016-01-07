using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BL.DinamicJsonHelper;
using BL.Handlers.CurrentStepsHandlers;
using BL.Handlers.DocumentTemplatesHandlers;
using BL.Handlers.StepsHandlers;
using BL.Handlers.UsersHandlers;
using BL.Interfaces;
using BL.Models;

namespace BL.Handlers.DocumentHandlers
{
    public class DocumentCreationHandler
    {
        private IRepositoryHandler<CurrentStep> _currentStepsHandler =
            new CurrentStepsRepositoryHandler();

        private IRepositoryHandler<Document> _documentsHandler =
            new DocumentsRepositoryHandler();

        private Regex _replaceRegex = new Regex(@"#(\w+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

        private IRepositoryHandler<Step> _stepsHandler =
            new StepsRepositoryHandler();

        private IRepositoryHandler<DocumentTemplate> _templatesHandler =
            new DocumentTemplatesRepositoryHandler();

        private int _userId;

        private IRepositoryHandler<User> _usersHandler =
            new UsersRepositoryHandler();

        public DocumentCreationHandler(int userId)
        {
            _userId = userId;
        }

        public void ParseInputToDocument(object[] inputs)
        {
            var insertsList = DynamicJsonConverter.Convert(inputs);
            var newDocument = new Document();

            var templateIdPair = insertsList.FirstOrDefault(x => x.Item1 == "Id");
            if (templateIdPair == null)
            {
                throw new NullReferenceException();
            }

            var namePair = insertsList.FirstOrDefault(x => x.Item1 == "DocName");

            var templateId = int.Parse(templateIdPair.Item2);
            var template = _templatesHandler.FindById(templateId);

            newDocument.Type = template.Type;

            var user = _usersHandler.FindById(_userId);

            newDocument.User = user;

            newDocument.Name = namePair.Item2;
            newDocument.DateTime = DateTime.Now;

            newDocument.Text = CreateDocumentText(template.Text,
                insertsList.Where(x => !x.Item1.Contains("*")).ToList());

            _documentsHandler.Add(newDocument);

            var a = ((DocumentsRepositoryHandler) _documentsHandler)
                .GetUserDocumentByTime(_userId, newDocument.DateTime);

            newDocument = (_documentsHandler as DocumentsRepositoryHandler)
                .GetUserDocumentByTime(_userId, newDocument.DateTime);

            var usersIds = CreateDocumentPath(template.PositionsPath,
                insertsList.Where(x => x.Item1.Contains("*")).ToList());

            if (usersIds == null)
                throw new ArgumentNullException();

            var serialNumber = 1;
            var steps = new List<Step>();
            foreach (var id in usersIds)
            {
                _stepsHandler.Add(
                    new Step
                    {
                        User = _usersHandler.FindById(id),
                        SerialNumber = serialNumber++,
                        Document = newDocument
                    });
            }

            var firstStep = ((StepsRepositoryHandler) _stepsHandler)
                .GetDocumentSteps(newDocument.Id)
                .FirstOrDefault(x => x.SerialNumber == 1);

            _currentStepsHandler.Add(
                new CurrentStep
                {
                    StepId = firstStep.Id,
                    UserId = firstStep.User.Id,
                    DocumentId = newDocument.Id
                });
        }

        private string CreateDocumentText(string text, List<Tuple<string, string>> inserts)
        {
            if (string.IsNullOrEmpty(text))
                throw new NullReferenceException();

            var matchCollection = _replaceRegex.Matches(text);

            foreach (Match match in matchCollection)
            {
                var regex = new Regex(Regex.Escape(match.Value));
                var description = match.Groups[1].ToString();
                var insert = inserts.FirstOrDefault(x => x.Item1 == description);
                if (insert == null || string.IsNullOrEmpty(insert.Item2))
                {
                    throw new ArgumentNullException();
                }
                text = regex.Replace(text, insert.Item2, 1);

                inserts.Remove(insert);
            }

            return text;
        }

        private List<int> CreateDocumentPath(string text, List<Tuple<string, string>> inserts)
        {
            if (string.IsNullOrEmpty(text))
                throw new NullReferenceException();

            var matchCollection = _replaceRegex.Matches(text);
            var path = new List<int>();

            if (matchCollection.Count != 0)
            {
                foreach (Match match in matchCollection)
                {
                    var description = "*" + match.Groups[1].ToString();
                    var insert = inserts.FirstOrDefault(x => x.Item1 == description);
                    if (insert == null)
                    {
                        throw new ArgumentNullException();
                    }

                    inserts.Remove(insert);
                    path.Add(Int32.Parse(insert.Item2));
                }
            }

            return path;
        }
    }
}