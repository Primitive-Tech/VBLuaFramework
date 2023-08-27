Imports Newtonsoft.Json
Imports System.IO
Imports System.Net, System.ComponentModel, System.Net.Http, System.Web, System.Linq, NLua, System.Text
'========================================================'========================================================
Public Class PServer

    Public DataGrabber
    Protected PW As String = ""
    '///////////////////////// Basic_Methods ///////////////////////////// BasicMethods /////////////////////////////
    Public mysettings As New JsonSerializerSettings
    Dim user As User
    '++++++++++++++++++++++++++ Constructors ++++++++++++++++++++++++++++++++++++++++++++
    Public Sub New()
        mysettings.Formatting = Formatting.Indented
    End Sub
    Public Sub New(user As User)
        mysettings.Formatting = Formatting.Indented
        Me.user = user
    End Sub
    Public Sub New(user As User, PW As String)
        mysettings.Formatting = Formatting.Indented
        Me.user = user
        Me.PW = PW
    End Sub
    '+++++++++++++++++++++++++++++++ Download Data over HTTPS +++++++++++++++++++++++++++++++++++
    ' <= Async with ResponseBody =>
    Public Async Function GetData(addr As String) As Task(Of String)
        Using client As New HttpClient
            Dim response As HttpResponseMessage = client.GetAsync(addr).Result
            Dim outp As String = Await response.Content.ReadAsStringAsync()
            If outp <> Nothing Then
                Return outp
            Else Return False.ToString
            End If
        End Using
    End Function
    ' <= Async with ResponseCode =>
    Public Async Function Connect(addr As String) As Task(Of (Boolean, String))
        Using client As New HttpClient
            Dim response As HttpResponseMessage = client.GetAsync(addr).Result
            Dim outp As String = Await response.Content.ReadAsStringAsync()
            If outp <> Nothing Then
                Return (True, outp)
            Else
                Return (False, outp)
            End If
        End Using
    End Function
    ' <= Low_Standart =>
    Public Function GetStrData(addr As String) As String
#Disable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        Dim client As New WebClient
#Enable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        Dim data As String = ""
        client.Credentials = New NetworkCredential(User.Name, PW)
        Try
            data = client.DownloadString(addr)
        Catch
            MsgBox("Downloading failed!")
        End Try
        Return data
    End Function
    '++++++++++++++++++++++++++++++++++ Upload Data over FTP ++++++++++++++++++++++++++++++++++++++++++++++++
    Public Async Function sendData(addr As String, filename As String, content As Byte()) As Task(Of Boolean)
#Disable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        Dim client As WebRequest = WebRequest.Create(addr + filename)
#Enable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        client.Credentials = New NetworkCredential(user.Login, PW)
        client.Method = WebRequestMethods.Ftp.UploadFile
        If content IsNot Nothing Then
            Dim strFile As Stream = Await client.GetRequestStreamAsync()
            strFile.Write(content, 0, content.Length)
            strFile.Close()
            strFile.Dispose()
            Return True
        Else
            Return False
        End If
    End Function
    Public Async Function AppendData(addr As String, filename As String, content As Byte()) As Task(Of Boolean)
#Disable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        Dim client As WebRequest = WebRequest.Create(addr + filename)
#Enable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        client.Credentials = New NetworkCredential(user.Login, PW)
        client.Method = WebRequestMethods.Ftp.AppendFile
        If content IsNot Nothing Then
            Dim strFile As Stream = Await client.GetRequestStreamAsync()
            strFile.Write(content, 0, content.Length)
            strFile.Close()
            strFile.Dispose()
            Return True
        Else
            Return False
        End If
    End Function
    '------------ !!! <= Veralteter Code(Sicherheits_Backup) => ---------- !!!'
    Public Async Function uploadFTP(addr As String, filename As String) As Task(Of String)
        'Create Request To Upload File'
