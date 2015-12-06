using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.AbstractRepository;
using DAL.Repositories;
using EntityModels;

namespace BL.DocumentHandlers
{
    public class HtmlDocumentHandler
    {
        protected string FullUserName;
        protected DataRepository<Position> PositionsRepository;

        protected Regex ReplaceRegex = new Regex(@"#(\w+)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

        protected DataRepository<DocumentTemplate> TemplatesRepository;
        protected DataRepository<User> UsersRepository;

        public HtmlDocumentHandler(string fullUserName)
        {
            UsersRepository = new UsersRepository();
            PositionsRepository = new PositionsRepository();
            TemplatesRepository = new DocumentTemplatesRepository();

            FullUserName = fullUserName;
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
            var matchCollection = ReplaceRegex.Matches(text);

            return matchCollection.Count == 0
                ? text
                : matchCollection.Cast<object>()
                    .Where(match => dictionary.ContainsKey(match.ToString()))
                    .Aggregate(text, (current, match) => current.Replace(match.ToString(), dictionary[match.ToString()]));
        }

        protected async Task<Dictionary<string, string>> BuildDictionary()
        {
            var positions = PositionsRepository.GetAll(x => true);

            var dictionary = new Dictionary<string, string>
            {
                {"#БольшойТекст", "<textarea name='БольшойТекст'></textarea>"},
                {"#Текст", "<input name='Текст' type='text'></input>"},
                {"#ФИО", "<p name='ФИО'>" + FullUserName + "</p>"},
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
            var position = await PositionsRepository.FindById(id);
            return "<select name='" + position.Name + "'>" + OptionsHtml(id) + "</select>";
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