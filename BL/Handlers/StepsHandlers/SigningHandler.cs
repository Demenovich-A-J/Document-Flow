using System.Linq;
using BL.Handlers.CurrentStepsHandlers;
using BL.Handlers.DocumentHandlers;
using BL.Interfaces;
using BL.Models;

namespace BL.Handlers.StepsHandlers
{
    public class SigningHandler
    {
        protected CurrentStepsRepositoryHandler _currentStepsHandler =
            new CurrentStepsRepositoryHandler();

        protected IRepositoryHandler<Document> _documentsHandler =
            new DocumentsRepositoryHandler();

        protected StepsRepositoryHandler _stepsHandler =
            new StepsRepositoryHandler();

        public void Sign(int documentId)
        {
            var currentStep = _currentStepsHandler.GetDocumentStep(documentId);
            if (currentStep != null)
            {
                _currentStepsHandler.Remove(currentStep);

                var steps = _stepsHandler.GetDocumentSteps(documentId);
                var oldStep = _stepsHandler.FindById(currentStep.StepId);

                if (steps.Any())
                {
                    oldStep.Status = true;
                    _stepsHandler.Update(oldStep);

                    var newStep = steps.FirstOrDefault(x => x.SerialNumber == oldStep.SerialNumber + 1);
                    if (newStep != null)
                    {
                        if (newStep.User != null)
                        {
                            _currentStepsHandler.Add(new CurrentStep
                            {
                                StepId = newStep.Id,
                                DocumentId = documentId,
                                UserId = newStep.User.Id
                            });
                        }
                    }
                    else
                    {
                        var document = _documentsHandler.FindById(documentId);
                        document.Status = true;
                        _documentsHandler.Update(document);
                    }
                }
            }
        }

        public void Reject(int id)
        {
            var currentStep = _currentStepsHandler.GetDocumentStep(id);
            if (currentStep != null)
            {
                _currentStepsHandler.Remove(currentStep);

                var documentId = currentStep.DocumentId;

                var steps = _stepsHandler.GetDocumentSteps(id);
                var oldStep = _stepsHandler.FindById(currentStep.StepId);

                oldStep.Status = false;
                _stepsHandler.Update(oldStep);

                var document = _documentsHandler.FindById(documentId);
                document.Status = false;
                _documentsHandler.Update(document);
            }
        }
    }
}