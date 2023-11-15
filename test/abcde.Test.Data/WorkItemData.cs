using abcde.Model;
using abcde.Test.Data.Base;
using Bogus;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace abcde.Test.Data
{
    public class WorkItemData : BaseData
    {
        public static IEnumerable<WorkItem> GetEnumerable()
        {
            Bogus.Faker faker = new Bogus.Faker();
            return new List<WorkItem>()
            {  //Goal Data

                new WorkItem
                {
                    Title = "Master Algebra",
                    Description = "Complete 10 algebraic equations",
                    OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                    StartDateTime = new DateTime(2023, 09, 13),
                    Status = ItemStatus.Notset,
                    //pick random Importance value
                    Important = faker.PickRandom<Importance>() ,
                    Urgent = faker.PickRandom<Urgency>(),
                    Complexity = Complexity.Easy,
                    IWantToDo = YesNo.Yes,
                    IHaveToDo = YesNo.Yes,
                    Children=new List<WorkItem>()
                    {
                           new WorkItem
                           {
                                Title = "Practice Equations",
                                Description = "Practice solving equations",
                                OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                StartDateTime = new DateTime(2023, 09, 13),
                                Status = ItemStatus.Notset,
                                //pick random Importance value
                                Important = faker.PickRandom<Importance>() ,
                                Urgent = faker.PickRandom<Urgency>(),
                                Complexity = Complexity.Easy,
                                IWantToDo = YesNo.Yes,
                                IHaveToDo = YesNo.Yes,
                                Children=new List<WorkItem>()
                                {
                                    new WorkItem
                                    {
                                        Title = "Solve Linear Equations",
                                        Description = "Solve 5 linear equations",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>() ,
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                  },
                                    new WorkItem
                                    {
                                        Title = "Solve Quadratic Equations",
                                        Description = "Solve 3 quadratic equations",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>() ,
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes,
                                    }
                                }
                           },
                           new WorkItem
                           {
                                Title = "Study Algebraic Theorems",
                                Description = "Study theorems related to algebra",
                                OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                                StartDateTime =  new DateTime(2023,09,13),
                                Status = ItemStatus.Notset,
                                //pick random Importance value
                                Important = faker.PickRandom<Importance>() ,
                                Urgent = faker.PickRandom<Urgency>(),
                                Complexity = Complexity.Easy,
                                IWantToDo = YesNo.Yes,
                                IHaveToDo = YesNo.Yes
                           },
                           new WorkItem
                           {
                                Title = "Algebraic Expressions",
                                Description = "Learn to simplify algebraic expressions",
                                OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                                StartDateTime =  new DateTime(2023,09,13),
                                Status = ItemStatus.Notset,
                                //pick random Importance value
                                Important = faker.PickRandom<Importance>() ,
                                Urgent = faker.PickRandom<Urgency>(),
                                Complexity = Complexity.Easy,
                                IWantToDo = YesNo.Yes,
                                IHaveToDo = YesNo.Yes
                           },
                           new WorkItem
                           {
                                Title = "Algebraic Functions",
                                Description = "Study functions and their properties",
                                OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                                StartDateTime =  new DateTime(2023,09,13),
                                Status = ItemStatus.Notset,
                                //pick random Importance value
                                Important = faker.PickRandom<Importance>() ,
                                Urgent = faker.PickRandom<Urgency>(),
                                Complexity = Complexity.Easy,
                                IWantToDo = YesNo.Yes,
                                IHaveToDo = YesNo.Yes
                           },
                           new WorkItem
                           {
                                Title = "Algebraic Graphs",
                                Description = "Learn to plot algebraic functions",
                                OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                                StartDateTime =  new DateTime(2023,09,13),
                                Status = ItemStatus.Notset,
                                //pick random Importance value
                                Important = faker.PickRandom<Importance>() ,
                                Urgent = faker.PickRandom<Urgency>(),
                                Complexity = Complexity.Easy,
                                IWantToDo = YesNo.Yes,
                                IHaveToDo = YesNo.Yes
                           }
                    },
                    Notes=new List<Note>(){ new Note {NoteText="Test note."} }
                },
                new WorkItem
                {
                    Title = "Learn Organic Chemistry",
                    Description = "Study the properties of alkanes",
                    OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                    StartDateTime =  new DateTime(2023,09,13),
                    Status = ItemStatus.Notset,
                    //pick random Importance value
                    Important = faker.PickRandom<Importance>() ,
                    Urgent = faker.PickRandom<Urgency>(),
                    Complexity = Complexity.Easy,
                    IWantToDo = YesNo.Yes,
                    IHaveToDo = YesNo.Yes,
                    Children=new List<WorkItem>()
                    {
                        new WorkItem
                        {
                            Title = "Study Alkanes",
                            Description = "Learn the basics of alkanes",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Study Alkanes",
                            Description = "Learn the basics of alkanes",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Study Alkanes",
                            Description = "Learn the basics of alkanes",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        }
                    }
                },
                new WorkItem
                {
                    Title = "Learn Programming",
                    Description = "Learn the basics of Python",
                    OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                    StartDateTime =  new DateTime(2023,09,13),
                    Status = ItemStatus.Notset,
                    //pick random Importance value
                    Important = faker.PickRandom<Importance>() ,
                    Urgent = faker.PickRandom<Urgency>(),
                    Complexity = Complexity.Easy,
                    IWantToDo = YesNo.Yes,
                    IHaveToDo = YesNo.Yes,
                    Children = new List<WorkItem>
                    {
                        new WorkItem
                        {
                            Title = "Learn Python Syntax",
                            Description = "Learn the basics of Python syntax",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Learn Python Functions",
                            Description = "Learn how to create functions in Python",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },

                        new WorkItem
                        {
                             Title = "Learn Python Loops",
                             Description = "Learn how to use loops in Python",
                             OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                             StartDateTime =  new DateTime(2023,09,13),
                             Status = ItemStatus.Notset,
                             //pick random Importance value
                             Important = faker.PickRandom<Importance>() ,
                             Urgent = faker.PickRandom<Urgency>(),
                             Complexity = Complexity.Easy,
                             IWantToDo = YesNo.Yes,
                             IHaveToDo = YesNo.Yes
                        }
                    }
                },
                new WorkItem
                {
                    Title = "Learn Spanish",
                    Description = "Learn basic Spanish phrases",
                    OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                    StartDateTime =  new DateTime(2023,09,13),
                    Status = ItemStatus.Notset,
                    //pick random Importance value
                    Important = faker.PickRandom<Importance>() ,
                    Urgent = faker.PickRandom<Urgency>(),
                    Complexity = Complexity.Easy,
                    IWantToDo = YesNo.Yes,
                    IHaveToDo = YesNo.Yes,
                    Children = new List<WorkItem>
                    {
                         new WorkItem
                         {
                             Title = "Learn Greetings",
                             Description = "Learn basic Spanish greetings",
                             OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                             StartDateTime =  new DateTime(2023,09,13),
                             Status = ItemStatus.Notset,
                             //pick random Importance value
                             Important = faker.PickRandom<Importance>() ,
                             Urgent = faker.PickRandom<Urgency>(),
                             Complexity = Complexity.Easy,
                             IWantToDo = YesNo.Yes,
                             IHaveToDo = YesNo.Yes,
                        },
                        new WorkItem
                        {
                            Title = "Learn Numbers",
                            Description = "Learn numbers in Spanish",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Learn Basic Phrases",
                            Description = "Learn basic phrases in Spanish",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        }
                    }
                },
                new WorkItem
                {
                    Title = "Master Cooking",
                    Description = "Learn to cook 5 new dishes",
                    OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                    StartDateTime =  new DateTime(2023,09,13),
                    Status = ItemStatus.Notset,
                    //pick random Importance value
                    Important = faker.PickRandom<Importance>() ,
                    Urgent = faker.PickRandom<Urgency>(),
                    Complexity = Complexity.Easy,
                    IWantToDo = YesNo.Yes,
                    IHaveToDo = YesNo.Yes,
                    Children =new List<WorkItem>()
                    {
                        new WorkItem
                        {
                            Title = "Learn Pasta",
                            Description = "Learn to cook pasta",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Learn Curry",
                            Description = "Learn to cook curry",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Learn Sushi",
                            Description = "Learn to make sushi",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Learn Desserts",
                            Description = "Learn to make desserts",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        },
                        new WorkItem
                        {
                            Title = "Learn Soup",
                            Description = "Learn to make soup",
                            OriginalPlannedEndDateTime =new DateTime(2023,09,17,17,00,00),
                            StartDateTime =  new DateTime(2023,09,13),
                            Status = ItemStatus.Notset,
                            //pick random Importance value
                            Important = faker.PickRandom<Importance>() ,
                            Urgent = faker.PickRandom<Urgency>(),
                            Complexity = Complexity.Easy,
                            IWantToDo = YesNo.Yes,
                            IHaveToDo = YesNo.Yes
                        }
                    }
                },

                //Learn German Data   
                new WorkItem
                {
                        Title = "Learn German to B1 Proficiency level",
                        Description = "The overarching goal is to achieve a B1 proficiency level in the German language. This level signifies that you can understand and communicate in everyday situations, describe experiences, and discuss plans. The B1 level is often considered the threshold of independent language use, allowing you to navigate through German-speaking countries without significant difficulty. This goal is comprehensive, covering various aspects of the language, including grammar, vocabulary, listening, and practical application. The plan is structured into weekly tasks and daily sub-tasks to provide a step-by-step approach to mastering the language.",
                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                        StartDateTime = new DateTime(2023, 09, 13),
                        Status = ItemStatus.Notset,
                        //pick random Importance value
                        Important = faker.PickRandom<Importance>(),
                        Urgent = faker.PickRandom<Urgency>(),
                        Complexity = Complexity.Easy,
                        IWantToDo = YesNo.Yes,
                        IHaveToDo = YesNo.Yes,
                        Children = new List<WorkItem>()
                        {

                                    new WorkItem
                                    {
                                        Title = "Day 1 : Introduction and Basic Phrases",
                                        Description = "Grammar: The day starts with familiarizing yourself with the German alphabet and pronunciation. This is the cornerstone for all future learning.Vocabulary: You'll learn common greetings like \"\"Hallo\"\" and \"\"Guten Tag,\"\" which are essential for basic interactions.\r\nListening: Spend 10 minutes on \"\"Langsam gesprochene Nachrichten\"\" to get used to the sound and rhythm of the language.Practice: Use Duolingo or Babbel for 15 minutes to reinforce what you've learned.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 2 : Introduction and Basic Phrases",
                                        Description = "Grammar: Introduction to noun genders (der, die, das) is crucial as they influence sentence structure.Vocabulary: Learn how to introduce yourself with phrases like Wie heißt du? and Ich heiße...Listening: Listen to a German beginner's podcast for 10 minutes to improve auditory comprehension.Practice: Spend 15 minutes on Babbel or Duolingo exercises to solidify your learning.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 3 : Introduction and Basic Phrases",
                                        Description = "Grammar: You'll learn about regular verb conjugation in the present tense, a fundamental aspect of sentence construction.Vocabulary: Numbers from 1-20 will be covered, useful for various situations like shopping or telling time.Listening: Continue with Langsam gesprochene Nachrichten or a beginner's podcast for 10 minutes.Practice: Take a vocabulary quiz focusing on greetings and introductions to assess your grasp of the words.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 4 : Introduction and Basic Phrases",
                                        Description = "Grammar: Personal pronouns like ich,du,er,etc., are introduced. These are essential for creating basic sentences.Vocabulary: Learn the days of the week, useful for making plans or appointments.Listening: Listen to German songs with simple lyrics to improve your listening skills.Practice: Use Duolingo or Babbel to focus on exercises related to personal pronouns.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 5 : Introduction and Basic Phrases",
                                        Description = "Grammar: The verb sein (to be) in the present tense is introduced, a critical verb used frequently in daily conversations.Vocabulary: Learn basic adjectives like groß (big) and klein (small) to describe objects or people.Listening: Spend 10 minutes on a German beginner's podcast to improve listening comprehension.Practice: Make sentences using the verb sein to understand its application better.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 6 : Introduction and Basic Phrases",
                                        Description = "Grammar: The verb haben (to have) in the present tense is introduced, another essential verb for daily interactions.Vocabulary: Learn about colors, which will help in descriptions and identifications.Listening: Watch a short German video for beginners with subtitles to improve listening and comprehension.Practice: Do exercises focusing on the verbs sein and haben to solidify your understanding.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 7 : Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.Listening: Listen to a story or short dialogue in German to test your comprehension.Practice: Engage in a mock conversation using the vocabulary and grammar you've learned to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },



                                    new WorkItem
                                    {
                                        Title = "Day 8 : Expanding Basics",
                                        Description = "Grammar: Introduction to the accusative case, which is essential for sentence structure and object identification.\r\nVocabulary: Learn about body parts, useful for describing physical conditions or medical situations.\r\nListening: Spend 10 minutes on Langsam gesprochene Nachrichten to continue honing your listening skills.\r\nPractice: Use Duolingo or Babbel to focus on exercises related to body parts.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 9 : Expanding Basics",
                                        Description = "Grammar: Further explore regular verbs in the present tense, expanding your ability to construct various sentences.\r\nVocabulary: Learn about common foods, useful for grocery shopping or dining out.\r\nListening: Listen to a German children's story to improve your comprehension.\r\nPractice: Make sentences using the new verbs you've learned.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 10 : Expanding Basics",
                                        Description = "Grammar: Learn how to construct negative sentences using nicht, essential for expressing negation or refusal.\r\nVocabulary: Learn about common drinks, useful for social situations or dining out.\r\nListening: Continue with a German beginner's podcast to improve your listening skills.\r\nPractice: Make negative sentences to practice the new grammar rule.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 11 : Expanding Basics",
                                        Description = "Grammar: Learn how to ask questions using wo (where) and was (what), crucial for gathering information.\r\nVocabulary: Learn about rooms in a house, useful for describing living conditions or giving directions.\r\nListening: Listen to German songs with simple lyrics to improve your auditory comprehension.\r\nPractice: Use Duolingo or Babbel to focus on the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 12 : Expanding Basics",
                                        Description = "Grammar: Introduction to modal verbs like können (can) and müssen (must), which are essential for expressing ability or necessity.\r\nVocabulary: Learn about common animals, useful for various contexts like travel or conversation.\r\nListening: Spend 10 minutes on a German beginner's podcast to improve your listening skills.\r\nPractice: Do exercises focusing on modal verbs to understand their usage better.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 13 : Expanding Basics",
                                        Description = "Grammar: Introduction to the dative case, which is crucial for indicating the indirect object in a sentence.Vocabulary: Learn about common professions, useful for social interactions or job-related discussions.Listening: Watch a short German video for beginners with subtitles to improve your listening and comprehension skills.Practice: Make sentences using the dative case to solidify your understanding.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 14 : Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.Listening: Listen to a story or short dialogue in German to test your comprehension.Practice: Engage in a mock conversation using the vocabulary and grammar you've learned to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },



                                    new WorkItem
                                    {
                                        Title = "Day 15 : Daily Life Vocabulary & Basic Sentence Structures",
                                        Description = "Grammar: Dive deeper into irregular verbs in the present tense, which are commonly used in daily conversations.Vocabulary: Learn about clothing items, essential for shopping or describing attire.Listening: Spend 15 minutes on Langsam gesprochene Nachrichten to continue improving your listening skills.Practice: Use Duolingo or Babbel to focus on exercises related to clothing vocabulary.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 16 : Daily Life Vocabulary & Basic Sentence Structures",
                                        Description = "Grammar: Learn about word order in main clauses, a fundamental aspect of German sentence structure.Vocabulary: Learn how to tell time, crucial for daily planning and appointments.Listening: Listen to a German podcast episode focusing on daily routines.Practice: Practice asking and answering questions about what time it is.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 17 : Daily Life Vocabulary & Basic Sentence Structures",
                                        Description = "Grammar: Introduction to separable verbs, which are commonly used in everyday language.Vocabulary: Learn about family members, useful for personal conversations.Listening: Listen to a German children's story about family to improve your comprehension.Practice: Describe your family using the new vocabulary you've learned.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 18 : Daily Life Vocabulary & Basic Sentence Structures",
                                        Description = "Grammar: Learn the negative form of irregular verbs, useful for expressing negation in various contexts.Vocabulary: Learn about hobbies and interests, useful for social interactions.Listening: Listen to German songs focusing on daily activities to improve your auditory comprehension.Practice: Use Duolingo or Babbel to focus on exercises related to hobbies.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 19 : Daily Life Vocabulary & Basic Sentence Structures",
                                        Description = "Grammar: Introduction to comparative adjectives, useful for making comparisons in conversations.Vocabulary: Learn about the days of the week and months, essential for planning and scheduling.Listening: Spend 15 minutes on a German beginner's podcast to improve your listening skills.Practice: Create sentences comparing two objects or people.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 20 : Daily Life Vocabulary & Basic Sentence Structures",
                                        Description = "Grammar: Introduction to superlative adjectives, useful for emphasizing qualities or characteristics.Vocabulary: Learn about weather terms, essential for daily planning and small talk.Listening: Listen to a German weather forecast video to improve your comprehension.Practice: Describe today's weather using the new vocabulary you've learned.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 21 : Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.Listening: Listen to a story or dialogue in German to test your comprehension.Practice: Engage in a mock conversation about your week to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },



                                    new WorkItem
                                    {
                                        Title = "Day 22 : Deeper Dive into Verb Tenses & Routine Activities",
                                        Description = "Grammar: Introduction to the past tense (Perfekt), essential for talking about past events or experiences.Vocabulary: Learn about daily routines, crucial for describing your day-to-day activities.Listening: Spend 15 minutes on Langsam gesprochene Nachrichten to continue improving your listening skills.Practice: Describe your day using the past tense to understand its application better.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 23 : Deeper Dive into Verb Tenses & Routine Activities",
                                        Description = "Grammar: Learn about using modals in the past tense, useful for expressing past abilities or necessities.Vocabulary: Learn cooking and food-related terms, useful for kitchen activities or dining out.Listening: Listen to a German cooking show or recipe video to improve your comprehension.Practice: Talk or write about a meal you had to apply the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 24 : Deeper Dive into Verb Tenses & Routine Activities",
                                        Description = "Grammar: Introduction to dative pronouns, which are essential for indicating the indirect object in sentences.Vocabulary: Learn about city and transportation, useful for travel or giving directions.Listening: Listen to a German travel podcast or vlog to improve your auditory comprehension.Practice: Ask and give directions in German to practice the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 25 : Deeper Dive into Verb Tenses & Routine Activities",
                                        Description = "Grammar: Learn about prepositions with the dative case, essential for sentence structure.Vocabulary: Learn about leisure activities, useful for social interactions or planning outings.Listening: Listen to German songs or podcasts focusing on hobbies to improve your listening skills.Practice: Describe your favorite leisure activity using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 26 : Deeper Dive into Verb Tenses & Routine Activities",
                                        Description = "Grammar: Introduction to the future tense, crucial for talking about plans or future events.Vocabulary: Learn about office and school terms, useful for academic or professional settings.Listening: Listen to a German video focusing on school or work routines to improve your comprehension.Practice: Talk or write about your plans for the weekend to apply the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 27 : Deeper Dive into Verb Tenses & Routine Activities",
                                        Description = "Grammar: Learn about using the future tense with modal verbs, useful for expressing future possibilities or plans.Vocabulary: Learn about shopping and money, essential for retail interactions or financial planning.Listening: Listen to a German shopping vlog to improve your auditory comprehension.Practice: Describe something you want to buy in the future using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 28: Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.Listening: Listen to a story or dialogue in German to test your comprehension.Practice: Engage in a mock conversation about your daily routines and plans to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Day 29 : Interactions and Describing Surroundings",
                                        Description = "Grammar: Introduction to accusative pronouns, essential for object identification and sentence structure.Vocabulary: Learn about furniture and household items, useful for describing your living space or shopping.Listening: Listen to a German podcast or video about home tours to improve your comprehension.Practice: Describe your living space using the new vocabulary.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Day 30 : Interactions and Describing Surroundings",
                                        Description = "Grammar: Learn about prepositions with the accusative case, crucial for describing locations and directions.Vocabulary: Learn how to describe locations and places in a city, useful for travel or giving directions.Listening: Listen to German songs about cities or places to improve your auditory comprehension.Practice: Use Duolingo or Babbel to focus on exercises related to prepositions.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 31 : Interactions and Describing Surroundings",
                                        Description = "Grammar: Introduction to two-way prepositions (Wechselpräpositionen), essential for describing spatial relationships.Vocabulary: Learn about landscape and nature terms, useful for travel or outdoor activities.Listening: Listen to snippets from a German nature documentary to improve your comprehension.Practice: Describe a memorable place you've visited using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 32 : Interactions and Describing Surroundings",
                                        Description = "Grammar: Introduction to the genitive case, useful for indicating possession or relationships.Vocabulary: Learn about personal relationships and emotions, essential for social interactions.Listening: Listen to a German podcast focusing on personal stories to improve your listening skills.Practice: Talk or write about a personal experience using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 33 : Interactions and Describing Surroundings",
                                        Description = "Grammar: Learn about possessive pronouns in all cases, crucial for indicating ownership or relationships.Vocabulary: Learn about physical appearances, useful for describing people.Listening: Listen to a German TV show or movie clip focusing on character descriptions to improve your comprehension.Practice: Describe a friend or family member using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 34 : Interactions and Describing Surroundings",
                                        Description = "Grammar: Introduction to the past tense (Präteritum) for sein and haben,essential for talking about past states or conditions.Vocabulary: Learn about sports and activities, useful for social interactions or planning leisure time. Listening: Listen to German sports commentary or news to improve your auditory comprehension. Practice: Discuss your favorite sport or activity using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 35 : Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.Listening: Listen to a longer story or dialogue in German to test your comprehension.Practice: Engage in a mock conversation about places, activities, and descriptions to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Day 36 : Travel, Food, and Advanced Interactions",
                                        Description = "Grammar: Introduction to reflexive verbs, essential for describing actions that reflect back onto the subject.\r\nVocabulary: Learn about travel and vacation, useful for planning trips or discussing travel experiences.\r\nListening: Listen to German travel vlogs or documentaries to improve your comprehension.\r\nPractice: Plan a hypothetical trip in German using the new vocabulary.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 37 : Travel, Food, and Advanced Interactions",
                                        Description = "Grammar: Learn about prepositions with the accusative case, crucial for describing locations and directions.\r\nVocabulary: Learn how to describe locations and places in a city, useful for travel or giving directions.\r\nListening: Listen to German songs about cities or places to improve your auditory comprehension.\r\nPractice: Use Duolingo or Babbel to focus on exercises related to prepositions.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 38 : Travel, Food, and Advanced Interactions",
                                        Description = "Grammar: Introduction to two-way prepositions (Wechselpräpositionen), essential for describing spatial relationships.\r\nVocabulary: Learn about landscape and nature terms, useful for travel or outdoor activities.\r\nListening: Listen to snippets from a German nature documentary to improve your comprehension.\r\nPractice: Describe a memorable place you've visited using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 39 : Travel, Food, and Advanced Interactions",
                                        Description = "Grammar: Introduction to the genitive case, useful for indicating possession or relationships.\r\nVocabulary: Learn about personal relationships and emotions, essential for social interactions.\r\nListening: Listen to a German podcast focusing on personal stories to improve your listening skills.\r\nPractice: Talk or write about a personal experience using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 40 : Travel, Food, and Advanced Interactions",
                                        Description = "Grammar: Learn about possessive pronouns in all cases, crucial for indicating ownership or relationships.\r\nVocabulary: Learn about physical appearances, useful for describing people.\r\nListening: Listen to a German TV show or movie clip focusing on character descriptions to improve your comprehension.\r\nPractice: Describe a friend or family member using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 41 : Travel, Food, and Advanced Interactions",
                                        Description = "Grammar: Introduction to the past tense (Präteritum) for sein and haben,essential for talking about past states or conditions.\r\nVocabulary: Learn about sports and activities, useful for social interactions or planning leisure time.\r\nListening: Listen to German sports commentary or news to improve your auditory comprehension. \r\nPractice: Discuss your favorite sport or activity using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 42 : Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.\r\nListening: Listen to a longer story or dialogue in German to test your comprehension.\r\nPractice: Engage in a mock conversation about places, activities, and descriptions to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Day 43 : Work, Education, and Advanced Vocabulary",
                                        Description = "Grammar: Introduction to the passive voice in the present tense, essential for formal writing and academic discussions.\r\nVocabulary: Learn about jobs and professions, useful for job interviews or career planning.\r\nListening: Listen to a German interview about someone's job to improve your comprehension.\r\nPractice: Describe your job or a job you'd like to have using the new vocabulary.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 44 : Work, Education, and Advanced Vocabulary",
                                        Description = "Grammar: Learn about using the passive voice in the past tense, useful for discussing past events in a formal context.\r\nVocabulary: Learn about office terms and equipment, essential for professional settings.\r\nListening: Listen to a German podcast about work culture to improve your auditory comprehension.\r\nPractice: Discuss a typical day at work or school using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 45 : Work, Education, and Advanced Vocabulary",
                                        Description = "Grammar: Introduction to subordinate clauses, essential for constructing complex sentences.\r\nVocabulary: Learn about university and school subjects, useful for academic discussions or planning.\r\nListening: Listen to a German educational documentary or lecture to improve your comprehension.\r\nPractice: Talk about your educational background or interests using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 46 : Work, Education, and Advanced Vocabulary",
                                        Description = "Grammar: Learn how to use dass in subordinate clauses, useful for adding detail or explanation to sentences.\r\nVocabulary: Learn about academic terms and expressions, essential for scholarly discussions.\r\nListening: Listen to a German news segment on education to improve your listening skills.\r\nPractice: Express your opinion on a recent educational trend or news using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 47 : Work, Education, and Advanced Vocabulary",
                                        Description = "Grammar: Learn about conjunctions for complex sentences like obwohl and trotzdem, useful for expressing contrast or contradiction.\r\nVocabulary: Learn about computer and technology terms, essential for modern-day interactions.\r\nListening: Listen to a German tech podcast or review video to improve your auditory comprehension.\r\nPractice: Describe a piece of technology you use daily using the new vocabulary.\"\r\n",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 48 : Work, Education, and Advanced Vocabulary",
                                        Description = "Grammar: Introduction to the imperative mood, useful for giving commands or instructions.\r\nVocabulary: Learn about instructions and commands, essential for directing actions or tasks.\r\nListening: Listen to a German DIY or instructional video to improve your comprehension.\r\nPractice: Give instructions on a simple task using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 49 : Review and Practice",
                                        Description = "Review: Go over all the grammar and vocabulary learned during the week to reinforce your memory.\r\nListening: Listen to a German podcast episode or documentary to test your comprehension.\r\nPractice: Engage in a mock conversation about your work or academic experiences to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Day 50 : Task: Review, Immersion, and Final Preparation",
                                        Description = "Grammar & Vocabulary: Review topics from Week 1 and 2 to reinforce foundational knowledge.\r\nListening: Watch a German movie with subtitles to improve your comprehension.\r\nPractice: Write a short essay about yourself, including your hobbies and interests, to apply what you've learned.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 51 : Task: Review, Immersion, and Final Preparation",
                                        Description = "Grammar & Vocabulary: Review topics from Week 3 and 4 to solidify your understanding of intermediate concepts.\r\nListening: Tune into German radio or a longer podcast episode to improve your listening skills.\r\nPractice: Engage in a mock conversation about daily activities and places to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Day 52 : Task: Review, Immersion, and Final Preparation",
                                        Description = "Grammar & Vocabulary: Review topics from Week 5 and 6 to ensure you're comfortable with more advanced concepts.\r\nListening: Listen to a longer German audiobook or story to test your comprehension.\r\nPractice: Discuss a recent trip or event in detail to apply the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 53 : Task: Review, Immersion, and Final Preparation",
                                        Description = "Grammar & Vocabulary: Review topics from Week 7 to reinforce your understanding of advanced grammar.\r\nListening: Listen to German interviews about various professional fields to improve your auditory comprehension.\r\nPractice: Talk about a field of interest, whether academic or professional, using the new vocabulary.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 54 : Task: Review, Immersion, and Final Preparation",
                                        Description = "Grammar: Dive deeper into any areas of struggle to ensure you're fully prepared.\r\nVocabulary: Learn about phrasal verbs and idiomatic expressions, useful for natural conversations.\r\nListening: Listen to German music, focusing on lyrics and expressions to improve your comprehension.\r\nPractice: Try to use new idioms in sentences to apply what you've learned.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 55 : Task: Review, Immersion, and Final Preparation",
                                        Description = "Listening & Reading: Engage in more comprehensive listening and reading exercises to prepare for the B1 level exam.\r\nPractice: Take a mock B1 level exam or practice test if available to assess your readiness.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },
                                    new WorkItem
                                    {
                                        Title ="Day 56 : Review and Practice",
                                        Description = "Grammar & Vocabulary: Final review to ensure you're ready for the assessment.\r\nListening: Listen to German talk shows or discussions to test your comprehension.\r\nPractice: Engage in an extended conversation with a native speaker or tutor to assess your practical skills.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                                    new WorkItem
                                    {
                                        Title = "Days 57-59 : Immersion and Practice",
                                        Description = "Immersion: Dedicate these days to immerse yourself in German as much as possible. Listen, read, speak, and write only in German.Practice: Watch German movies, read German articles or books, and try to think in German to fully immerse yourself.",
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },

                                    new WorkItem
                                    {
                                        Title = "Day 60 : Assessment and Reflection",
                                        Description = "Assessment: Take a B1 level practice test or assessment to gauge your proficiency.\r\nReflection: Reflect on your progress and areas for improvement.\r\nNext Steps: Plan your next steps for further proficiency in German, setting new goals and strategies.",
                                        OriginalPlannedEndDateTime = new DateTime(2023, 09, 17, 17, 00, 00),
                                        StartDateTime = new DateTime(2023, 09, 13),
                                        Status = ItemStatus.Notset,
                                        //pick random Importance value
                                        Important = faker.PickRandom<Importance>(),
                                        Urgent = faker.PickRandom<Urgency>(),
                                        Complexity = Complexity.Easy,
                                        IWantToDo = YesNo.Yes,
                                        IHaveToDo = YesNo.Yes
                                    },


                        }
                    }
                };

        }
                            

        }
    }
