using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BL.Handlers.PositionsHandlers;
using BL.Handlers.UsersHandlers;
using DAL.Repositories;
using DocumentTemplate = BL.Models.DocumentTemplate;
using Position = BL.Models.Position;

namespace BL.Handlers.DocumentHandlers
{
    public class HtmlDocumentHandler
    {
        protected string _fullUserName;

        protected IEnumerable<Position> _positions;
        protected PositionsRepositoryHandler _positionsRepository;

        protected Regex _replaceRegex = new Regex(@"#(\w+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

        protected DocumentTemplatesRepository _templatesRepository;
        protected UsersRepositoryHandler _usersRepository;

        public HtmlDocumentHandler(string fullUserName)
        {
            _usersRepository = new UsersRepositoryHandler();
            _positionsRepository = new PositionsRepositoryHandler();
            _templatesRepository = new DocumentTemplatesRepository();

            _fullUserName = fullUserName;
        }

        public DocumentTemplate ConvertView(DocumentTemplate template)
        {
            _positions = _positionsRepository.GetAll();

            template.Text = ReplaceBy(template.Text, BuildTextDictionary());
            template.PositionsPath = ReplaceBy(template.PositionsPath, BuildPathDictionary("*"));
            return template;
        }


        protected string ReplaceBy(string text, Dictionary<string, string> dictionary, string marker = "")
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var matchCollection = _replaceRegex.Matches(text);

            return matchCollection.Count == 0
                ? text
                : matchCollection.Cast<object>()
                    .Where(match => dictionary.ContainsKey(match.ToString()))
                    .Aggregate(text, (current, match) =>
                        current.Replace(match.ToString(), dictionary[match.ToString()]));
        }

        protected static string SelectHtml(Position position, Func<Position, string> getOption, string marker = "")
        {
            return "<select class = 'form-control required' style = 'width: auto; display: inline-block;' name='" + marker +
                   position.Name + "'>" + getOption(position) + "</select>";
        }

        #region Path

        private Dictionary<string, string> BuildPathDictionary(string marker = "")
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var position in _positions)
            {
                dictionary.Add("#" + position.Name, SelectHtml(position, PathOptionsHtml, marker));
            }

            return dictionary;
        }

        protected string PathOptionsHtml(Position position)
        {
            var users = _usersRepository.GetUserByPositionId(position.Id);

            var optionsString = new StringBuilder();
            foreach (var user in users)
            {
                optionsString.Append
                    ("<option value=" + user.Id + ">" +
                     user.FirstName + " " + user.LastName + " " + user.Patronymic +
                     "</option>");
            }
            return optionsString.ToString();
        }

        #endregion

        #region Text

        protected Dictionary<string, string> BuildTextDictionary()
        {
            var dictionary = new Dictionary<string, string>
            {
                {"#БольшойТекст", "<textarea class = 'form-control required' name='БольшойТекст'></textarea>"},
                {
                    "#Текст",
                    "<input class = 'form-control required' style = 'width: auto; display: inline-block;' name='Текст' type='text'></input>"
                },
                {
                    "#ФИО",
                    "<input class = 'form-control required' style = 'width: auto; display: inline-block;' type='hidden' name='ФИО' value='" +
                    _fullUserName + "'/><span>" + _fullUserName + "</span>"
                },
                {
                    "#Дата",
                    "<input class = 'form-control required' style = 'width: auto; display: inline-block;' name='Дата' type='date'></input>"
                },
                {
                    "#Время",
                    "<input class = 'form-control required' style = 'width: auto; display: inline-block;' name='Время' type='time'></input>"
                }
            };

            foreach (var position in _positions)
            {
                dictionary.Add("#" + position.Name, SelectHtml(position, TextOptionsHtml));
            }

            return dictionary;
        }

        protected string TextOptionsHtml(Position position)
        {
            var users = _usersRepository.GetUserByPositionId(position.Id);

            var optionsString = new StringBuilder();
            foreach (var user in users)
            {
                var fullName = user.FirstName + " " + user.LastName + " " + user.Patronymic;
                optionsString.Append
                    ("<option value='" + fullName + "'>" + fullName + "</option>");
            }
            return optionsString.ToString();
        }

        #endregion
    }
}