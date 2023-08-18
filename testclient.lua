local socket = require( "socket" )
local host= "192.168.178.36"
--local host="www.primitive-games.net"

-- Connect to the client
local client = socket.connect(host, 8080 )
-- Get IP and port from client
local ip, port = client:getsockname()
  
-- Print the IP address and port to the terminal

print("Attempting connection to host '" ..host.. "' and port " ..port.. "...")
print("Connected! Please type stuff (empty line to stop):")
txt="hallooooo"
assert(client:send(txt .. "\n"))