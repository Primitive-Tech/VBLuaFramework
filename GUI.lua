local composer = require("composer")
local widget = require("widget")
--#########################################################################################
local Button = widget.newButton({
    label = "Button",
    x = 100,
    y = 100,
    onPress = function(event)
        -- Code, der bei Klick auf den Button ausgeführt wird        button:setLabel("Clicked!") -- Ändert den Text des Buttons nach dem Klick
    end
})
local RoundButton = widget.newButton({
    label = "RoundButton",
    x = 100,
    y = 100,
    onPress = function(event) print("Click1")
    end
})

--TextField:

local textField = native.newTextField(100, 150, 200, 30)
textField:addEventListener("userInput", function(event)
    if event.phase == "ended" or event.phase == "submitted" then
        print("Eingegebener Text: " .. event.target.text)
    end
end)

--UP/Down Scroller:
local ScrollWidget = createScrollWidget()
scrollWidget:addEventListener("scroll", function(event)
    if event.phase == "ended" then
        print("Aktuelle ScrollPosition: " .. event.target:getContentPosition())
    end
end)
--Switch:

local Switch = widget.newSwitch({
    x = 100,
    y = 250,
    onPress = function(event)
         if event.target.isOn then
        else
        end
    end
})
--Slider:

local Slider = widget.newSlider({
    x = 100,
    y = 300,
    listener = function(event)
        if event.phase == "ended" then
            print(event.value)
        end
    end
})
--PickerWheel:

local PickerWheel = widget.newPickerWheel({
    x = 100,
    y = 350
})
[[--pickerWheel:addEventListener("userInput", function(event)
    if event.phase == "ended" then
        local selectedValues = pickerWheel:getValues()
        print("Ausgewählte Werte:")
        for i, value in ipairs(selectedValues) do
            print(value.value)
        end
    end
end)
--]]

--ToggleButton:

local ToggleButton = widget.newSwitch({
    style = "toggle",
    x = 100,
    y = 400,
    onPress = function(event)
        if event.target.isOn then
        else
        end
    end
})
--Stepper:

local Stepper = widget.newStepper({
    x = 100,
    y = 500,
    onPress = function(event)
        -- Code, der bei Änderung des Steppers ausgeführt wird
    end
})

-- //TabBar:

local tabButtons = {
    { label="First", defaultFile="button1.png", overFile="button1-down.png", width = 32, height = 32, onPress= function() end, selected=true },
    { label="Second", defaultFile="button2.png", overFile="button2-down.png", width = 32, height = 32, onPress=onSecondView },
}
local TabBar = widget.newTabBar({
    x = 100,
    y = 550,  
    top = display.contentHeight - 50,   -- 50 is default height for tabBar widget
    buttons = tabButtons
})

--#########################################################################################
--ImageRect:

local ImageRect = display.newImageRect("image.png", 100, 100)
ImageRect.x = 100
ImageRect.y = 100

--Image:

local Image = display.newImage("image.png")
Image.x = 100
Image.y = 100

----------------------------------------------
function createStar(halfW,halfH,file,strokeW,mult)
    local vertices = { 0*mult,-110*mult, 27*mult,-35*mult, 105*mult,-35*mult, 43*mult,16*mult, 65*mult,90*mult, 0*mult,45*mult, -65*mult,90*mult, -43*mult,15*mult, -105*mult,-35*mult, -27*mult,-35*mult }    
    local star = display.newPolygon( halfW, halfH, vertices )
    star.fill = { type="image", filename=file }
    star.strokeWidth  = strokeW
    star:setStrokeColor( 1, 0, 0 )
    return star
end

function createCircle( w, h, radius,strokeWidth,fill )
    local myCircle = display.newCircle(w, h, radius  )
    myCircle:setFillColor( fill )--0.5
    myCircle.strokeWidth = strokeWidth
    myCircle:setStrokeColor( 1, 0, 0 )
    return myCircle
end