using System.Text.Json.Serialization;	//新的JSON反序列化類別

namespace WebFront.Models
{

	public class Rootobject
	{
		public string Date { get; set; }

		//屬性名稱包含斜線，使用JsonPropertyName屬性來指定JSON中的名稱
		[JsonPropertyName("USD/NTD")]   //JSON屬性名稱
		public string USDNTD { get; set; } //C#屬性名稱
		public string RMBNTD { get; set; }
		public string EURUSD { get; set; }
		public string USDJPY { get; set; }
		public string GBPUSD { get; set; }
		public string AUDUSD { get; set; }
		public string USDHKD { get; set; }
		public string USDRMB { get; set; }
		public string USDZAR { get; set; }
		public string NZDUSD { get; set; }
	}

}
