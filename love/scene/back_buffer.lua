local colors = require "game.colors"
local M = {}

local canvas = nil
local CANVAS_WIDTH = 160
local CANVAS_HEIGHT = 90
local CANVAS_SCALE_X = 8
local CANVAS_SCALE_Y = 8

function M.load()
    canvas = love.graphics.newCanvas(CANVAS_WIDTH, CANVAS_HEIGHT)
    canvas:setFilter("nearest","nearest")
end

function M.update(predicate)
    love.graphics.setCanvas(canvas)
    predicate()
    love.graphics.setColor(colors.WHITE)
    love.graphics.setCanvas()
end

function M.draw()
    love.graphics.draw(canvas, 0, 0, 0, CANVAS_SCALE_X, CANVAS_SCALE_Y)
end

return M