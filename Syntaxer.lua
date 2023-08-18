local fnc=require("Module")
local function toStr(x) return tostring(x) end
--=============================================================================
StartTxt =
[[

private VBExt= imports "VBExt" 

private running = true 

]]
function Code(array) return "liste" end

FunctionsTxt =
[[

    private function _()
        foreach x from liste=>
        print(tostring(x))
        next

        return true
    end function

]]

SubTxt =
[[

    private Sub Main()
        
    End Sub

]]
meinCode =
[[

]]

object = { 
    "private obj".." = ".."newObj:new({\n\n\t"..
    "group = Objgroup, id = "..tostring(counter).."\n\r})\n "
}
SchlussTxt ="\n\nreturn true, 'No Errors'\n\n"

--############################################################################################"Name\tVorname\nMeier\tyMax"
--------- Ausdrücke & Operatoren ------------ Ausdrücke & Operatoren  ------------
--===========================================================================================================================
local keys = {  
    ["require"] = {"using","imports","Import","Imports"}, ["nil"] = {"null","nothing","Nothing"}, ["local"] = { "Dim","dim","Private","private"},
    ["= number "] = {"as Integer","as integer","as Integer","as int"},["= { }"] = {"as table","as Table"}, ["= string "] = {"as String","as String"},

    [" end"] = {" next"," Next"}, ["repeat"] = {"loop"}, [","] = {" to ", " by "},
    ["elseif"] = {"elif ","else if ","elseif "},["then"] = {"Then "},["nil"] = {"Nothing","nothing","null"},
    
    [" in pairs("]={" from "," From "}, [" in ipairs("]={ " To "," To "," outof "," outof "},["pcall("]={"try:","Try:"}, --Konvertierung:Funktion(Lua) <=  => Schlagwort(VB)
    ["for "] = { "foreach ","ForEach ","For Each " },
	["end"] = { "end for","end while","end sub","end function","End For","End While","End Sub","End Function", "End Class" },
    [".."] = {"&"}, ["~="] = {"<>","!="},
    
    ["tostring("] = {"cStr("}, ["tonumber("] = {"cInt("},

    [" function "] = { "void ","Void ","sub ","Sub ","Function " ,"function " }, ["function()"] = {"del","delegate"}, [")do "] = {"=>","Do "}, -- "=>"
    
    ["print"] = {"log","Console.Write("},
    ["os.time()"] = {"me.Time()","Me.Time()"}, ["os.clock()"] = {"me.Clock()","Me.Clock()"}, ["os.difftime:"] = {"Difftime:"}
} 

--============================================================================  MainFunc:Syntax ====================================================
function CreateCode(subtext) return StartTxt..SubTxt..FunctionsTxt..SchlussTxt end --ZsmSetzen --local char1=")"  local ConvertToVB = function(mycodeword) for i in pairs(keys) ["pcall("] do if mycodeword== v then return (char1) end return "" endend
function ConvertCode(Txt)
    if Txt~= nil then local convertedText,vbkeys=Txt,{}	--	"for i=1,obj in pairs(collection) do print(obj) end "
	for key,vbkey in pairs(keys) do 
		for i=1,#vbkey,1 do 
			convertedText = str:replace(convertedText,vbkey[i],key) end
		--table.insert(vbkeys,vbkey)
	end	 
	convertedText = str:replace(convertedText,"End function","end")    
    return convertedText    end
end
function ReverseConvertCode(Txt)
    if Txt ~= nil then
        local convertedText, vbkeys = Txt, {}

        for key, vbkey in pairs(keys) do 
            local replacement = vbkey[1]  -- Den ersten Eintrag aus vbkey als Ersatz verwenden
            for i = 1, #vbkey, 1 do 
                convertedText = str:replace(convertedText, vbkey[i], replacement)
            end
            table.insert(vbkeys, replacement)  -- Den Ersatzwert zur Liste der vbkeys hinzufügen
        end

        convertedText = str:replace(convertedText, "End function", "end")    
        return convertedText
    end
end

--========================================================================================================
local preparing= ConvertCode(CreateCode())
local preparing2= ReverseConvertCode(CreateCode())
print("--####################################################")
print(toStr(preparing))
print(tostring(preparing2))r2

return preparing or false
