using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;
using System;

namespace BL.DocumentHandlers
{
    public class HtmlDocumentHandler
    {
        protected string _fullUserName;
        protected DataRepository<Position> _positionsRepository;

        protected Regex ReplaceRegex = new Regex(@"#(\w+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

        protected DataRepository<DocumentTemplate> TemplatesRepository;
        protected DataRepository<User> _usersRepository;

        protected IEnumerable<Position> _positions;

        public HtmlDocumentHandler(string fullUserName)
        {
            _usersRepository = new UsersRepository();
            _positionsRepository = new PositionsRepository();
            TemplatesRepository = new DocumentTemplatesRepository();

            _fullUserName = fullUserName;
        }

        public async Task<DocumentTemplate> ConvertView(DocumentTemplate template)
        {
            _positions = _positionsRepository.GetAll(x => true);

            template.Text = ReplaceBy(template.Text, await BuildTextDictionary());
            template.PositionsPath = ReplaceBy(template.PositionsPath, await BuildPathDictionary());
            return template;
        }


        protected string ReplaceBy(string text, Dictionary<string, string> dictionary)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            var matchCollection = ReplaceRegex.Matches(text);

            return matchCollection.Count == 0
                ? text
                : matchCollection.Cast<object>()
                    .Where(match => dictionary.ContainsKey(match.ToString()))
                    .Aggregate(text, (current, match) => current.Replace(match.ToString(), dictionary[match.ToString()]));
        }

        #region Path
        private async Task<Dictionary<string, string>> BuildPathDictionary()
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var position in _positions)
            {
                dictionary.Add("#" + position.Name, await SelectHtml(position.Id, PathOptionsHtml));
            }

            return dictionary;
        }

        protected string PathOptionsHtml(int id)
        {
            var users = _usersRepository.GetAll(x => x.PositionId == id);

            StringBuilder optionsString = new StringBuilder();
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
        protected async Task<Dictionary<string, string>> BuildTextDictionary()
        {
            var dictionary = new Dictionary<string, string>
                {
                    {"#БольшойТекст", "<textarea name='БольшойТекст'></textarea>"},
                    {"#Текст", "<input name='Текст' type='text'></input>"},
                    {"#ФИО", "<p name='ФИО'>" + _fullUserName + "</p>"},
                    {"#Дата", "<input name='Дата' type='date'></input>"},
                    {"#Время", "<input name='Время' type='time'></input>"}
                };

            foreach (var position in _positions)
            {
                dictionary.Add("#" + position.Name, await SelectHtml(position.Id, TextOptionsHtml));
            }

            return dictionary;
        }

        protected string TextOptionsHtml(int id)
        {
            var users = _usersRepository.GetAll(x => x.PositionId == id);

            StringBuilder optionsString = new StringBuilder();
            foreach (var user in users)
            {
                var fullName = user.FirstName + " " + user.LastName + " " + user.Patronymic;
                optionsString.Append
                    ("<option value='" + fullName + "'>" + fullName + "</option>");
            }
            return optionsString.ToString();
        }
        #endregion

        protected async Task<string> SelectHtml(int id, Func<int, string> getOption)
        {
            var position = await _positionsRepository.FindById(id);
            return "<select name='" + position.Name + "'>" + getOption(id) + "</select>";
        }
    }
}