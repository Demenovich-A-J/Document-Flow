using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentHandlers
{
    public class HtmlDocumentHandler
    {
        protected string FullUserName;
        protected DataRepository<Position> PositionsRepository;
        protected DataRepository<DocumentTemplate> TemplatesRepository;
        protected DataRepository<User> UsersRepository;
        protected Regex ReplaceRegex = new Regex(@"#(\w+)",RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        public HtmlDocumentHandler(string fullUserName)
        {
            UsersRepository = new UsersRepository();
            PositionsRepository = new PositionsRepository();
            TemplatesRepository = new DocumentTemplatesRepository();
            FullUserName = fullUserName;
        }

        public DocumentTemplate ConvertView(DocumentTemplate template)
        {
            template.Text = ReplaceBy(template.Text, BuildDictionary());
            return template;
        }

        protected string ReplaceBy(string text, Dictionary<string, string> dictionary)
        {

            var matchCollection = ReplaceRegex.Matches(text);

            return matchCollection.Count == 0 ? text : matchCollection.Cast<object>().Where(match => dictionary.ContainsKey(match.ToString())).Aggregate(text, (current, match) => current.Replace(match.ToString(), dictionary[match.ToString()]));
        }

        protected Dictionary<string, string> BuildDictionary()
        {
            var positions = PositionsRepository.GetAll(x => true);

            var dictionary = new Dictionary<string, string>
            {
                {"#БольшойТекст", "<textarea></textarea>"},
                {"#Текст", "<input type='text'></input>"},
                {"#ФИО", FullUserName},
                {"#Дата", "<input type='date'></input>"},
                {"#Время", "<input type='time'></input>"}
            };

            foreach (var position in positions)
            {
                dictionary.Add("#" + position.Name, SelectHtml(position.Id));
            }

            return dictionary;
        }

        protected string SelectHtml(int id)
        {
            return "<select>" + OptionsHtml(id) + "</select>";
        }

        protected string OptionsHtml(int id)
        {
            var users = UsersRepository.GetAll(x => x.PositionId == id);

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
    }
}