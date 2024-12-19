using ContactPro.Services.Interfaces;

namespace ContactPro.Services
{
	public class ImageService : IImageService
	{
		#region Properties
		private readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
		private readonly string defaultImage = "img/DefaultContactImage.png";
		#endregion

		#region Convert Byte Array To File
		public string ConvertByteArrayToFile(byte[] fileData, string extension)
		{
			if (fileData is null) return defaultImage;

			try
			{
				string imageBase64Data = Convert.ToBase64String(fileData);
				return string.Format($"data:{extension};base64,{imageBase64Data}");
			}
			catch (Exception)
			{
				throw;
			}
		}
		#endregion

		#region Convert File To Byte Array
		public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
		{
			try
			{
				using MemoryStream memoryStream = new();
				await file.CopyToAsync(memoryStream);
				byte[] byteFile = memoryStream.ToArray();
				return byteFile;
			}
			catch (Exception)
			{
				throw;
			}
		} 
		#endregion
	}
}
