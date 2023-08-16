using Newtonsoft.Json;
using System.Net;
using System.Text;
using NLua;
using  VBLua.Core;
using static VBLua.Core.Utilities;

public class User
{
    public string ConnectionInfo, ActualGame;
    public string Name, Host, Login, Status;
    public bool Online;
    public int Rang;
    public bool LoggedIn = false, Ready = false, ReadyIngame = false, IsHost = false;

    public User(string name, int rang, bool online)
    {
        Rang = 1;
        Status = "Inactive";
        Name = name;
        Rang = rang;
        Online = online;
        Host = "@primitive-games.net";
        Login = Name + Host;
    }

    public User(string name, int rang, bool online = false, string host = "@primitive-games.net")
    {
        Rang = 1;
        Status = "Inactive";
        Name = name;
        Rang = rang;
        Online = online;
        Host = host;
        Login = Name + Host;
    }

    public User()
    {
        Status = "Inactive";
        Rang = 0;
        Name = "Guest";
        Host = "@primitive-games.net";
        Login = Name + Host;
        Online = true;
    }
}
public class Server
{
    public string DataGrabber; public JsonSerializerSettings mysettings;

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
            UserList = await GetInGameData().WaitAsync(new TimeSpan(Ticktime + delay));
            ChatLogs = await GetMsgData().WaitAsync(new TimeSpan(Ticktime + delay));
            t += 1;
        }
    }

    public async Task<List<User>> GetInGameData()
    {
        (bool, string) response = await Connect("https://primitive-games.net/" + Lobby.mainLobbyFile);
        bool responseCode = response.Item1;
        string responseBody = response.Item2;
        t += 1;

        if (responseCode)
        {
            return JsonConvert.DeserializeObject<List<User>>(responseBody);
        }
        return UserList;
    }

    public async Task<string[]> GetMsgData()
    {
        (bool, string) response = await Connect("https://primitive-games.net/Server1/Chat1.wrze");
        bool responseCode = response.Item1;

        if (responseCode)
        {
            return ChatLogs;
        }
        t += 1;
        return ChatLogs;
    }
    public async void PostInChat(string chatInput, User user, string pw)
    {
        Lobby lobby = new Lobby();
        var newChatlogs = ChatLogs.Append(DateTime.UtcNow.Hour.ToString() + ";" + DateTime.UtcNow.Minute.ToString() + "/" + user.Name + ":" + chatInput);
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

public class Lobby : Server
{
    protected User MyAcc = new User();
    protected List<User> users = new List<User>();

    public List<User> userlist
    {
        get { return users; }
        set { users = value; }
    }

    public const string mainLobbyFile = "Lobbys.json";
    private string http, ftp, extra;
    protected string data, lobbyData;
    public bool connected;

    public Lobby()
    {
    }

    public Lobby(string httpUrl, string ftpUrl, User user)
    {
        http = httpUrl;
        ftp = ftpUrl;
        MyAcc = user;
        connected = Connect(httpUrl + mainLobbyFile).Result.Item1;
    }

    public byte[] getByteArray(object x)
    {
        try
        {
            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(x, mysettings));
        }
        catch
        {
            return null;
        }
    }

    public (bool, User) checkList(User account = null)
    {
        if (account == null)
            account = MyAcc;

        foreach (User user in users)
        {
            if (user.Name == account.Name)
                return (true, user);
        }

        return (false, null);
    }

    public User GetUserByID(string username)
    {
        User user = new User
        {
            Name = username
        };

        if (checkList(user).Item1)
        {
            return checkList(user).Item2;
        }
        else
        {
            return null;
        }
    }

    public bool Join(string pw)
    {
        connected = Connect(http + mainLobbyFile).Result.Item1;

        if (connected && !checkList().Item1)
        {
            try
            {
                MyAcc.Online = true;
                MyAcc.LoggedIn = true;
                MyAcc.Status = "chillin";
                users.Add(MyAcc);
                byte[] content = getByteArray(users);
                sendData(ftp, mainLobbyFile, content, MyAcc, pw);
                return true;
            }
            catch
            {
                MyAcc.LoggedIn = false;
                MyAcc.Online = true;
                MyAcc.Status = "LoginFailed";
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool Leave(string pw)
    {
        connected = Connect(http + mainLobbyFile).Result.Item1;

        if (connected && checkList().Item1)
        {
            try
            {
                users.RemoveAll(user => user.Name == MyAcc.Name);
                byte[] content = getByteArray(users);
                sendData(ftp, mainLobbyFile, content, MyAcc, pw);
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}