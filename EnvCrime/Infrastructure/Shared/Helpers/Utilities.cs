namespace EnvCrime.Infrastructure.Shared.Helpers
{
	public class Utilities
	{
		private readonly IWebHostEnvironment environment;

		public Utilities(IWebHostEnvironment env)
		{
			environment = env;
		}

		public static String MakeUniqueName(String fileName)
		{
			return Guid.NewGuid().ToString() + "_" + fileName;
		}

		public async Task<String> UploadFile(IFormFile file, string subfolderName)
		{
			var tempFilePath = Path.GetTempFileName();
			using (var stream = new FileStream(tempFilePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			String uniqueFileName = MakeUniqueName(file.FileName);

			var finalFilePath = Path.Combine(environment.WebRootPath, "uploads", subfolderName, uniqueFileName);
			File.Move(tempFilePath, finalFilePath);

			return uniqueFileName;
		}
	}
}
