local M = {}

local back_buffer = nil
local BACK_BUFFER_WIDTH = 160
local BACK_BUFFER_HEIGHT = 90
local BACK_BUFFER_SCALE_X = 8
local BACK_BUFFER_SCALE_Y = 8

function M.init()
    back_buffer = love.graphics.newCanvas(BACK_BUFFER_WIDTH, BACK_BUFFER_HEIGHT)
    back_buffer:setFilter("nearest","nearest")
end

function M.update()
    love.graphics.setCanvas(back_buffer)
    love.graphics.setBackgroundColor(1,1,1)
    love.graphics.clear()
    love.graphics.setCanvas()
end

function M.draw()
    love.graphics.draw(back_buffer, 0, 0, 0, BACK_BUFFER_SCALE_X, BACK_BUFFER_SCALE_Y)
end

return M