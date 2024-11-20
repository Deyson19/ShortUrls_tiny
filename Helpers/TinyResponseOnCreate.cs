namespace ShortUrls.Helpers
{
     public class TinyResponseOnCreate
     {
          public Data Data { get; set; }

          public long Code { get; set; }

          public object[] Errors { get; set; }
     }

     public class Data
     {
          public string Domain { get; set; }

          public string Alias { get; set; }

          public bool Deleted { get; set; }

          public bool Archived { get; set; }

          public Analytics Analytics { get; set; }

          public object[] Tags { get; set; }

          public DateTimeOffset CreatedAt { get; set; }

          public object ExpiresAt { get; set; }

          public Uri TinyUrl { get; set; }

          public Uri Url { get; set; }
     }

     public class Analytics
     {
          public bool Enabled { get; set; }

          public bool Public { get; set; }
     }
     

}
