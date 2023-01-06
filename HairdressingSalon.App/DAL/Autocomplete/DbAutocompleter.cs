using HairdressingSalon.App.DAL.Models;

namespace HairdressingSalon.App.DAL.Autocomplete
{
    public static class DbAutocompleter
    {
        public static List<Client> Clients { get; private set; }
        public static List<Feedback> Feedbacks { get; private set; }
        public static List<Order> Orders { get; private set; }
        public static List<Service> Services { get; private set; }
        public static List<ServiceKind> ServiceKinds { get; private set; }
        public static List<Worker> Workers { get; private set; }

        #region Data

        private readonly static string[] _surnames =
        {
            "Smirnov", "Ivanov", "Kuznecov", "Sokolov", "Popov", "Lebedev", "Kozlov", "Novikov", "Morozov",
            "Petrov", "Volkov", "Soloviev", "Vasiliev", "Zaicev", "Pavlov", "Semenov", "Golubev", "Vinogradov",
            "Bogdanov", "Vorobiev", "Fedorov", "Michailov", "Belyaev", "Tarasov", "Belov", "Komarov",
            "Orlov", "Kiselev", "Makarov", "Andreev", "Kovalev", "Iliin", "Gusev"
        };

        private readonly static string[] _womanNames =
        {
            "Anastasiya", "Anna", "Mariya", "Elena", "Daria", "Alina", "Irina", "Ekaterina", "Arina", "Vladislava",
            "Polina", "Olga", "Julia", "Tatiana", "Natalia", "Viktoria", "Elizaveta", "Ksenia", "Milana", "Veronika",
            "Alisa", "Valeria", "Aleksandra", "Uliana", "Christina", "Sophia", "Lilia"
        };

        private readonly static string[] _manNames =
        {
            "Aleksandr", "Dmitriy", "Maksim", "Sergey", "Andrew", "Aleksey", "Artem", "Iliya", "Kirill", "Michail",
            "Nikita", "Matvei", "Roman", "Egor", "Arseniy", "Ivan", "Denis", "Evgeniy", "Daniil", "Timofey",
            "Vladislav", "Igor", "Vladimir", "Pavel", "Ruslan", "Mark", "Konstantin"
        };

        private readonly static string[] _middleNames =
        {
            "Aleksandrov", "Dmitriev", "Maksimov", "Sergeev", "Andreev", "Alekseev", "Kirillov", "Michailov",
            "Matveev", "Romanov", "Egorov", "Arseniev", "Ivanov", "Denisov", "Evgeniev", "Danilov",
            "Timofeev", "Vladislavov", "Igorev", "Vladimirov", "Pavlov", "Raslanov", "Konstantinov"
        };

        private readonly static string[] _towns =
        {
            "Minsk", "Gomel", "Brest", "Mogilev", "Zhlobin", "Slonim", "Rechica", "Baranovichi",
            "Bobruisk", "Vitebsk", "Shklov", "Grodno"
        };

        private readonly static string[] _streets =
        {
            "Volkova", "Pervomaiskaya", "Shosseynaya", "Uritskogo", "Solnechnaya", "Sovietskaya",
            "Bakunina", "Lenina", "Barykina", "Polevaya", "Rabochiaya", "Kozlova", "Karibskogo"
        };

