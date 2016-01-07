using System;
using System.Collections.Generic;
using System.Text;
using BL.Handlers.StepsHandlers;
using BL.Interfaces;
using BL.Models;

namespace BL.Handlers.DocumentHandlers
{
    public class DocumentViewCreator
    {
        protected IRepositoryHandler<Document> _documentsHandler =
            new DocumentsRepositoryHandler();

        protected IRepositoryHandler<Step> _stepsHandler =
            new StepsRepositoryHandler();

        public DocumentView GetDocumentView(int id)
        {
            var documentView = new DocumentView();

            if (id == null)
                throw new NullReferenceException();

            var document = _documentsHandler.FindById(id);

            documentView.Id = document.Id;
            documentView.Name = document.Name;
            documentView.Text = document.Text;

            var steps = new List<Step>(((StepsRepositoryHandler) _stepsHandler)
                .GetDocumentSteps(document.Id));

            var path = new StringBuilder();
            foreach (var step in steps)
            {
                string fullName = step.User == null ? 
                    "Неизвестный" : 
                    step.User.FirstName + " " + step.User.LastName + " " + step.User.Patronymic;
               
                var status = step.Status == true
                    ? "Подписано"
                    : step.Status == false ? "Отклонено" : "Не подписано";

                path.Append("<span>" + fullName + "-" + status + "</span></br>");
            }
            documentView.Path = path.ToString();
            return documentView;
        }
    }
}