#Disable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        Dim client As FtpWebRequest = DirectCast(WebRequest.Create(addr + filename), FtpWebRequest)
#Enable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        client.Credentials = New NetworkCredential(user.Login, PW)
        client.Method = WebRequestMethods.Ftp.UploadFile
        'Locate File And Store It In Byte Array'->'Get File'
        Dim btfile() As Byte = File.ReadAllBytes("data/" + filename)
        Dim strFile As Stream = Await client.GetRequestStreamAsync()
        strFile.Write(btfile, 0, btfile.Length)
        'Close'
        strFile.Close()
        strFile.Dispose()
        Return "Finished update"
    End Function
    Public Overridable Sub Hosting()

    End Sub

End Class
'#########################################################################################################
Public Class GameHosting
    Inherits PServer
    Public UserList As New List(Of User)
    Dim lua As New Lua()
    Dim pathOfFile As String = "Server1/Chat1.wrze"
    Public ChatLogs As String() = {}
    Protected http, ftp As String '    Private ftp = "ftp://ssh.strato.de/"
    Public Players As (User, User), Gamelogic As Script, Game As Script
    Public Sub New(Players As (User, User), Gamelogic As Script, Adresses As (String, String), Optional ticktime As Integer = 350)
        Me.Ticktime = ticktime
        Me.Players = Players
        Me.Gamelogic = Gamelogic
        Me.http = Adresses.Item1
        Me.ftp = Adresses.Item2
        Mainloop()
        ' Dim x = lua.GetTable("data/client.vbl")
    End Sub
    Overrides Sub Hosting()

    End Sub
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Private Ticktime = 350
    Public __IsRunning As Boolean = True
    Public Property IsRunning As Boolean
        Get
            Return __IsRunning
        End Get
        Set(value As Boolean)
            __IsRunning = value
        End Set
    End Property
    Public t = 0
    Public erg1, erg2
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Async Sub Mainloop()
        While IsRunning
            Dim UserList = Await GetInGameData().Delay(Ticktime + delay)
            Dim Chatlogs = Await GetMsgData().Delay(Ticktime + delay)
            t += 1
        End While
    End Sub
    '---------------------------------- Methods -----------------------------------
    Async Function GetInGameData() As Task(Of String)
        Dim response = Await Connect(http & Lobby.mainLobbyFile)
        Dim responseCode = response.Item1
        Dim responseBody = response.Item2
        t += 1
        If responseCode Then
            Me.UserList = JsonConvert.DeserializeObject(Of List(Of User))(responseBody)
        End If
        Return responseBody
    End Function
    Async Function GetMsgData() As Task(Of (Boolean, String()))
        Dim response = Await Connect(http & pathOfFile)
        Dim responseCode = response.Item1
        If responseCode Then
            Me.ChatLogs = JsonConvert.DeserializeObject(Of String())(response.Item2)
        End If
        t += 1
        Return (responseCode, ChatLogs)
    End Function
    Public Async Sub PostInChat(chatInput As String)
        Dim lobby As New Lobby(Players.Item1)
        Dim newChatlogs = ChatLogs.Append(DateTime.UtcNow.Hour.ToStr() + ";" + DateTime.UtcNow.Minute.ToStr() + "/" + Players.Item1.Name + ":" + chatInput)
        Await sendData(ftp, pathOfFile, lobby.getByteArray(newChatlogs))
    End Sub

    Public Function MessageLoading() As String()
        Dim ChatArray As String() = {}
        If ChatLogs.Any Then
            Dim Chatresponse As New List(Of String)
            For Each line As String In ChatLogs
                Dim zeit = line.Split("/").First
                Dim txt = line.Split("/")(1)
                Dim message = txt.Split(":")(1)
                Dim hour = zeit.Split(";").First
                Dim minute = zeit.Split(";")(1)
                If hour.ToInt = DateTime.UtcNow.Hour Or minute - 15 > DateTime.UtcNow.Minute Then Chatresponse.Add(line)
            Next
            Select Case Chatresponse.Any
                Case True
                    ChatArray = Chatresponse.ToArray
                    Return ChatArray
                Case False
                    Return ChatArray
            End Select
        End If
        Return ChatArray
    End Function
    ReadOnly Property delay As Integer
        Get
            Dim rnd As New Random
            Return rnd.NextInt64(11, 91)
        End Get
    End Property
