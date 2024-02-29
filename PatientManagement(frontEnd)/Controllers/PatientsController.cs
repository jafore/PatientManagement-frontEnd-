using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevApplication.Connection;
using Newtonsoft.Json;
using PatientManagement_frontEnd_.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatientManagement_frontEnd_.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly HttpClient _client;
             
        private Uri baseUri = new Uri("http://localhost:5021/api/");
        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
            _client = new HttpClient();
            _client.BaseAddress = baseUri;
        }

        // GET: Patients
        public IActionResult Index()
        {
            return View();
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            PatientView? patient = new PatientView();
            List<AllergiesDetailView>? allergies = new List<AllergiesDetailView>();
            List<NcdDetailView>? ncd = new List<NcdDetailView>();


            HttpResponseMessage res_patient = _client.GetAsync(_client.BaseAddress + $"Patients/{id}").Result;
            HttpResponseMessage res_Allergies = _client.GetAsync(_client.BaseAddress + $"AllergiesDetails/AllergiesDetailName?id={id}").Result;
            HttpResponseMessage  res_Ncds= _client.GetAsync(_client.BaseAddress + $"Ncddetails/NcdDetailsName?id={id}").Result;

            if (res_patient.IsSuccessStatusCode)
            {
                string data = res_patient.Content.ReadAsStringAsync().Result;
                patient = JsonConvert.DeserializeObject<PatientView>(data);
            }


            if (res_Allergies.IsSuccessStatusCode)
            {
                string data = res_Allergies.Content.ReadAsStringAsync().Result;
                allergies = JsonConvert.DeserializeObject<List<AllergiesDetailView>>(data);
            }

            if (res_Ncds.IsSuccessStatusCode)
            {
                string data = res_Ncds.Content.ReadAsStringAsync().Result;
                ncd = JsonConvert.DeserializeObject<List<NcdDetailView>>(data);
            }


            ViewBag.patient = patient;
            ViewBag.allergies = allergies;
            ViewBag.ncd = ncd;

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DiseasesId,Epilepsy")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DiseasesId,Epilepsy")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
