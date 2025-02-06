local M = {}

local COLUMNS = 16
local ROWS = 6
local STARTING_CHARACTER = 32

function M.new(filename)
    local instance = {}
    instance.image = love.graphics.newImage(filename)
    instance.quads = {}
    instance.cell_width = instance.image:getWidth() / COLUMNS
    instance.cell_height = instance.image:getHeight() / ROWS
    local character = STARTING_CHARACTER
    for row = 1, ROWS do
        local y = instance.cell_height * (row - 1)
        for column = 1, COLUMNS do
            local x = instance.cell_width * (column - 1)
            instance.quads[character] = love.graphics.newQuad(x, y, instance.cell_width, instance.cell_height, instance.image)
            character = character + 1
        end
    end
    function instance:draw_character(character, x, y, color)
        local quad = self.quads[character]
        local r,g,b,a = love.graphics.getColor()
        love.graphics.setColor(color)
        love.graphics.draw(self.image, quad, x, y)
        love.graphics.setColor(r,g,b,a)
        return x + self.cell_width
    end
    function instance:draw_text(text, x, y, color)
        for index = 1, #text do
            self:draw_character(string.byte(text, index),x,y,color)
            x = x + self.cell_width
        end
    end
    function instance:draw_text_centered(text, x, y, color)
        self:draw_text(text, x - #text * self.cell_width / 2, y, color)
    end
    return instance
end

return M