End Class

'/////////////////////// LOBBY /////////////////////////// LOBBY ///////////////////////////// LOBBY //////////////////////////////////////////////
Public Class Lobby
    Inherits PServer
    Dim MyAcc As New User
    Public List As New List(Of User)
    Public Property userlist As List(Of User)
        Set(value As List(Of User))
            List = value
        End Set
        Get
            Return List
        End Get
    End Property
    Public Const mainLobbyFile As String = "Lobbys.json" 'Credentials
    Private http, extra As String 'Commomn
    Protected data, lobbyData As String
    Public connected As Boolean
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ' <=  Initialize_Lobby =>
    Public Sub New(user As User)
        MyAcc = user
    End Sub
    Public Sub New(user As User, httpUrl As String, ftpUrl As String)
        httpUrl = httpUrl
        ftpUrl = ftpUrl
        MyAcc = user
        connected = Connect(httpUrl + mainLobbyFile).Result.Item1
    End Sub
    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ' <= 1#Convert for Patching_Request
    Public Function getByteArray(x As Object) As Byte()
        Try
            Return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(x, mysettings))
        Catch
            Return Nothing
        End Try
    End Function
    ' <= 2#Search for Matching Users 
    Public Function checkList(Optional account As User = Nothing) As (Boolean, User)
        If account Is Nothing Then account = MyAcc
        For Each user In List
            If user.Name = account.Name Then
                Return (True, user)
            End If
        Next
        Return (False, Nothing)
    End Function
    '++++++++++++++++++++++++++++++++++ Main_Functions +++++++++++++++++++++++++++++++++++++++++++++++

    Public Function GetUserByID(username As String) As User
        Dim user As New User
        With user
            .Name = username
        End With
        If checkList(user).Item1 Then
            Return checkList(user).Item2
        Else
            Return Nothing
        End If
    End Function
    Public Async Function Join(pw As String, url As String) As Task(Of Boolean)
        connected = Connect(http + mainLobbyFile).Result.Item1
        If connected AndAlso checkList().Item1 = False Then
            Try
                MyAcc.Online = True
                MyAcc.LoggedIn = True
                MyAcc.Status = "chillin"
                List.Add(MyAcc)
                Dim content As Byte() = getByteArray(List)
                Await sendData(url, mainLobbyFile, content) 'uploadFTP(ftp, mainLobbyFile, MyAcc, pw)
                Return True
            Catch
                MyAcc.LoggedIn = False
                MyAcc.Online = True
                MyAcc.Status = "LoginFailed"
                Return False
            End Try
        Else
            Return False
        End If
    End Function
    Public Async Function Leave(pw As String, url As String) As Task(Of Boolean)
        connected = Connect(http + mainLobbyFile).Result.Item1
        If connected And checkList().Item1 Then
            Try
                List.RemoveAll(Function(user As User) user.Name = MyAcc.Name)
                Dim content As Byte() = getByteArray(List)
                Await sendData(url, mainLobbyFile, content)
                Return True
            Catch
                Return False
            End Try
        Else
            Return False
        End If
    End Function

End Class


'''########################################################################################################################
Public Class User
    Public ConnectionInfo, ActualGame As (String, String)
    Public Name, Login, Status As String
    Public Shared Host = "@primitive-games.net"
    Public Online As Boolean
    Public Rang As Integer
    Public LoggedIn As Boolean = False, Ready = False, ReadyIngame = False, IsHost = False
    '========================================================'========================================================
    Public Sub New(name As String, rang As Integer)
        Me.Rang = 1
        Status = "Inactive"
        Me.Name = name
        Me.Rang = rang
        Me.Online = False
        Login = Me.Name + Host
    End Sub
    Public Sub New(name As String, test As Boolean)
        Me.Rang = 1
        Status = "Inactive"
        Me.Name = name
        Me.Rang = Rang
        Login = Me.Name + Host
        Me.Online = True
    End Sub
    Public Sub New()
        Status = "Inactive"
        Rang = 0
        Name = "Guest"
        Login = Me.Name + Host
        Me.Online = False
    End Sub
End Class
