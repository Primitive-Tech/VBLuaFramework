local widget = require "widget" local display = require "display"
local composer = require "composer" local Module = require("Module")

--#####################################################################################################################
------------------------------------------------
This ={
    centerX =display.contentCenterX, centerY= display.contentCenterY,
    X=  display.actualContentWidth , Y=display.actualContentHeight 
}

--#####################################################################################################################
--#####################################################################################################################
------------------------------------------------
function createText(txt,size, myr,myg,myb,mycolor)
    local r,g,b=0,0,0 
    if r == nil then mycolor = mycolor end
    local myText = display.newText(txt,0,0,native.systemFont, size, "center" )
    myText.x = x ; myText.y = y
    myText:setFillColor(r,g,b )
end
------------------------------------------------
function createCheckbox(sheet ,fnc)
    local function onSwitchPress( event )
        local switch = event.target
        end
    local options = {
        width = 40,height = 20,numFrames = 2,sheetContentWidth = 40,sheetContentHeight = 40
    } local checkboxSheet = graphics.newImageSheet( sheet, options )
    local checkbox = widget.newSwitch({
            left = 50,top = 100,style = "checkbox",id = "Checkbox",
            width = 40,height = 20,
            onPress = fnc or onSwitchPress,sheet = checkboxSheet,
            frameOff = 1,frameOn = 2})
    return checkbox
end

------------------------------------------------
function createOnOff(x,y,fnc)
    local function onSwitchPress( event )
        local switch = event.target print("Switched.")
    end
    -- Create the widget
    local onOffSwitch = widget.newSwitch({
            left = x,top = y,
            style = "onOff",id = "onOffSwitch",
            onPress = fnc or onSwitchPress
        })  
    return onOffSwitch
end

------------------------------------------------
function createProgressor(x,y,width,percentage,animated )
    -- Create the widget
    local progressView = widget.newProgressView(
        {
            left = x,
            top = y,
            width = width,
            isAnimated = animated
        }
    )
    progressView:setProgress(percentage )
    return progressView
end

------------------------------------------------
function createTabBar(position,tabButtons)
    local tabButtons = {
        { label="First", defaultFile="button1.png", overFile="button1-down.png", width = 32, height = 32, onPress=onFirstView, selected=true },
        { label="Second", defaultFile="button2.png", overFile="button2-down.png", width = 32, height = 32, onPress=onSecondView },
    }

    local tabBar = widget.newTabBar{
        top = display.contentHeight - 50,   -- 50 is default height for tabBar widget
        buttons = tabButtons
    }return tabBar
end

------------------------------------------------
function createTextField(  x,y,w,h,text)
    -- TextField
    local textField = native.newTextField(100, 150, 200, 30)
    textField:addEventListener("userInput", function(event)
        if event.phase == "ended" or event.phase == "submitted" then   print("Sending Input=> ".. event.target.text) end end)
    return textField
end

------------------------------------------------
function createSwitch(fnc)
    -- Switch
    local switch = widget.newSwitch({
        x = 100,
        y = 250,
        onPress = function(event)
            if event.target.isOn then
                print("Switch wurde eingeschaltet!")
            else
                print("Switch wurde ausgeschaltet!")
            end
        end
    })
    return switch
end

-----------------------------------------------
function createWebBrowser(X,Y, W,h)
    local webView = native.newWebView(X,Y, W,h)
    return webView
end


--#####################################################################################################################
-- Function 1: Using display.newGroup() to organize elements
function createInterfaceElement1(x, y,w,h, size, dataSet,fnc,align,btC,fC)
    local group = display.newGroup()
    local Direction = align or "right"
    local dummyFnc = function(event) end

    -- Scroll View erstellen
    local scrollView = widget.newScrollView({
        width = w,
        height = h,
        horizontalScrollDisabled = true,
        verticalScrollDisabled = false
    }) scrollView.x = x or 0 scrollView.y = y or 0

    for i, Entry in ipairs(dataSet) do
        local text = display.newText({
            text = Entry.txt,
            x = x,
            y = y + (i - 1) * size, font = native.systemFontBold,
            fontSize = 9, align = Direction  -- Alignment parameter
        })
        text.anchorX = 1 text.anchorY=0
        text:setFillColor( fC or 0 )

         local button = widget.newButton({
            width = widgetWidth,height = scrollView.height/#dataSet,label = ID,name=ID
            ,defaultFile="button2.png", overFile="button2-down.png",
            onRelease = Entry.fnc or dummyFnc
        })
        button.id = Entry.id button:setFillColor(btC or 1)
        button.x, button.y = x + size * 2, y + (i - 1) * size
        text.y=button.y-(text.height/2)
  
        scrollView:insert(text) scrollView:insert(button)
        group:insert(scrollView)
    end
    return group
