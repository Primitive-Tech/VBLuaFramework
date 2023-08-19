using System.ComponentModel;using System.IO;using System.Net.Http;using System.Net;using System.Web;using System.Linq;using Newtonsoft.Json;using NLua;using System.Text;

public class Server
{
    public string DataGrabber;public JsonSerializerSettings mysettings;
    
    public Server()
    {
        mysettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented
        };
    }

    // Basic_Methods 
    // Download Data over HTTPS (Async with ResponseBody)
    public async Task<string> GetData(string addr)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(addr);
            string outp = await response.Content.ReadAsStringAsync();
            
            if (outp != null)
            {
                return outp;
            }
            else
            {
                return false.ToString();
            }
        }
    }
    
    // Download Data over HTTPS (Async with ResponseCode)
    public async Task<(bool, string)> Connect(string addr)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(addr);
            string outp = await response.Content.ReadAsStringAsync();
            
            if (outp != null)
            {
                return (true, outp);
            }
            else
            {
                return (false, outp);
            }
        }
    }
    
    // Download Data over HTTPS (Low_Standart)
    public string GetStrData(string addr, User user, string pw)
    {
        #pragma warning disable SYSLIB0014
        WebClient client = new WebClient();
        #pragma warning restore SYSLIB0014
        
        string data = "";
        client.Credentials = new NetworkCredential(user.Name, pw);
        
        try
        {
            data = client.DownloadString(addr);
        }
        catch
        {
            MessageBox.Show("Downloading failed!");
        }
        
        return data;
    }
    
    // Upload Data over FTP
    public async Task<bool> sendData(string addr, string filename, byte[] content, User user, string pw)
    {
        #pragma warning disable SYSLIB0014
        WebRequest client = WebRequest.Create(addr + filename);
        #pragma warning restore SYSLIB0014
        
        client.Credentials = new NetworkCredential(user.Login, pw);
        client.Method = WebRequestMethods.Ftp.UploadFile;
        
        if (content != null)
        {
            Stream strFile = await client.GetRequestStreamAsync();
            strFile.Write(content, 0, content.Length);
            strFile.Close();
            strFile.Dispose();
            
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public async Task<bool> AppendData(string addr, string filename, byte[] content, User user, string pw)
    {
        #pragma warning disable SYSLIB0014
        WebRequest client = WebRequest.Create(addr + filename);
        #pragma warning restore SYSLIB0014
        
        client.Credentials = new NetworkCredential(user.Login, pw);
        client.Method = WebRequestMethods.Ftp.AppendFile;
        
        if (content != null)
        {
            Stream strFile = await client.GetRequestStreamAsync();
            strFile.Write(content, 0, content.Length);
            strFile.Close();
            strFile.Dispose();
            
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public async Task<string> uploadFTP(string addr, string filename, User user, string pw)
    {
        #pragma warning disable SYSLIB0014
        FtpWebRequest client = (FtpWebRequest)WebRequest.Create(addr + filename);
        #pragma warning restore SYSLIB0014
        
        client.Credentials = new NetworkCredential(user.Login, pw);
        client.Method = WebRequestMethods.Ftp.UploadFile;
        
        byte[] btfile = File.ReadAllBytes("data/" + filename);
        Stream strFile = await client.GetRequestStreamAsync();
        strFile.Write(btfile, 0, btfile.Length);
        strFile.Close();
        strFile.Dispose();
        
        return "Finished update";
    } 
    public void NewHosting()
    {
    }
}

public class GameHosting : Server
{
    public List<User> UserList;
    private Lua lua;
    public string[] ChatLogs;
    private string http, ftp;
    public (User, User) Players;
    public Script Gamelogic, Game;
    
    public GameHosting((User, User) Players, Script Gamelogic, (string, string) GetAndSetAdresses, int ticktime = 350)
    {
        this.Ticktime = ticktime;
        this.Players = Players;
        this.Gamelogic = Gamelogic;
        this.http = GetAndSetAdresses.Item1;
        this.ftp = GetAndSetAdresses.Item2;
        Mainloop();
    }
    
    private int Ticktime = 350;
    public bool __IsRunning = true;
    public bool IsRunning
    {
        get
        {
            return __IsRunning;
        }
        set
        {
            __IsRunning = value;
        }
    }
    public int t = 0;
    public object erg1, erg2;
    
    public async void Mainloop()
    {
        while (IsRunning)
        {
            UserList = await GetInGameData().Delay(Ticktime + delay);
            ChatLogs = await GetMsgData().Delay(Ticktime + delay);
            t += 1;
        }
    }
    
    public async Task<string> GetInGameData()
    {
        (bool, string) response = await Connect("https://primitive-games.net/" + Lobby.mainLobbyFile);
        bool responseCode = response.Item1;
        string responseBody = response.Item2;
        t += 1;
        
        if (responseCode)
        {
            UserList = JsonConvert.DeserializeObject<List<User>>(responseBody);
        }
        
        return responseBody;
    }
    
    public async Task<(bool, string[])> GetMsgData()
    {
        (bool, string) response = await Connect("https://primitive-games.net/Server1/Chat1.wrze");
        bool responseCode = response.Item1;
        
        if (responseCode)
        {
            ChatLogs = JsonConvert.DeserializeObject<string[]>(response.Item2);
        }
        
        t += 1;
        return (responseCode, ChatLogs);
    }
    
    public async void PostInChat(string chatInput, User user, string pw)
    {
        Lobby lobby = new Lobby();
        string[] newChatlogs = ChatLogs.Append(DateTime.UtcNow.Hour.ToString() + ";" + DateTime.UtcNow.Minute.ToString() + "/" + user.Name + ":" + chatInput);
        await sendData(ftp, "Server1/Chat1.wrze", lobby.getByteArray(newChatlogs), user, pw);
    }
    
    public string[] MessageLoading()
    {
        string[] ChatArray = { };
        
        if (ChatLogs.Any())
        {
            List<string> Chatresponse = new List<string>();
            
            foreach (string line in ChatLogs)
            {
                string[] zeit = line.Split("/").First().Split(";");
                string txt = line.Split("/").Last();
                string message = txt.Split(":").Last();
                int hour = Convert.ToInt32(zeit.First());
                int minute = Convert.ToInt32(zeit.Last());
                
                if (hour == DateTime.UtcNow.Hour || minute - 15 > DateTime.UtcNow.Minute)
                {
                    Chatresponse.Add(line);
                }
            }
            
            switch (Chatresponse.Any())
            {
                case true:
                    ChatArray = Chatresponse.ToArray();
                    return ChatArray;
                
                case false:
                    return ChatArray;
            }
        }
        
        return ChatArray;
    }
    
    private int delay
    {
        get
        {
            Random rnd = new Random();
            return rnd.Next(11, 91);
        }
    }
}

