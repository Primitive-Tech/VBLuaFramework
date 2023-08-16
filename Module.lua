local math = require("math") 
--############################################################################################################
------------------------------- Implement_ObjectMethods ----------------------------------------
--==========================================================================================
getByID = function(collection,n) t = {} for collectionInt,obj in pairs(collection) do if obj.name == n then return obj end  end end
function typeOf(self)
    if asString then return toString(type(self))
    else
        return type(self)
    end 
end
--------------- Implement_Prototypes -----------------
local dataset = { 
        type = typeOf, count = count, get = get, set = set, 
        "vbl", -1,  me = me , group = group, id = id }
function newObject(dataset)
    local obj = {}
    function obj:new(data) 
        data = data or {}
        self.me = me or { }
        self.type = typeOf
        self.id = id or 0.1
        self.group = group or {}
        self.count = function(obj) return #obj end
        self.get = function(var) return self.me[tostring(var)] end -- refIn = newObj:set("x", "ID")
        self.set = function(var, val) self.me[tostring(var)] = val end-- refOut = newObj:get("ID"))
        setmetatable(data,self)
        self.__index = self
        return data
    end
    local newObj = obj:new(dataset)
    return newObj 
end
newObj = newObject(dataset) -- <== BaseClass("Konstruktor")
------ TestExamples -----
Class = newObj:new({ group = nil, id = 0.11})
Me={};
--############################################################################################################
-------------------------------  "ME" Namespace Imitiator & Console  ----------------------------------
--==========================================================================================
function Me:exec_silent(command)
    local console = assert(io.popen(command))
    local response = console:read("*a")
    console:close()
    return response
end
function Me:cmd(command)
    local console = io.popen(command) 
    local response = console:read("*a")
    console:close()
  return response,console
end
function Me:WorkingDirectory() return os.getenv() end
function Me:Remove() return os.remove() end
function Me:Rename() return os.rename() end

--######################### Path & Directory #######################--###########################
function FromFile( file,setting,readmode )
    local mode,readerMode,stringified
    if setting == 1 then mode = "w" else mode = "r" end
    if readmode == 1 then readerMode = "*a" else readerMode = "*l" end
    local f=io.open(file,mode)
    if f ~= nil then stringified= f:read(readerMode)
    end f:close() 
    return stringified
end

------------------ MATHE ------------------
function round( x )
  if x ~= nil then
  return math.floor(x)
  else return 0
  end
end
function sum(x,y)
    if y == nil then 
        return x + x
    else
        return x + y 
    end
end
function diff(x,y)
        if y == nil then 
            return x - x
    else
        return x - y 
    end
end
function multipl(x,y)
    if y == nil then 
        return x * x
    else
        return x * y 
    end
end
function ratio(x,y)
        if y == nil then 
            return x / x
    else
        return x / y 
    end
end
------------------- Strings -----------------------
str={ }
function str:replace (s,word,rep )
    return s:gsub( word, rep) or ""
end

function str:split(str, startIndex,lastIndex)
    last = -1 if lastIndex ~= nil then last = lastIndex end
    if str ~= nil then return true,string.sub(str,startIndex,lastIndex) else return false end
end

function str:find(str,x)
    if #str == 1 then x = "%"..x end
    foundIndex, lastIndex= string.find(str, x) --("%") für einzelne Buchstaben
    i=foundIndex+1 --NextCut:Start_Index
    if foundIndex ~= nil then return true, foundIndex-1, lastIndex-1 else return false, 0, 0 end
end
-------------------------------------------
function cStr( x )
    return tostring(x) or x
end
function cInt( x )
    return tonumber(x) or x
end
function Randomize(x,y,z) return math.random(x,y,z) end

function Console(command)
    local console = assert(io.popen(command))
    output =console:read("*a")
    console:close()
    return output
end
function ConsoleEmbedded()
    local console = assert(io.popen(commandStatement))
    output =console:read("*a")
    console:close()
    return output
end

function chk(event)
    if event ~= nil then return true
    else
        return false
    end
end

--------------------------------------------
local function onTimer(event)
    -- Code, der beim Ablauf des Timers ausgeführt wird
end

function delay(int, fnc )
     fnc=onTimer
     timer.performWithDelay(int, onTimer) 
end

