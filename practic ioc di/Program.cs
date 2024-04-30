using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Autofac;

namespace practic_ioc_di
{

    public interface IOutput
    {
        void Show(string content);
    }
    public class ConsoleOutput : IOutput
    {
        public void Show(string content)
        {
            Console.WriteLine(content);
        }
    }
    public class FileOutput : IOutput
    {
        public void Show(string content)
        {
            using (var file = new StreamWriter("test.txt"))
            { 
                file.WriteLine(content); 
            }
        }
    }
    public class Person
    {
        private readonly IOutput _output;

        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string OtherInfo { get; set; }

        public Person(IOutput output)
        {
            _output = output;
        }

        public void Show()
        {
            string info = $" Имя: {Name}\n Фамилия: {Lastname}\n Дата рождения: {BirthDate}\n Прочее: {OtherInfo}";
            _output.Show(info);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            using (var container = builder.Build())
            {
                var outputs = container.Resolve<IOutput>();
                Person person = new Person(outputs)
                {
                    Name = "Иван",
                    Lastname = "Иванов",
                    BirthDate = new DateTime(1990, 1, 1),
                    OtherInfo = "Тестовое поле"
                };              
                person.Show();            
            };

            /*IOutput output = new ConsoleOutput();
            IOutput Fileoutput = new FileOutput();

            Person person = new Person(Fileoutput);
            person.Name = "Иван";
            person.Lastname = "Иванов";
            person.BirthDate = new DateTime(1990, 1, 1);
            person.OtherInfo = "Тестовое поле";
            person.Show();*/
            

        }
    }
}