end

--[[ Usage example:
local data = {
    {id = 1, txt = "Option 1", img = "button1.png"},
    {id = 2, txt = "Option 2", img = "button2.png"},
    {id = 3, txt = "Option 3", img = "button2.png"},
}
local element1 = createInterfaceElement1(100, 0, 75, data)
end
--]]

--==================================================================
function createScroll(w,h,x,y,bgC,btC)
    local sceneGroup = display.newGroup()  -- Gruppe zur Aufnahme aller Widgets
    local sceneData = {
          { ID = "view1", txt = "xxxxx" },
          { ID = "view2", txt = "txt2" },
          { ID = "MainScene", txt = "txt3" },
          { ID = "Game", txt = "sceneDatabase4" }
    }   current=sceneData[3].ID
    -- Hintergrund erstellen
    local background = display.newRect(display.contentCenterX, display.contentCenterY, display.actualContentWidth, display.actualContentHeight) background:setFillColor(bgC)
    -- Scroll View erstellen
    local scrollView = widget.newScrollView({
        width = w,
        height = h,
        horizontalScrollDisabled = true,
        verticalScrollDisabled = false
    }) scrollView.x = x  scrollView.y = y
    -- Positionsinformationen
    local widgetWidth = scrollView.width/3  local widgetPadding = 5  -- Abstand zwischen den Widgets
    -- Funktion zum Erstellen eines Szenen-Buttons
    local function createButton(ID)
        local button = widget.newButton({
            width = widgetWidth,height = scrollView.height/#sceneData,label = ID,name=ID,
            onRelease = function(event) end
        }) button:setFillColor(btC)
        return button
    end
    -- Widgets erstellen und zur Scroll View hinzufügen
    local startX = widgetPadding  -- Startposition des ersten Widgets
    for i = 1, #sceneData do
        local ID = sceneData[i].txt --scenesceneData[i].ID
        local button = createButton(ID)
        button.y = startX + widgetWidth * 0.5
        button.x = scrollView.x-widgetWidth*0.5+widgetPadding
        scrollView:insert(button)
        startX = startX + widgetWidth + widgetPadding
    end
    sceneGroup:insert(background)   sceneGroup:insert(scrollView)
    return sceneGroup
end
--------------------------------------
--######################################################################
----------------------------------------------------------------------
------------------------------------------------
function createScrollWidget(w,h,x,y,bgC,btC)
    local sceneGroup = display.newGroup()  -- Gruppe zur Aufnahme aller Widgets
    local sceneData = {
          { ID = "view1", text = "xxxxx" },
          { ID = "view2", text = "text2" },
          { ID = "MainScene", text = "text3" },
          { ID = "Game", text = "sceneDatabase4" }
    }   current=sceneData[3].ID
    -- Hintergrund erstellen
    local background = display.newRect(display.contentCenterX, display.contentCenterY, display.actualContentWidth, display.actualContentHeight) background:setFillColor(bgC)
    -- Scroll View erstellen
    local scrollView = widget.newScrollView({
        width = w,
        height = h,
        horizontalScrollDisabled = true,
        verticalScrollDisabled = false
    }) scrollView.x = x  scrollView.y = y
    -- Positionsinformationen
    local widgetWidth = scrollView.width/3  local widgetPadding = 5  -- Abstand zwischen den Widgets
    -- Funktion zum Erstellen eines Szenen-Buttons
    local function createSceneButton(ID)
        local button = widget.newButton({
            width = widgetWidth,height = scrollView.height/#sceneData,label = ID,name=ID,
            onRelease = function(event)composer.removeScene(current)composer.gotoScene(ID, { effect = "fade", time = 420 }) current = ID end
        }) button:setFillColor(btC)
        return button
    end
    -- Widgets erstellen und zur Scroll View hinzufügen
    local startX = widgetPadding  -- Startposition des ersten Widgets
    for i = 1, #sceneData do
        local ID = sceneData[i].text --scenesceneData[i].ID
        local button = createSceneButton(ID)
        button.y = startX + widgetWidth * 0.5
        button.x = scrollView.x-widgetWidth*0.5+widgetPadding
        scrollView:insert(button)
        startX = startX + widgetWidth + widgetPadding
    end
    sceneGroup:insert(background)   sceneGroup:insert(scrollView)
    return sceneGroup
