namespace SeanProfileBlazor.Models
{
	public class ImageFile
	{
		public string base64data { get; set; } = string.Empty;
		public string contentType { get; set; } = string.Empty;
		public string fileName { get; set; } = string.Empty;
	}
	public class File
	{
		public string? Name { get; set; }
	}

}
