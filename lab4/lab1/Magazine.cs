using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4
{
    public class Magazine : Edition, IRateAndCopy
    {
        private Frequency frequency;
        private List<Person> persons = new List<Person>();
        private List<Article> articles = new List<Article>();

        public Magazine()
        {
            this.name = "Название журнала";
            this.frequency = Frequency.Weekly;
            this.date = new DateTime(2008, 5, 1);
            this.circulation = 0;
            this.articles.Add(new Article());
            this.persons.Add(new Person());
        }

        public Magazine(string titleValue, Frequency frequencyValue, DateTime dateValue, int editionValue, List<Person> personsValue, List<Article> articlesValue)
        {
            this.name = titleValue;
            this.frequency = frequencyValue;
            this.date = dateValue;
            this.circulation = editionValue;
            this.persons = personsValue;
            this.articles = articlesValue;
        }

        public List<Article> Articles
        {
            get => articles;
            set { this.articles = value; }
        }

        public double Rating { get; }

        public Frequency Frequency { get; }

        public List<Person> Persons
        {
            get => persons;
            set { this.persons = value; }
        }

        public double avgRating()
        {
            int N = 0;
            double sumRating = 0.0;
            foreach (Article article in articles)
            {
                sumRating += article.Rating;
                N++;
            }

            return sumRating / N;
        }

        public bool this[Frequency index]
        {
            get => index == frequency;
        }

        public void addArticle(Article article)
        {
            this.articles.Add(article);
        }

        public void addEditors(Person person)
        {
            this.persons.Add(person);
        }

        public Edition edition
        {
            get
            {
                Edition edition = new Edition(this.name, this.circulation, this.date);
                return edition;
            }
            set
            {
                this.name = value.Name;
                this.circulation = this.Circulation;
                this.date = value.Date;
            }
        }

        public int GetHashCode()
        {
            return HashCode.Combine(name.GetHashCode(), frequency.GetHashCode(), date.GetHashCode(), circulation.GetHashCode(), articles.GetHashCode(), persons.GetHashCode());
        }


        public override string ToString()
        {
            string result = "";
            double ratingSum = 0.0;

            foreach (Article article in articles)
            {
                ratingSum += article.Rating;
            }

            result += "\n";
            result += "*****************************\n";
            result += "Название журнала: " + this.name + '\n';
            result += "Периодичность выхода журнала: " + this.frequency + '\n';
            result += "Дата публикации: " + this.date.ToShortDateString() + '\n';
            result += "Издание: " + this.circulation + '\n';
            if (articles.Count != 0)
            {
                foreach (Article article in articles)
                {
                    result += "------------------------------\n";
                    result += "Автор статьи: " + article.Author.Name + " " + article.Author.Surname + '\n';
                    result += "Название статьи: " + article.Title + '\n';
                    result += "Рейтинг статьи: " + article.Rating + '\n';
                }
            }
            else
            {
                Console.WriteLine("Статьи не обнаружены!");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            result += "*****************************\n";
            Console.ForegroundColor = ConsoleColor.White;
            result += "\n";
            return result;
        }

        public virtual void ToShortString()
        {
            string result = "";
            double ratingSum = 0.0;

            foreach (Article article in articles)
            {
                ratingSum += article.Rating;
            }

            result += "\n";
            result += "*****************************\n";
            result += "Название журнала: " + this.name + '\n';
            result += "Периодичность выхода журнала: " + this.frequency + '\n';
            result += "Дата публикации: " + this.date.ToShortDateString() + '\n';
            result += "Издание: " + this.circulation + '\n';
            if (articles.Count != 0)
            {
                foreach (Article article in articles)
                {
                    result += "------------------------------\n";
                    result += "Автор статьи: " + article.Author.Name + " " + article.Author.Surname + '\n';
                    result += "Название статьи: " + article.Title + '\n';
                }
            }
            else
            {
                Console.WriteLine("Статьи не обнаружены!");
            }
            result += "------------------------------\n";
            result += "Средний рейтинг статей: " + this.avgRating() + '\n';
            result += "*****************************\n";
            result += "\n";
        }

        public virtual object DeepCopy()
        {
            Magazine magazine = new Magazine();
            magazine.name = this.Name;
            magazine.date = this.Date;
            magazine.circulation = this.Circulation;
            magazine.frequency = this.frequency;
            magazine.edition = this.edition;
            magazine.persons = this.persons;
            magazine.articles = this.articles;
            return (object)magazine;
        }

        public IEnumerable<Article> byRating(double ratingValue)
        {
            foreach (Article a in this.articles)
            {
                if (a.Rating >= ratingValue)
                    yield return a;
            }
        }

        public IEnumerable<Article> byNameSubstring(string subString)
        {
            foreach (Article a in this.articles)
            {
                if (a.Author.Name.IndexOf(subString) > -1)
                    yield return a;
            }
        }

        public void sortByRating()
        {
            articles.Sort();
        }

        public void sortBySurname()
        {
            ArticleComparerBySurname comparer = new ArticleComparerBySurname();
            articles.Sort(comparer);
        }

        public void sortByTitle()
        {
            ArticleComparerByTitle comparer = new ArticleComparerByTitle();
            articles.Sort(comparer);
        }
    }
}