        private readonly static Dictionary<string, string[]> _serviceKinds = new Dictionary<string, string[]>
        {
            // стрижка
            { "Haircut", new string[2] {
                "Haircut for short, medium and long hair.",
                "https://www.hitekgroup.ru/upload/school/str/3.png" }
            }, 

            // мелирование
            { "Highlighting", new string[2] {
                "Partial lightening is a popular hair coloring technique, with which you can revive the main " +
                "color, make the image bright, stylish and memorable.",
                "https://i.pinimg.com/736x/94/5a/56/945a56ded4d4150c72f534a6cccb09ba.jpg" }
            }, 

            // окрашивание
            { "Staining", new string[2] {
                "Staining for short, medium and long hair.",
                "https://images.glavred.info/2019_11/1574522784-4663.jpg" }
            }, 

            // выпрямление
            { "Alignment (straightening)", new string[2] {
                "After keratin, the hair really becomes smooth and pleasant to the touch. Porous hair becomes shiny. " +
                "With proper care, the effect can be maintained for three to four months.",
                "https://shop.salonsecret.ru/media/wysiwyg/blog/post/instagram/CSZ0JeEtwJF.jpg" }
            }, 

            // укладка
            { "Styling", new string[2] {
                "Styling is a way to give shape and volume to your hair using a blow dryer and hot power tools.",
                "https://oir.mobi/uploads/posts/2021-04/1617651978_36-p-ukladka-volos-na-dlinnie-volosi-36.jpg" }
            }, 

            // ламинирование
            { "Lamination", new string[2] {
                "Hair lamination is a mechanical effect on the surface of the hair with the help of a special " +
                "straightening composition. This composition envelops the hair with the thinnest veil, " +
                "filling voids in the porous structure and smoothing out irregularities.",
                "https://maroshka.com/files/uploads/articles/laminirovanie-volos/2-plusy-minysy-laminirovaniya.jpg" }
            }, 

            // завивка
            { "Perm", new string[2] {
                "Perming hair is a procedure aimed at transforming straight strands into wavy ones.",
                "https://maroshka.com/files/uploads/articles/himzavivka/1-chto-takoe-himka.jpg" }
            }, 

            // наращивание
            { "Building", new string[2] {
                "The hair extension procedure consists in fixing donor strands in the root zone. As the native " +
                "hair grows, a correction is carried out, and the curls are re-fixed closer to the roots. " +
                "There are different extension techniques - cold and hot, using keratin capsules, ribbons, braids, etc.",
                "https://flario.by/img/foto_v_statyi/VOLOSI/mif-o-narash-4-.jpg" }
            },

            // лечение
            { "Hair treatment", new string[2] {
                "Hair restoration in a salon is a range of techniques. Usually, sessions with the use of special " +
                "equipment can achieve the greatest effect, and the result lasts longer.",
                "https://expertpovolosam.com/sites/default/files/styles/large/public/images/933-8861.jpg?itok=IwRNaao8" }
            },
        };

        private readonly static Dictionary<string, int> _feedbacks = new Dictionary<string, int>
        {
            { "So cool!", 5 },
            { "It can be better...", 3 },
            { "It was the worst service in my life! I am so ugly now!", 1 },
            { "Good.", 4 },
            { "I didnt wont exactly it.", 2 }
        };

        #endregion

        public static void GenerateRandomValues()
        {
            Clients = new List<Client>();
            Workers = new List<Worker>();
            ServiceKinds = new List<ServiceKind>();
            Orders = new List<Order>();
            Feedbacks = new List<Feedback>();
            Services = new List<Service>();

            for (int i = 0; i < 50; i++)
            {
                int sex = new Random((int)DateTime.Now.Ticks + i).Next(2);
                int surnameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_surnames.Length);
                int middleNameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_middleNames.Length);

                int townIndex = new Random((int)DateTime.Now.Ticks + i).Next(_towns.Length);
                int streetIndex = new Random((int)DateTime.Now.Ticks + i).Next(_streets.Length);
                int houseNumber = new Random((int)DateTime.Now.Ticks + i).Next(1, 50);
                int phone = new Random((int)DateTime.Now.Ticks + i).Next(1000000, 10000000);

                if (sex == 0)
                {
                    int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_womanNames.Length);

                    Clients.Add(
                        new Client
                        {
                            Id = i + 1,
                            Surname = _surnames[surnameIndex] + "a",
                            Name = _womanNames[nameIndex],
                            MiddleName = _middleNames[middleNameIndex] + "na",
                            Address = _towns[townIndex] + ", " + _streets[streetIndex] + ", " + houseNumber,
                            Telephone = $"+375 (29) {phone}"
                        });
                }
                else
                {
                    int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_manNames.Length);

