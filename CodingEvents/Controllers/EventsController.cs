using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {

        //GET: /<controllers>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());

            return View(events);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            Event newEvent = new Event
            {
                Name = addEventViewModel.Name,
                Description = addEventViewModel.Description,
                ContactEmail = addEventViewModel.ContactEmail
            };

            EventData.Add(newEvent);

            return Redirect("/Events");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int id in eventIds)
            {
                EventData.Remove(id);
            }

            return Redirect("/Events");
        }

        
        [Route("/Events/Edit/{eventId}")] //I did put a ? in the {} so it read {eventId?} but that wasn't correct
        public IActionResult Edit(int eventId)
        {
            Event thisEvent = EventData.GetById(eventId);
            ViewBag.thisEvent = thisEvent;
            ViewBag.title = $"Edit Event {thisEvent.Name} (id={thisEvent.Id})";
            return View();
        }

        [HttpPost]
        [Route("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event thisEvent = EventData.GetById(eventId);
            thisEvent.Name = name;
            thisEvent.Description = description;
            return Redirect("/Events");
        }
    }
}
