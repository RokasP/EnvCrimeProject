﻿using EnvCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
	public class ManagerController : Controller
	{

		private readonly IEnvCrimeRepository repository;

		public ManagerController(IEnvCrimeRepository repo)
		{
			repository = repo;
		}

		public ViewResult StartManager()
		{
			return View(repository);
		}

		public ViewResult CrimeManager(string errandId)
		{
			ViewBag.RoleName = "avdelningschef";
			ViewBag.ErrandId = errandId;
			return View(repository);
		}
	}
}