end

-- ###################################################################################################################:addEventListener( "tap", gotoGame )lateUpdate  enterFrame 
-- ###################################################################################################################:addEventListener( "tap", gotoGame )lateUpdate  enterFrame 
--------------------------------------------------
function enableDragAndDrop(myObject, restrictToParent)
    local isDragging = false local touchOffsetX, touchOffsetY  -- Versatz des Objekts zur Touch-Position
    local function onTouch(event)
        local target = event.target local phase = event.phase
        if phase == "began" then
            display.getCurrentStage():setFocus(target)
            target.isFocus = true   target.alpha = 0.7  -- Objekt heller leuchten lassen
            -- Berechnung des Versatzes des Objekts zur Touch-Position
            touchOffsetX = event.x - target.x touchOffsetY = event.y - target.y
            isDragging = true
        elseif target.isFocus then
            if phase == "moved" then
                if isDragging then
                    -- Objekt zur aktuellen Touch-Position bewegen
                    target.x = event.x - touchOffsetX   target.y = event.y - touchOffsetY
                    if restrictToParent then
                        local parent = target.parent
                        local parentWidth = parent.contentWidth
                        local parentHeight = parent.contentHeight
                        local minX = target.width * 0.5
                        local maxX = parentWidth - target.width * 0.5
                        local minY = target.height * 0.5
                        local maxY = parentHeight - target.height * 0.5
                        -- Zurückbewegung des Objekts, wenn es den Bereich verlässt
                        if target.x < minX or target.x > maxX or target.y < minY or target.y > maxY then
                            target.x = target.xStart target.y = target.yStart
                        end
                    end
                end
            elseif phase == "ended" or phase == "cancelled" then
                display.getCurrentStage():setFocus(nil)
                target.isFocus = false target.alpha = 1.0  -- Helligkeit des Objekts zurücksetzen
                isDragging = false
            end
        end
        return true
    end
    myObject:addEventListener('touch', onTouch)     
end
-----------------------------------------------------------------------
local testObj = display.newRect( display.contentCenterX, display.contentCenterY, 20, 20 )
testObj.deltaPerFrame = { 0, 0 }
 
local function frameUpdate()
    testObj.x = testObj.x + testObj.deltaPerFrame[1]
    testObj.y = testObj.y + testObj.deltaPerFrame[2]
end
Runtime:addEventListener( "enterFrame", frameUpdate )

--#####################################################################################################################
------------------------------------------------
function RenderScreen(formX,formY,w,h)
    return  formX.y + w * 0.5,formY - h* 0.5
end
math.randomseed( os.time() )
------------------------------------------------

--##################################################################################################
--[[
local sheetOptions =
{
    width = 512,
    height = 256,
    numFrames = 8
}

local sequences_NewSpriteObj = {
    {
        name = "Default",
        start = 1,
        count = 8,
        time = 800,
        loopCount = 4
    },
    {
        name = "StartingP",
        frames = { 1,3,5,7 },
        time = 400,
        loopCount = 0
    },
}
-- sprite listener function
local function spriteListener( event )
 
    local thisSprite = event.target  -- "event.target" references the sprite
 
    if ( event.phase == "ended" ) then 
        thisSprite:setSequence( "StartingP" )  -- switch to "StartingP" sequence
        thisSprite:play()  -- play the new sequence
    end
end
 
-- add the event listener to the sprite
NewSprteObj:addEventListener( "sprite", spriteListener )--]]