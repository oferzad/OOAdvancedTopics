using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOAdvancedTopics
{
    // Define a delegate for event registration
    delegate void EventRegistration(string participantName);

    // Create an Event class
    class Event
    {
        public string EventName { get; }
        public int MaxParticipants { get; }
        public int CurrentParticipants { get; private set; }

        public Event(string eventName, int maxParticipants)
        {
            EventName = eventName;
            MaxParticipants = maxParticipants;
            CurrentParticipants = 0;
        }

        public void RegisterParticipant(EventRegistration registrationDelegate)
        {
            if (CurrentParticipants < MaxParticipants)
            {
                Console.Write("Enter participant's name: ");
                string name = Console.ReadLine();
                registrationDelegate(name);
                CurrentParticipants++;
            }
            else
            {
                Console.WriteLine("Sorry, the event is at maximum capacity and cannot accept more registrations.");
            }
        }
    }

    class ProgramMain
    {
        static void Main()
        {
            // Create an Event object for a coding workshop with a max of 10 participants
            Event codingWorkshop = new Event("Coding Workshop", 10);

            // Lambda expression to handle event registrations
            EventRegistration registerParticipant = (name) =>
            {
                Console.WriteLine($"{name} registered for the {codingWorkshop.EventName} event.");
            };

            Console.WriteLine($"Welcome to the {codingWorkshop.EventName} registration system!");

            while (codingWorkshop.CurrentParticipants < codingWorkshop.MaxParticipants)
            {
                codingWorkshop.RegisterParticipant(registerParticipant);
            }

            Console.WriteLine($"The {codingWorkshop.EventName} event is now full.");
        }
    }

}
