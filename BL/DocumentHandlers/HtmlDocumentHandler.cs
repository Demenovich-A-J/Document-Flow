using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL.DocumentFandler
{
    public class HtmlDocumentHandler
    {
        protected DataRepository<User> _usersRepository;
        protected DataRepository<Position> _positionsRepository;
        protected DataRepository<DocumentTemplate> _templatesRepository;

        protected string _fullUserName;

        public HtmlDocumentHandler(string fullUserName)
        {
            _usersRepository = new UsersRepository();
            _positionsRepository = new PositionsRepository();
            _templatesRepository = new DocumentTemplatesRepository();

            _fullUserName = fullUserName;
        }

        public async Task<DocumentTemplate> ConvertView(DocumentTemplate template)
        {
            var dictionary = await BuildDictionary();
            template.Text = ReplaceBy(template.Text, dictionary);
            template.PositionsPath = ReplaceBy(template.PositionsPath, dictionary);
            return template;
        }

        protected string ReplaceBy(string text, Dictionary<string, string> dictionary)
        {
            string pattern = @"#(\w+)";

            var matchCollection =
                Regex.Matches(text, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

            if (matchCollection.Count != 0)
            {
                foreach (var match in matchCollection)
                {
                    if (dictionary.ContainsKey(match.ToString()))
                    {
                        text = text.Replace(match.ToString(), dictionary[match.ToString()]);
                    }
                }
            }

            return text;
        }

        protected async Task<Dictionary<string, string>> BuildDictionary()
        {
            var positions = _positionsRepository.GetAll(x => true);

            var dictionary = new Dictionary<string, string>
                {
                    {"#БольшойТекст", "<textarea name='БольшойТекст'></textarea>"},
                    {"#Текст", "<input name='Текст' type='text'></input>"},
                    {"#ФИО", "<p name='ФИО'>" + _fullUserName + "</p>"},
                    {"#Дата", "<input name='Дата' type='date'></input>"},
                    {"#Время", "<input name='Время' type='time'></input>"}
                };

            foreach (var position in positions)
            {
                dictionary.Add("#" + position.Name, await SelectHtml(position.Id));
            }

            return dictionary;
        }

        protected async Task<string> SelectHtml(int id)
        {
            var position = await _positionsRepository.FindById(id);
            return "<select name='" + position.Name + "'>" + OptionsHtml(id) + "</select>";
        }

        protected string OptionsHtml(int id)
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
    }
}
