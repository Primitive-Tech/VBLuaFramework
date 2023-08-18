--=================================================================================
local socket = require("socket")
host =  "192.168.178.36"
port =  8080
--------------------------------------------------------------------------------
print("Binding to host '" ..host.. "' and port " ..port.. "...")
s = assert(socket.bind(host, port))
i, p  = s:getsockname()
assert(i, p)
--=================================================================================
print("Waiting connection from" .. i .. ":" .. p .. "...")
c = assert(s:accept())
print("Connected. Here is the stuff:")
l, e = c:receive()
while not e do
	print(l)
	l, e = c:receive()
end
c:close()
--=================================================================================
print(e)
