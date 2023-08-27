
-- Beispielverwendung scale
local object = display.newRect(display.contentCenterX, display.contentCenterY, 100, 100)
object:setFillColor(1, 0, 0)

-- Ursprüngliche Position des Objekts speichern
object.xStart = object.x
object.yStart = object.y

-- Beispielaufruf spr
local imagePath = "pfad/zur/bilddatei.png"
local name = "sprite"
local time = 1000
local size = { x = display.contentWidth, y = display.contentHeight }
local loopCount = 0
local mySprite = createSpriteFromImage(imagePath, name, time, size, loopCount)
