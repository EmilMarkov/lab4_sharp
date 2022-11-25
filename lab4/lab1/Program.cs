using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab4
{
    class Program
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static public KeyValuePair<Edition, Magazine> generator(int j)
        {
            string value = (j * j * j * j * j * j).ToString();
            List<Person> persons = new List<Person>();
            List<Article> articles = new List<Article>();
            persons.Add(new Person(RandomString(j), RandomString(j), DateTime.Now));
            articles.Add(new Article(persons[0], RandomString(j), random.NextDouble()));
            Edition edition = new Edition(RandomString(j), j, DateTime.Now);
            Magazine magazine = new Magazine(RandomString(j), Frequency.Monthly, DateTime.Now, j, persons, articles);
            return new KeyValuePair<Edition, Magazine>(edition, magazine);
        }

        public static void Main()
        {
            // First magazine
            List<Person> persons1 = new List<Person>(2);
            persons1.Add(new Person("Emil", "Markov", DateTime.Parse("14.11.2003")));
            persons1.Add(new Person("Kolya", "Homyzhenko", DateTime.Parse("11.06.2003")));
            List<Article> articles1 = new List<Article>(2);
            articles1.Add(new Article(new Person("Egor", "Teterchev", DateTime.Parse("10.08.2003")), "FirstArticle", 10));
            articles1.Add(new Article(new Person("Dima", "Panfilov", DateTime.Parse("09.07.2003")), "SecondArticle", 8));
            Magazine t1 = new Magazine("MIET", Frequency.Monthly, DateTime.Now, 1, persons1, articles1);
            t1.Persons.AddRange(persons1);
            t1.Articles.AddRange(articles1);
            
            // Second magazine
            List<Person> persons2 = new List<Person>(2);
            persons2.Add(new Person("Petr", "Petrov", DateTime.Parse("14.11.2003")));
            persons2.Add(new Person("Artem", "Artemov", DateTime.Parse("11.06.2003")));
            List<Article> articles2 = new List<Article>(2);
            articles2.Add(new Article(new Person("Alex", "Hirsch", DateTime.Parse("10.08.2003")), "FirstArticle", 10));
            articles2.Add(new Article(new Person("Billy", "Harrington", DateTime.Parse("09.07.2003")), "SecondArticle", 8));
            Magazine t2 = new Magazine("MFTI", Frequency.Monthly, DateTime.Now, 1, persons2, articles2);
            t1.Persons.AddRange(persons2);
            t1.Articles.AddRange(articles2);
            
            // Third magazine
            List<Person> persons3 = new List<Person>(2);
            persons3.Add(new Person("Vladimir", "Krasnov", DateTime.Parse("14.11.2003")));
            persons3.Add(new Person("Joseph", "Gilgan", DateTime.Parse("11.06.2003")));
            List<Article> articles3 = new List<Article>(2);
            articles3.Add(new Article(new Person("Fred", "Weasley", DateTime.Parse("10.08.2003")), "FirstArticle", 10));
            articles3.Add(new Article(new Person("George", "Weasley", DateTime.Parse("09.07.2003")), "SecondArticle", 8));
            Magazine t3 = new Magazine("VSE", Frequency.Monthly, DateTime.Now, 1, persons3, articles3);
            t1.Persons.AddRange(persons3);
            t1.Articles.AddRange(articles3);
            
            // Forth magazine
            List<Person> persons4 = new List<Person>(2);
            persons4.Add(new Person("Ivan", "Ivanovich", DateTime.Parse("14.11.2003")));
            persons4.Add(new Person("Lil", "Peep", DateTime.Parse("11.06.2003")));
            List<Article> articles4 = new List<Article>(2);
            articles4.Add(new Article(new Person("Douglas", "Murray", DateTime.Parse("10.08.2003")), "FirstArticle", 10));
            articles4.Add(new Article(new Person("Johnny", "Depp", DateTime.Parse("09.07.2003")), "SecondArticle", 8));
            Magazine t4 = new Magazine("MSU", Frequency.Monthly, DateTime.Now, 1, persons4, articles4);
            t1.Persons.AddRange(persons4);
            t1.Articles.AddRange(articles4);
            
            //coolections for first task
            KeySelector<String> selector = magazine => magazine.GetHashCode().ToString();
            MagazineCollection<string> test_collection1 = new MagazineCollection<string>(selector);
            test_collection1.AddMagazines(t1);
            test_collection1.AddMagazines(t2);
            MagazineCollection<string> test_collection2 = new MagazineCollection<string>(selector);
            test_collection2.AddMagazines(t3);
            test_collection2.AddMagazines(t4);
            
            Console.WriteLine(test_collection1.ToString());
            Console.WriteLine("\n._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._.\n");
            Console.WriteLine(test_collection2.ToString());
            Console.WriteLine("\n._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._._.\n");
            
            //ex 2 in main
            Listener listener = new Listener();
            test_collection1.MagazinesChanged += listener.Add_changes;
            test_collection2.MagazinesChanged += listener.Add_changes;

            //ex 3 in main
            //for fifth magazine
            List<Person> persons5 = new List<Person>(2);
            persons5.Add(new Person("Alexandr", "Qqq", DateTime.Parse("14.11.2003")));
            persons5.Add(new Person("Eee", "Rrr", DateTime.Parse("14.11.2003")));
            List<Article> articles5 = new List<Article>(2);
            articles5.Add(new Article(new Person("Ddd", "Ggg", DateTime.Parse("14.11.2003")), "Hhh", 6));
            articles5.Add(new Article(new Person("Ccc", "Vvv", DateTime.Parse("14.11.2003")), "Jjj", 3));
            Magazine test_magazine = new Magazine("BMW", Frequency.Monthly, DateTime.Now, 2, persons5, articles5);
            test_magazine.Articles.AddRange(articles5);
            test_magazine.Persons.AddRange(persons5);
            

            //add element in collections
            test_collection1.AddMagazines(test_magazine);
            test_collection2.AddMagazines(test_magazine);

            //replace properties data
            t1.Circulation = 2;

            //replace element in collection
            test_collection1.Replace(t1, t3);
            test_collection2.Replace(t4, t2);

            //change data of deleted element
            t1.Circulation = 54321;
            t4.Date = new DateTime(2002, 8, 10);


            //ex 4 in main
            Console.WriteLine(listener.ToString());
        }
    }
}