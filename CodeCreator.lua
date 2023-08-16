--############# CodeBlocks ################## CodeBlocks ################################### CodeBlocks###################################
--enableDragAndDrop(object, true)
templates = {
    ["Start"] =
[[
-----------------------------------------------------------------------------------------
--Standarts
local bgColor,myBGColor,myFontSize,myFontColor = 1,255, 14, 0
local Me ={
    centerX =display.contentCenterX,centerY= display.contentCenterY,
    X=  display.actualContentWidth , Y=display.actualContentHeight 
}
-----------------------------------------------------------------------------------------
local composer = require( "composer" )local widget = require( "widget" )
local scene = composer.newScene()
--################################################################################################################################################
]],
    ["create"] =
[[
local objectGroup = display.newGroup()  --:addEventListener( "tap", yourFnc )

function scene:create( event )
    local sceneGroup = self.view
    -- add Display objects, touch listeners, etc .--  
    local background = display.newRect( display.contentCenterX, display.contentCenterY, display.contentWidth, display.contentHeight )background:setFillColor(myBGColor)
    local title = display.newText( "", Me.centerX, 125, native.systemFont, 32 ) title:setFillColor( myFontColor )   -- black
    local newText = { text = "x", x = Me.centerX + 10, y = title.y + 215, 
                            font = native.systemFont, fontSize = myFontSize, align = "center" }

]],
    groups =
[[
    -- Starting_Items into Groups to show up
    local objects = { title=title,bgImg=background }  
    -- Starting_Items into Groups to show up       
    for k,v in pairs(objects) do
        sceneGroup:insert(v) print(k..": AddedToScene")
    end

]],
   --####################################################################################### 
    ["show"]  =
[[

function scene:show( event )
    local sceneGroup = self.view
    local phase = event.phase
    
]],
    ["hide"]  =[[

function scene:hide( event )
    local sceneGroup = self.view
    local phase = event.phase
]],
    ["destroy"]  = [[

function scene:destroy( event )
    local sceneGroup = self.view
    -- Remove display objects, touch listeners, save state, etc. --
    sceneGroup:removeSelf()
    sceneGroup=nil
]],
    ["sceneEnd"]  =
[[

--#######################################################################################
---------------------------------------------------------------------------------
-- Listener setup --
scene:addEventListener( "create", scene )
scene:addEventListener( "show", scene )
scene:addEventListener( "hide", scene )
scene:addEventListener( "destroy", scene )
---------------------------------------------------------------------------------
]],
    phaseWill = [[
    if phase == 'will' then

]],
    phaseDid = [[
    elseif phase == 'did' then 

]],
    phaseNext= 
[[
    end
end
]]
}
--------------------------------------------------------------------------
Usercode={
    Interface=[[    --Designer_Generated:
]],
    Logic=[[
]],
    Scale=[[
]],
    Events=[[
    -- Preperation-UserLogic
    --Runtime:addEventListener("enterFrame", onEnterFrame)
]]
}

--################### Building Code ################## Building Code ########################### Building Code###################################
scriptGen = function() 
    local codeBlocks =     
        templates["Start"]..
        templates["create"]..templates["groups"]..
        Usercode.Interface..ElementsOnScreen..Usercode.Logic..[[

end
]]..
        templates["show"]..templates["phaseWill"]..templates["phaseDid"]..templates["phaseNext"]..
        templates["hide"]..templates["phaseWill"]..templates["phaseDid"]..templates["phaseNext"]..
        templates["destroy"]..Usercode.Scale..[[

end
]]..
        templates["sceneEnd"]..[[
return scene

]]
    return codeBlocks 
end
--#########################################################################################################
print( scriptGen() )

return true,{output= scriptGen()}