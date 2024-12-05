using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5001/api/Patients") };

        for (int i = 0; i < 100; i++)
        {
            var patient = GeneratePatient();
            var response = await httpClient.PostAsJsonAsync("", patient);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Patient {i + 1} added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add patient {i + 1}.");
            }
        }
    }

    static Patient GeneratePatient()
    {
        var random = new Random();
        var genders = new[] { "male", "female", "other", "unknown" };
        var familyNames = new[] { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов" };
        var givenNames = new[] { "Иван", "Алексей", "Дмитрий", "Сергей", "Андрей" };

        return new Patient
        {
            Id = Guid.NewGuid(),
            Name = new Name
            {
                Id = Guid.NewGuid(),
                Use = "official",
                Family = familyNames[random.Next(familyNames.Length)],
                Given = new List<string> { givenNames[random.Next(givenNames.Length)], givenNames[random.Next(givenNames.Length)] }
            },
            Gender = genders[random.Next(genders.Length)],
            BirthDate = DateTime.Now.AddYears(-random.Next(1, 10)),
            Active = random.Next(2) == 0
        };
    }
}

public class Patient
{
    public Guid Id { get; set; }
    public Name Name { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}

public class Name
{
    public Guid Id { get; set; }
    public string Use { get; set; }
    public string Family { get; set; }
    public List<string> Given { get; set; }
}