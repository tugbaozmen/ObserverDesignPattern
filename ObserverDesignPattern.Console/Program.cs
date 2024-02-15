//Observer Design Pattern
//Observer Design Pattern, aralarında one-to-many ilişki bulunan ve nesneler arası bağımlılıkların söz konusu olduğu durumlarda, bağımlı nesnelerin bağlı olunan nesnenin durumuna göre güncellenebilmesi/haberdar olabilmesi amacı ile kullanılır. Bu durumda bağımlı olunan(takip edilen, abone olunan) nesne Subject; bağımlı olan(abone) nesne ise Observer olarak isimlendirilir. 
//Ürünün stoğa eklenmesinde bize bildirim gelmesi mantığıdır.

var channelName = new Channel("Noluyo Yaa", "Sokak Lezzetleri");
var videoUploadObserver = new VideoUploadObserver("Noluyo Yaa youtube");
var youtube = new Youtube();

//Nolu yaa kanalına(Channel) video yüklenirse bana(VideoUploadObserver) haber verin demektir.
youtube.ToBeInformed(videoUploadObserver, channelName);
youtube.NotifyForChannelName("Noluyo Yaa");
class Youtube
{
    //Burada 1 kişinin 1 kanaldan haberdar olmasını yaptık. İsteğe bağlı olarak list de yapılabilir.
    private Dictionary<IObserver, Channel> observers = new();
    //Bu metot bize abone olunan kanala video yüklendiğinde yüklendi bilgisinin gideceği metot. O yüzden kanal bilgilerini ve hangi observer'a(bildirimi kimler açtıysa onun bilgisi) gideceğinin bilgilerini değişken olarak eklemeliyiz.
    //Yani hangi kanala video yüklenecek onun bilgisi girilecek ve kanala yeni video yüklendiğinde hangi observer çalışacak onun a bilgisi girilecek değişken olarak
    public void ToBeInformed(IObserver observer, Channel channel)
    {
        observers.Add(observer, channel);
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    //Tüm kanallara yüklenen videonun haberdar edilmesi
    public void NotifyAll()
    {
        foreach (var kv in observers)
        {
            kv.Key.VideoUpdate(kv.Value);
        }
    }

    //Kanala göre haber verme
    public void NotifyForChannelName(string channelName)
    {
        foreach(var kv in observers)
        {
            if (kv.Value.Name==channelName)
            {
                kv.Key.VideoUpdate(kv.Value);
            }
        }
    }
}

interface IObserver
{
    public string ChannelName { get; set; }
    void VideoUpdate(Channel channel);
}
class VideoUploadObserver : IObserver
{
    public string ChannelName { get; set; }
    public VideoUploadObserver(string channelName)
    {
        ChannelName = channelName ?? throw new ArgumentNullException(nameof(channelName));
    }

    //Bu metot hangi kanala yeni video yüklendiğinin bilgisini verecek kullanıcıya
    public void VideoUpdate(Channel channel)
    {
        Console.WriteLine(ChannelName + " Kanalına " + channel.Description + " adında video yüklendi.");
    }
}
//Kanal Bilgileri
class Channel
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Channel(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