                    Clients.Add(
                        new Client
                        {
                            Id = i + 1,
                            Surname = _surnames[surnameIndex],
                            Name = _manNames[nameIndex],
                            MiddleName = _middleNames[middleNameIndex] + "ich",
                            Address = _towns[townIndex] + ", " + _streets[streetIndex] + ", " + houseNumber,
                            Telephone = $"+375 (29) {phone}"
                        });
                }
            }

            for (int i = 0; i < 20; i++)
            {
                int sex = new Random((int)DateTime.Now.Ticks + i).Next(2);
                int surnameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_surnames.Length);
                int middleNameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_middleNames.Length);

                if (sex == 0)
                {
                    int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_womanNames.Length);

                    Workers.Add(
                        new Worker
                        {
                            Id = i + 1,
                            Surname = _surnames[surnameIndex] + "a",
                            Name = _womanNames[nameIndex],
                            MiddleName = _middleNames[middleNameIndex] + "na",
                        });
                }
                else
                {
                    int nameIndex = new Random((int)DateTime.Now.Ticks + i).Next(_manNames.Length);

                    Workers.Add(
                        new Worker
                        {
                            Id = i + 1,
                            Surname = _surnames[surnameIndex],
                            Name = _manNames[nameIndex],
                            MiddleName = _middleNames[middleNameIndex] + "ich",
                        });
                }
            }

            int index = 1;
            foreach (var key in _serviceKinds.Keys)
            {
                ServiceKinds.Add(new ServiceKind
                {
                    Id = index,
                    Name = key,
                    Description = _serviceKinds[key][0],
                    ImageUrl = _serviceKinds[key][1]
                });

                index++;
            }

            for (int i = 0; i < 500; i++)
            {
                int[] mins = new int[4] { 0, 15, 30, 45 };

                int year = new Random((int)DateTime.Now.Ticks + i).Next(2020, 2024);
                int month = new Random((int)DateTime.Now.Ticks + i).Next(1, 13);
                int day = new Random((int)DateTime.Now.Ticks + i).Next(1, 29);
                int hour = new Random((int)DateTime.Now.Ticks + i).Next(24);
                int minIndex = new Random((int)DateTime.Now.Ticks + i).Next(4);

                int clientIndex = new Random((int)DateTime.Now.Ticks + i).Next(Clients.Count);
                int workerIndex = new Random((int)DateTime.Now.Ticks + i).Next(Workers.Count);

                Orders.Add(new Order
                {
                    Id = i + 1,
                    DateTime = new DateTime(year, month, day, hour, mins[minIndex], 0),
                    ClientId = Clients[clientIndex].Id,
                    WorkerId = Workers[workerIndex].Id
                });
            }

            for (int i = 0; i < 1000; i++)
            {
                var keys = _feedbacks.Keys;
                int keyIndex = new Random((int)DateTime.Now.Ticks + i).Next(keys.Count);

                int orderIndex = new Random((int)DateTime.Now.Ticks + i).Next(Orders.Count);
                var order = Orders[orderIndex];

                int year = 0;
                int month = 0;
                if (order.DateTime.Month != 12)
                {
                    year = new Random((int)DateTime.Now.Ticks + i).Next(order.DateTime.Year, 2024);
                    month = new Random((int)DateTime.Now.Ticks + i).Next(order.DateTime.Month + 1, 13);
                }
                else
                {
                    year = new Random((int)DateTime.Now.Ticks + i).Next(order.DateTime.Year + 1, 2025);
                    month = new Random((int)DateTime.Now.Ticks + i).Next(1, 13);
                }

                int day = new Random((int)DateTime.Now.Ticks + i).Next(1, 29);
                int hour = new Random((int)DateTime.Now.Ticks + i).Next(24);
                int min = new Random((int)DateTime.Now.Ticks + i).Next(60);

                Feedbacks.Add(new Feedback
                {
                    Id = i + 1,
                    Text = keys.AsEnumerable().ElementAt(keyIndex),
                    Mark = _feedbacks[keys.AsEnumerable().ElementAt(keyIndex)],
                    DateTime = new DateTime(year, month, day, hour, min, 0),
                    OrderId = order.Id
                });
            }

            for (int i = 0; i < 1000; i++)
            {
                int code = new Random((int)DateTime.Now.Ticks + i).Next(10000, 100000);
                decimal price = new Random((int)DateTime.Now.Ticks + i).Next(25, 200);
                int serviceKindIndex = new Random((int)DateTime.Now.Ticks + i).Next(ServiceKinds.Count);
                int orderIndex = new Random((int)DateTime.Now.Ticks + i).Next(Orders.Count);

                Services.Add(new Service
                {
                    Id = i + 1,
                    Code = code,
                    Price = price,
                    ServiceKindId = ServiceKinds[serviceKindIndex].Id,
                    OrderId = Orders[orderIndex].Id
                });
            }
        }
    }
}
