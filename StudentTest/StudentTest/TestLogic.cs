using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentTest
{
    class TestLogic
    {
        //1 Список студентів, які здали тести
        public void Query1()
        {
            var query = from tw in _testwork
                        join us in _users on tw.UserId equals us.UserId
                        join t in _tests on tw.UserId equals t.UserId
                        where tw.TestMark >= t.PassMark
                        orderby us.Name
                        select new
                        {
                            us.Name,
                            tw.TestMark
                        };
            foreach (var item in query)
            {
                Console.WriteLine("{0}, mark: {1}", item.Name, item.TestMark);
                
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }
        //2 Список студентів, які здали тести, та вклалися в час
        public void Query2()
        {
            var query = from tw in _testwork
                           join us in _users on tw.UserId equals us.UserId
                           join t in _tests on tw.UserId equals t.UserId
                           where tw.TestMark >= t.PassMark && tw.Time <= t.MaxTime
                           orderby us.Name
                           select new
                           {
                               us.Name,
                               tw.TestMark,
                               tw.Time
                           };
            foreach (var item in query)
            {
                Console.WriteLine("{0}, mark: {1}, test time: {2} min.", item.Name, item.TestMark, item.Time);

            }
            Console.WriteLine("------------------------------------------------------------------------");
        }
        
        //3 Список студентів, які здали тести, але не вклалися в час
        public void Query3()
        {
            var query = from tw in _testwork
                           join us in _users on tw.UserId equals us.UserId
                           join t in _tests on tw.UserId equals t.UserId
                           where tw.TestMark >= t.PassMark && t.MaxTime <= tw.Time
                           orderby us.Name
                           select new
                           {
                               us.Name,
                               tw.TestMark,
                               tw.Time
                           };
            foreach (var item in query)
            {
                Console.WriteLine("{0}, mark: {1}, test time: {2} min.", item.Name, item.TestMark, item.Time);
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }
        //4 Список студентів по містам(з Львова: 10, з Київа: 20)
        public void Query4()
        {
            var query = 
                           from tw in _testwork
                           join us in _users on tw.UserId equals us.UserId
                           where (us.City == "Lviv" || us.City == "Kyiv") 
                           orderby us.City + us.Name ascending 
                           select new
                           {
                               us.Name,
                               us.City
                           };
            foreach (var item in query)
            {
                Console.WriteLine("{0}, City: {1}", item.Name, item.City);
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }
        //5 Список успішних студентів по містам
        public void Query5()
        {
            var query =
                           from tw in _testwork
                           join us in _users on tw.UserId equals us.UserId
                           join t in _tests on tw.UserId equals t.UserId
                           where tw.TestMark >= t.PassMark && tw.Time <= t.MaxTime
                           orderby us.City + us.Name ascending
                           select new
                           {
                               us.Name,
                               us.City
                           };
            foreach (var item in query)
            {
                Console.WriteLine("{0}, City: {1}", item.Name, item.City);
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }
        //6 Результат по кожному студенту - його бали, час, бали у відсотках для кожної категорії.
        public void Query6()
        {
            //максимальний бал тестування: 90
            const int maxMark = 90;
            var query = from tw in _testwork
                           join us in _users on tw.UserId equals us.UserId
                           join t in _tests on tw.UserId equals t.UserId
                           join c in _categories on t.Category equals c.CategoryId
                           orderby us.Name
                           select new
                           {
                               us.Name,
                               tw.TestMark,
                               tw.Time,
                               percent = tw.TestMark * 100 / maxMark,
                               IdName = c.CategoryName
                           };
            foreach (var item in query)
            {
                Console.WriteLine("{0}, time:{1} min, mark:{2}, percent pass:{3}%, category: {4} ", item.Name, item.Time, item.TestMark, item.percent, item.IdName);
            }
            Console.WriteLine("------------------------------------------------------------------------");
        }

        public TestLogic()
        {
            DataInitialize();
        }

        static List<Users> _users;
        static List<Category> _categories;
        static List<Questions> _questions;
        static List<TestWork> _testwork;
        static List<Tests> _tests;
        private void DataInitialize()
        {
            UserInitialize();
            CategoryInitialize();
            QuestionsInitialize();
            TestWorkInitialize();
            TestsInitialize();
        }

        public void TestsInitialize()
        {
            _tests = new List<Tests>
            {
                new Tests
                {
                    TestsName = "Test 1",
                    Category = 1,
                    Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==1),
                                        _questions.Single(i => i.QuestionId==2),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 1
                },
                new Tests
                {
                    TestsName = "Test 2",
                    Category = 2,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==3),
                                        _questions.Single(i => i.QuestionId==4),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 2
                },
                new Tests
                {
                    TestsName = "Test 3",
                     Category = 6,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==5),
                                        _questions.Single(i => i.QuestionId==6),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 3
                },
                new Tests
                {
                    TestsName = "Test 4",
                    Category = 5,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==7),
                                        _questions.Single(i => i.QuestionId==8),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 4
                },
                new Tests
                {
                    TestsName = "Test 5",
                    Category = 4,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==9),
                                        _questions.Single(i => i.QuestionId==10),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 5
                },
                new Tests
                {
                    TestsName = "Test 6",
                    Category = 3,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==11),
                                        _questions.Single(i => i.QuestionId==12),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 6
                },
                new Tests
                {
                    TestsName = "Test 7",
                    Category = 4,
                    Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==1),
                                        _questions.Single(i => i.QuestionId==2),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 7
                },
                new Tests
                {
                    TestsName = "Test 8",
                    Category = 2,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==3),
                                        _questions.Single(i => i.QuestionId==4),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 8
                },
                new Tests
                {
                    TestsName = "Test 9",
                     Category = 1,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==5),
                                        _questions.Single(i => i.QuestionId==6),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 9
                },
                new Tests
                {
                    TestsName = "Test 10",
                    Category = 5,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==7),
                                        _questions.Single(i => i.QuestionId==8),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 10
                },
                new Tests
                {
                    TestsName = "Test 11",
                    Category = 6,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==9),
                                        _questions.Single(i => i.QuestionId==10),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 11
                },
                new Tests
                {
                    TestsName = "Test 12",
                    Category = 2,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==11),
                                        _questions.Single(i => i.QuestionId==12),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 12
                },
                new Tests
                {
                    TestsName = "Test 13",
                    Category = 1,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==1),
                                        _questions.Single(i => i.QuestionId==2),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 13
                },
                new Tests
                {
                    TestsName = "Test 14",
                    Category = 1,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==3),
                                        _questions.Single(i => i.QuestionId==4),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 14
                },
                new Tests
                {
                    TestsName = "Test 15",
                    Category = 4,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==5),
                                        _questions.Single(i => i.QuestionId==6),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 15
                },
                new Tests
                {
                    TestsName = "Test 16",
                    Category = 6,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==7),
                                        _questions.Single(i => i.QuestionId==8),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 16
                },
                new Tests
                {
                    TestsName = "Test 17",
                    Category = 5,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==9),
                                        _questions.Single(i => i.QuestionId==10),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 17
                },
                new Tests
                {
                    TestsName = "Test 18",
                    Category = 3,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==11),
                                        _questions.Single(i => i.QuestionId==12),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 18
                },
                new Tests
                {
                    TestsName = "Test 19",
                    Category = 3,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==1),
                                        _questions.Single(i => i.QuestionId==2),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 19
                },
                new Tests
                {
                    TestsName = "Test 20",
                    Category = 1,
                     Questions = new List<Questions> 
                                    { 
                                        _questions.Single(i => i.QuestionId==3),
                                        _questions.Single(i => i.QuestionId==4),
                                    },
                    MaxTime = 60,
                    PassMark = 50,
                    UserId = 20
                },
            };
        }

        public static void TestWorkInitialize()
        {
           _testwork = new List<TestWork>
            {
                new TestWork
                {
                    TestsName = "Test 1",
                    UserId = 1,
                    TestMark = 20,
                    Time = 55
                },
                new TestWork
                {
                    TestsName = "Test 2",
                    UserId = 2,
                    TestMark = 40,
                    Time = 63
                },
                new TestWork
                {
                    TestsName = "Test 3",
                    UserId = 3,
                    TestMark = 65,
                    Time = 70
                },
                new TestWork
                {
                    TestsName = "Test 4",
                    UserId = 4,
                    TestMark = 80,
                    Time = 59
                },
                new TestWork
                {
                    TestsName = "Test 5",
                    UserId = 5,
                    TestMark = 90,
                    Time = 45
                },
                new TestWork
                {
                    TestsName = "Test 6",
                    UserId = 6,
                    TestMark = 55,
                    Time = 65
                },
                new TestWork
                {
                    TestsName = "Test 7",
                    UserId = 7,
                    TestMark = 45,
                    Time = 52
                },
                new TestWork
                {
                    TestsName = "Test 8",
                    UserId = 8,
                    TestMark = 47,
                    Time = 40
                },
                new TestWork
                {
                    TestsName = "Test 9",
                    UserId = 9,
                    TestMark = 25,
                    Time = 39
                },
                new TestWork
                {
                    TestsName = "Test 10",
                    UserId = 10,
                    TestMark = 75,
                    Time = 55
                },
                new TestWork
                {
                    TestsName = "Test 11",
                    UserId = 11,
                    TestMark = 65,
                    Time = 59
                },
                new TestWork
                {
                    TestsName = "Test 12",
                    UserId = 12,
                    TestMark = 45,
                    Time = 65
                },
                new TestWork
                {
                    TestsName = "Test 13",
                    UserId = 13,
                    TestMark = 80,
                    Time = 49
                },
                new TestWork
                {
                    TestsName = "Test 14",
                    UserId = 14,
                    TestMark = 73,
                    Time = 52
                },
                new TestWork
                {
                    TestsName = "Test 15",
                    UserId = 15,
                    TestMark = 49,
                    Time = 55
                },
                new TestWork
                {
                    TestsName = "Test 16",
                    UserId = 16,
                    TestMark = 87,
                    Time = 55
                },
                new TestWork
                {
                    TestsName = "Test 17",
                    UserId = 17,
                    TestMark = 73,
                    Time = 57
                },
                new TestWork
                {
                    TestsName = "Test 18",
                    UserId = 18,
                    TestMark = 44,
                    Time = 38
                },
                new TestWork
                {
                    TestsName = "Test 19",
                    UserId = 19,
                    TestMark = 89,
                    Time = 54
                },
                new TestWork
                {
                    TestsName = "Test 20",
                    UserId = 20,
                    TestMark = 47,
                    Time = 61
                },
            };
        }

        public static void QuestionsInitialize()
        {
           _questions = new List<Questions>
            {
                new Questions
                {
                    QuestionId = 1, CategoryId = 1,
                    QuestionText = "Test Question 1",
                },
                new Questions
                {
                    QuestionId = 2, CategoryId = 1,
                    QuestionText = "Test Question 2",
                },
                new Questions
                {
                    QuestionId = 3, CategoryId = 2,
                    QuestionText = "Test Question 3",
                },
                new Questions
                {
                    QuestionId = 4, CategoryId = 2,
                    QuestionText = "Test Question 4",
                },
                new Questions
                {
                    QuestionId = 5, CategoryId = 3,
                    QuestionText = "Test Question 5",
                },
                new Questions
                {
                    QuestionId = 6, CategoryId = 3,
                    QuestionText = "Test Question 6",
                },
                new Questions
                {
                    QuestionId = 7, CategoryId = 4,
                    QuestionText = "Test Question 7",
                },
                new Questions
                {
                    QuestionId = 8, CategoryId = 4,
                    QuestionText = "Test Question 8",
                },
                new Questions
                {
                    QuestionId = 9, CategoryId = 5,
                    QuestionText = "Test Question 9",
                },
                new Questions
                {
                    QuestionId = 10, CategoryId = 5,
                    QuestionText = "Test Question 10",
                },
                new Questions
                {
                    QuestionId = 11, CategoryId = 6,
                    QuestionText = "Test Question 11",
                },
                new Questions
                {
                    QuestionId = 12, CategoryId = 6,
                    QuestionText = "Test Question 12",
                }
            };
        }

        public static void CategoryInitialize()
        {
            _categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1, CategoryName = ".NET"
                },
                new Category
                {
                    CategoryId = 2, CategoryName = "JS"
                },
                new Category
                {
                    CategoryId = 3, CategoryName = "PHP"
                },
                new Category
                {
                    CategoryId = 4, CategoryName = "DB"
                },
                new Category
                {
                    CategoryId = 5, CategoryName = "OOP"
                },
                new Category
                {
                    CategoryId = 6, CategoryName = "English"
                }
            };
        }

        public static void UserInitialize()
        {
            _users = new List<Users>
            {
                new Users
                {
                    Name = "Vovk Tatyana",
                    Email = "vovktatyana@ukr.net",
                    UserAge = 23,
                    City = "Lviv",
                    University = "Lviv Polytechnic National University",
                    Category = 1,
                    UserId = 1
                },
                new Users
                {
                    Name = "Andriy Nechyporenko",
                    Email = "nechuporenko@gmail.com",
                    UserAge = 26,
                    City = "Lviv",
                    University = "Lviv Polytechnic University",
                    Category = 2,
                    UserId = 2
                },
                new Users
                {
                    Name = "Nadiya Pavlyuk",
                    Email = "pavlukn@gmail.com",
                    UserAge = 27,
                    City = "Kyiv",
                    University = "Kyiv Polytechnic Institute",
                    Category = 6,
                    UserId = 3
                },
                new Users
                {
                    Name = "Valentyna Zadorozhna",
                    Email = "pzadorozhna@outlook.com",
                    UserAge = 22,
                    City = "Kyiv",
                    University = "National Aviation University",
                    Category = 5,
                    UserId = 4
                },
                new Users
                {
                    Name = "Valentyn Nalyvayko",
                    Email = "naluvaykovalentyn@gmail.com",
                    UserAge = 20,
                    City = "Kyiv",
                    University = "National Aviation University",
                    Category = 4,
                    UserId = 5
                },
                new Users
                {
                    Name = "Serhiy Nalyvayko",
                    Email = "naluvayko@yandex.ru",
                    UserAge = 25,
                    City = "Lviv",
                    University = "Lviv Polytechnic National University",
                    Category = 3,
                    UserId = 6
                },
                new Users
                {
                    Name = "Yuliya Onyshchuk",
                    Email = "onuschukyulia@mail.ua",
                    UserAge = 20,
                    City = "Lviv",
                    University = "Ivan Franko National University",
                    Category = 4,
                    UserId = 7
                },
                new Users
                {
                    Name = "Mykola Andriychuk",
                    Email = "mukolaandriychuk@gmail.com",
                    UserAge = 24,
                    City = "Ternopil",
                    University = "Ternopil National Technical University",
                    Category = 2,
                    UserId = 8
                },
                new Users
                {
                    Name = "Anna Savchuk",
                    Email = "savchuka@gmail.com",
                    UserAge = 21,
                    City = "Uzhhorod",
                    University = "Uzhhorod National University",
                    Category = 1,
                    UserId = 9
                },
                new Users
                {
                    Name = "Mariya Hryshko",
                    Email = "mariagrushko@gmail.com",
                    UserAge = 26,
                    City = "Lviv",
                    University = "Ivan Franko National University",
                    Category = 5,
                    UserId = 10
                },
                new Users
                {
                    Name = "Vasylyna Andrukhovych",
                    Email = "andruhovichv@yandex.ru",
                    UserAge = 29,
                    City = "Kyiv",
                    University = "Shevchenko National University of Kyiv",
                    Category = 6,
                    UserId = 11
                },
                new Users
                {
                    Name = "Andriana Mamay",
                    Email = "mamai@yandex.ru",
                    UserAge = 24,
                    City = "Kyiv",
                    University = "Shevchenko National University of Kyiv",
                    Category = 2,
                    UserId = 12
                },
                new Users
                {
                    Name = "Serhiy Pasichnyk",
                    Email = "sergiypasichnuk@gmail.com",
                    UserAge = 21,
                    City = "Lviv",
                    University = "Lviv Polytechnic National University",
                    Category = 1,
                    UserId = 13
                },
                new Users
                {
                    Name = "Bohdan Chumak",
                    Email = "chumakbohdan@gmail.com",
                    UserAge = 28,
                    City = "Lviv",
                    University = "Ivan Franko National University",
                    Category = 1,
                    UserId = 14
                },
                new Users
                {
                    Name = "Valentyn Romaniv",
                    Email = "romanivvalentun@outlook.com",
                    UserAge = 23,
                    City = "Kyiv",
                    University = "Shevchenko National University of Kyiv",
                    Category = 4,
                    UserId = 15
                },
                new Users
                {
                    Name = "Ihor Yakymiv",
                    Email = "yakumivigor@hotmail.com",
                    UserAge = 26,
                    City = "Kyiv",
                    University = "National Aviation University",
                    Category = 6,
                    UserId = 16
                },
                new Users
                {
                    Name = "Borys Melnychenko",
                    Email = "melnuchenko@mail.ru",
                    UserAge = 20,
                    City = "Kyiv",
                    University = "Kyiv Polytechnic Institute",
                    Category = 5,
                    UserId = 17
                },
                new Users
                {
                    Name = "Vasyl Luhovyy",
                    Email = "vasyllugoviy@ukr.net",
                    UserAge = 19,
                    City = "Lviv",
                    University = "Kyiv Polytechnic Institute",
                    Category = 3,
                    UserId = 18
                },
                new Users
                {
                    Name = "Stanislav Senchuk",
                    Email = "senchuk@mail.ua",
                    UserAge = 21,
                    City = "Lviv",
                    University = "National Aviation University",
                    Category = 3,
                    UserId = 19
                },
                 new Users
                {
                    Name = "Inna Ponomarenko",
                    Email = "innaponomarenko@gmail.com",
                    UserAge = 27,
                    City = "Kyiv",
                    University = "National Aviation University",
                    Category = 1,
                    UserId = 24
                },
           };
        }
    }
}
